namespace SimpleBackup
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.tabControl = new System.Windows.Forms.TabControl();
            this.mainTabPage = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.backupButton = new System.Windows.Forms.Button();
            this.infoTextBox = new System.Windows.Forms.TextBox();
            this.backupProgressBar = new System.Windows.Forms.ProgressBar();
            this.dataTransferedLabel = new System.Windows.Forms.Label();
            this.transferSpeedLabel = new System.Windows.Forms.Label();
            this.estimatedTimeLabel = new System.Windows.Forms.Label();
            this.settingsTabPage = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel12 = new System.Windows.Forms.TableLayoutPanel();
            this.saveSettingsButton = new System.Windows.Forms.Button();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.destinationsListBox = new System.Windows.Forms.ListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.sourcesListBox = new System.Windows.Forms.ListBox();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.addFileSourceButton = new System.Windows.Forms.Button();
            this.removeSourceButton = new System.Windows.Forms.Button();
            this.addFolderSourceButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.removeDestinationButton = new System.Windows.Forms.Button();
            this.addFolderDestinationButton = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.backupNameTextBox = new System.Windows.Forms.TextBox();
            this.label21 = new System.Windows.Forms.Label();
            this.numberOfConcurrentBackupsNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.useMD5ForTransferCheckBox = new System.Windows.Forms.CheckBox();
            this.label22 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.transferUnchangedFilesCheckBox = new System.Windows.Forms.CheckBox();
            this.useMD5ForComparisonCheckBox = new System.Windows.Forms.CheckBox();
            this.scheduleTabPage = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel11 = new System.Windows.Forms.TableLayoutPanel();
            this.saveScheduleButton = new System.Windows.Forms.Button();
            this.scheduleTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.label4 = new System.Windows.Forms.Label();
            this.scheduleTabControl = new System.Windows.Forms.TabControl();
            this.dailyTabPage = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel8 = new System.Windows.Forms.TableLayoutPanel();
            this.label3 = new System.Windows.Forms.Label();
            this.dailyTimePicker = new System.Windows.Forms.DateTimePicker();
            this.weeklyTabPage = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel9 = new System.Windows.Forms.TableLayoutPanel();
            this.weeklyDayToRunComboBox = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.weeklyTimePicker = new System.Windows.Forms.DateTimePicker();
            this.label10 = new System.Windows.Forms.Label();
            this.monthlyTabPage = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel10 = new System.Windows.Forms.TableLayoutPanel();
            this.monthlyDayToRunComboBox = new System.Windows.Forms.ComboBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.monthlyTimePicker = new System.Windows.Forms.DateTimePicker();
            this.tableLayoutPanel6 = new System.Windows.Forms.TableLayoutPanel();
            this.label8 = new System.Windows.Forms.Label();
            this.scheduleEnabledCheckBox = new System.Windows.Forms.CheckBox();
            this.emailTabPage = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel7 = new System.Windows.Forms.TableLayoutPanel();
            this.emailSaveSettingsButton = new System.Windows.Forms.Button();
            this.emailTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.label30 = new System.Windows.Forms.Label();
            this.sendTestEmailButton = new System.Windows.Forms.Button();
            this.tableLayoutPanel14 = new System.Windows.Forms.TableLayoutPanel();
            this.emailSendModeComboBox = new System.Windows.Forms.ComboBox();
            this.label20 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.emailReceiverTextBox = new System.Windows.Forms.TextBox();
            this.emailSenderTextBox = new System.Windows.Forms.TextBox();
            this.emailPasswordTextBox = new System.Windows.Forms.TextBox();
            this.emailUserNameTextBox = new System.Windows.Forms.TextBox();
            this.emailServerTextBox = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.emailUseSSLCheckBox = new System.Windows.Forms.CheckBox();
            this.emailServerPortNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.label29 = new System.Windows.Forms.Label();
            this.tableLayoutPanel13 = new System.Windows.Forms.TableLayoutPanel();
            this.useEmailCheckBox = new System.Windows.Forms.CheckBox();
            this.label13 = new System.Windows.Forms.Label();
            this.filterTabPage = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel15 = new System.Windows.Forms.TableLayoutPanel();
            this.filterSaveSettingsButton = new System.Windows.Forms.Button();
            this.tableLayoutPanel16 = new System.Windows.Forms.TableLayoutPanel();
            this.label23 = new System.Windows.Forms.Label();
            this.tableLayoutPanel17 = new System.Windows.Forms.TableLayoutPanel();
            this.filterRemoveButton = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.filterIgnoreCaseCheckBox = new System.Windows.Forms.CheckBox();
            this.label31 = new System.Windows.Forms.Label();
            this.filterPathTextBox = new System.Windows.Forms.TextBox();
            this.label28 = new System.Windows.Forms.Label();
            this.filterComparerTypeComboBox = new System.Windows.Forms.ComboBox();
            this.label27 = new System.Windows.Forms.Label();
            this.filterPathTypeComboBox = new System.Windows.Forms.ComboBox();
            this.label26 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.filterIgnoreComboBox = new System.Windows.Forms.ComboBox();
            this.filterAddButton = new System.Windows.Forms.Button();
            this.label32 = new System.Windows.Forms.Label();
            this.writeToLogCheckBox = new System.Windows.Forms.CheckBox();
            this.filterListView = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tableLayoutPanel18 = new System.Windows.Forms.TableLayoutPanel();
            this.label33 = new System.Windows.Forms.Label();
            this.scheduleAutoCloseCheckBox = new System.Windows.Forms.CheckBox();
            this.tabControl.SuspendLayout();
            this.mainTabPage.SuspendLayout();
            this.tableLayoutPanel5.SuspendLayout();
            this.settingsTabPage.SuspendLayout();
            this.tableLayoutPanel12.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numberOfConcurrentBackupsNumericUpDown)).BeginInit();
            this.scheduleTabPage.SuspendLayout();
            this.tableLayoutPanel11.SuspendLayout();
            this.scheduleTableLayoutPanel.SuspendLayout();
            this.scheduleTabControl.SuspendLayout();
            this.dailyTabPage.SuspendLayout();
            this.tableLayoutPanel8.SuspendLayout();
            this.weeklyTabPage.SuspendLayout();
            this.tableLayoutPanel9.SuspendLayout();
            this.monthlyTabPage.SuspendLayout();
            this.tableLayoutPanel10.SuspendLayout();
            this.tableLayoutPanel6.SuspendLayout();
            this.emailTabPage.SuspendLayout();
            this.tableLayoutPanel7.SuspendLayout();
            this.emailTableLayoutPanel.SuspendLayout();
            this.tableLayoutPanel14.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.emailServerPortNumericUpDown)).BeginInit();
            this.tableLayoutPanel13.SuspendLayout();
            this.filterTabPage.SuspendLayout();
            this.tableLayoutPanel15.SuspendLayout();
            this.tableLayoutPanel16.SuspendLayout();
            this.tableLayoutPanel17.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel18.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.mainTabPage);
            this.tabControl.Controls.Add(this.settingsTabPage);
            this.tabControl.Controls.Add(this.scheduleTabPage);
            this.tabControl.Controls.Add(this.emailTabPage);
            this.tabControl.Controls.Add(this.filterTabPage);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.HotTrack = true;
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(612, 560);
            this.tabControl.TabIndex = 1;
            this.tabControl.SelectedIndexChanged += new System.EventHandler(this.tabControl_SelectedIndexChanged);
            // 
            // mainTabPage
            // 
            this.mainTabPage.AutoScroll = true;
            this.mainTabPage.Controls.Add(this.tableLayoutPanel5);
            this.mainTabPage.Location = new System.Drawing.Point(4, 22);
            this.mainTabPage.Name = "mainTabPage";
            this.mainTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.mainTabPage.Size = new System.Drawing.Size(604, 534);
            this.mainTabPage.TabIndex = 0;
            this.mainTabPage.Text = "Main";
            this.mainTabPage.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.ColumnCount = 1;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel5.Controls.Add(this.backupButton, 0, 6);
            this.tableLayoutPanel5.Controls.Add(this.infoTextBox, 0, 1);
            this.tableLayoutPanel5.Controls.Add(this.backupProgressBar, 0, 5);
            this.tableLayoutPanel5.Controls.Add(this.dataTransferedLabel, 0, 4);
            this.tableLayoutPanel5.Controls.Add(this.transferSpeedLabel, 0, 3);
            this.tableLayoutPanel5.Controls.Add(this.estimatedTimeLabel, 0, 2);
            this.tableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel5.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 7;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel5.Size = new System.Drawing.Size(598, 528);
            this.tableLayoutPanel5.TabIndex = 2;
            // 
            // backupButton
            // 
            this.backupButton.AutoSize = true;
            this.backupButton.Dock = System.Windows.Forms.DockStyle.Top;
            this.backupButton.Location = new System.Drawing.Point(3, 491);
            this.backupButton.Name = "backupButton";
            this.backupButton.Size = new System.Drawing.Size(592, 33);
            this.backupButton.TabIndex = 0;
            this.backupButton.Text = "Start Backup";
            this.backupButton.UseVisualStyleBackColor = true;
            this.backupButton.Click += new System.EventHandler(this.backupButton_Click);
            // 
            // infoTextBox
            // 
            this.infoTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.infoTextBox.Location = new System.Drawing.Point(3, 3);
            this.infoTextBox.Multiline = true;
            this.infoTextBox.Name = "infoTextBox";
            this.infoTextBox.ReadOnly = true;
            this.infoTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.infoTextBox.Size = new System.Drawing.Size(592, 410);
            this.infoTextBox.TabIndex = 1;
            // 
            // backupProgressBar
            // 
            this.backupProgressBar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.backupProgressBar.Location = new System.Drawing.Point(3, 458);
            this.backupProgressBar.Name = "backupProgressBar";
            this.backupProgressBar.Size = new System.Drawing.Size(592, 27);
            this.backupProgressBar.TabIndex = 3;
            this.backupProgressBar.Value = 100;
            // 
            // dataTransferedLabel
            // 
            this.dataTransferedLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.dataTransferedLabel.AutoSize = true;
            this.dataTransferedLabel.Location = new System.Drawing.Point(508, 442);
            this.dataTransferedLabel.Name = "dataTransferedLabel";
            this.dataTransferedLabel.Size = new System.Drawing.Size(87, 13);
            this.dataTransferedLabel.TabIndex = 1;
            this.dataTransferedLabel.Text = "Data Transfered:";
            // 
            // transferSpeedLabel
            // 
            this.transferSpeedLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.transferSpeedLabel.AutoSize = true;
            this.transferSpeedLabel.Location = new System.Drawing.Point(512, 429);
            this.transferSpeedLabel.Name = "transferSpeedLabel";
            this.transferSpeedLabel.Size = new System.Drawing.Size(83, 13);
            this.transferSpeedLabel.TabIndex = 5;
            this.transferSpeedLabel.Text = "Transfer Speed:";
            // 
            // estimatedTimeLabel
            // 
            this.estimatedTimeLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.estimatedTimeLabel.AutoSize = true;
            this.estimatedTimeLabel.Location = new System.Drawing.Point(513, 416);
            this.estimatedTimeLabel.Name = "estimatedTimeLabel";
            this.estimatedTimeLabel.Size = new System.Drawing.Size(82, 13);
            this.estimatedTimeLabel.TabIndex = 4;
            this.estimatedTimeLabel.Text = "Estimated Time:";
            // 
            // settingsTabPage
            // 
            this.settingsTabPage.AutoScroll = true;
            this.settingsTabPage.Controls.Add(this.tableLayoutPanel12);
            this.settingsTabPage.Location = new System.Drawing.Point(4, 22);
            this.settingsTabPage.Name = "settingsTabPage";
            this.settingsTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.settingsTabPage.Size = new System.Drawing.Size(604, 534);
            this.settingsTabPage.TabIndex = 1;
            this.settingsTabPage.Text = "Settings";
            this.settingsTabPage.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel12
            // 
            this.tableLayoutPanel12.ColumnCount = 1;
            this.tableLayoutPanel12.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel12.Controls.Add(this.saveSettingsButton, 0, 1);
            this.tableLayoutPanel12.Controls.Add(this.tableLayoutPanel2, 0, 0);
            this.tableLayoutPanel12.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel12.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel12.Name = "tableLayoutPanel12";
            this.tableLayoutPanel12.RowCount = 2;
            this.tableLayoutPanel12.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel12.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel12.Size = new System.Drawing.Size(598, 528);
            this.tableLayoutPanel12.TabIndex = 3;
            // 
            // saveSettingsButton
            // 
            this.saveSettingsButton.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.saveSettingsButton.Location = new System.Drawing.Point(261, 499);
            this.saveSettingsButton.Name = "saveSettingsButton";
            this.saveSettingsButton.Size = new System.Drawing.Size(75, 23);
            this.saveSettingsButton.TabIndex = 2;
            this.saveSettingsButton.Text = "Save";
            this.saveSettingsButton.UseVisualStyleBackColor = true;
            this.saveSettingsButton.Click += new System.EventHandler(this.saveSettingsButton_Click);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 54.22297F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 45.77703F));
            this.tableLayoutPanel2.Controls.Add(this.destinationsListBox, 1, 3);
            this.tableLayoutPanel2.Controls.Add(this.label2, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.sourcesListBox, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel3, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel4, 0, 3);
            this.tableLayoutPanel2.Controls.Add(this.label5, 0, 4);
            this.tableLayoutPanel2.Controls.Add(this.backupNameTextBox, 1, 4);
            this.tableLayoutPanel2.Controls.Add(this.label21, 0, 5);
            this.tableLayoutPanel2.Controls.Add(this.numberOfConcurrentBackupsNumericUpDown, 1, 5);
            this.tableLayoutPanel2.Controls.Add(this.label7, 0, 6);
            this.tableLayoutPanel2.Controls.Add(this.useMD5ForTransferCheckBox, 1, 6);
            this.tableLayoutPanel2.Controls.Add(this.label22, 0, 8);
            this.tableLayoutPanel2.Controls.Add(this.label24, 0, 7);
            this.tableLayoutPanel2.Controls.Add(this.transferUnchangedFilesCheckBox, 1, 8);
            this.tableLayoutPanel2.Controls.Add(this.useMD5ForComparisonCheckBox, 1, 7);
            this.tableLayoutPanel2.Controls.Add(this.label32, 0, 9);
            this.tableLayoutPanel2.Controls.Add(this.writeToLogCheckBox, 1, 9);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 10;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.Size = new System.Drawing.Size(592, 490);
            this.tableLayoutPanel2.TabIndex = 1;
            // 
            // destinationsListBox
            // 
            this.destinationsListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.destinationsListBox.FormattingEnabled = true;
            this.destinationsListBox.HorizontalScrollbar = true;
            this.destinationsListBox.Location = new System.Drawing.Point(323, 195);
            this.destinationsListBox.Name = "destinationsListBox";
            this.destinationsListBox.Size = new System.Drawing.Size(266, 160);
            this.destinationsListBox.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(111, 179);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(98, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Where To Backup:";
            // 
            // sourcesListBox
            // 
            this.sourcesListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sourcesListBox.FormattingEnabled = true;
            this.sourcesListBox.HorizontalScrollbar = true;
            this.sourcesListBox.Location = new System.Drawing.Point(323, 16);
            this.sourcesListBox.Name = "sourcesListBox";
            this.sourcesListBox.Size = new System.Drawing.Size(266, 160);
            this.sourcesListBox.TabIndex = 0;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 1;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Controls.Add(this.addFileSourceButton, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.removeSourceButton, 0, 2);
            this.tableLayoutPanel3.Controls.Add(this.addFolderSourceButton, 0, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 16);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 3;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel3.Size = new System.Drawing.Size(314, 160);
            this.tableLayoutPanel3.TabIndex = 1;
            // 
            // addFileSourceButton
            // 
            this.addFileSourceButton.AutoSize = true;
            this.addFileSourceButton.Dock = System.Windows.Forms.DockStyle.Top;
            this.addFileSourceButton.Location = new System.Drawing.Point(3, 32);
            this.addFileSourceButton.Name = "addFileSourceButton";
            this.addFileSourceButton.Size = new System.Drawing.Size(308, 23);
            this.addFileSourceButton.TabIndex = 2;
            this.addFileSourceButton.Text = "Add File";
            this.addFileSourceButton.UseVisualStyleBackColor = true;
            this.addFileSourceButton.Click += new System.EventHandler(this.addFileSourceButton_Click);
            // 
            // removeSourceButton
            // 
            this.removeSourceButton.AutoSize = true;
            this.removeSourceButton.Dock = System.Windows.Forms.DockStyle.Top;
            this.removeSourceButton.Location = new System.Drawing.Point(3, 61);
            this.removeSourceButton.Name = "removeSourceButton";
            this.removeSourceButton.Size = new System.Drawing.Size(308, 23);
            this.removeSourceButton.TabIndex = 1;
            this.removeSourceButton.Text = "Remove Selected";
            this.removeSourceButton.UseVisualStyleBackColor = true;
            this.removeSourceButton.Click += new System.EventHandler(this.removeSourceButton_Click);
            // 
            // addFolderSourceButton
            // 
            this.addFolderSourceButton.AutoSize = true;
            this.addFolderSourceButton.Dock = System.Windows.Forms.DockStyle.Top;
            this.addFolderSourceButton.Location = new System.Drawing.Point(3, 3);
            this.addFolderSourceButton.Name = "addFolderSourceButton";
            this.addFolderSourceButton.Size = new System.Drawing.Size(308, 23);
            this.addFolderSourceButton.TabIndex = 0;
            this.addFolderSourceButton.Text = "Add Folder";
            this.addFolderSourceButton.UseVisualStyleBackColor = true;
            this.addFolderSourceButton.Click += new System.EventHandler(this.addFolderSourceButton_Click);
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(97, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(126, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Files/Folders To Backup:";
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 1;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.Controls.Add(this.removeDestinationButton, 0, 1);
            this.tableLayoutPanel4.Controls.Add(this.addFolderDestinationButton, 0, 0);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(3, 195);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 2;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel4.Size = new System.Drawing.Size(314, 160);
            this.tableLayoutPanel4.TabIndex = 4;
            // 
            // removeDestinationButton
            // 
            this.removeDestinationButton.AutoSize = true;
            this.removeDestinationButton.Dock = System.Windows.Forms.DockStyle.Top;
            this.removeDestinationButton.Location = new System.Drawing.Point(3, 32);
            this.removeDestinationButton.Name = "removeDestinationButton";
            this.removeDestinationButton.Size = new System.Drawing.Size(308, 23);
            this.removeDestinationButton.TabIndex = 2;
            this.removeDestinationButton.Text = "Remove Selected";
            this.removeDestinationButton.UseVisualStyleBackColor = true;
            this.removeDestinationButton.Click += new System.EventHandler(this.removeDestinationButton_Click);
            // 
            // addFolderDestinationButton
            // 
            this.addFolderDestinationButton.AutoSize = true;
            this.addFolderDestinationButton.Dock = System.Windows.Forms.DockStyle.Top;
            this.addFolderDestinationButton.Location = new System.Drawing.Point(3, 3);
            this.addFolderDestinationButton.Name = "addFolderDestinationButton";
            this.addFolderDestinationButton.Size = new System.Drawing.Size(308, 23);
            this.addFolderDestinationButton.TabIndex = 1;
            this.addFolderDestinationButton.Text = "Add Folder";
            this.addFolderDestinationButton.UseVisualStyleBackColor = true;
            this.addFolderDestinationButton.Click += new System.EventHandler(this.addFolderDestinationButton_Click);
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(207, 364);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(110, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "Backup Folder Name:";
            // 
            // backupNameTextBox
            // 
            this.backupNameTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.backupNameTextBox.Location = new System.Drawing.Point(323, 361);
            this.backupNameTextBox.Name = "backupNameTextBox";
            this.backupNameTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.backupNameTextBox.Size = new System.Drawing.Size(266, 20);
            this.backupNameTextBox.TabIndex = 11;
            this.backupNameTextBox.Enter += new System.EventHandler(this.backupNameTextBox_Enter);
            // 
            // label21
            // 
            this.label21.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(60, 390);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(257, 13);
            this.label21.TabIndex = 16;
            this.label21.Text = "Number Of Backups To Keep (Oldest Is Overwritten):";
            // 
            // numberOfConcurrentBackupsNumericUpDown
            // 
            this.numberOfConcurrentBackupsNumericUpDown.Dock = System.Windows.Forms.DockStyle.Fill;
            this.numberOfConcurrentBackupsNumericUpDown.Location = new System.Drawing.Point(323, 387);
            this.numberOfConcurrentBackupsNumericUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numberOfConcurrentBackupsNumericUpDown.Name = "numberOfConcurrentBackupsNumericUpDown";
            this.numberOfConcurrentBackupsNumericUpDown.Size = new System.Drawing.Size(266, 20);
            this.numberOfConcurrentBackupsNumericUpDown.TabIndex = 17;
            this.numberOfConcurrentBackupsNumericUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label7
            // 
            this.label7.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(125, 413);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(192, 13);
            this.label7.TabIndex = 14;
            this.label7.Text = "Use MD5 Checksum For File Transfers:";
            // 
            // useMD5ForTransferCheckBox
            // 
            this.useMD5ForTransferCheckBox.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.useMD5ForTransferCheckBox.AutoSize = true;
            this.useMD5ForTransferCheckBox.Location = new System.Drawing.Point(323, 413);
            this.useMD5ForTransferCheckBox.Name = "useMD5ForTransferCheckBox";
            this.useMD5ForTransferCheckBox.Size = new System.Drawing.Size(15, 14);
            this.useMD5ForTransferCheckBox.TabIndex = 15;
            this.useMD5ForTransferCheckBox.UseVisualStyleBackColor = true;
            // 
            // label22
            // 
            this.label22.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(32, 453);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(285, 13);
            this.label22.TabIndex = 18;
            this.label22.Text = "Force Transfer All Files (Even If File Is Already Backed Up):";
            // 
            // label24
            // 
            this.label24.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(61, 433);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(256, 13);
            this.label24.TabIndex = 20;
            this.label24.Text = "Use MD5 Checksum For File Overwrite Comparisons:";
            // 
            // transferUnchangedFilesCheckBox
            // 
            this.transferUnchangedFilesCheckBox.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.transferUnchangedFilesCheckBox.AutoSize = true;
            this.transferUnchangedFilesCheckBox.Location = new System.Drawing.Point(323, 453);
            this.transferUnchangedFilesCheckBox.Name = "transferUnchangedFilesCheckBox";
            this.transferUnchangedFilesCheckBox.Size = new System.Drawing.Size(15, 14);
            this.transferUnchangedFilesCheckBox.TabIndex = 19;
            this.transferUnchangedFilesCheckBox.UseVisualStyleBackColor = true;
            // 
            // useMD5ForComparisonCheckBox
            // 
            this.useMD5ForComparisonCheckBox.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.useMD5ForComparisonCheckBox.AutoSize = true;
            this.useMD5ForComparisonCheckBox.Location = new System.Drawing.Point(323, 433);
            this.useMD5ForComparisonCheckBox.Name = "useMD5ForComparisonCheckBox";
            this.useMD5ForComparisonCheckBox.Size = new System.Drawing.Size(15, 14);
            this.useMD5ForComparisonCheckBox.TabIndex = 21;
            this.useMD5ForComparisonCheckBox.UseVisualStyleBackColor = true;
            // 
            // scheduleTabPage
            // 
            this.scheduleTabPage.Controls.Add(this.tableLayoutPanel11);
            this.scheduleTabPage.Location = new System.Drawing.Point(4, 22);
            this.scheduleTabPage.Name = "scheduleTabPage";
            this.scheduleTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.scheduleTabPage.Size = new System.Drawing.Size(604, 534);
            this.scheduleTabPage.TabIndex = 2;
            this.scheduleTabPage.Text = "Schedule";
            this.scheduleTabPage.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel11
            // 
            this.tableLayoutPanel11.ColumnCount = 1;
            this.tableLayoutPanel11.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel11.Controls.Add(this.saveScheduleButton, 0, 2);
            this.tableLayoutPanel11.Controls.Add(this.scheduleTableLayoutPanel, 0, 1);
            this.tableLayoutPanel11.Controls.Add(this.tableLayoutPanel6, 0, 0);
            this.tableLayoutPanel11.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel11.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel11.Name = "tableLayoutPanel11";
            this.tableLayoutPanel11.RowCount = 3;
            this.tableLayoutPanel11.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel11.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel11.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel11.Size = new System.Drawing.Size(598, 528);
            this.tableLayoutPanel11.TabIndex = 1;
            // 
            // saveScheduleButton
            // 
            this.saveScheduleButton.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.saveScheduleButton.Location = new System.Drawing.Point(261, 305);
            this.saveScheduleButton.Name = "saveScheduleButton";
            this.saveScheduleButton.Size = new System.Drawing.Size(75, 23);
            this.saveScheduleButton.TabIndex = 3;
            this.saveScheduleButton.Text = "Save";
            this.saveScheduleButton.UseVisualStyleBackColor = true;
            this.saveScheduleButton.Click += new System.EventHandler(this.saveScheduleButton_Click);
            // 
            // scheduleTableLayoutPanel
            // 
            this.scheduleTableLayoutPanel.ColumnCount = 1;
            this.scheduleTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.scheduleTableLayoutPanel.Controls.Add(this.tableLayoutPanel18, 0, 0);
            this.scheduleTableLayoutPanel.Controls.Add(this.label4, 0, 2);
            this.scheduleTableLayoutPanel.Controls.Add(this.scheduleTabControl, 0, 1);
            this.scheduleTableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.scheduleTableLayoutPanel.Location = new System.Drawing.Point(3, 36);
            this.scheduleTableLayoutPanel.Name = "scheduleTableLayoutPanel";
            this.scheduleTableLayoutPanel.RowCount = 3;
            this.scheduleTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.scheduleTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.scheduleTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.scheduleTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.scheduleTableLayoutPanel.Size = new System.Drawing.Size(592, 263);
            this.scheduleTableLayoutPanel.TabIndex = 0;
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.label4.Location = new System.Drawing.Point(4, 250);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(584, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "(Note: If the SimpleBackup application changes location at any point, then the sc" +
    "hedule feature will need to be re-enabled)";
            // 
            // scheduleTabControl
            // 
            this.scheduleTabControl.Controls.Add(this.dailyTabPage);
            this.scheduleTabControl.Controls.Add(this.weeklyTabPage);
            this.scheduleTabControl.Controls.Add(this.monthlyTabPage);
            this.scheduleTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scheduleTabControl.Location = new System.Drawing.Point(3, 41);
            this.scheduleTabControl.Name = "scheduleTabControl";
            this.scheduleTabControl.SelectedIndex = 0;
            this.scheduleTabControl.Size = new System.Drawing.Size(586, 200);
            this.scheduleTabControl.TabIndex = 1;
            this.scheduleTabControl.SelectedIndexChanged += new System.EventHandler(this.scheduleTabControl_SelectedIndexChanged);
            // 
            // dailyTabPage
            // 
            this.dailyTabPage.Controls.Add(this.tableLayoutPanel8);
            this.dailyTabPage.Location = new System.Drawing.Point(4, 22);
            this.dailyTabPage.Name = "dailyTabPage";
            this.dailyTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.dailyTabPage.Size = new System.Drawing.Size(578, 174);
            this.dailyTabPage.TabIndex = 0;
            this.dailyTabPage.Text = "Daily";
            this.dailyTabPage.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel8
            // 
            this.tableLayoutPanel8.ColumnCount = 2;
            this.tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 34.96503F));
            this.tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 65.03497F));
            this.tableLayoutPanel8.Controls.Add(this.label3, 0, 0);
            this.tableLayoutPanel8.Controls.Add(this.dailyTimePicker, 1, 0);
            this.tableLayoutPanel8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel8.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel8.Name = "tableLayoutPanel8";
            this.tableLayoutPanel8.RowCount = 2;
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel8.Size = new System.Drawing.Size(572, 168);
            this.tableLayoutPanel8.TabIndex = 0;
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(95, 6);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(101, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "What Time To Run:";
            // 
            // dailyTimePicker
            // 
            this.dailyTimePicker.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dailyTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dailyTimePicker.Location = new System.Drawing.Point(202, 3);
            this.dailyTimePicker.Name = "dailyTimePicker";
            this.dailyTimePicker.ShowUpDown = true;
            this.dailyTimePicker.Size = new System.Drawing.Size(367, 20);
            this.dailyTimePicker.TabIndex = 0;
            // 
            // weeklyTabPage
            // 
            this.weeklyTabPage.Controls.Add(this.tableLayoutPanel9);
            this.weeklyTabPage.Location = new System.Drawing.Point(4, 22);
            this.weeklyTabPage.Name = "weeklyTabPage";
            this.weeklyTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.weeklyTabPage.Size = new System.Drawing.Size(578, 174);
            this.weeklyTabPage.TabIndex = 1;
            this.weeklyTabPage.Text = "Weekly";
            this.weeklyTabPage.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel9
            // 
            this.tableLayoutPanel9.ColumnCount = 2;
            this.tableLayoutPanel9.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35.13986F));
            this.tableLayoutPanel9.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 64.86014F));
            this.tableLayoutPanel9.Controls.Add(this.weeklyDayToRunComboBox, 1, 0);
            this.tableLayoutPanel9.Controls.Add(this.label9, 0, 1);
            this.tableLayoutPanel9.Controls.Add(this.weeklyTimePicker, 1, 1);
            this.tableLayoutPanel9.Controls.Add(this.label10, 0, 0);
            this.tableLayoutPanel9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel9.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel9.Name = "tableLayoutPanel9";
            this.tableLayoutPanel9.RowCount = 3;
            this.tableLayoutPanel9.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel9.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel9.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel9.Size = new System.Drawing.Size(572, 168);
            this.tableLayoutPanel9.TabIndex = 1;
            // 
            // weeklyDayToRunComboBox
            // 
            this.weeklyDayToRunComboBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.weeklyDayToRunComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.weeklyDayToRunComboBox.FormattingEnabled = true;
            this.weeklyDayToRunComboBox.Items.AddRange(new object[] {
            "Monday",
            "Tuesday",
            "Wednesday",
            "Thursday",
            "Friday",
            "Saturday",
            "Sunday"});
            this.weeklyDayToRunComboBox.Location = new System.Drawing.Point(203, 3);
            this.weeklyDayToRunComboBox.MaxDropDownItems = 3;
            this.weeklyDayToRunComboBox.Name = "weeklyDayToRunComboBox";
            this.weeklyDayToRunComboBox.Size = new System.Drawing.Size(366, 21);
            this.weeklyDayToRunComboBox.TabIndex = 7;
            // 
            // label9
            // 
            this.label9.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(96, 33);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(101, 13);
            this.label9.TabIndex = 4;
            this.label9.Text = "What Time To Run:";
            // 
            // weeklyTimePicker
            // 
            this.weeklyTimePicker.Dock = System.Windows.Forms.DockStyle.Fill;
            this.weeklyTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.weeklyTimePicker.Location = new System.Drawing.Point(203, 30);
            this.weeklyTimePicker.Name = "weeklyTimePicker";
            this.weeklyTimePicker.ShowUpDown = true;
            this.weeklyTimePicker.Size = new System.Drawing.Size(366, 20);
            this.weeklyTimePicker.TabIndex = 1;
            // 
            // label10
            // 
            this.label10.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(83, 7);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(114, 13);
            this.label10.TabIndex = 5;
            this.label10.Text = "What Day To Run On:";
            // 
            // monthlyTabPage
            // 
            this.monthlyTabPage.Controls.Add(this.tableLayoutPanel10);
            this.monthlyTabPage.Location = new System.Drawing.Point(4, 22);
            this.monthlyTabPage.Name = "monthlyTabPage";
            this.monthlyTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.monthlyTabPage.Size = new System.Drawing.Size(578, 174);
            this.monthlyTabPage.TabIndex = 2;
            this.monthlyTabPage.Text = "Monthly";
            this.monthlyTabPage.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel10
            // 
            this.tableLayoutPanel10.ColumnCount = 2;
            this.tableLayoutPanel10.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 34.96503F));
            this.tableLayoutPanel10.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 65.03497F));
            this.tableLayoutPanel10.Controls.Add(this.monthlyDayToRunComboBox, 1, 0);
            this.tableLayoutPanel10.Controls.Add(this.label12, 0, 0);
            this.tableLayoutPanel10.Controls.Add(this.label11, 0, 1);
            this.tableLayoutPanel10.Controls.Add(this.monthlyTimePicker, 1, 1);
            this.tableLayoutPanel10.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel10.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel10.Name = "tableLayoutPanel10";
            this.tableLayoutPanel10.RowCount = 3;
            this.tableLayoutPanel10.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel10.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel10.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel10.Size = new System.Drawing.Size(572, 168);
            this.tableLayoutPanel10.TabIndex = 1;
            // 
            // monthlyDayToRunComboBox
            // 
            this.monthlyDayToRunComboBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.monthlyDayToRunComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.monthlyDayToRunComboBox.FormattingEnabled = true;
            this.monthlyDayToRunComboBox.Items.AddRange(new object[] {
            "First Day",
            "Last Day"});
            this.monthlyDayToRunComboBox.Location = new System.Drawing.Point(202, 3);
            this.monthlyDayToRunComboBox.MaxDropDownItems = 3;
            this.monthlyDayToRunComboBox.Name = "monthlyDayToRunComboBox";
            this.monthlyDayToRunComboBox.Size = new System.Drawing.Size(367, 21);
            this.monthlyDayToRunComboBox.TabIndex = 8;
            // 
            // label12
            // 
            this.label12.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(25, 7);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(171, 13);
            this.label12.TabIndex = 7;
            this.label12.Text = "Which Day Of The Month To Run:";
            // 
            // label11
            // 
            this.label11.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(95, 33);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(101, 13);
            this.label11.TabIndex = 5;
            this.label11.Text = "What Time To Run:";
            // 
            // monthlyTimePicker
            // 
            this.monthlyTimePicker.Dock = System.Windows.Forms.DockStyle.Fill;
            this.monthlyTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.monthlyTimePicker.Location = new System.Drawing.Point(202, 30);
            this.monthlyTimePicker.Name = "monthlyTimePicker";
            this.monthlyTimePicker.ShowUpDown = true;
            this.monthlyTimePicker.Size = new System.Drawing.Size(367, 20);
            this.monthlyTimePicker.TabIndex = 6;
            // 
            // tableLayoutPanel6
            // 
            this.tableLayoutPanel6.ColumnCount = 2;
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35.64189F));
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 64.35811F));
            this.tableLayoutPanel6.Controls.Add(this.label8, 0, 0);
            this.tableLayoutPanel6.Controls.Add(this.scheduleEnabledCheckBox, 1, 0);
            this.tableLayoutPanel6.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel6.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel6.Name = "tableLayoutPanel6";
            this.tableLayoutPanel6.RowCount = 1;
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel6.Size = new System.Drawing.Size(592, 27);
            this.tableLayoutPanel6.TabIndex = 1;
            // 
            // label8
            // 
            this.label8.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(131, 8);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(77, 13);
            this.label8.TabIndex = 3;
            this.label8.Text = "Use Schedule:";
            // 
            // scheduleEnabledCheckBox
            // 
            this.scheduleEnabledCheckBox.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.scheduleEnabledCheckBox.AutoSize = true;
            this.scheduleEnabledCheckBox.Location = new System.Drawing.Point(214, 8);
            this.scheduleEnabledCheckBox.Name = "scheduleEnabledCheckBox";
            this.scheduleEnabledCheckBox.Size = new System.Drawing.Size(15, 14);
            this.scheduleEnabledCheckBox.TabIndex = 4;
            this.scheduleEnabledCheckBox.UseVisualStyleBackColor = true;
            this.scheduleEnabledCheckBox.CheckedChanged += new System.EventHandler(this.scheduleEnabledCheckBox_CheckedChanged);
            // 
            // emailTabPage
            // 
            this.emailTabPage.Controls.Add(this.tableLayoutPanel7);
            this.emailTabPage.Location = new System.Drawing.Point(4, 22);
            this.emailTabPage.Name = "emailTabPage";
            this.emailTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.emailTabPage.Size = new System.Drawing.Size(604, 534);
            this.emailTabPage.TabIndex = 4;
            this.emailTabPage.Text = "Email";
            this.emailTabPage.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel7
            // 
            this.tableLayoutPanel7.ColumnCount = 1;
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel7.Controls.Add(this.emailSaveSettingsButton, 0, 2);
            this.tableLayoutPanel7.Controls.Add(this.emailTableLayoutPanel, 0, 1);
            this.tableLayoutPanel7.Controls.Add(this.tableLayoutPanel13, 0, 0);
            this.tableLayoutPanel7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel7.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel7.Name = "tableLayoutPanel7";
            this.tableLayoutPanel7.RowCount = 3;
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel7.Size = new System.Drawing.Size(598, 528);
            this.tableLayoutPanel7.TabIndex = 1;
            // 
            // emailSaveSettingsButton
            // 
            this.emailSaveSettingsButton.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.emailSaveSettingsButton.Location = new System.Drawing.Point(261, 334);
            this.emailSaveSettingsButton.Name = "emailSaveSettingsButton";
            this.emailSaveSettingsButton.Size = new System.Drawing.Size(75, 23);
            this.emailSaveSettingsButton.TabIndex = 3;
            this.emailSaveSettingsButton.Text = "Save";
            this.emailSaveSettingsButton.UseVisualStyleBackColor = true;
            this.emailSaveSettingsButton.Click += new System.EventHandler(this.emailSaveSettingsButton_Click);
            // 
            // emailTableLayoutPanel
            // 
            this.emailTableLayoutPanel.ColumnCount = 1;
            this.emailTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.emailTableLayoutPanel.Controls.Add(this.label30, 0, 2);
            this.emailTableLayoutPanel.Controls.Add(this.sendTestEmailButton, 0, 1);
            this.emailTableLayoutPanel.Controls.Add(this.tableLayoutPanel14, 0, 0);
            this.emailTableLayoutPanel.Controls.Add(this.label29, 0, 3);
            this.emailTableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.emailTableLayoutPanel.Location = new System.Drawing.Point(3, 49);
            this.emailTableLayoutPanel.Name = "emailTableLayoutPanel";
            this.emailTableLayoutPanel.RowCount = 4;
            this.emailTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.emailTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.emailTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.emailTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.emailTableLayoutPanel.Size = new System.Drawing.Size(592, 279);
            this.emailTableLayoutPanel.TabIndex = 0;
            // 
            // label30
            // 
            this.label30.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.label30.AutoSize = true;
            this.label30.Location = new System.Drawing.Point(155, 239);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(281, 13);
            this.label30.TabIndex = 5;
            this.label30.Text = "(Note: All email settings are encrypted before being saved)";
            // 
            // sendTestEmailButton
            // 
            this.sendTestEmailButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.sendTestEmailButton.Location = new System.Drawing.Point(489, 213);
            this.sendTestEmailButton.Name = "sendTestEmailButton";
            this.sendTestEmailButton.Size = new System.Drawing.Size(100, 23);
            this.sendTestEmailButton.TabIndex = 3;
            this.sendTestEmailButton.Text = "Send Test Email";
            this.sendTestEmailButton.UseVisualStyleBackColor = true;
            this.sendTestEmailButton.Click += new System.EventHandler(this.sendTestEmailButton_Click);
            // 
            // tableLayoutPanel14
            // 
            this.tableLayoutPanel14.ColumnCount = 2;
            this.tableLayoutPanel14.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 21.95946F));
            this.tableLayoutPanel14.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 78.04054F));
            this.tableLayoutPanel14.Controls.Add(this.emailSendModeComboBox, 1, 7);
            this.tableLayoutPanel14.Controls.Add(this.label20, 0, 7);
            this.tableLayoutPanel14.Controls.Add(this.label19, 0, 6);
            this.tableLayoutPanel14.Controls.Add(this.emailReceiverTextBox, 1, 5);
            this.tableLayoutPanel14.Controls.Add(this.emailSenderTextBox, 1, 4);
            this.tableLayoutPanel14.Controls.Add(this.emailPasswordTextBox, 1, 3);
            this.tableLayoutPanel14.Controls.Add(this.emailUserNameTextBox, 1, 2);
            this.tableLayoutPanel14.Controls.Add(this.emailServerTextBox, 1, 0);
            this.tableLayoutPanel14.Controls.Add(this.label17, 0, 0);
            this.tableLayoutPanel14.Controls.Add(this.label18, 0, 1);
            this.tableLayoutPanel14.Controls.Add(this.label15, 0, 4);
            this.tableLayoutPanel14.Controls.Add(this.label6, 0, 2);
            this.tableLayoutPanel14.Controls.Add(this.label16, 0, 5);
            this.tableLayoutPanel14.Controls.Add(this.label14, 0, 3);
            this.tableLayoutPanel14.Controls.Add(this.emailUseSSLCheckBox, 1, 6);
            this.tableLayoutPanel14.Controls.Add(this.emailServerPortNumericUpDown, 1, 1);
            this.tableLayoutPanel14.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel14.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel14.Name = "tableLayoutPanel14";
            this.tableLayoutPanel14.RowCount = 8;
            this.tableLayoutPanel14.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel14.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel14.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel14.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel14.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel14.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel14.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel14.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel14.Size = new System.Drawing.Size(586, 204);
            this.tableLayoutPanel14.TabIndex = 1;
            // 
            // emailSendModeComboBox
            // 
            this.emailSendModeComboBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.emailSendModeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.emailSendModeComboBox.FormattingEnabled = true;
            this.emailSendModeComboBox.Items.AddRange(new object[] {
            "On Backup Finished & Errors",
            "Errors Only"});
            this.emailSendModeComboBox.Location = new System.Drawing.Point(131, 179);
            this.emailSendModeComboBox.MaxDropDownItems = 3;
            this.emailSendModeComboBox.Name = "emailSendModeComboBox";
            this.emailSendModeComboBox.Size = new System.Drawing.Size(452, 21);
            this.emailSendModeComboBox.TabIndex = 26;
            // 
            // label20
            // 
            this.label20.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(3, 183);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(116, 13);
            this.label20.TabIndex = 25;
            this.label20.Text = "When To Send Emails:";
            // 
            // label19
            // 
            this.label19.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(3, 159);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(52, 13);
            this.label19.TabIndex = 23;
            this.label19.Text = "Use SSL:";
            // 
            // emailReceiverTextBox
            // 
            this.emailReceiverTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.emailReceiverTextBox.Location = new System.Drawing.Point(131, 133);
            this.emailReceiverTextBox.Name = "emailReceiverTextBox";
            this.emailReceiverTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.emailReceiverTextBox.Size = new System.Drawing.Size(452, 20);
            this.emailReceiverTextBox.TabIndex = 22;
            // 
            // emailSenderTextBox
            // 
            this.emailSenderTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.emailSenderTextBox.Location = new System.Drawing.Point(131, 107);
            this.emailSenderTextBox.Name = "emailSenderTextBox";
            this.emailSenderTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.emailSenderTextBox.Size = new System.Drawing.Size(452, 20);
            this.emailSenderTextBox.TabIndex = 20;
            // 
            // emailPasswordTextBox
            // 
            this.emailPasswordTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.emailPasswordTextBox.Location = new System.Drawing.Point(131, 81);
            this.emailPasswordTextBox.Name = "emailPasswordTextBox";
            this.emailPasswordTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.emailPasswordTextBox.Size = new System.Drawing.Size(452, 20);
            this.emailPasswordTextBox.TabIndex = 18;
            this.emailPasswordTextBox.UseSystemPasswordChar = true;
            // 
            // emailUserNameTextBox
            // 
            this.emailUserNameTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.emailUserNameTextBox.Location = new System.Drawing.Point(131, 55);
            this.emailUserNameTextBox.Name = "emailUserNameTextBox";
            this.emailUserNameTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.emailUserNameTextBox.Size = new System.Drawing.Size(452, 20);
            this.emailUserNameTextBox.TabIndex = 16;
            // 
            // emailServerTextBox
            // 
            this.emailServerTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.emailServerTextBox.Location = new System.Drawing.Point(131, 3);
            this.emailServerTextBox.Name = "emailServerTextBox";
            this.emailServerTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.emailServerTextBox.Size = new System.Drawing.Size(452, 20);
            this.emailServerTextBox.TabIndex = 12;
            // 
            // label17
            // 
            this.label17.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(3, 6);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(69, 13);
            this.label17.TabIndex = 19;
            this.label17.Text = "Email Server:";
            // 
            // label18
            // 
            this.label18.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(3, 32);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(91, 13);
            this.label18.TabIndex = 21;
            this.label18.Text = "Email Server Port:";
            // 
            // label15
            // 
            this.label15.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(3, 110);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(72, 13);
            this.label15.TabIndex = 15;
            this.label15.Text = "Sender Email:";
            // 
            // label6
            // 
            this.label6.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(3, 58);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(58, 13);
            this.label6.TabIndex = 0;
            this.label6.Text = "Username:";
            // 
            // label16
            // 
            this.label16.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(3, 136);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(81, 13);
            this.label16.TabIndex = 17;
            this.label16.Text = "Receiver Email:";
            // 
            // label14
            // 
            this.label14.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(3, 84);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(56, 13);
            this.label14.TabIndex = 13;
            this.label14.Text = "Password:";
            // 
            // emailUseSSLCheckBox
            // 
            this.emailUseSSLCheckBox.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.emailUseSSLCheckBox.AutoSize = true;
            this.emailUseSSLCheckBox.Location = new System.Drawing.Point(131, 159);
            this.emailUseSSLCheckBox.Name = "emailUseSSLCheckBox";
            this.emailUseSSLCheckBox.Size = new System.Drawing.Size(15, 14);
            this.emailUseSSLCheckBox.TabIndex = 24;
            this.emailUseSSLCheckBox.UseVisualStyleBackColor = true;
            // 
            // emailServerPortNumericUpDown
            // 
            this.emailServerPortNumericUpDown.Dock = System.Windows.Forms.DockStyle.Top;
            this.emailServerPortNumericUpDown.Location = new System.Drawing.Point(131, 29);
            this.emailServerPortNumericUpDown.Maximum = new decimal(new int[] {
            1410065407,
            2,
            0,
            0});
            this.emailServerPortNumericUpDown.Name = "emailServerPortNumericUpDown";
            this.emailServerPortNumericUpDown.Size = new System.Drawing.Size(452, 20);
            this.emailServerPortNumericUpDown.TabIndex = 27;
            // 
            // label29
            // 
            this.label29.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.label29.AutoSize = true;
            this.label29.Location = new System.Drawing.Point(13, 253);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(565, 26);
            this.label29.TabIndex = 4;
            this.label29.Text = "(Note: If your email account uses Two Factor Authentication, then you will need t" +
    "o first create an application password through your email provider, and use that" +
    " instead)";
            // 
            // tableLayoutPanel13
            // 
            this.tableLayoutPanel13.ColumnCount = 2;
            this.tableLayoutPanel13.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 22.12838F));
            this.tableLayoutPanel13.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 77.87162F));
            this.tableLayoutPanel13.Controls.Add(this.useEmailCheckBox, 1, 0);
            this.tableLayoutPanel13.Controls.Add(this.label13, 0, 0);
            this.tableLayoutPanel13.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel13.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel13.Name = "tableLayoutPanel13";
            this.tableLayoutPanel13.RowCount = 1;
            this.tableLayoutPanel13.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel13.Size = new System.Drawing.Size(592, 40);
            this.tableLayoutPanel13.TabIndex = 1;
            // 
            // useEmailCheckBox
            // 
            this.useEmailCheckBox.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.useEmailCheckBox.AutoSize = true;
            this.useEmailCheckBox.Location = new System.Drawing.Point(134, 13);
            this.useEmailCheckBox.Name = "useEmailCheckBox";
            this.useEmailCheckBox.Size = new System.Drawing.Size(15, 14);
            this.useEmailCheckBox.TabIndex = 25;
            this.useEmailCheckBox.UseVisualStyleBackColor = true;
            this.useEmailCheckBox.CheckedChanged += new System.EventHandler(this.useEmailCheckBox_CheckedChanged);
            // 
            // label13
            // 
            this.label13.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(3, 13);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(57, 13);
            this.label13.TabIndex = 20;
            this.label13.Text = "Use Email:";
            // 
            // filterTabPage
            // 
            this.filterTabPage.Controls.Add(this.tableLayoutPanel15);
            this.filterTabPage.Location = new System.Drawing.Point(4, 22);
            this.filterTabPage.Name = "filterTabPage";
            this.filterTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.filterTabPage.Size = new System.Drawing.Size(604, 534);
            this.filterTabPage.TabIndex = 5;
            this.filterTabPage.Text = "Filter";
            this.filterTabPage.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel15
            // 
            this.tableLayoutPanel15.ColumnCount = 1;
            this.tableLayoutPanel15.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel15.Controls.Add(this.filterSaveSettingsButton, 0, 2);
            this.tableLayoutPanel15.Controls.Add(this.tableLayoutPanel16, 0, 0);
            this.tableLayoutPanel15.Controls.Add(this.tableLayoutPanel1, 0, 1);
            this.tableLayoutPanel15.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel15.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel15.Name = "tableLayoutPanel15";
            this.tableLayoutPanel15.RowCount = 3;
            this.tableLayoutPanel15.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel15.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel15.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel15.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel15.Size = new System.Drawing.Size(598, 528);
            this.tableLayoutPanel15.TabIndex = 0;
            // 
            // filterSaveSettingsButton
            // 
            this.filterSaveSettingsButton.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.filterSaveSettingsButton.Location = new System.Drawing.Point(261, 414);
            this.filterSaveSettingsButton.Name = "filterSaveSettingsButton";
            this.filterSaveSettingsButton.Size = new System.Drawing.Size(75, 23);
            this.filterSaveSettingsButton.TabIndex = 4;
            this.filterSaveSettingsButton.Text = "Save";
            this.filterSaveSettingsButton.UseVisualStyleBackColor = true;
            this.filterSaveSettingsButton.Click += new System.EventHandler(this.filterSaveSettingsButton_Click);
            // 
            // tableLayoutPanel16
            // 
            this.tableLayoutPanel16.ColumnCount = 2;
            this.tableLayoutPanel16.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 27.7027F));
            this.tableLayoutPanel16.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 72.29729F));
            this.tableLayoutPanel16.Controls.Add(this.label23, 0, 0);
            this.tableLayoutPanel16.Controls.Add(this.tableLayoutPanel17, 0, 1);
            this.tableLayoutPanel16.Controls.Add(this.filterListView, 1, 1);
            this.tableLayoutPanel16.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel16.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel16.Name = "tableLayoutPanel16";
            this.tableLayoutPanel16.RowCount = 2;
            this.tableLayoutPanel16.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel16.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel16.Size = new System.Drawing.Size(592, 235);
            this.tableLayoutPanel16.TabIndex = 0;
            // 
            // label23
            // 
            this.label23.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(63, 0);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(37, 13);
            this.label23.TabIndex = 17;
            this.label23.Text = "Filters:";
            // 
            // tableLayoutPanel17
            // 
            this.tableLayoutPanel17.ColumnCount = 1;
            this.tableLayoutPanel17.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel17.Controls.Add(this.filterRemoveButton, 0, 0);
            this.tableLayoutPanel17.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel17.Location = new System.Drawing.Point(3, 16);
            this.tableLayoutPanel17.Name = "tableLayoutPanel17";
            this.tableLayoutPanel17.RowCount = 1;
            this.tableLayoutPanel17.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel17.Size = new System.Drawing.Size(158, 216);
            this.tableLayoutPanel17.TabIndex = 19;
            // 
            // filterRemoveButton
            // 
            this.filterRemoveButton.AutoSize = true;
            this.filterRemoveButton.Dock = System.Windows.Forms.DockStyle.Top;
            this.filterRemoveButton.Location = new System.Drawing.Point(3, 3);
            this.filterRemoveButton.Name = "filterRemoveButton";
            this.filterRemoveButton.Size = new System.Drawing.Size(152, 23);
            this.filterRemoveButton.TabIndex = 2;
            this.filterRemoveButton.Text = "Remove Selected";
            this.filterRemoveButton.UseVisualStyleBackColor = true;
            this.filterRemoveButton.Click += new System.EventHandler(this.filterRemoveButton_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 27.7027F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 72.29729F));
            this.tableLayoutPanel1.Controls.Add(this.filterComparerTypeComboBox, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.label27, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.filterPathTypeComboBox, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.label26, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.label25, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.filterIgnoreComboBox, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.filterAddButton, 1, 5);
            this.tableLayoutPanel1.Controls.Add(this.label28, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.label31, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.filterPathTextBox, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.filterIgnoreCaseCheckBox, 1, 4);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 244);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 6;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(592, 164);
            this.tableLayoutPanel1.TabIndex = 5;
            // 
            // filterIgnoreCaseCheckBox
            // 
            this.filterIgnoreCaseCheckBox.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.filterIgnoreCaseCheckBox.AutoSize = true;
            this.filterIgnoreCaseCheckBox.Location = new System.Drawing.Point(167, 110);
            this.filterIgnoreCaseCheckBox.Name = "filterIgnoreCaseCheckBox";
            this.filterIgnoreCaseCheckBox.Size = new System.Drawing.Size(15, 14);
            this.filterIgnoreCaseCheckBox.TabIndex = 37;
            this.filterIgnoreCaseCheckBox.UseVisualStyleBackColor = true;
            // 
            // label31
            // 
            this.label31.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label31.AutoSize = true;
            this.label31.Location = new System.Drawing.Point(94, 110);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(67, 13);
            this.label31.TabIndex = 36;
            this.label31.Text = "Ignore Case:";
            // 
            // filterPathTextBox
            // 
            this.filterPathTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.filterPathTextBox.Location = new System.Drawing.Point(167, 84);
            this.filterPathTextBox.Name = "filterPathTextBox";
            this.filterPathTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.filterPathTextBox.Size = new System.Drawing.Size(422, 20);
            this.filterPathTextBox.TabIndex = 34;
            // 
            // label28
            // 
            this.label28.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label28.AutoSize = true;
            this.label28.Location = new System.Drawing.Point(16, 87);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(145, 13);
            this.label28.TabIndex = 33;
            this.label28.Text = "Path/File/Extension To Filter:";
            // 
            // filterComparerTypeComboBox
            // 
            this.filterComparerTypeComboBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.filterComparerTypeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.filterComparerTypeComboBox.FormattingEnabled = true;
            this.filterComparerTypeComboBox.Location = new System.Drawing.Point(167, 57);
            this.filterComparerTypeComboBox.MaxDropDownItems = 3;
            this.filterComparerTypeComboBox.Name = "filterComparerTypeComboBox";
            this.filterComparerTypeComboBox.Size = new System.Drawing.Size(422, 21);
            this.filterComparerTypeComboBox.TabIndex = 32;
            // 
            // label27
            // 
            this.label27.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label27.AutoSize = true;
            this.label27.Location = new System.Drawing.Point(94, 61);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(67, 13);
            this.label27.TabIndex = 31;
            this.label27.Text = "Match Type:";
            // 
            // filterPathTypeComboBox
            // 
            this.filterPathTypeComboBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.filterPathTypeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.filterPathTypeComboBox.FormattingEnabled = true;
            this.filterPathTypeComboBox.Location = new System.Drawing.Point(167, 30);
            this.filterPathTypeComboBox.MaxDropDownItems = 3;
            this.filterPathTypeComboBox.Name = "filterPathTypeComboBox";
            this.filterPathTypeComboBox.Size = new System.Drawing.Size(422, 21);
            this.filterPathTypeComboBox.TabIndex = 30;
            // 
            // label26
            // 
            this.label26.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(77, 34);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(84, 13);
            this.label26.TabIndex = 29;
            this.label26.Text = "Filter Path Type:";
            // 
            // label25
            // 
            this.label25.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(94, 7);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(67, 13);
            this.label25.TabIndex = 28;
            this.label25.Text = "Ignore Type:";
            // 
            // filterIgnoreComboBox
            // 
            this.filterIgnoreComboBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.filterIgnoreComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.filterIgnoreComboBox.FormattingEnabled = true;
            this.filterIgnoreComboBox.Location = new System.Drawing.Point(167, 3);
            this.filterIgnoreComboBox.MaxDropDownItems = 3;
            this.filterIgnoreComboBox.Name = "filterIgnoreComboBox";
            this.filterIgnoreComboBox.Size = new System.Drawing.Size(422, 21);
            this.filterIgnoreComboBox.TabIndex = 27;
            // 
            // filterAddButton
            // 
            this.filterAddButton.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.filterAddButton.Location = new System.Drawing.Point(340, 130);
            this.filterAddButton.Name = "filterAddButton";
            this.filterAddButton.Size = new System.Drawing.Size(75, 23);
            this.filterAddButton.TabIndex = 35;
            this.filterAddButton.Text = "Add";
            this.filterAddButton.UseVisualStyleBackColor = true;
            this.filterAddButton.Click += new System.EventHandler(this.filterAddButton_Click);
            // 
            // label32
            // 
            this.label32.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label32.AutoSize = true;
            this.label32.Location = new System.Drawing.Point(185, 473);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(132, 13);
            this.label32.TabIndex = 22;
            this.label32.Text = "Write To External Log File:";
            // 
            // writeToLogCheckBox
            // 
            this.writeToLogCheckBox.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.writeToLogCheckBox.AutoSize = true;
            this.writeToLogCheckBox.Location = new System.Drawing.Point(323, 473);
            this.writeToLogCheckBox.Name = "writeToLogCheckBox";
            this.writeToLogCheckBox.Size = new System.Drawing.Size(15, 14);
            this.writeToLogCheckBox.TabIndex = 23;
            this.writeToLogCheckBox.UseVisualStyleBackColor = true;
            // 
            // filterListView
            // 
            this.filterListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader6});
            this.filterListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.filterListView.FullRowSelect = true;
            this.filterListView.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.filterListView.HideSelection = false;
            this.filterListView.Location = new System.Drawing.Point(167, 16);
            this.filterListView.MultiSelect = false;
            this.filterListView.Name = "filterListView";
            this.filterListView.ShowGroups = false;
            this.filterListView.Size = new System.Drawing.Size(422, 216);
            this.filterListView.TabIndex = 20;
            this.filterListView.UseCompatibleStateImageBehavior = false;
            this.filterListView.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "#";
            this.columnHeader1.Width = 22;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "IgnoreType";
            this.columnHeader2.Width = 68;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "PathType";
            this.columnHeader3.Width = 61;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "MatchType";
            this.columnHeader4.Width = 67;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Value";
            this.columnHeader5.Width = 134;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "IgnoreCase";
            this.columnHeader6.Width = 66;
            // 
            // tableLayoutPanel18
            // 
            this.tableLayoutPanel18.ColumnCount = 2;
            this.tableLayoutPanel18.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35.64189F));
            this.tableLayoutPanel18.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 64.35811F));
            this.tableLayoutPanel18.Controls.Add(this.label33, 0, 0);
            this.tableLayoutPanel18.Controls.Add(this.scheduleAutoCloseCheckBox, 1, 0);
            this.tableLayoutPanel18.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel18.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel18.Name = "tableLayoutPanel18";
            this.tableLayoutPanel18.RowCount = 1;
            this.tableLayoutPanel18.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel18.Size = new System.Drawing.Size(586, 32);
            this.tableLayoutPanel18.TabIndex = 7;
            // 
            // label33
            // 
            this.label33.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label33.AutoSize = true;
            this.label33.Location = new System.Drawing.Point(60, 9);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(145, 13);
            this.label33.TabIndex = 3;
            this.label33.Text = "Auto Close Upon Completion:";
            // 
            // scheduleAutoCloseCheckBox
            // 
            this.scheduleAutoCloseCheckBox.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.scheduleAutoCloseCheckBox.AutoSize = true;
            this.scheduleAutoCloseCheckBox.Location = new System.Drawing.Point(211, 9);
            this.scheduleAutoCloseCheckBox.Name = "scheduleAutoCloseCheckBox";
            this.scheduleAutoCloseCheckBox.Size = new System.Drawing.Size(15, 14);
            this.scheduleAutoCloseCheckBox.TabIndex = 4;
            this.scheduleAutoCloseCheckBox.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(612, 560);
            this.Controls.Add(this.tabControl);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(628, 599);
            this.Name = "Form1";
            this.Text = "Simple Backup v1.0.0";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.tabControl.ResumeLayout(false);
            this.mainTabPage.ResumeLayout(false);
            this.tableLayoutPanel5.ResumeLayout(false);
            this.tableLayoutPanel5.PerformLayout();
            this.settingsTabPage.ResumeLayout(false);
            this.tableLayoutPanel12.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numberOfConcurrentBackupsNumericUpDown)).EndInit();
            this.scheduleTabPage.ResumeLayout(false);
            this.tableLayoutPanel11.ResumeLayout(false);
            this.scheduleTableLayoutPanel.ResumeLayout(false);
            this.scheduleTableLayoutPanel.PerformLayout();
            this.scheduleTabControl.ResumeLayout(false);
            this.dailyTabPage.ResumeLayout(false);
            this.tableLayoutPanel8.ResumeLayout(false);
            this.tableLayoutPanel8.PerformLayout();
            this.weeklyTabPage.ResumeLayout(false);
            this.tableLayoutPanel9.ResumeLayout(false);
            this.tableLayoutPanel9.PerformLayout();
            this.monthlyTabPage.ResumeLayout(false);
            this.tableLayoutPanel10.ResumeLayout(false);
            this.tableLayoutPanel10.PerformLayout();
            this.tableLayoutPanel6.ResumeLayout(false);
            this.tableLayoutPanel6.PerformLayout();
            this.emailTabPage.ResumeLayout(false);
            this.tableLayoutPanel7.ResumeLayout(false);
            this.emailTableLayoutPanel.ResumeLayout(false);
            this.emailTableLayoutPanel.PerformLayout();
            this.tableLayoutPanel14.ResumeLayout(false);
            this.tableLayoutPanel14.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.emailServerPortNumericUpDown)).EndInit();
            this.tableLayoutPanel13.ResumeLayout(false);
            this.tableLayoutPanel13.PerformLayout();
            this.filterTabPage.ResumeLayout(false);
            this.tableLayoutPanel15.ResumeLayout(false);
            this.tableLayoutPanel16.ResumeLayout(false);
            this.tableLayoutPanel16.PerformLayout();
            this.tableLayoutPanel17.ResumeLayout(false);
            this.tableLayoutPanel17.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel18.ResumeLayout(false);
            this.tableLayoutPanel18.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage settingsTabPage;
        private System.Windows.Forms.TabPage mainTabPage;
        private System.Windows.Forms.TabPage emailTabPage;
        private System.Windows.Forms.TabPage scheduleTabPage;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel12;
        private System.Windows.Forms.Button saveSettingsButton;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.ListBox destinationsListBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListBox sourcesListBox;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Button addFileSourceButton;
        private System.Windows.Forms.Button removeSourceButton;
        private System.Windows.Forms.Button addFolderSourceButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.Button removeDestinationButton;
        private System.Windows.Forms.Button addFolderDestinationButton;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox backupNameTextBox;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.CheckBox useMD5ForTransferCheckBox;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel7;
        private System.Windows.Forms.TableLayoutPanel emailTableLayoutPanel;
        private System.Windows.Forms.Button sendTestEmailButton;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel14;
        private System.Windows.Forms.ComboBox emailSendModeComboBox;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.TextBox emailReceiverTextBox;
        private System.Windows.Forms.TextBox emailSenderTextBox;
        private System.Windows.Forms.TextBox emailPasswordTextBox;
        private System.Windows.Forms.TextBox emailUserNameTextBox;
        private System.Windows.Forms.TextBox emailServerTextBox;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.CheckBox emailUseSSLCheckBox;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel13;
        private System.Windows.Forms.CheckBox useEmailCheckBox;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Button emailSaveSettingsButton;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel11;
        private System.Windows.Forms.Button saveScheduleButton;
        private System.Windows.Forms.TableLayoutPanel scheduleTableLayoutPanel;
        private System.Windows.Forms.TabControl scheduleTabControl;
        private System.Windows.Forms.TabPage dailyTabPage;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel8;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dailyTimePicker;
        private System.Windows.Forms.TabPage weeklyTabPage;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel9;
        private System.Windows.Forms.ComboBox weeklyDayToRunComboBox;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.DateTimePicker weeklyTimePicker;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TabPage monthlyTabPage;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel10;
        private System.Windows.Forms.ComboBox monthlyDayToRunComboBox;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.DateTimePicker monthlyTimePicker;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel6;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.CheckBox scheduleEnabledCheckBox;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.NumericUpDown numberOfConcurrentBackupsNumericUpDown;
        private System.Windows.Forms.NumericUpDown emailServerPortNumericUpDown;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.CheckBox transferUnchangedFilesCheckBox;
        private System.Windows.Forms.TabPage filterTabPage;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel15;
        private System.Windows.Forms.Button filterSaveSettingsButton;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.CheckBox useMD5ForComparisonCheckBox;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
        private System.Windows.Forms.Button backupButton;
        private System.Windows.Forms.TextBox infoTextBox;
        private System.Windows.Forms.ProgressBar backupProgressBar;
        private System.Windows.Forms.Label dataTransferedLabel;
        private System.Windows.Forms.Label transferSpeedLabel;
        private System.Windows.Forms.Label estimatedTimeLabel;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TextBox filterPathTextBox;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.ComboBox filterComparerTypeComboBox;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.ComboBox filterPathTypeComboBox;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.ComboBox filterIgnoreComboBox;
        private System.Windows.Forms.Button filterAddButton;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel16;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel17;
        private System.Windows.Forms.Button filterRemoveButton;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox filterIgnoreCaseCheckBox;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.Label label32;
        private System.Windows.Forms.CheckBox writeToLogCheckBox;
        private System.Windows.Forms.ListView filterListView;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel18;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.CheckBox scheduleAutoCloseCheckBox;
    }
}

