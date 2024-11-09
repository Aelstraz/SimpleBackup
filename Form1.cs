using Microsoft.Win32.TaskScheduler;
using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Threading;
using System.Windows.Forms;
using Action = System.Action;
using File = System.IO.File;
using ThreadState = System.Threading.ThreadState;
using Timer = System.Timers.Timer;

namespace SimpleBackup
{
    public partial class Form1 : Form
    {
        private static Thread thread = null;
        private static Timer transferStatTimer = null;
        private static bool backupIsRunning = false;
        private static bool autoRun = false;
        private static SmtpClient smtpClient = null;
        private static bool isSendingEmail = false;
        private static bool isSendingTestEmail = false;
        private static readonly int maxMD5ExtraTransferAttempts = 1;
        private static readonly int maxThreadAbortWaitTime = 1000;
        private static readonly int maxInfoTextBoxLines = 400;

        //variables for tracking transfer stats
        private static DateTime backupStartTime = new DateTime();
        private static string currentFilePathStat = null;
        private static ulong totalFileSizeStat = 0;
        private static ulong totalTransferedFileSizeStat = 0;
        private static ulong lastFileSizeStat = 0;
        private static ulong currentTransferedFileSizeStat = 0;
        private static List<ulong> transferSpeedHistoryListStat = new List<ulong>();
        private static readonly int transferSpeedHistoryMaxSize = 25;

        public Form1(string[] args)
        {
            InitializeComponent();

            Settings.Load();
            Log.OnLogUpdated += OnLogUpdatedCallback;
            //get build version
            string buildVersion = "v" + System.Diagnostics.FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).FileVersion;
            Log.WriteLine("--Welcome to SimpleBackup " + buildVersion + "--");
            //set window title with the latest build version
            Text = "SimpleBackup " + buildVersion;

            //check if args were passed for automated run
            if (args.Length > 0 && args.Contains("scheduledRun"))
            {
                autoRun = true;
                WindowState = FormWindowState.Minimized;
                StartBackupThread();
            }
        }

        /// <summary>
        /// Begins the backup process and starts a background thread to continue the backup process (in order to not block the main/UI thread)
        /// </summary>
        private void StartBackupThread()
        {
            //check if a backup is already running
            if (!backupIsRunning)
            {
                //set UI elements to starting values
                infoTextBox.Text = "";
                Log.WriteLine("--Starting backup--");
                backupIsRunning = true;
                backupButton.Text = "Stop Backup";
                backupProgressBar.Value = 0;
                //save the backup start time
                backupStartTime = DateTime.Now;

                //create a new thread to run the backup on
                ThreadStart threadStart = new ThreadStart(Backup);
                thread = new Thread(threadStart);
                thread.IsBackground = true;
                thread.Start();

                //start a timer to track transfer stats
                transferStatTimer = new Timer(1000);
                transferStatTimer.Elapsed += TransferStatTimerElapsed;
                transferStatTimer.Start();
            }
            else
            {
                //cancel backup early if user clicks yes
                DialogResult dialogResult = MessageBox.Show("Backup in progress. Are you sure you wish to cancel?", "Caution", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (dialogResult == DialogResult.Yes)
                {
                    StopBackupEarly();
                }
            }
        }

        /// <summary>
        /// Runs through the main process of collecting directory data, comparing directory data, transfering/deleting files and updating backup folders
        /// </summary>
        private void Backup()
        {
            if (!BackupPathsAreValid())
            {
                FinishTransfer();
                return;
            }

            List<TransferDirectory> transferDirectoryList = new List<TransferDirectory>();
            List<TransferFile> transferFileList = new List<TransferFile>();
            List<Tuple<string, string>> backupFolderUpdateList = new List<Tuple<string, string>>();
            int totalNumberOfFiles = 0;
            ulong totalSizeOfFiles = 0;

            CollectAndCompareDirectoryData(ref transferDirectoryList, ref transferFileList, ref totalNumberOfFiles, ref totalSizeOfFiles, ref backupFolderUpdateList);

            //delete any files/folders from the root dst that aren't found in the root src
            DeleteRootUnlinkedFiles(ref backupFolderUpdateList);

            //check if we found any files to be transfered
            if (totalNumberOfFiles == 0)
            {
                Log.WriteLine("No new/modified files found");
            }
            else
            {
                Log.WriteLine("Number of files to transfer: " + totalNumberOfFiles);
                Log.WriteLine("Total data size: " + ConvertFileSize(totalSizeOfFiles));
                Log.WriteLine("Starting file transfer");

                //calback to main thread
                if (thread.ThreadState != ThreadState.AbortRequested && thread.ThreadState != ThreadState.Aborted)
                {
                    Invoke(new Action(() => OnStartBackupCallback(totalSizeOfFiles)));
                }

                StartTransfer(ref transferDirectoryList, ref transferFileList);
            }

            //update any modified backup folders with the latest DateTime
            UpdateBackupFolderNames(ref backupFolderUpdateList);

            //calback to main thread
            if (thread.ThreadState != ThreadState.AbortRequested && thread.ThreadState != ThreadState.Aborted)
            {
                Invoke(new Action(() => FinishTransfer()));
            }
        }

        /// <summary>
        /// Collects all directory/file data from the source and destination paths, and their sub-directories at every depth, and compares them to see what needs to be transfered
        /// </summary>
        /// <param name="transferDirectoryList">Stores which directories (and their contents) need to be transfered</param>
        /// <param name="transferFileList">Stores which individual files need to be transfered</param>
        /// <param name="totalNumberOfFiles">Adds the total number of files which need to be transfered</param>
        /// <param name="totalSizeOfFiles">Adds the total size of files which need to be transfered</param>
        /// <param name="backupFolderUpdateList">Stores which backup folders need to be updated after transfer</param>
        private void CollectAndCompareDirectoryData(ref List<TransferDirectory> transferDirectoryList, ref List<TransferFile> transferFileList, ref int totalNumberOfFiles, ref ulong totalSizeOfFiles, ref List<Tuple<string, string>> backupFolderUpdateList)
        {
            int numberOfFoundFilesInDst = 0;
            ulong totalSizeOfFilesFoundInDst = 0;
            TransferDirectory transferDirectory = new TransferDirectory();
            BackupDirectory srcBackupDirectory = new BackupDirectory();
            BackupDirectory dstBackupDirectory;
            BackupComparison backupComparison;
            string srcPath;
            string dstPath;
            string dstFullPath;
            bool isFolder;
            TransferFile transferFile = new TransferFile();
            FileInfo srcFileInfo = null;
            FileInfo dstFileInfo;
            BackupFile backupFile;
            string dstFileName;
            bool writeFile;
            string backupName;
            Tuple<string, string> backupFolderPathToUpdate;

            Log.WriteLine("Collecting file/directory data");

            //collect file/directory data at each src
            for (int i = 0; i < Settings.data.sourcePaths.Count; i++)
            {
                srcPath = Settings.data.sourcePaths[i];
                isFolder = PathIsFolder(srcPath);

                //check if src is folder or file
                if (isFolder)
                {
                    if (Directory.Exists(srcPath))
                    {
                        //collect src directory data
                        srcBackupDirectory = new BackupDirectory(srcPath, new List<BackupDirectory>(), new List<BackupFile>());
                        GetDirectoryDataRecursive(ref srcBackupDirectory, applyFilter: true);
                        //add src directory data to transfer directory list
                        transferDirectory = new TransferDirectory(srcBackupDirectory,
                            new string[Settings.data.destinationPaths.Count],
                            new BackupComparison[Settings.data.destinationPaths.Count],
                            new string[Settings.data.destinationPaths.Count]);
                        transferDirectoryList.Add(transferDirectory);
                    }
                    else
                    {
                        Log.WriteLine("Folder to backup could not be found " + srcPath, isError: true);
                        continue;
                    }
                }
                else
                {
                    if (File.Exists(srcPath))
                    {
                        //collect single file data
                        srcFileInfo = new FileInfo(srcPath);
                        backupFile = new BackupFile(srcPath, (ulong)srcFileInfo.Length, srcFileInfo.LastWriteTime);
                        //add single file data to transfer file list
                        transferFile = new TransferFile(backupFile, new string[Settings.data.destinationPaths.Count], new string[Settings.data.destinationPaths.Count]);
                        transferFileList.Add(transferFile);
                    }
                    else
                    {
                        Log.WriteLine("File to backup could not be found " + srcPath, isError: true);
                        continue;
                    }
                }

                //collect file/directory data at each dst and compare it against the current src data
                for (int x = 0; x < Settings.data.destinationPaths.Count; x++)
                {
                    dstPath = Settings.data.destinationPaths[x];
                    //get the oldest backup name at dst path
                    backupName = GetOldestBackupFolderName(dstPath);

                    if (backupName != null)
                    {
                        backupFolderPathToUpdate = new Tuple<string, string>(dstPath, backupName);

                        //check that we haven't already added the backup name
                        if (!backupFolderUpdateList.Contains(backupFolderPathToUpdate))
                        {
                            //add backup names that need to be updated once we've finished transfering files
                            backupFolderUpdateList.Add(backupFolderPathToUpdate);
                        }
                    }
                    else
                    {
                        //couldn't find oldest backup, create new backup name
                        backupName = GetNewBackupFolderName();
                    }

                    //check if src is folder or file
                    if (isFolder)
                    {
                        //src is folder
                        //collect dst directory data
                        dstFullPath = Path.Combine(dstPath, backupName, Path.GetFileName(srcBackupDirectory.path));
                        dstBackupDirectory = new BackupDirectory(dstFullPath, new List<BackupDirectory>(), new List<BackupFile>());
                        GetDirectoryDataRecursive(ref dstBackupDirectory);

                        //compare the dst directory with src directory (to see what needs to be transfered)
                        backupComparison = new BackupComparison(new List<BackupComparison>(), new bool[srcBackupDirectory.backupFileList.Count]);
                        numberOfFoundFilesInDst = 0;
                        CompareDirectoryDataRecursive(ref srcBackupDirectory, ref dstBackupDirectory, ref backupComparison, ref numberOfFoundFilesInDst, ref totalSizeOfFilesFoundInDst, dstFullPath);

                        //check if we found any files to transfer after comparing the src and dst directory
                        if (numberOfFoundFilesInDst > 0)
                        {
                            //add the dst path, backup name and backup comparison data to the current transfer directory
                            transferDirectory.dstPathArray[x] = dstPath;
                            transferDirectory.backupComparisonArray[x] = backupComparison;
                            transferDirectory.backupNameArray[x] = backupName;
                            //add to total
                            totalSizeOfFiles += totalSizeOfFilesFoundInDst;
                            totalNumberOfFiles += numberOfFoundFilesInDst;
                        }
                    }
                    else
                    {
                        //src is file
                        //collect dst single file data
                        dstFileName = Path.Combine(dstPath, backupName, srcFileInfo.Name);
                        writeFile = false;

                        if (!Settings.data.transferUnchangedFiles && File.Exists(dstFileName))
                        {
                            //dst file already exists
                            dstFileInfo = new FileInfo(dstFileName);
                            backupFile = new BackupFile(dstFileName, (ulong)dstFileInfo.Length, dstFileInfo.LastWriteTime);

                            //check if the src file data matches dst file data
                            if (transferFile.backupFile.Equals(backupFile))
                            {
                                if (Settings.data.useMD5ForComparison && !MD5HashesAreEqual(srcFileInfo.FullName, dstFileInfo.FullName))
                                {
                                    //the dst file is different from the src, add the file to be transfered/overwritten
                                    writeFile = true;
                                }
                            }
                            else
                            {
                                //the dst file is different from the src, add the file to be transfered/overwritten
                                writeFile = true;
                            }
                        }
                        else
                        {
                            //file doesn't exist at the dst, add the file to be transfered
                            writeFile = true;
                        }

                        if (writeFile)
                        {
                            //add the dst path and backup name to the current transfer file
                            transferFile.dstPathArray[x] = dstPath;
                            transferFile.backupNameArray[x] = backupName;
                            //add to total
                            totalNumberOfFiles++;
                            totalSizeOfFiles += transferFile.backupFile.fileSize;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Collects all file/folder data at the specified directory path, and if checkChildren is set to true, then it will also collect all file/folder data from each sub-directory at every depth, recursively
        /// </summary>
        /// <param name="backupDirectory">Stores all files/folders found in the directory</param>
        /// <param name="directoryPath">The path of the directory to collect data from</param>
        /// <param name="checkChildren">Specifies whether or not to also collect data from each sub-directory recursively</param>
        /// <param name="applyFilter">Specifies whether or not to apply the user-defined file/path filters when collecting data</param>
        private void GetDirectoryDataRecursive(ref BackupDirectory backupDirectory, bool checkChildren = true, bool applyFilter = false)
        {
            //check if directory exists, and if it passes the filter
            if (!Directory.Exists(backupDirectory.path) || (applyFilter && !PathPassesBackupFilters(backupDirectory.path)))
            {
                backupDirectory.path = null;
                return;
            }

            FileInfo fileInfo;
            //backupDirectory.path = directoryPath;

            //add each file to the backup directory
            foreach (string filePath in Directory.GetFiles(backupDirectory.path))
            {
                try
                {
                    //check if the file passes the filter
                    if (!applyFilter || (applyFilter && PathPassesBackupFilters(filePath)))
                    {
                        fileInfo = new FileInfo(filePath);
                        backupDirectory.backupFileList.Add(new BackupFile(fileInfo.Name, (ulong)fileInfo.Length, fileInfo.LastWriteTime));
                    }
                }
                catch (ThreadAbortException)
                {
                    break;
                }
                catch (Exception e)
                {
                    Log.WriteLine("Couldn't get data from file: '" + filePath + "'. " + e.Message, isError: true);
                }
            }

            //add each sub-directory to the backup directory
            foreach (string subDirectoryPath in Directory.GetDirectories(backupDirectory.path))
            {
                try
                {
                    BackupDirectory subDirectory = new BackupDirectory(subDirectoryPath, new List<BackupDirectory>(), new List<BackupFile>());

                    //run recursively
                    if (checkChildren)
                    {
                        GetDirectoryDataRecursive(ref subDirectory, checkChildren, applyFilter);
                    }
                    //check if the sub-directory path passes the filter
                    else if (!checkChildren && applyFilter && !PathPassesBackupFilters(subDirectoryPath))
                    {
                        subDirectory.path = null;
                    }

                    //only add sub-directory if its path is valid
                    if (!string.IsNullOrEmpty(subDirectory.path))
                    {
                        backupDirectory.subDirectoryList.Add(subDirectory);
                    }
                }
                catch (ThreadAbortException)
                {
                    break;
                }
                catch (Exception e)
                {
                    Log.WriteLine("Couldn't get data from sub-directory: '" + subDirectoryPath + "'. " + e.Message, isError: true);
                }
            }
        }

        /// <summary>
        /// Recursivley compares all file data (to check which files need to be transfered) between the source and destination directories, and each of their sub-directories at every depth
        /// </summary>
        /// <param name="srcBackupDirectory">The source backup directory containing all the folders/files that it currently holds</param>
        /// <param name="dstBackupDirectory">The destination backup directory containing all the folders/files that it currently holds, if null is supplied then the whole source directory is written to the destination</param>
        /// <param name="backupComparison">Stores which files are to be ignored when transfering</param>
        /// <param name="totalNumberOfFiles">Adds the total number of files which need to be transfered</param>
        /// <param name="totalSizeOfFiles">Adds the total size of files which need to be transfered</param>
        /// <param name="dstFullPath">The full path of the dst directory being compared</param>
        private void CompareDirectoryDataRecursive(ref BackupDirectory srcBackupDirectory, ref BackupDirectory dstBackupDirectory, ref BackupComparison backupComparison, ref int totalNumberOfFiles, ref ulong totalSizeOfFiles, string dstFullPath)
        {
            int foundFileIndex;
            string srcPath;
            string dstPath;
            bool writeFile = false;
            bool writeAll = false;

            //if the dst folder is null or empty, then the whole dst folder needs to be written
            if (string.IsNullOrEmpty(dstBackupDirectory.path))
            {
                writeAll = true;

                //create directory at dst path
                try
                {
                    Directory.CreateDirectory(dstFullPath);
                }
                catch (ThreadAbortException)
                {
                    return;
                }
                catch (Exception e)
                {
                    Log.WriteLine("Couldn't create directory at dst: '" + dstFullPath + "'. " + e.Message, isError: true);
                    return;
                }
            }

            //compare files
            for (int i = 0; i < srcBackupDirectory.backupFileList.Count; i++)
            {
                //if writeAll isn't set, do file comparisons
                if (!writeAll)
                {
                    writeFile = false;
                    foundFileIndex = -1;

                    //compare src files against dst files
                    for (int x = 0; x < dstBackupDirectory.backupFileList.Count; x++)
                    {
                        //check if src file exists at the dst
                        if (srcBackupDirectory.backupFileList[i].name == dstBackupDirectory.backupFileList[x].name)
                        {
                            //the file exists at both locations
                            foundFileIndex = x;
                            break;
                        }
                    }

                    //check that we found a matching file, and that if we're going to transfer it regardless
                    if (foundFileIndex != -1 && !Settings.data.transferUnchangedFiles)
                    {
                        //check if the file hasn't been modified
                        if (srcBackupDirectory.backupFileList[i].Equals(dstBackupDirectory.backupFileList[foundFileIndex]))
                        {
                            srcPath = Path.Combine(srcBackupDirectory.path, srcBackupDirectory.backupFileList[i].name);
                            dstPath = Path.Combine(dstBackupDirectory.path, dstBackupDirectory.backupFileList[foundFileIndex].name);

                            //check if we're using md5 checks and if so, if the md5 hashes are equal
                            if (Settings.data.useMD5ForComparison && !MD5HashesAreEqual(srcPath, dstPath))
                            {
                                //md5 hashes do no match or we're not checking md5, file needs to be transfered
                                writeFile = true;
                            }
                        }
                        else
                        {
                            //files do not match, file needs to be transfered
                            writeFile = true;
                        }
                    }
                    else
                    {
                        //we didn't find a file, or we're going to transfer it regardless, file needs to be tranfered
                        writeFile = true;
                    }
                }

                //check if file needs to be written
                if (writeAll || writeFile)
                {
                    //add to total
                    totalNumberOfFiles++;
                    totalSizeOfFiles += (ulong)srcBackupDirectory.backupFileList[i].fileSize;
                }
                else
                {
                    //ignore the file for backup since it doesn't need to backed up
                    backupComparison.filesToIgnoreArray[i] = true;
                }
            }

            //delete any files/folders from the dst backup that aren't in found in src
            if (!string.IsNullOrEmpty(dstBackupDirectory.path) && !string.IsNullOrEmpty(dstBackupDirectory.path))
            {
                DeleteUnlinkedFiles(ref srcBackupDirectory, ref dstBackupDirectory);
            }

            BackupDirectory srcSubDirectoryRef;
            BackupDirectory dstSubDirectoryRef;
            BackupComparison backupComparisonRef;
            string srcSubDirectoryName;

            //compare subdirectories recursively
            for (int i = 0; i < srcBackupDirectory.subDirectoryList.Count; i++)
            {
                //add new backup comparison data for each subdirectory
                backupComparison.subDirectoryList.Add(new BackupComparison(new List<BackupComparison>(), new bool[srcBackupDirectory.subDirectoryList[i].backupFileList.Count]));
                //setup references to pass recursively
                backupComparisonRef = backupComparison.subDirectoryList.Last();
                dstSubDirectoryRef = new BackupDirectory(null, new List<BackupDirectory>(), new List<BackupFile>());
                srcSubDirectoryRef = srcBackupDirectory.subDirectoryList[i];
                srcSubDirectoryName = Path.GetFileName(srcBackupDirectory.subDirectoryList[i].path);

                if (!string.IsNullOrEmpty(dstBackupDirectory.path))
                {
                    //check if src folder exists at the dst
                    for (int x = 0; x < dstBackupDirectory.subDirectoryList.Count; x++)
                    {
                        if (srcSubDirectoryName == Path.GetFileName(dstBackupDirectory.subDirectoryList[x].path))
                        {
                            //folder exists, set it as our reference to be compared recursively
                            dstSubDirectoryRef = dstBackupDirectory.subDirectoryList[x];
                            break;
                        }
                    }
                }

                CompareDirectoryDataRecursive(ref srcSubDirectoryRef, ref dstSubDirectoryRef, ref backupComparisonRef, ref totalNumberOfFiles, ref totalSizeOfFiles, Path.Combine(dstFullPath, srcSubDirectoryName));
            }
        }

        /// <summary>
        /// Begins transfering files from each source directory to each destination directory
        /// </summary>
        /// <param name="transferDirectoryList">The list of directories and their contents that need to be transfered</param>
        /// <param name="transferFileList">The list of individual files that need to be transfered</param>
        private void StartTransfer(ref List<TransferDirectory> transferDirectoryList, ref List<TransferFile> transferFileList)
        {
            int filesBackedUpCount = 0;
            string dstBackupFolder;
            string dstPath;
            string dstRootPath;
            BackupDirectory srcDirectoryRef;

            //transfer each directory
            for (int i = 0; i < transferDirectoryList.Count; i++)
            {
                //transfer to each dst
                for (int x = 0; x < transferDirectoryList[i].dstPathArray.Length; x++)
                {
                    dstBackupFolder = "";

                    try
                    {
                        dstPath = transferDirectoryList[i].dstPathArray[x];
                        if (!string.IsNullOrEmpty(dstPath))
                        {
                            dstBackupFolder = Path.Combine(dstPath, transferDirectoryList[i].backupNameArray[x]);
                            dstRootPath = Path.Combine(dstBackupFolder, Path.GetFileName(transferDirectoryList[i].srcDirectory.path));
                            //create/overwrite backup folder directory
                            Directory.CreateDirectory(dstBackupFolder);
                            //recursively transfer all files to the current dst
                            srcDirectoryRef = transferDirectoryList[i].srcDirectory;
                            TransferDirectoryFilesRecursive(ref srcDirectoryRef, ref transferDirectoryList[i].backupComparisonArray[x], dstRootPath, ref filesBackedUpCount);
                        }
                    }
                    catch (ThreadAbortException)
                    {
                        break;
                    }
                    catch (Exception e)
                    {
                        Log.WriteLine("Couldn't create/overwrite dst directory: '" + dstBackupFolder + "' when starting transfer of folder: '" + transferDirectoryList[i].srcDirectory.path + "'. " + e.Message, isError: true);
                    }
                }
            }

            //transfer single files
            for (int i = 0; i < transferFileList.Count; i++)
            {
                for (int x = 0; x < transferFileList[i].dstPathArray.Length; x++)
                {
                    dstBackupFolder = "";
                    try
                    {
                        dstPath = transferFileList[i].dstPathArray[x];

                        if (!string.IsNullOrEmpty(dstPath))
                        {
                            dstBackupFolder = Path.Combine(dstPath, transferFileList[i].backupNameArray[x]);
                            dstRootPath = Path.Combine(dstBackupFolder, Path.GetFileName(transferFileList[i].backupFile.name));
                            //create/overwrite directory
                            Directory.CreateDirectory(dstBackupFolder);
                            //transfer the file to dst
                            TransferFile(transferFileList[i].backupFile.name, dstRootPath, transferFileList[i].backupFile.lastModified, transferFileList[i].backupFile.fileSize);
                            filesBackedUpCount++;
                        }
                    }
                    catch (ThreadAbortException)
                    {
                        break;
                    }
                    catch (Exception e)
                    {
                        Log.WriteLine("Couldn't create/overwrite dst directory: '" + dstBackupFolder + "' when starting transfer of file: '" + transferFileList[i].backupFile.name + "'. " + e.Message, isError: true);
                    }
                }
            }
        }

        /// <summary>
        /// Recursively transfers all files in the current backup directory, and for each sub-directory at every depth
        /// </summary>
        /// <param name="backupDirectory">The backup directory containing all the folders/files that it currently holds</param>
        /// <param name="backupComparison">The backup comparison containing which files need to ignored when transfering</param>
        /// <param name="dstPath">The backup destination path</param>
        /// <param name="filesBackedUpCount">Adds the total number of files that have been transfered</param>
        private void TransferDirectoryFilesRecursive(ref BackupDirectory backupDirectory, ref BackupComparison backupComparison, string dstPath, ref int filesBackedUpCount)
        {
            if (!Directory.Exists(dstPath))
            {
                Log.WriteLine("Couldn't find dst directory for transfer: '" + dstPath + "'", isError: true);
                return;
            }

            string srcPath = "";

            for (int i = 0; i < backupDirectory.backupFileList.Count; i++)
            {
                try
                {
                    //check that the file isn't being ignored
                    if (!backupComparison.filesToIgnoreArray[i])
                    {
                        srcPath = Path.Combine(backupDirectory.path, backupDirectory.backupFileList[i].name);
                        //transfer the file to dst
                        TransferFile(srcPath, Path.Combine(dstPath, backupDirectory.backupFileList[i].name), backupDirectory.backupFileList[i].lastModified, backupDirectory.backupFileList[i].fileSize);
                        filesBackedUpCount++;
                    }
                }
                catch (ThreadAbortException)
                {
                    break;
                }
                catch (Exception e)
                {
                    Log.WriteLine("Couldn't transfer file: '" + srcPath + "'. " + e.Message, isError: true);
                }
            }

            BackupDirectory subDirectoryRef;
            BackupComparison subDirectoryComparisonRef;

            //recursively transfer all files in each sub-directory
            for (int i = 0; i < backupDirectory.subDirectoryList.Count; i++)
            {
                //collect references
                subDirectoryRef = backupDirectory.subDirectoryList[i];
                subDirectoryComparisonRef = backupComparison.subDirectoryList[i];

                TransferDirectoryFilesRecursive(ref subDirectoryRef, ref subDirectoryComparisonRef, Path.Combine(dstPath, Path.GetFileName(backupDirectory.subDirectoryList[i].path)), ref filesBackedUpCount);
            }
        }

        /// <summary>
        /// Transfers an individual file from the source path to the destination path. If MD5 transfer is enabled by the user, 
        /// then the two files will also compare their MD5 checksums, and if the comparison fails, another transfer attempt is made
        /// </summary>
        /// <param name="srcPath">The path of the file to transfer</param>
        /// <param name="dstPath">The destination path (where to transfer the file)</param>
        /// <param name="fileLastModified">The DateTime the file was last modified</param>
        /// <param name="fileSize">The size of the file to transfer</param>
        /// <param name="extraMD5TransferAttempts">Used internally to track how many additional transfer attempts have been made when using the MD5 checksum, should be set to 0 by default</param>
        private void TransferFile(string srcPath, string dstPath, DateTime fileLastModified, ulong fileSize, int extraMD5TransferAttempts = 0)
        {
            if (!File.Exists(srcPath))
            {
                Log.WriteLine("Couldn't find file at src to transfer: '" + srcPath + "'", isError: true);

                if (thread.ThreadState != ThreadState.AbortRequested && thread.ThreadState != ThreadState.Aborted)
                {
                    Invoke(new Action(() => OnFailedFileTransferCallback(fileSize)));
                }

                return;
            }

            //callback to main thread
            if (thread.ThreadState != ThreadState.AbortRequested && thread.ThreadState != ThreadState.Aborted)
            {
                Invoke(new Action(() => OnStartFileTransferCallback(dstPath)));
            }

            bool finishTransfer = false;
            FileStream srcStream = null;
            FileStream dstStream = null;

            Log.WriteLine("Transfering file: " + Path.GetFileName(srcPath) + " (" + ConvertFileSize(fileSize) + ")");

            try
            {
                ulong freeBytesForUser;
                ulong totalBytes;
                ulong freeBytes;
                string diskDriveLetter = Path.GetPathRoot(dstPath).Replace("\\", "");

                //get free disk space at dst
                if (GetDiskFreeSpaceEx(diskDriveLetter, out freeBytesForUser, out totalBytes, out freeBytes))
                {
                    //check if the dst drive has enough space for the file
                    if (fileSize <= freeBytesForUser)
                    {
                        //transfer file from src to dst
                        srcStream = File.OpenRead(srcPath);
                        dstStream = File.Create(dstPath);
                        srcStream.CopyTo(dstStream);
                        dstStream.Flush();

                        srcStream.Close();
                        srcStream = null;
                        dstStream.Close();
                        dstStream = null;

                        //update file write time
                        File.SetLastWriteTime(dstPath, fileLastModified);

                        //do MD5 checks if required
                        if (Settings.data.useMD5ForTransfer)
                        {
                            if (!MD5HashesAreEqual(srcPath, dstPath))
                            {
                                //attempt to re-transfer the file if MD5 hashes dont match
                                if (extraMD5TransferAttempts <= maxMD5ExtraTransferAttempts)
                                {
                                    Log.WriteLine("MD5 file hashes do not match, attempting to transfer file again");
                                    TransferFile(srcPath, dstPath, fileLastModified, fileSize, extraMD5TransferAttempts++);
                                }
                                //max number of re-transfer attempts reached and still failed to transfer the file
                                else
                                {
                                    Log.WriteLine("MD5 file hashes do not match, even after additional attempts. Deleting corrupted file: " + dstPath, isError: true);
                                    //delete the corrupted file
                                    File.Delete(dstPath);
                                    finishTransfer = true;
                                }
                            }
                            else
                            {
                                finishTransfer = true;
                            }
                        }
                        else
                        {
                            finishTransfer = true;
                        }
                    }
                    else
                    {
                        Log.WriteLine("Disk " + diskDriveLetter + " does not have enough space. Couldn't write file: " + srcPath + " to " + diskDriveLetter + ". " +
                            "File size: " + ConvertFileSize(fileSize) + " vs disk space left: " + ConvertFileSize(freeBytesForUser), isError: true);
                    }
                }
                else
                {
                    Log.WriteLine("Could not get free disk space from disk " + diskDriveLetter, isError: true);
                }
            }
            catch (ThreadAbortException)
            {
                //close streams if thread aborted
                if (srcStream != null)
                {
                    srcStream.Close();
                }
                if (dstStream != null)
                {
                    dstStream.Close();
                }
                return;
            }
            catch (Exception e)
            {
                Log.WriteLine("Couldn't transfer file: '" + dstPath + "'. " + e.Message, isError: true);
            }

            //callback to main thread
            if (finishTransfer && thread.ThreadState != ThreadState.AbortRequested && thread.ThreadState != ThreadState.Aborted)
            {
                Invoke(new Action(() => OnFinishFileTransferCallback(fileSize)));
            }
        }

        /// <summary>
        /// Finishes the transfer process by resetting some UI elements, clearing memory, and if enabled by the user, sends an email
        /// </summary>
        /// <param name="stoppedEarly">Indicates whether or not the transfer was stopped early by the user</param>
        private void FinishTransfer(bool stoppedEarly = false)
        {
            StopTransferStatTimer();

            //reset UI elements
            transferSpeedLabel.Text = "Transfer Speed:";
            estimatedTimeLabel.Text = "Estimated Time:";
            dataTransferedLabel.Text = "Data Transfered:";
            backupProgressBar.Value = 100;

            //clear transfer stat memory
            currentFilePathStat = null;
            lastFileSizeStat = 0;
            totalTransferedFileSizeStat = 0;
            totalFileSizeStat = 0;
            currentTransferedFileSizeStat = 0;
            transferSpeedHistoryListStat.Clear();

            //send email if required
            if (Settings.data.useEmail)
            {
                if (isSendingEmail && smtpClient != null)
                {
                    smtpClient.SendAsyncCancel();
                }
                else
                {
                    SendBackupFinishedEmail(stoppedEarly);
                }
            }
            else
            {
                FinishBackup();
            }
        }

        /// <summary>
        /// Finishes the backup process by resetting some UI elements to allow a new backup to start, displaying errors generated during the backup to the user, or automatically closing the program if it was supplied with args at run time
        /// </summary>
        private void FinishBackup()
        {
            //calculate time taken to backup everything
            TimeSpan timeTaken = DateTime.Now - backupStartTime;
            Log.WriteLine("Total duration: " + TimeSpanToFormattedString(timeTaken));
            Log.WriteLine("--Backup finished--");
            //reset UI elements
            backupButton.Text = "Start Backup";
            backupIsRunning = false;
            thread = null;
            backupStartTime = new DateTime();

            //automatically close the program if the 'scheduledRun' argument was supplied and 'scheduleAutoClose' is set
            if (autoRun && Settings.data.scheduleAutoClose)
            {
                Close();
            }
            else
            {
                string errorString = Log.GetSessionErrorsString();

                if (!string.IsNullOrEmpty(errorString))
                {
                    //show the user all errors that were accumulated during backup
                    errorString = Environment.NewLine + "Backup finished with the following error(s):" + Environment.NewLine + errorString;
                    AppendInfoTextBoxText(errorString);

                    if (!autoRun)
                    {
                        MessageBox.Show(errorString, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }

            //clear error memory
            Log.ClearSessionErrors();
        }

        /// <summary>
        /// Used to stop a backup while it is currently running
        /// </summary>
        private void StopBackupEarly()
        {
            Log.WriteLine("Stopping backup early");

            StopTransferStatTimer();

            //abort the backup thread
            if (thread != null)
            {
                thread.Abort();

                //wait for thread to be aborted (or maxThreadAbortWaitTime reached)
                int timeCounter = 0;
                while (thread.ThreadState != ThreadState.AbortRequested && timeCounter < maxThreadAbortWaitTime)
                {
                    timeCounter += 100;
                    Thread.Sleep(100);
                }
            }

            //delete partially written file & the folder that contains it, if its empty
            if (!string.IsNullOrEmpty(currentFilePathStat) && File.Exists(currentFilePathStat))
            {
                Log.WriteLine("Deleting partially written file: " + currentFilePathStat);

                try
                {
                    File.Delete(currentFilePathStat);
                }
                catch (Exception e)
                {
                    Log.WriteLine("Couldn't delete partially written file: " + currentFilePathStat + ". " + e.Message, isError: true);
                }

                try
                {
                    //check that the directory is empty
                    if (Directory.GetFiles(Path.GetDirectoryName(currentFilePathStat)).Length == 0 && Directory.GetDirectories(Path.GetDirectoryName(currentFilePathStat)).Length == 0)
                    {
                        Log.WriteLine("Deleting empty folder: " + Path.GetDirectoryName(currentFilePathStat));
                        Directory.Delete(Path.GetDirectoryName(currentFilePathStat));
                    }
                }
                catch (Exception e)
                {
                    Log.WriteLine("Couldn't delete empty directory: " + Path.GetDirectoryName(currentFilePathStat) + ". " + e.Message, isError: true);
                }
            }

            FinishTransfer(true);
        }

        /// <summary>
        /// Renames backup folders which need to be updated with the latest DateTime
        /// </summary>
        /// <param name="backupFolderUpdateList">A list containing all the backup folders which need to be updated</param>
        private void UpdateBackupFolderNames(ref List<Tuple<string, string>> backupFolderUpdateList)
        {
            string dstPath;
            string backupName;
            string dstFullPath;
            string dstNewPath;

            //update overwritten backup folder names with the latest date time
            for (int i = 0; i < backupFolderUpdateList.Count; i++)
            {
                try
                {
                    dstPath = backupFolderUpdateList[i].Item1;
                    backupName = backupFolderUpdateList[i].Item2;
                    dstFullPath = Path.Combine(dstPath, backupName);
                    dstNewPath = Path.Combine(dstPath, GetNewBackupFolderName());

                    if (dstFullPath != dstNewPath)
                    {
                        Directory.Move(dstFullPath, dstNewPath);
                        Directory.SetLastWriteTime(dstNewPath, DateTime.Now);
                    }
                }
                catch (Exception e)
                {
                    Log.WriteLine("Couldn't update the backup folder name with the latest date and time. Please close the backup folder in any open programs and retry backing up. " + e.Message, isError: true);
                }
            }
        }

        /// <summary>
        /// Finds the oldest backup within the specified folder path
        /// </summary>
        /// <param name="folderPath">The directory path</param>
        /// <returns>The folder name of the oldest backup directory, or null if no backup directory is found</returns>
        private string GetOldestBackupFolderName(string folderPath)
        {
            //check that directory even exists
            if (Directory.Exists(folderPath))
            {
                string[] folderNames = Directory.GetDirectories(folderPath);

                //check if directory has any folders
                if (folderNames.Length > 0)
                {
                    List<string> foundSavedBackupNames = new List<string>();
                    List<DateTime> foundSavedBackupDateTimes = new List<DateTime>();
                    DateTime dateTime;
                    string folderName;
                    string extractedFolderName;

                    //go through each folder name
                    for (int i = 0; i < folderNames.Length; i++)
                    {
                        folderName = Path.GetFileName(folderNames[i]);

                        //check if the current folder name contains the backup folder name
                        if (!folderName.Contains(Settings.data.backupName))
                        {
                            continue;
                        }

                        //remove the backup folder name from the current folder name
                        extractedFolderName = folderName.Replace(Settings.data.backupName + " ", "");

                        //check that the DateTime portion of the current folder name is enclosed in brackets
                        if (extractedFolderName[0] != '[' || extractedFolderName.Last() != ']')
                        {
                            continue;
                        }

                        //remove the brackets from the current folder name
                        extractedFolderName = extractedFolderName.Substring(1, extractedFolderName.Length - 2);

                        //convert the remaining text to DateTime
                        if (FormattedStringToDateTime(extractedFolderName, out dateTime))
                        {
                            //add the backup name and its DateTime
                            foundSavedBackupNames.Add(folderName);
                            foundSavedBackupDateTimes.Add(dateTime);
                        }
                    }

                    //check if we found any backup names
                    if (foundSavedBackupNames.Count > 0)
                    {
                        //check if the number of backups in the folder has exceeded the max number of backups
                        if (foundSavedBackupNames.Count >= Settings.data.numberOfConcurrentBackups)
                        {
                            int oldestIndex = 0;
                            DateTime oldestDateTime = foundSavedBackupDateTimes[0];

                            //find the oldest backup folder
                            for (int i = 1; i < foundSavedBackupDateTimes.Count; i++)
                            {
                                if (foundSavedBackupDateTimes[i] < oldestDateTime)
                                {
                                    oldestDateTime = foundSavedBackupDateTimes[i];
                                    oldestIndex = i;
                                }
                            }

                            //return the oldest backup folder
                            return foundSavedBackupNames[oldestIndex];
                        }
                    }
                }
            }

            return null;
        }

        private bool MD5HashesAreEqual(string srcPath, string dstPath)
        {
            byte[] srcHash;
            byte[] dstHash;
            MD5 md5 = null;
            FileStream srcStream = null;
            FileStream dstStream = null;

            Log.WriteLine("Comparing MD5 file hashes");

            try
            {
                md5 = MD5.Create();
                //open the src file and compute its hash
                srcStream = File.OpenRead(srcPath);
                srcHash = md5.ComputeHash(srcStream);
                srcStream.Close();
                srcStream = null;

                //open the dst file and compute its hash
                dstStream = File.OpenRead(dstPath);
                dstHash = md5.ComputeHash(dstStream);
                dstStream.Close();
                dstStream = null;

                md5.Dispose();
                md5 = null;

                //compare the two hashes
                return ByteArraysAreEqual(ref srcHash, ref dstHash);
            }
            catch (ThreadAbortException)
            {
                //close streams if thread aborted
                if (srcStream != null)
                {
                    srcStream.Close();
                }
                if (dstStream != null)
                {
                    dstStream.Close();
                }
                if (md5 != null)
                {
                    md5.Dispose();
                }
            }
            catch (Exception e)
            {
                Log.WriteLine("Couldn't compare MD5 hashes. " + e.Message, isError: true);
            }

            return false;
        }

        private bool ByteArraysAreEqual(ref byte[] hashA, ref byte[] hashB)
        {
            if (hashA.Length == hashB.Length)
            {
                bool equals = true;
                for (int i = 0; i < hashA.Length; i++)
                {
                    if (hashA[i] != hashB[i])
                    {
                        equals = false;
                        break;
                    }
                }
                return equals;
            }
            return false;
        }

        /// <summary>
        /// Deletes any files from each backup folder root path location that don't match the source files
        /// </summary>
        /// <param name="backupFolderUpdateList">A list containing all the backup folders which need to be updated</param>
        private void DeleteRootUnlinkedFiles(ref List<Tuple<string, string>> backupFolderUpdateList)
        {
            BackupDirectory dstRootBackupDirectory;
            string dstPath;
            string dstBackupFolderName;
            BackupDirectory srcRootBackupDirectory = new BackupDirectory(null, new List<BackupDirectory>(), new List<BackupFile>());
            bool isFolder = false;

            //collect all files/folders at the src root
            for (int i = 0; i < Settings.data.sourcePaths.Count; i++)
            {
                isFolder = PathIsFolder(Settings.data.sourcePaths[i]);
                if (isFolder)
                {
                    BackupDirectory subDirectory = new BackupDirectory(Settings.data.sourcePaths[i], new List<BackupDirectory>(), new List<BackupFile>());
                    srcRootBackupDirectory.subDirectoryList.Add(subDirectory);
                }
                else
                {
                    srcRootBackupDirectory.backupFileList.Add(new BackupFile(Path.GetFileName(Settings.data.sourcePaths[i]), 0, new DateTime()));
                }
            }

            for (int i = 0; i < backupFolderUpdateList.Count; i++)
            {
                dstPath = backupFolderUpdateList[i].Item1;
                dstBackupFolderName = backupFolderUpdateList[i].Item2;
                dstRootBackupDirectory = new BackupDirectory(Path.Combine(dstPath, dstBackupFolderName), new List<BackupDirectory>(), new List<BackupFile>());

                //collect all files/folders at the dst root
                GetDirectoryDataRecursive(ref dstRootBackupDirectory, checkChildren: false);
                //delete any unlinked files/folders at the dst root
                DeleteUnlinkedFiles(ref srcRootBackupDirectory, ref dstRootBackupDirectory);
            }
        }

        /// <summary>
        /// Deletes any files and folders from the destination backup location that are not found in the source backup
        /// </summary>
        /// <param name="srcBackupDirectory">The source backup directory containing all the folders/files that it currently holds</param>
        /// <param name="dstBackupDirectory">The destination backup directory containing all the folders/files that it currently holds</param>
        private void DeleteUnlinkedFiles(ref BackupDirectory srcBackupDirectory, ref BackupDirectory dstBackupDirectory)
        {
            bool foundFile;

            //delete files from dst that aren't found in src
            for (int i = 0; i < dstBackupDirectory.backupFileList.Count; i++)
            {
                foundFile = false;

                //check if folder is found in both the src and dst paths
                for (int x = 0; x < srcBackupDirectory.backupFileList.Count; x++)
                {
                    if (dstBackupDirectory.backupFileList[i].name == srcBackupDirectory.backupFileList[x].name)
                    {
                        foundFile = true;
                        break;
                    }
                }

                //check if we found a file
                if (!foundFile)
                {
                    try
                    {
                        Log.WriteLine("Deleting unlinked file: " + dstBackupDirectory.backupFileList[i].name);
                        File.Delete(Path.Combine(dstBackupDirectory.path, dstBackupDirectory.backupFileList[i].name));
                    }
                    catch (ThreadAbortException)
                    {

                    }
                    catch (Exception e)
                    {
                        Log.WriteLine("Couldn't delete unlinked file: " + dstBackupDirectory.backupFileList[i].name + ". " + e.Message, isError: true);
                    }
                }
            }

            //delete folders from dst thant aren't found in src
            for (int i = 0; i < dstBackupDirectory.subDirectoryList.Count; i++)
            {
                foundFile = false;

                //check if folder is found in both the src and dst paths
                for (int x = 0; x < srcBackupDirectory.subDirectoryList.Count; x++)
                {
                    if (Path.GetFileName(dstBackupDirectory.subDirectoryList[i].path) == Path.GetFileName(srcBackupDirectory.subDirectoryList[x].path))
                    {
                        foundFile = true;
                        break;
                    }
                }

                //check if we found a folder
                if (!foundFile)
                {
                    try
                    {
                        Log.WriteLine("Deleting unlinked folder and all contents: " + dstBackupDirectory.subDirectoryList[i].path);
                        Directory.Delete(dstBackupDirectory.subDirectoryList[i].path, true);
                    }
                    catch (ThreadAbortException)
                    {

                    }
                    catch (Exception e)
                    {
                        Log.WriteLine("Couldn't delete unlinked folder: " + dstBackupDirectory.subDirectoryList[i].path + ". " + e.Message, isError: true);
                    }
                }
            }
        }

        /// <summary>
        /// Callback to the main thread when a backup starts (for tracking file transfer stats)
        /// </summary>
        /// <param name="totalSizeOfFiles">The total size of files which need to be transfered</param>
        private void OnStartBackupCallback(ulong totalSizeOfFiles)
        {
            totalFileSizeStat = totalSizeOfFiles;
            transferStatTimer.Start();
        }

        /// <summary>
        /// Callback to the main thread when starting a file transfer (for tracking file transfer stats)
        /// </summary>
        /// <param name="filePath">The path of the file that is to be transfered</param>
        private void OnStartFileTransferCallback(string filePath)
        {
            currentFilePathStat = filePath;
            currentTransferedFileSizeStat = totalTransferedFileSizeStat;
            lastFileSizeStat = 0;
        }

        /// <summary>
        /// Callback to the main thread when finishing a file transfer (for tracking file transfer stats)
        /// </summary>
        /// <param name="fileSize">The size of the file that was transfered</param>
        private void OnFinishFileTransferCallback(ulong fileSize)
        {
            totalTransferedFileSizeStat = currentTransferedFileSizeStat + fileSize;
            currentFilePathStat = null;
            dataTransferedLabel.Text = "Data Transfered: " + ConvertFileSize(totalTransferedFileSizeStat) + " / " + ConvertFileSize(totalFileSizeStat);
            SetBackupProgressBarValue(totalTransferedFileSizeStat, totalFileSizeStat);
        }

        /// <summary>
        /// Callback to the main thread when a file transfer fails (for tracking file transfer stats)
        /// </summary>
        /// <param name="fileSize">The size of the file that failed to be transfered</param>
        private void OnFailedFileTransferCallback(ulong fileSize)
        {
            totalTransferedFileSizeStat += fileSize;
            dataTransferedLabel.Text = "Data Transfered: " + ConvertFileSize(totalTransferedFileSizeStat) + " / " + ConvertFileSize(totalFileSizeStat);
            SetBackupProgressBarValue(totalTransferedFileSizeStat, totalFileSizeStat);
        }

        /// <summary>
        /// Runs whenever a transfer stat timer tick has elapsed
        /// </summary>
        /// <param name="sender">The object that triggered the timer elapsed event</param>
        /// <param name="e">Additional data provided by the timer elapsed event</param>
        private void TransferStatTimerElapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            //callback to the main thread
            Invoke(new Action(() => CalculateTransferStats()));
        }

        /// <summary>
        /// Calculates transfer stats to be displayed to the user during a backup transfer
        /// </summary>
        private void CalculateTransferStats()
        {
            ulong fileSizeDiff = 0;

            //check if current file exists
            if (!string.IsNullOrEmpty(currentFilePathStat) && File.Exists(currentFilePathStat))
            {
                ulong fileSize = (ulong)new FileInfo(currentFilePathStat).Length;
                //calculate how much of the file we've transfered this tick
                fileSizeDiff = fileSize - lastFileSizeStat;
                //add it to the total amount transfered
                totalTransferedFileSizeStat += fileSizeDiff;
                //update last file size for next tick
                lastFileSizeStat = fileSize;

                //if the file size diff is greater than 0, then add it to the transfer speed history list
                if (fileSizeDiff > 0)
                {
                    transferSpeedHistoryListStat.Add(fileSizeDiff);
                }
            }

            //if the transfer speed history list has exceeded its max size, remove the earliest slot
            if (transferSpeedHistoryListStat.Count > transferSpeedHistoryMaxSize)
            {
                transferSpeedHistoryListStat.RemoveAt(0);
            }

            //calculate the average transfer speed
            ulong averageTransferSpeed = 0;
            for (int i = 0; i < transferSpeedHistoryListStat.Count; i++)
            {
                averageTransferSpeed += transferSpeedHistoryListStat[i];
            }
            if (transferSpeedHistoryListStat.Count > 0)
            {
                averageTransferSpeed = averageTransferSpeed / (ulong)transferSpeedHistoryListStat.Count;
            }

            //update estimated time label
            if (averageTransferSpeed > 0)
            {
                TimeSpan estimatedTime = new TimeSpan(0, 0, (int)(totalFileSizeStat / averageTransferSpeed));
                estimatedTimeLabel.Text = "Estimated Time: " + estimatedTime.ToString();
            }
            else
            {
                estimatedTimeLabel.Text = "Estimated Time: N/A";
            }

            //update transfer stat labels
            transferSpeedLabel.Text = "Transfer Speed: " + ConvertFileSize(fileSizeDiff) + "/s";
            dataTransferedLabel.Text = "Data Transfered: " + ConvertFileSize(totalTransferedFileSizeStat) + " / " + ConvertFileSize(totalFileSizeStat);

            //update the progress bar
            SetBackupProgressBarValue(totalTransferedFileSizeStat, totalFileSizeStat);
        }

        private void StopTransferStatTimer()
        {
            if (transferStatTimer != null)
            {
                transferStatTimer.Stop();
                transferStatTimer.Elapsed -= TransferStatTimerElapsed;
                transferStatTimer.Close();
                transferStatTimer = null;
            }
        }

        private void LoadSettingsTab()
        {
            destinationsListBox.Items.Clear();
            destinationsListBox.Items.AddRange(Settings.data.destinationPaths.ToArray());
            destinationsListBox.ClearSelected();

            sourcesListBox.Items.Clear();
            sourcesListBox.Items.AddRange(Settings.data.sourcePaths.ToArray());
            sourcesListBox.ClearSelected();

            backupNameTextBox.Text = Settings.data.backupName.ToString();

            numberOfConcurrentBackupsNumericUpDown.Text = Settings.data.numberOfConcurrentBackups.ToString();
            useMD5ForTransferCheckBox.Checked = Settings.data.useMD5ForTransfer;
            useMD5ForComparisonCheckBox.Checked = Settings.data.useMD5ForComparison;
            transferUnchangedFilesCheckBox.Checked = Settings.data.transferUnchangedFiles;
            writeToLogCheckBox.Checked = Settings.data.writeToLog;
        }

        private void LoadScheduleTab()
        {
            scheduleTabControl.SelectedTab = dailyTabPage;
            weeklyDayToRunComboBox.SelectedItem = DateTime.Now.DayOfWeek.ToString();
            dailyTimePicker.Value = DateTime.Now;
            weeklyTimePicker.Value = DateTime.Now;
            monthlyTimePicker.Value = DateTime.Now;
            monthlyDayToRunComboBox.SelectedIndex = 0;
            scheduleAutoCloseCheckBox.Checked = Settings.data.scheduleAutoClose;

            using (TaskService ts = new TaskService())
            {
                Microsoft.Win32.TaskScheduler.Task task = ts.GetTask("\\SimpleBackup");

                if (task != null)
                {
                    bool taskHasCorrectPath = false;
                    foreach (Microsoft.Win32.TaskScheduler.Action action in task.Definition.Actions)
                    {
                        if (action.GetType() == typeof(ExecAction))
                        {
                            ExecAction execAction = (ExecAction)action;
                            if (execAction.Path == Assembly.GetExecutingAssembly().Location)
                            {
                                taskHasCorrectPath = true;
                                break;
                            }
                        }
                    }

                    if (!taskHasCorrectPath)
                    {
                        UnregisterTaskSchedule();
                        scheduleEnabledCheckBox.Checked = false;
                        scheduleTableLayoutPanel.Enabled = false;
                        return;
                    }

                    if (task.Definition.Triggers.Count == 1)
                    {
                        Trigger trigger = task.Definition.Triggers[0];

                        if (trigger.TriggerType == TaskTriggerType.Daily)
                        {
                            DailyTrigger dailyTrigger = (DailyTrigger)trigger;
                            DateTime dateTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day,
                               dailyTrigger.StartBoundary.TimeOfDay.Hours,
                               dailyTrigger.StartBoundary.TimeOfDay.Minutes,
                               dailyTrigger.StartBoundary.TimeOfDay.Seconds);
                            dailyTimePicker.Value = dateTime;

                            scheduleTabControl.SelectedTab = dailyTabPage;
                        }
                        else if (trigger.TriggerType == TaskTriggerType.Weekly)
                        {
                            WeeklyTrigger weeklyTrigger = (WeeklyTrigger)trigger;

                            DayOfWeek dayOfWeek = DaysOfTheWeekToDayOfWeek(weeklyTrigger.DaysOfWeek);
                            weeklyDayToRunComboBox.SelectedIndex = weeklyDayToRunComboBox.Items.IndexOf(dayOfWeek.ToString());
                            DateTime dateTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day,
                                weeklyTrigger.StartBoundary.TimeOfDay.Hours,
                                weeklyTrigger.StartBoundary.TimeOfDay.Minutes,
                                weeklyTrigger.StartBoundary.TimeOfDay.Seconds);
                            weeklyTimePicker.Value = dateTime;

                            scheduleTabControl.SelectedTab = weeklyTabPage;
                        }
                        else if (trigger.TriggerType == TaskTriggerType.Monthly)
                        {
                            MonthlyTrigger monthlyTrigger = (MonthlyTrigger)trigger;
                            if (monthlyTrigger.RunOnLastDayOfMonth)
                            {
                                monthlyDayToRunComboBox.SelectedIndex = monthlyDayToRunComboBox.Items.IndexOf("Last Day");
                            }
                            else
                            {
                                monthlyDayToRunComboBox.SelectedIndex = monthlyDayToRunComboBox.Items.IndexOf("First Day");
                            }

                            DateTime dateTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day,
                                monthlyTrigger.StartBoundary.TimeOfDay.Hours,
                                monthlyTrigger.StartBoundary.TimeOfDay.Minutes,
                                monthlyTrigger.StartBoundary.TimeOfDay.Seconds);
                            monthlyTimePicker.Value = dateTime;

                            scheduleTabControl.SelectedTab = monthlyTabPage;
                        }

                        scheduleEnabledCheckBox.Checked = true;
                        scheduleTableLayoutPanel.Enabled = true;
                    }
                    else
                    {
                        UnregisterTaskSchedule();
                        scheduleEnabledCheckBox.Checked = false;
                        scheduleTableLayoutPanel.Enabled = false;
                    }
                }
                else
                {
                    scheduleEnabledCheckBox.Checked = false;
                    scheduleTableLayoutPanel.Enabled = false;
                }
            }
        }

        private void LoadEmailTab()
        {
            emailSendModeComboBox.SelectedIndex = Settings.data.emailSendMode;
            useEmailCheckBox.Checked = Settings.data.useEmail;
            emailTableLayoutPanel.Enabled = useEmailCheckBox.Checked;

            try
            {
                emailServerTextBox.Text = Settings.DecryptString(Settings.data.emailServer);
                emailServerPortNumericUpDown.Text = Settings.DecryptInt(Settings.data.emailServerPort).ToString();
                emailUserNameTextBox.Text = Settings.DecryptString(Settings.data.emailUserName);
                emailPasswordTextBox.Text = Settings.DecryptString(Settings.data.emailPassword);
                emailSenderTextBox.Text = Settings.DecryptString(Settings.data.emailSender);
                emailReceiverTextBox.Text = Settings.DecryptString(Settings.data.emailReceiver);
                emailUseSSLCheckBox.Checked = Settings.DecryptBool(Settings.data.emailUseSSL);
            }
            catch (Exception e)
            {
                MessageBox.Show("Error decrypting email data: " + e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                emailServerTextBox.Text = "";
                emailServerPortNumericUpDown.Value = 0;
                emailServerPortNumericUpDown.Text = "0";
                emailUserNameTextBox.Text = "";
                emailPasswordTextBox.Text = "";
                emailSenderTextBox.Text = "";
                emailReceiverTextBox.Text = "";
                emailUseSSLCheckBox.Checked = false;
            }
        }

        private void LoadFilterTab()
        {
            filterPathTextBox.Text = "";
            filterIgnoreCaseCheckBox.Checked = true;

            filterIgnoreComboBox.Items.Clear();
            foreach (Enum value in Enum.GetValues(typeof(BackupFilterIgnoreType)))
            {
                filterIgnoreComboBox.Items.Add(EnumToString(value));
            }
            filterIgnoreComboBox.SelectedIndex = 0;

            filterPathTypeComboBox.Items.Clear();
            foreach (Enum value in Enum.GetValues(typeof(BackupFilterPathType)))
            {
                filterPathTypeComboBox.Items.Add(EnumToString(value));
            }
            filterPathTypeComboBox.SelectedIndex = 0;

            filterComparerTypeComboBox.Items.Clear();
            foreach (Enum value in Enum.GetValues(typeof(BackupFilterComparerType)))
            {
                filterComparerTypeComboBox.Items.Add(EnumToString(value));
            }
            filterComparerTypeComboBox.SelectedIndex = 0;

            filterListView.Items.Clear();
            for (int i = 0; i < Settings.data.backupFilters.Count; i++)
            {
                FilterListViewAdd(Settings.data.backupFilters[i]);
            }
        }

        private void SaveGeneralSettings()
        {
            Settings.data.backupName = backupNameTextBox.Text.Trim();
            Settings.data.numberOfConcurrentBackups = (int)numberOfConcurrentBackupsNumericUpDown.Value;
            Settings.data.useMD5ForTransfer = useMD5ForTransferCheckBox.Checked;
            Settings.data.useMD5ForComparison = useMD5ForComparisonCheckBox.Checked;
            Settings.data.sourcePaths = ListBoxItemsToList<string>(ref sourcesListBox);
            Settings.data.destinationPaths = ListBoxItemsToList<string>(ref destinationsListBox);
            Settings.data.transferUnchangedFiles = transferUnchangedFilesCheckBox.Checked;
            Settings.data.writeToLog = writeToLogCheckBox.Checked;
            Settings.Save();
        }

        private void SaveScheduleSettings()
        {
            if (scheduleEnabledCheckBox.Checked)
            {
                RegisterTaskSchedule();
            }
            else
            {
                UnregisterTaskSchedule();
            }

            Settings.data.scheduleAutoClose = scheduleAutoCloseCheckBox.Checked;
            Settings.Save();
        }

        private void SaveEmailSettings()
        {
            Settings.data.useEmail = useEmailCheckBox.Checked;
            Settings.data.emailServer = Settings.Encrypt(emailServerTextBox.Text.Trim());
            Settings.data.emailServerPort = Settings.Encrypt(emailServerPortNumericUpDown.Value.ToString());
            Settings.data.emailUserName = Settings.Encrypt(emailUserNameTextBox.Text.Trim());
            Settings.data.emailPassword = Settings.Encrypt(emailPasswordTextBox.Text.Trim());
            Settings.data.emailSender = Settings.Encrypt(emailSenderTextBox.Text.Trim());
            Settings.data.emailReceiver = Settings.Encrypt(emailReceiverTextBox.Text.Trim());
            Settings.data.emailUseSSL = Settings.Encrypt(emailUseSSLCheckBox.Checked.ToString());
            Settings.data.emailSendMode = emailSendModeComboBox.SelectedIndex;
            Settings.Save();
        }

        private void SaveFilterSettings()
        {
            Settings.data.backupFilters = FilterListViewGetAll();
            Settings.Save();
        }

        /// <summary>
        /// Checks and warns whether the UI input values for general settings are valid
        /// </summary>
        /// <returns>Whether the input values are valid</returns>
        private bool GeneralSettingsInputsAreValid()
        {
            bool valid = true;

            if (string.IsNullOrWhiteSpace(backupNameTextBox.Text))
            {
                MessageBox.Show("Backup name can't be empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            char[] invalidChars = Path.GetInvalidFileNameChars();
            char? invalidChar = null;

            for (int i = 0; i < invalidChars.Length; i++)
            {
                if (backupNameTextBox.Text.Contains(invalidChars[i].ToString()))
                {
                    invalidChar = invalidChars[i];
                    break;
                }
            }

            if (invalidChar != null)
            {
                MessageBox.Show("Backup name contains invalid character(s):\n" + invalidChar, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            else if (string.IsNullOrEmpty(numberOfConcurrentBackupsNumericUpDown.Text))
            {
                MessageBox.Show("Number of backups can't be empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            else if (numberOfConcurrentBackupsNumericUpDown.Value < 0)
            {
                MessageBox.Show("Number of backups can't be negative", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            else if (sourcesListBox.Items.Count <= 0)
            {
                MessageBox.Show("No locations/files added to be backed up", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            else if (destinationsListBox.Items.Count <= 0)
            {
                MessageBox.Show("No destinations added to back up to", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return valid;
        }

        /// <summary>
        /// Checks and warns whether the UI input values for email settings are valid
        /// </summary>
        /// <returns>Whether the input values are valid</returns>
        private bool EmailInputsAreValid()
        {
            if (string.IsNullOrEmpty(emailServerTextBox.Text))
            {
                MessageBox.Show("Email server can't be empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            else if (string.IsNullOrEmpty(emailServerPortNumericUpDown.Text))
            {
                MessageBox.Show("Email server port can't be empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            else if (emailServerPortNumericUpDown.Value < 0)
            {
                MessageBox.Show("Email server port can't be negative", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            else if (string.IsNullOrEmpty(emailUserNameTextBox.Text))
            {
                MessageBox.Show("Username can't be empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            else if (string.IsNullOrEmpty(emailPasswordTextBox.Text))
            {
                MessageBox.Show("Password can't be empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            else if (string.IsNullOrEmpty(emailSenderTextBox.Text))
            {
                MessageBox.Show("Sender email can't be empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            else if (string.IsNullOrEmpty(emailReceiverTextBox.Text))
            {
                MessageBox.Show("Receiver email can't be empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        /// <summary>
        /// Checks and warns whether the UI input values for filter settings are valid
        /// </summary>
        /// <returns>Whether the input values are valid</returns>
        private bool FilterInputsAreValid()
        {
            if (string.IsNullOrEmpty(filterPathTextBox.Text))
            {
                MessageBox.Show("The path/file/extension to filter cannot be empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        /// <summary>
        /// Registers/overwrites a task schedule for this application in Windows Task Scheduler
        /// </summary>
        private void RegisterTaskSchedule()
        {
            using (TaskService ts = new TaskService())
            {
                //create a new task definition and set default settings
                TaskDefinition td = ts.NewTask();
                td.RegistrationInfo.Description = "Created by the SimpleBackup application to automatically run a file backup at the users configured time(s).";
                td.Settings.WakeToRun = true;
                td.Settings.MultipleInstances = TaskInstancesPolicy.Queue;
                td.Settings.StartWhenAvailable = true;
                td.Settings.Enabled = true;
                //set this programs path for the task scheduler to launch
                td.Actions.Add(new ExecAction(Assembly.GetExecutingAssembly().Location, "scheduledRun", null));

                //check what selected tab is selected
                if (scheduleTabControl.SelectedTab == dailyTabPage)
                {
                    //set daily trigger
                    td.Triggers.Add(new DailyTrigger { DaysInterval = 1, StartBoundary = dailyTimePicker.Value });
                }
                else if (scheduleTabControl.SelectedTab == weeklyTabPage)
                {
                    //set weekly trigger
                    DaysOfTheWeek daysOfTheWeek = DayOfWeekToDaysOfTheWeek((DayOfWeek)Enum.Parse(typeof(DayOfWeek), weeklyDayToRunComboBox.SelectedItem.ToString()));
                    td.Triggers.Add(new WeeklyTrigger { DaysOfWeek = daysOfTheWeek, StartBoundary = weeklyTimePicker.Value });
                }
                else if (scheduleTabControl.SelectedTab == monthlyTabPage)
                {
                    //set monthly trigger
                    int[] daysOfMonth = new int[1] { 1 };
                    bool runOnLastDayOfMonth = false;
                    if (monthlyDayToRunComboBox.SelectedItem.ToString() == "Last Day")
                    {
                        runOnLastDayOfMonth = true;
                        daysOfMonth = new int[0];
                    }
                    td.Triggers.Add(new MonthlyTrigger { RunOnLastDayOfMonth = runOnLastDayOfMonth, DaysOfMonth = daysOfMonth, MonthsOfYear = MonthsOfTheYear.AllMonths, StartBoundary = weeklyTimePicker.Value });
                }

                //register the task definition
                ts.RootFolder.RegisterTaskDefinition(@"SimpleBackup", td);
            }
        }

        /// <summary>
        /// Removes the task schedule for this application from Windows Task Scheduler
        /// </summary>
        private void UnregisterTaskSchedule()
        {
            using (TaskService ts = new TaskService())
            {
                ts.RootFolder.DeleteTask(@"SimpleBackup", false);
            }
        }

        private void SendBackupFinishedEmail(bool stoppedEarly)
        {
            //calculate how long the backup took
            TimeSpan timeTaken = DateTime.Now - backupStartTime;
            string totalDurationString = "Total duration: (" + TimeSpanToFormattedString(timeTaken) + ").";

            //check what email send mode is set, and how many errors were accumulated during the backup
            if (Settings.data.emailSendMode == 0 && Log.GetSessionErrors().Count == 0)
            {
                Log.WriteLine("Sending email");

                if (stoppedEarly)
                {
                    SendEmail("SimpleBackup: Backup Stopped Early", "Backup was stopped early at (" + DateTime.Now.ToString() + "). " + totalDurationString + " Not all files have been backed up.");
                }
                else
                {
                    SendEmail("SimpleBackup: Backup Complete", "Backup completed at (" + DateTime.Now.ToString() + "). " + totalDurationString);
                }
            }
            else if (Log.GetSessionErrors().Count > 0 && (Settings.data.emailSendMode == 0 || Settings.data.emailSendMode == 1))
            {
                Log.WriteLine("Sending email");
                string errorString = Environment.NewLine + "The following error(s) were generated during backup:" + Environment.NewLine + Log.GetSessionErrorsString();

                if (stoppedEarly)
                {
                    SendEmail("SimpleBackup: Error Backing Up", "Backup was stopped early at (" + DateTime.Now.ToString() + ") and has generated errors. " + totalDurationString + " Not all files have been backed up." + errorString);
                }
                else
                {
                    SendEmail("SimpleBackup: Error Backing Up", "Backup completed at (" + DateTime.Now.ToString() + ") and has generated errors. " + totalDurationString + errorString);
                }
            }
        }

        private void SendTestEmail()
        {
            if (!isSendingTestEmail && !isSendingEmail)
            {
                isSendingTestEmail = true;
                sendTestEmailButton.Text = "Sending Email...";
                tabControl.Enabled = false;
                SendEmail("SimpleBackup: Test", "This is a test message from the SimpleBackup application.");
            }
        }

        private void SendEmail(string subject, string message)
        {
            isSendingEmail = true;

            try
            {
                if (smtpClient != null)
                {
                    smtpClient.Dispose();
                    smtpClient = null;
                }

                //setup SMTP client
                smtpClient = new SmtpClient(Settings.DecryptString(Settings.data.emailServer))
                {
                    Port = Settings.DecryptInt(Settings.data.emailServerPort),
                    Credentials = new NetworkCredential(Settings.DecryptString(Settings.data.emailUserName), Settings.DecryptString(Settings.data.emailPassword)),
                    EnableSsl = Settings.DecryptBool(Settings.data.emailUseSSL)
                };

                smtpClient.SendCompleted += OnEmailSendCompletedCallback;
                smtpClient.Timeout = 20000;
                //send email async
                smtpClient.SendMailAsync(Settings.DecryptString(Settings.data.emailSender), Settings.DecryptString(Settings.data.emailReceiver), subject, message);
            }
            catch (Exception e)
            {
                if (smtpClient != null)
                {
                    smtpClient.Dispose();
                    smtpClient = null;
                }

                if (isSendingTestEmail)
                {
                    MessageBox.Show("Error sending test email: " + e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    OnTestEmailSendCompletedCallback();
                }
                else
                {
                    Log.WriteLine("Couldn't send email: " + e.Message, isError: true);
                }

                isSendingEmail = false;
            }
        }

        /// <summary>
        /// Callback from send test email completed async method
        /// </summary>
        private void OnTestEmailSendCompletedCallback()
        {
            sendTestEmailButton.Text = "Send Test Email";
            tabControl.Enabled = true;
            isSendingTestEmail = false;
        }

        /// <summary>
        /// Callback from send email completed async method
        /// </summary>
        /// <param name="sender">The object that triggered the email completed event</param>
        /// <param name="e">Additional data provided by the email completed event</param>
        private void OnEmailSendCompletedCallback(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            //check status of email being sent
            if (e.Error != null)
            {
                if (isSendingTestEmail)
                {
                    MessageBox.Show("Error sending test email: " + e.Error.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    Log.WriteLine("Error sending email: " + e.Error.Message, isError: true);
                }
            }
            else if (e.Cancelled)
            {
                Log.WriteLine("Sending email has been canceled");
            }
            else
            {
                if (isSendingTestEmail)
                {
                    MessageBox.Show("Email successfully sent", "Success", MessageBoxButtons.OK);
                }
                else
                {
                    Log.WriteLine("Email successfully sent");
                }
            }

            if (smtpClient != null)
            {
                smtpClient.Dispose();
                smtpClient = null;
            }

            isSendingEmail = false;

            if (isSendingTestEmail)
            {
                OnTestEmailSendCompletedCallback();
            }
            else
            {
                FinishBackup();
            }
        }

        /// <summary>
        /// Updates the backup progress bar value based on a current value and the max value (a percentage is calculated). Prevents the progress bar value from being updated to a lesser value than it is currently
        /// </summary>
        /// <param name="value">The current value of progress</param>
        /// <param name="maxValue">The max value of progress</param>
        private void SetBackupProgressBarValue(ulong value, ulong maxValue)
        {
            //calculate percantage
            int newValue = (int)Math.Ceiling((double)value / (double)maxValue * 100d);
            //make sure percentage is between 0 and 100 and that it has a higher value than the current value
            if (newValue >= 0 && newValue <= 100 && newValue > backupProgressBar.Value)
            {
                backupProgressBar.Value = newValue;
            }
        }

        /// <summary>
        /// Callback when the log is updated with new text
        /// </summary>
        /// <param name="sender">The object that triggered the log updated event</param>
        /// <param name="e">Additional data provided by the log updated event</param>
        private void OnLogUpdatedCallback(object sender, EventArgs e)
        {
            OnLogUpdatedEventArgs args = (OnLogUpdatedEventArgs)e;
            //callback to main thread
            if (InvokeRequired && thread != null && thread.ThreadState != ThreadState.AbortRequested && thread.ThreadState != ThreadState.Aborted)
            {
                Invoke(new Action(() =>
                {
                    AppendInfoTextBoxText(args.text);
                }));
            }
            else
            {
                AppendInfoTextBoxText(args.text);
            }
        }

        /// <summary>
        /// Checks if the source and destination paths are valid
        /// </summary>
        /// <returns>Whether or not the backup paths are valid</returns>
        private bool BackupPathsAreValid()
        {
            if (Settings.data.sourcePaths.Count == 0)
            {
                Log.WriteLine("No backup files or folders have been added", isError: true);
                return false;
            }
            else if (Settings.data.destinationPaths.Count == 0)
            {
                Log.WriteLine("No destinations to backup to have been added", isError: true);
                return false;
            }
            else if (string.IsNullOrWhiteSpace(Settings.data.backupName))
            {
                Log.WriteLine("No backup name has been entered", isError: true);
                return false;
            }

            int count = 0;
            foreach (string src in Settings.data.sourcePaths)
            {
                if (string.IsNullOrWhiteSpace(src))
                {
                    count++;
                }
            }
            if (count == Settings.data.sourcePaths.Count)
            {
                Log.WriteLine("All backup paths are empty", isError: true);
                return false;
            }

            count = 0;
            foreach (string dst in Settings.data.destinationPaths)
            {
                if (string.IsNullOrWhiteSpace(dst))
                {
                    count++;
                }
            }
            if (count == Settings.data.destinationPaths.Count)
            {
                Log.WriteLine("All destination paths are empty", isError: true);
                return false;
            }

            return true;
        }

        private string GetNewBackupFolderName()
        {
            return Settings.data.backupName + " [" + DateTimeToFormattedString(DateTime.Now) + "]";
        }

        private string DateTimeToFormattedString(DateTime dateTime)
        {
            string dateTimeString = dateTime.ToString();
            dateTimeString = dateTimeString.Replace('/', '.');
            dateTimeString = dateTimeString.Replace(':', '-');
            return dateTimeString;
        }

        private bool FormattedStringToDateTime(string convertedFileName, out DateTime dateTime)
        {
            convertedFileName = convertedFileName.Replace('.', '/');
            convertedFileName = convertedFileName.Replace('-', ':');
            return DateTime.TryParse(convertedFileName, out dateTime);
        }

        private void AppendInfoTextBoxText(string text)
        {
            if (infoTextBox.Lines.Length == 0)
            {
                infoTextBox.AppendText(text);
            }
            else
            {
                infoTextBox.AppendText(Environment.NewLine + text);
            }

            //make sure the info text box does not exceed the maximum allowed amount of lines
            if (infoTextBox.Lines.Length >= maxInfoTextBoxLines)
            {
                List<string> lines = infoTextBox.Lines.ToList();
                while (lines.Count >= maxInfoTextBoxLines)
                {
                    lines.RemoveAt(0);
                }
                infoTextBox.Lines = lines.ToArray();
            }
        }

        /// <summary>
        /// Formats a file size by converting it into a larger unit to display the smallest number possible and combines it into a string with the unit that it represents
        /// </summary>
        /// <param name="fileSize">The file size to convert</param>
        /// <returns>A string containing the converted file size and its unit</returns>
        private string ConvertFileSize(ulong fileSize)
        {
            string fileSizeUnit = "Bytes";

            if (fileSize > 1000)
            {
                double convertedFileSize = 0;

                //KB
                convertedFileSize = fileSize / 1000d;
                fileSizeUnit = "KB";

                if (convertedFileSize > 1000)
                {
                    //MB
                    convertedFileSize /= 1000d;
                    fileSizeUnit = "MB";

                    if (convertedFileSize > 1000)
                    {
                        //GB
                        convertedFileSize /= 1000d;
                        fileSizeUnit = "GB";

                        if (convertedFileSize > 1000)
                        {
                            //TB
                            convertedFileSize /= 1000d;
                            fileSizeUnit = "TB";

                            if (convertedFileSize > 1000)
                            {
                                //PB
                                convertedFileSize /= 1000d;
                                fileSizeUnit = "PB";
                            }
                        }
                    }
                }
                return convertedFileSize.ToString("0.##") + " " + fileSizeUnit;
            }

            return fileSize + " " + fileSizeUnit;
        }

        private List<T> ListBoxItemsToList<T>(ref ListBox listBox)
        {
            List<T> list = new List<T>();
            foreach (object item in listBox.Items)
            {
                T type = (T)item;
                list.Add(type);
            }
            return list;
        }

        /// <summary>
        /// Checks whether a file/folder path passes the user defined filters
        /// </summary>
        /// <param name="path">The path to be filtered</param>
        /// <returns>Whether or not the path passes through the filter</returns>
        private bool PathPassesBackupFilters(string path)
        {
            if (Settings.data.backupFilters.Count == 0)
            {
                return true;
            }

            bool passes = true;
            bool pathIsFolder = PathIsFolder(path);

            foreach (BackupFilter filter in Settings.data.backupFilters)
            {
                //check what path type the current filter has, and pass it to the comparer with the correct path
                if (filter.backupFilterPathType == BackupFilterPathType.FULL_PATH)
                {
                    passes = PathPassesBackupFilterComparer(path, filter);
                }
                else if (pathIsFolder && filter.backupFilterPathType == BackupFilterPathType.FOLDER_NAME)
                {
                    passes = PathPassesBackupFilterComparer(Path.GetDirectoryName(path), filter);
                }
                else if (!pathIsFolder)
                {
                    if (filter.backupFilterPathType == BackupFilterPathType.FILE_NAME)
                    {
                        passes = PathPassesBackupFilterComparer(Path.GetFileNameWithoutExtension(path), filter);
                    }
                    else if (filter.backupFilterPathType == BackupFilterPathType.FILE_EXTENSION)
                    {
                        passes = PathPassesBackupFilterComparer(Path.GetExtension(path), filter);
                    }
                    else if (filter.backupFilterPathType == BackupFilterPathType.FILE_NAME_AND_EXTENSION)
                    {
                        passes = PathPassesBackupFilterComparer(Path.GetFileName(path), filter);
                    }
                }

                if (!passes)
                {
                    break;
                }
            }

            return passes;
        }

        /// <summary>
        /// Checks whether the input path passes the current filters comparer against the current filters value
        /// </summary>
        /// <param name="path">The path to compare with</param>
        /// <param name="filter">The filter to compare against</param>
        /// <returns>Whether or not the path passes through the filter comparer</returns>
        private bool PathPassesBackupFilterComparer(string path, BackupFilter filter)
        {
            bool passes = false;
            string value = filter.value;

            //convert both the path and value to the same case if we're ignore case
            if (filter.ignoreCase)
            {
                path = path.ToUpper();
                value = value.ToUpper();
            }

            //check what comparer type the filter is using and apply that comparer
            if (filter.backupFilterComparerType == BackupFilterComparerType.EQUALS)
            {
                passes = path == value;
            }
            else if (filter.backupFilterComparerType == BackupFilterComparerType.DOES_NOT_EQUAL)
            {
                passes = path != value;
            }
            else if (filter.backupFilterComparerType == BackupFilterComparerType.CONTAINS)
            {
                passes = path.Contains(value);
            }
            else if (filter.backupFilterComparerType == BackupFilterComparerType.DOES_NOT_CONTAIN)
            {
                passes = !path.Contains(value);
            }

            //if its ignored, then we flip resulting value
            if (filter.backupFilterIgnoreType == BackupFilterIgnoreType.IGNORE)
            {
                passes = !passes;
            }
            return passes;
        }

        private bool PathIsFolder(string path)
        {
            return !Path.HasExtension(path) || (Path.GetExtension(path) == Path.GetFileName(path));
        }

        private void FilterListViewAdd(BackupFilter filter)
        {
            ListViewItem item = filterListView.Items.Add((filterListView.Items.Count + 1).ToString());
            item.SubItems.Add(EnumToString(filter.backupFilterIgnoreType));
            item.SubItems.Add(EnumToString(filter.backupFilterPathType));
            item.SubItems.Add(EnumToString(filter.backupFilterComparerType));
            item.SubItems.Add(filter.value);
            item.SubItems.Add(filter.ignoreCase.ToString());
            filterListView.SelectedItems.Clear();
            item.EnsureVisible();
        }

        private void FilterListViewRemoveAt(int index)
        {
            filterListView.Items.RemoveAt(index);
            for (int i = index; i < filterListView.Items.Count; i++)
            {
                filterListView.Items[i].Text = (i + 1).ToString();
            }
            filterListView.SelectedItems.Clear();
        }

        private bool FilterListViewContains(BackupFilter filter)
        {
            return FilterListViewGetAll().Contains(filter);
        }

        private List<BackupFilter> FilterListViewGetAll()
        {
            List<BackupFilter> filters = new List<BackupFilter>();
            for (int i = 0; i < filterListView.Items.Count; i++)
            {
                filters.Add(FilterListViewGetAt(i));
            }
            return filters;
        }

        private BackupFilter FilterListViewGetAt(int index)
        {
            BackupFilterIgnoreType backupFilterIgnoreType = StringToEnum<BackupFilterIgnoreType>(filterListView.Items[index].SubItems[1].Text);
            BackupFilterPathType backupFilterPathType = StringToEnum<BackupFilterPathType>(filterListView.Items[index].SubItems[2].Text);
            BackupFilterComparerType backupFilterComparerType = StringToEnum<BackupFilterComparerType>(filterListView.Items[index].SubItems[3].Text);
            string value = filterListView.Items[index].SubItems[4].Text;
            bool ignoreCase = bool.Parse(filterListView.Items[index].SubItems[5].Text);

            return new BackupFilter(backupFilterIgnoreType, backupFilterPathType, backupFilterComparerType, ignoreCase, value);
        }

        private string EnumToString(Enum e)
        {
            string enumName = e.ToString();
            string newString = "";
            bool capitalized = false;

            for (int i = 0; i < enumName.Length; i++)
            {
                if (enumName[i] == '_')
                {
                    newString += ' ';
                    capitalized = false;
                }
                else if (char.IsLetter(enumName[i]))
                {
                    if (capitalized)
                    {
                        newString += char.ToLower(enumName[i]);
                    }
                    else
                    {
                        capitalized = true;
                        newString += enumName[i];
                    }
                }
                else
                {
                    capitalized = false;
                    newString += enumName[i];
                }
            }
            return newString;
        }

        private T StringToEnum<T>(string enumString)
        {
            enumString = enumString.Trim();
            enumString = enumString.ToUpper();
            enumString = enumString.Replace(' ', '_');
            return (T)Enum.Parse(typeof(T), enumString);
        }

        private DayOfWeek DaysOfTheWeekToDayOfWeek(DaysOfTheWeek daysOfTheWeek)
        {
            return (DayOfWeek)Enum.Parse(typeof(DayOfWeek), daysOfTheWeek.ToString());
        }

        private DaysOfTheWeek DayOfWeekToDaysOfTheWeek(DayOfWeek dayOfWeek)
        {
            return (DaysOfTheWeek)Enum.Parse(typeof(DaysOfTheWeek), dayOfWeek.ToString());
        }

        private string TimeSpanToFormattedString(TimeSpan timeSpan)
        {
            return timeSpan.Days + "d " + timeSpan.Hours + "h " + timeSpan.Minutes + "m " + timeSpan.Seconds + "s " + timeSpan.Milliseconds + "ms";
        }

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        internal static extern bool GetDiskFreeSpaceEx(string drive, out ulong freeBytesForUser, out ulong totalBytes, out ulong freeBytes);

        #region UI Event Handlers

        private void backupButton_Click(object sender, EventArgs e)
        {
            StartBackupThread();
        }

        private void removeSourceButton_Click(object sender, EventArgs e)
        {
            if (sourcesListBox.SelectedItem != null)
            {
                sourcesListBox.Items.RemoveAt(sourcesListBox.SelectedIndex);
                sourcesListBox.ClearSelected();
            }
            else
            {
                MessageBox.Show("No file/folder to backup selected to be removed", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void removeDestinationButton_Click(object sender, EventArgs e)
        {
            if (destinationsListBox.SelectedItem != null)
            {
                destinationsListBox.Items.RemoveAt(destinationsListBox.SelectedIndex);
                destinationsListBox.ClearSelected();
            }
            else
            {
                MessageBox.Show("No destination selected to be removed", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void addFolderDestinationButton_Click(object sender, EventArgs e)
        {
            CommonOpenFileDialog dialog = new CommonOpenFileDialog();
            dialog.IsFolderPicker = true;
            CommonFileDialogResult result = dialog.ShowDialog();

            if (result != CommonFileDialogResult.Ok)
            {
                return;
            }

            if (!destinationsListBox.Items.Contains(dialog.FileName))
            {
                destinationsListBox.Items.Add(dialog.FileName);
                destinationsListBox.ClearSelected();
                destinationsListBox.TopIndex = destinationsListBox.Items.Count - 1;
            }
            else
            {
                MessageBox.Show("Destination folder has already been added\n" + dialog.FileName, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void addFileSourceButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            DialogResult result = dialog.ShowDialog();

            if (result != DialogResult.OK)
            {
                return;
            }

            if (!sourcesListBox.Items.Contains(dialog.FileName))
            {
                sourcesListBox.Items.Add(dialog.FileName);
                sourcesListBox.ClearSelected();
                sourcesListBox.TopIndex = sourcesListBox.Items.Count - 1;
            }
            else
            {
                MessageBox.Show("File to backup already has already been added\n" + dialog.FileName, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void addFolderSourceButton_Click(object sender, EventArgs e)
        {
            CommonOpenFileDialog dialog = new CommonOpenFileDialog();
            dialog.IsFolderPicker = true;
            CommonFileDialogResult result = dialog.ShowDialog();

            if (result != CommonFileDialogResult.Ok)
            {
                return;
            }

            if (!sourcesListBox.Items.Contains(dialog.FileName))
            {
                sourcesListBox.Items.Add(dialog.FileName);
                sourcesListBox.ClearSelected();
                sourcesListBox.TopIndex = sourcesListBox.Items.Count - 1;
            }
            else
            {
                MessageBox.Show("Folder to backup has already been added\n" + dialog.FileName, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (backupIsRunning && tabControl.SelectedTab != mainTabPage)
            {
                tabControl.SelectedTab = mainTabPage;
                MessageBox.Show("Cannot change settings while backup is running", "Caution", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                if (tabControl.SelectedTab == settingsTabPage)
                {
                    LoadSettingsTab();
                }
                else if (tabControl.SelectedTab == scheduleTabPage)
                {
                    LoadScheduleTab();
                }
                else if (tabControl.SelectedTab == emailTabPage)
                {
                    LoadEmailTab();
                }
                else if (tabControl.SelectedTab == filterTabPage)
                {
                    LoadFilterTab();
                }
            }
        }

        private void scheduleEnabledCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            scheduleTableLayoutPanel.Enabled = scheduleEnabledCheckBox.Checked;
        }

        private void saveScheduleButton_Click(object sender, EventArgs e)
        {
            SaveScheduleSettings();
            MessageBox.Show("Settings successfully saved!", "Settings Saved");
        }

        private void saveSettingsButton_Click(object sender, EventArgs e)
        {
            if (GeneralSettingsInputsAreValid())
            {
                SaveGeneralSettings();
                MessageBox.Show("Settings successfully saved!", "Settings Saved");
            }
        }

        private void sendTestEmailButton_Click(object sender, EventArgs e)
        {
            if (EmailInputsAreValid())
            {
                Settings.Save();
                SendTestEmail();
            }
        }

        private void emailSaveSettingsButton_Click(object sender, EventArgs e)
        {
            if (!useEmailCheckBox.Checked || EmailInputsAreValid())
            {
                SaveEmailSettings();
                MessageBox.Show("Settings successfully saved!", "Settings Saved");
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (backupIsRunning)
            {
                DialogResult result = MessageBox.Show("Backup in progress. Are you sure you wish to cancel?", "Caution", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    tabControl.Enabled = false;
                    autoRun = true;
                    StopBackupEarly();
                }
                else
                {
                    e.Cancel = true;
                }
            }
        }

        private void scheduleTabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            //reset time pickers to current time
            dailyTimePicker.Value = DateTime.Now;
            weeklyTimePicker.Value = DateTime.Now;
            monthlyTimePicker.Value = DateTime.Now;
        }

        private void useEmailCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            emailTableLayoutPanel.Enabled = useEmailCheckBox.Checked;
        }

        private void backupNameTextBox_Enter(object sender, EventArgs e)
        {
            MessageBox.Show("It is advised not to change the backup name once a backup has already been made, as any backup with the older name will no longer be found", "Caution", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void filterAddButton_Click(object sender, EventArgs e)
        {
            if (FilterInputsAreValid())
            {
                BackupFilterIgnoreType backupFilterIgnoreType = (BackupFilterIgnoreType)filterIgnoreComboBox.SelectedIndex;
                BackupFilterComparerType backupFilterComparerType = (BackupFilterComparerType)filterComparerTypeComboBox.SelectedIndex;
                BackupFilterPathType backupFilterPathType = (BackupFilterPathType)filterPathTypeComboBox.SelectedIndex;
                string value = filterPathTextBox.Text;
                bool ignoreCase = filterIgnoreCaseCheckBox.Checked;
                BackupFilter backupFilter = new BackupFilter(backupFilterIgnoreType, backupFilterPathType, backupFilterComparerType, ignoreCase, value);
                filterPathTextBox.Text = "";

                if (FilterListViewContains(backupFilter))
                {
                    MessageBox.Show("An identical filter has already been added", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                FilterListViewAdd(backupFilter);
            }
        }

        private void filterSaveSettingsButton_Click(object sender, EventArgs e)
        {
            SaveFilterSettings();
            MessageBox.Show("Settings successfully saved!", "Settings Saved");
        }

        private void filterRemoveButton_Click(object sender, EventArgs e)
        {
            if (filterListView.SelectedIndices.Count > 0)
            {
                FilterListViewRemoveAt(filterListView.SelectedIndices[0]);
            }
            else
            {
                MessageBox.Show("No filter selected to be removed", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion
    }
}