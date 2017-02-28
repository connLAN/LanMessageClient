namespace LANChat
{
    partial class SettingsDialog
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
            if (disposing && (components != null)) {
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
            this.components = new System.ComponentModel.Container();
            this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPageGeneral = new System.Windows.Forms.TabPage();
            this.groupBoxSystem = new System.Windows.Forms.GroupBox();
            this.lnkLabelSilentMode = new System.Windows.Forms.LinkLabel();
            this.chkShowNotifications = new System.Windows.Forms.CheckBox();
            this.chkLaunchAtStartup = new System.Windows.Forms.CheckBox();
            this.chkSilentMode = new System.Windows.Forms.CheckBox();
            this.groupBoxUserList = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtIdleTime = new LANMedia.CustomControls.TextBoxEx();
            this.chkIdleTime = new System.Windows.Forms.CheckBox();
            this.lblSeconds = new System.Windows.Forms.Label();
            this.txtUserRefreshTime = new LANMedia.CustomControls.TextBoxEx();
            this.chkAutoRefreshUser = new System.Windows.Forms.CheckBox();
            this.tabPageAppearance = new System.Windows.Forms.TabPage();
            this.groupBoxThemes = new System.Windows.Forms.GroupBox();
            this.chkUseThemes = new System.Windows.Forms.CheckBox();
            this.lblUseTheme = new System.Windows.Forms.Label();
            this.cboTheme = new System.Windows.Forms.ComboBox();
            this.groupBoxTabs = new System.Windows.Forms.GroupBox();
            this.rdbChatInWindow = new System.Windows.Forms.RadioButton();
            this.rdbChatInTab = new System.Windows.Forms.RadioButton();
            this.tabPageMessages = new System.Windows.Forms.TabPage();
            this.groupBoxInstantMessages = new System.Windows.Forms.GroupBox();
            this.chkShowEmoticons = new System.Windows.Forms.CheckBox();
            this.chkEmotTextToImage = new System.Windows.Forms.CheckBox();
            this.rdbMessageToTaskbar = new System.Windows.Forms.RadioButton();
            this.rdbMessageToForeground = new System.Windows.Forms.RadioButton();
            this.chkAddDateToTimeStamp = new System.Windows.Forms.CheckBox();
            this.chkShowTimeStamp = new System.Windows.Forms.CheckBox();
            this.groupBoxFont = new System.Windows.Forms.GroupBox();
            this.btnChangeFont = new System.Windows.Forms.Button();
            this.lblFontPreview = new System.Windows.Forms.Label();
            this.lblDefaultFont = new System.Windows.Forms.Label();
            this.tabPageHistory = new System.Windows.Forms.TabPage();
            this.groupBoxHistoryLocation = new System.Windows.Forms.GroupBox();
            this.rdbCustomLogFile = new System.Windows.Forms.RadioButton();
            this.rdbDefaultLogFile = new System.Windows.Forms.RadioButton();
            this.btnSelectLogFile = new System.Windows.Forms.Button();
            this.txtLogFilePath = new System.Windows.Forms.TextBox();
            this.groupBoxHistorySettings = new System.Windows.Forms.GroupBox();
            this.btnClearHistory = new System.Windows.Forms.Button();
            this.chkSaveHistory = new System.Windows.Forms.CheckBox();
            this.tabPageAlerts = new System.Windows.Forms.TabPage();
            this.groupBoxSounds = new System.Windows.Forms.GroupBox();
            this.chkSuspendSoundsOnDND = new System.Windows.Forms.CheckBox();
            this.chkSuspendSoundsOnBusy = new System.Windows.Forms.CheckBox();
            this.chkPlaySounds = new System.Windows.Forms.CheckBox();
            this.groupBoxAlerts = new System.Windows.Forms.GroupBox();
            this.chkSuspendAlertsOnDND = new System.Windows.Forms.CheckBox();
            this.chkSuspendAlertsOnBusy = new System.Windows.Forms.CheckBox();
            this.chkDisplayAlerts = new System.Windows.Forms.CheckBox();
            this.tabPageNetwork = new System.Windows.Forms.TabPage();
            this.groupBoxConnection = new System.Windows.Forms.GroupBox();
            this.lblConnectionRetriesVal = new System.Windows.Forms.Label();
            this.lblConnectionTimeOutVals = new System.Windows.Forms.Label();
            this.txtConnectionRetries = new LANMedia.CustomControls.TextBoxEx();
            this.lblConnectionRetries = new System.Windows.Forms.Label();
            this.lblConnectionTimeOut = new System.Windows.Forms.Label();
            this.txtConnectionTimeOut = new LANMedia.CustomControls.TextBoxEx();
            this.lblStar = new System.Windows.Forms.Label();
            this.groupBoxBroadcast = new System.Windows.Forms.GroupBox();
            this.txtTCPPort = new LANMedia.CustomControls.TextBoxEx();
            this.lblTCPPort = new System.Windows.Forms.Label();
            this.lblUDPPort = new System.Windows.Forms.Label();
            this.txtUDPPort = new LANMedia.CustomControls.TextBoxEx();
            this.txtBroadcastAddress = new LANMedia.CustomControls.TextBoxEx();
            this.lblBroadcastAddress = new System.Windows.Forms.Label();
            this.tabPageFileTransfer = new System.Windows.Forms.TabPage();
            this.lnkFileSecurityRisks = new System.Windows.Forms.LinkLabel();
            this.groupBoxReceivedFileFolder = new System.Windows.Forms.GroupBox();
            this.btnViewFiles = new System.Windows.Forms.Button();
            this.btnChangeReceivedFileFolder = new System.Windows.Forms.Button();
            this.lblReceivedFileFolder = new System.Windows.Forms.Label();
            this.groupBoxIncomingFileRequest = new System.Windows.Forms.GroupBox();
            this.chkAutoReceiveFile = new System.Windows.Forms.CheckBox();
            this.rdbFileToTaskbar = new System.Windows.Forms.RadioButton();
            this.rdbFileToForeground = new System.Windows.Forms.RadioButton();
            this.tabPageHotKeys = new System.Windows.Forms.TabPage();
            this.groupBoxHotKeys = new System.Windows.Forms.GroupBox();
            this.rdbMessageHotKeyMod = new System.Windows.Forms.RadioButton();
            this.rdbMessageHotKey = new System.Windows.Forms.RadioButton();
            this.lblSendMessageHotKey = new System.Windows.Forms.Label();
            this.fontDialog = new System.Windows.Forms.FontDialog();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.toolTipInfo = new System.Windows.Forms.ToolTip(this.components);
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.lblEveryText = new System.Windows.Forms.Label();
            this.lblForText = new System.Windows.Forms.Label();
            this.tableLayoutPanel.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.tabPageGeneral.SuspendLayout();
            this.groupBoxSystem.SuspendLayout();
            this.groupBoxUserList.SuspendLayout();
            this.tabPageAppearance.SuspendLayout();
            this.groupBoxThemes.SuspendLayout();
            this.groupBoxTabs.SuspendLayout();
            this.tabPageMessages.SuspendLayout();
            this.groupBoxInstantMessages.SuspendLayout();
            this.groupBoxFont.SuspendLayout();
            this.tabPageHistory.SuspendLayout();
            this.groupBoxHistoryLocation.SuspendLayout();
            this.groupBoxHistorySettings.SuspendLayout();
            this.tabPageAlerts.SuspendLayout();
            this.groupBoxSounds.SuspendLayout();
            this.groupBoxAlerts.SuspendLayout();
            this.tabPageNetwork.SuspendLayout();
            this.groupBoxConnection.SuspendLayout();
            this.groupBoxBroadcast.SuspendLayout();
            this.tabPageFileTransfer.SuspendLayout();
            this.groupBoxReceivedFileFolder.SuspendLayout();
            this.groupBoxIncomingFileRequest.SuspendLayout();
            this.tabPageHotKeys.SuspendLayout();
            this.groupBoxHotKeys.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel
            // 
            this.tableLayoutPanel.ColumnCount = 2;
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 81F));
            this.tableLayoutPanel.Controls.Add(this.btnOK, 0, 1);
            this.tableLayoutPanel.Controls.Add(this.btnCancel, 1, 1);
            this.tableLayoutPanel.Controls.Add(this.tabControl, 0, 0);
            this.tableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel.Location = new System.Drawing.Point(8, 6);
            this.tableLayoutPanel.Name = "tableLayoutPanel";
            this.tableLayoutPanel.RowCount = 2;
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tableLayoutPanel.Size = new System.Drawing.Size(320, 382);
            this.tableLayoutPanel.TabIndex = 0;
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(161, 356);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 3;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.CausesValidation = false;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(242, 356);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // tabControl
            // 
            this.tableLayoutPanel.SetColumnSpan(this.tabControl, 2);
            this.tabControl.Controls.Add(this.tabPageGeneral);
            this.tabControl.Controls.Add(this.tabPageAppearance);
            this.tabControl.Controls.Add(this.tabPageMessages);
            this.tabControl.Controls.Add(this.tabPageHistory);
            this.tabControl.Controls.Add(this.tabPageAlerts);
            this.tabControl.Controls.Add(this.tabPageNetwork);
            this.tabControl.Controls.Add(this.tabPageFileTransfer);
            this.tabControl.Controls.Add(this.tabPageHotKeys);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Margin = new System.Windows.Forms.Padding(0);
            this.tabControl.Multiline = true;
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(320, 350);
            this.tabControl.SizeMode = System.Windows.Forms.TabSizeMode.FillToRight;
            this.tabControl.TabIndex = 2;
            // 
            // tabPageGeneral
            // 
            this.tabPageGeneral.Controls.Add(this.groupBoxSystem);
            this.tabPageGeneral.Controls.Add(this.groupBoxUserList);
            this.tabPageGeneral.Location = new System.Drawing.Point(4, 40);
            this.tabPageGeneral.Name = "tabPageGeneral";
            this.tabPageGeneral.Padding = new System.Windows.Forms.Padding(4, 6, 6, 6);
            this.tabPageGeneral.Size = new System.Drawing.Size(312, 306);
            this.tabPageGeneral.TabIndex = 0;
            this.tabPageGeneral.Text = "General";
            this.tabPageGeneral.UseVisualStyleBackColor = true;
            // 
            // groupBoxSystem
            // 
            this.groupBoxSystem.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxSystem.Controls.Add(this.lnkLabelSilentMode);
            this.groupBoxSystem.Controls.Add(this.chkShowNotifications);
            this.groupBoxSystem.Controls.Add(this.chkLaunchAtStartup);
            this.groupBoxSystem.Controls.Add(this.chkSilentMode);
            this.groupBoxSystem.Location = new System.Drawing.Point(7, 9);
            this.groupBoxSystem.Name = "groupBoxSystem";
            this.groupBoxSystem.Padding = new System.Windows.Forms.Padding(6);
            this.groupBoxSystem.Size = new System.Drawing.Size(296, 98);
            this.groupBoxSystem.TabIndex = 0;
            this.groupBoxSystem.TabStop = false;
            this.groupBoxSystem.Text = "System";
            // 
            // lnkLabelSilentMode
            // 
            this.lnkLabelSilentMode.AutoSize = true;
            this.lnkLabelSilentMode.LinkArea = new System.Windows.Forms.LinkArea(1, 10);
            this.lnkLabelSilentMode.Location = new System.Drawing.Point(134, 69);
            this.lnkLabelSilentMode.Name = "lnkLabelSilentMode";
            this.lnkLabelSilentMode.Size = new System.Drawing.Size(69, 17);
            this.lnkLabelSilentMode.TabIndex = 3;
            this.lnkLabelSilentMode.TabStop = true;
            this.lnkLabelSilentMode.Text = "(Learn more)";
            this.lnkLabelSilentMode.UseCompatibleTextRendering = true;
            this.lnkLabelSilentMode.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkLabelSilentMode_LinkClicked);
            this.lnkLabelSilentMode.MouseLeave += new System.EventHandler(this.lnkLabel_MouseLeave);
            // 
            // chkShowNotifications
            // 
            this.chkShowNotifications.AutoSize = true;
            this.chkShowNotifications.Location = new System.Drawing.Point(9, 45);
            this.chkShowNotifications.Name = "chkShowNotifications";
            this.chkShowNotifications.Size = new System.Drawing.Size(167, 17);
            this.chkShowNotifications.TabIndex = 1;
            this.chkShowNotifications.Text = "Show system tray notifications";
            this.chkShowNotifications.UseVisualStyleBackColor = true;
            // 
            // chkLaunchAtStartup
            // 
            this.chkLaunchAtStartup.AutoSize = true;
            this.chkLaunchAtStartup.Location = new System.Drawing.Point(9, 22);
            this.chkLaunchAtStartup.Name = "chkLaunchAtStartup";
            this.chkLaunchAtStartup.Size = new System.Drawing.Size(182, 17);
            this.chkLaunchAtStartup.TabIndex = 0;
            this.chkLaunchAtStartup.Text = "Start %TITLE% on system startup";
            this.chkLaunchAtStartup.UseVisualStyleBackColor = true;
            // 
            // chkSilentMode
            // 
            this.chkSilentMode.AutoSize = true;
            this.chkSilentMode.Location = new System.Drawing.Point(9, 68);
            this.chkSilentMode.Name = "chkSilentMode";
            this.chkSilentMode.Size = new System.Drawing.Size(118, 17);
            this.chkSilentMode.TabIndex = 2;
            this.chkSilentMode.Text = "Enable Silent Mode";
            this.chkSilentMode.UseVisualStyleBackColor = true;
            // 
            // groupBoxUserList
            // 
            this.groupBoxUserList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxUserList.Controls.Add(this.lblForText);
            this.groupBoxUserList.Controls.Add(this.lblEveryText);
            this.groupBoxUserList.Controls.Add(this.label1);
            this.groupBoxUserList.Controls.Add(this.txtIdleTime);
            this.groupBoxUserList.Controls.Add(this.chkIdleTime);
            this.groupBoxUserList.Controls.Add(this.lblSeconds);
            this.groupBoxUserList.Controls.Add(this.txtUserRefreshTime);
            this.groupBoxUserList.Controls.Add(this.chkAutoRefreshUser);
            this.groupBoxUserList.Location = new System.Drawing.Point(7, 116);
            this.groupBoxUserList.Name = "groupBoxUserList";
            this.groupBoxUserList.Padding = new System.Windows.Forms.Padding(6);
            this.groupBoxUserList.Size = new System.Drawing.Size(296, 123);
            this.groupBoxUserList.TabIndex = 1;
            this.groupBoxUserList.TabStop = false;
            this.groupBoxUserList.Text = "User List";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(126, 93);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "minutes";
            // 
            // txtIdleTime
            // 
            this.txtIdleTime.Enabled = false;
            this.txtIdleTime.ErrorText = null;
            this.txtIdleTime.ErrorTitle = null;
            this.txtIdleTime.InputMask = "^\\d*$";
            this.txtIdleTime.Location = new System.Drawing.Point(77, 90);
            this.txtIdleTime.MaximumValue = "60";
            this.txtIdleTime.MinimumValue = "5";
            this.txtIdleTime.Name = "txtIdleTime";
            this.txtIdleTime.RangeValidation = true;
            this.txtIdleTime.RequiredField = true;
            this.txtIdleTime.Size = new System.Drawing.Size(43, 20);
            this.txtIdleTime.TabIndex = 4;
            this.txtIdleTime.ValidationExpression = "^\\d*$";
            this.txtIdleTime.ValidationMessage = null;
            // 
            // chkIdleTime
            // 
            this.chkIdleTime.AutoSize = true;
            this.chkIdleTime.Location = new System.Drawing.Point(9, 70);
            this.chkIdleTime.Name = "chkIdleTime";
            this.chkIdleTime.Size = new System.Drawing.Size(234, 17);
            this.chkIdleTime.TabIndex = 3;
            this.chkIdleTime.Text = "Show me as Away when my computer is idle";
            this.chkIdleTime.UseVisualStyleBackColor = true;
            this.chkIdleTime.CheckedChanged += new System.EventHandler(this.chkIdleTime_CheckedChanged);
            // 
            // lblSeconds
            // 
            this.lblSeconds.AutoSize = true;
            this.lblSeconds.Location = new System.Drawing.Point(126, 45);
            this.lblSeconds.Name = "lblSeconds";
            this.lblSeconds.Size = new System.Drawing.Size(47, 13);
            this.lblSeconds.TabIndex = 2;
            this.lblSeconds.Text = "seconds";
            // 
            // txtUserRefreshTime
            // 
            this.txtUserRefreshTime.Enabled = false;
            this.txtUserRefreshTime.ErrorText = null;
            this.txtUserRefreshTime.ErrorTitle = null;
            this.txtUserRefreshTime.InputMask = "^\\d*$";
            this.txtUserRefreshTime.Location = new System.Drawing.Point(77, 42);
            this.txtUserRefreshTime.MaximumValue = "60";
            this.txtUserRefreshTime.MinimumValue = "5";
            this.txtUserRefreshTime.Name = "txtUserRefreshTime";
            this.txtUserRefreshTime.RangeValidation = true;
            this.txtUserRefreshTime.RequiredField = true;
            this.txtUserRefreshTime.Size = new System.Drawing.Size(43, 20);
            this.txtUserRefreshTime.TabIndex = 1;
            this.txtUserRefreshTime.ValidationExpression = "^\\d*$";
            this.txtUserRefreshTime.ValidationMessage = null;
            // 
            // chkAutoRefreshUser
            // 
            this.chkAutoRefreshUser.AutoSize = true;
            this.chkAutoRefreshUser.Enabled = false;
            this.chkAutoRefreshUser.Location = new System.Drawing.Point(9, 22);
            this.chkAutoRefreshUser.Name = "chkAutoRefreshUser";
            this.chkAutoRefreshUser.Size = new System.Drawing.Size(165, 17);
            this.chkAutoRefreshUser.TabIndex = 0;
            this.chkAutoRefreshUser.Text = "Refresh user list automatically";
            this.chkAutoRefreshUser.UseVisualStyleBackColor = true;
            this.chkAutoRefreshUser.CheckedChanged += new System.EventHandler(this.chkAutoRefreshUser_CheckedChanged);
            // 
            // tabPageAppearance
            // 
            this.tabPageAppearance.Controls.Add(this.groupBoxThemes);
            this.tabPageAppearance.Controls.Add(this.groupBoxTabs);
            this.tabPageAppearance.Location = new System.Drawing.Point(4, 40);
            this.tabPageAppearance.Name = "tabPageAppearance";
            this.tabPageAppearance.Padding = new System.Windows.Forms.Padding(4, 6, 6, 6);
            this.tabPageAppearance.Size = new System.Drawing.Size(312, 306);
            this.tabPageAppearance.TabIndex = 5;
            this.tabPageAppearance.Text = "Appearance";
            this.tabPageAppearance.UseVisualStyleBackColor = true;
            // 
            // groupBoxThemes
            // 
            this.groupBoxThemes.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxThemes.Controls.Add(this.chkUseThemes);
            this.groupBoxThemes.Controls.Add(this.lblUseTheme);
            this.groupBoxThemes.Controls.Add(this.cboTheme);
            this.groupBoxThemes.Location = new System.Drawing.Point(7, 98);
            this.groupBoxThemes.Name = "groupBoxThemes";
            this.groupBoxThemes.Padding = new System.Windows.Forms.Padding(6);
            this.groupBoxThemes.Size = new System.Drawing.Size(296, 76);
            this.groupBoxThemes.TabIndex = 1;
            this.groupBoxThemes.TabStop = false;
            this.groupBoxThemes.Text = "Themes";
            // 
            // chkUseThemes
            // 
            this.chkUseThemes.AutoSize = true;
            this.chkUseThemes.Location = new System.Drawing.Point(9, 22);
            this.chkUseThemes.Name = "chkUseThemes";
            this.chkUseThemes.Size = new System.Drawing.Size(96, 17);
            this.chkUseThemes.TabIndex = 2;
            this.chkUseThemes.Text = "Enable themes";
            this.chkUseThemes.UseVisualStyleBackColor = true;
            this.chkUseThemes.CheckedChanged += new System.EventHandler(this.chkUseThemes_CheckedChanged);
            // 
            // lblUseTheme
            // 
            this.lblUseTheme.AutoSize = true;
            this.lblUseTheme.Location = new System.Drawing.Point(30, 45);
            this.lblUseTheme.Name = "lblUseTheme";
            this.lblUseTheme.Size = new System.Drawing.Size(61, 13);
            this.lblUseTheme.TabIndex = 1;
            this.lblUseTheme.Text = "Use theme:";
            // 
            // cboTheme
            // 
            this.cboTheme.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTheme.Enabled = false;
            this.cboTheme.Location = new System.Drawing.Point(97, 42);
            this.cboTheme.Name = "cboTheme";
            this.cboTheme.Size = new System.Drawing.Size(121, 21);
            this.cboTheme.TabIndex = 0;
            // 
            // groupBoxTabs
            // 
            this.groupBoxTabs.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxTabs.Controls.Add(this.rdbChatInWindow);
            this.groupBoxTabs.Controls.Add(this.rdbChatInTab);
            this.groupBoxTabs.Location = new System.Drawing.Point(7, 9);
            this.groupBoxTabs.Name = "groupBoxTabs";
            this.groupBoxTabs.Padding = new System.Windows.Forms.Padding(6);
            this.groupBoxTabs.Size = new System.Drawing.Size(296, 80);
            this.groupBoxTabs.TabIndex = 0;
            this.groupBoxTabs.TabStop = false;
            this.groupBoxTabs.Text = "Tabs and Windows";
            // 
            // rdbChatInWindow
            // 
            this.rdbChatInWindow.AutoSize = true;
            this.rdbChatInWindow.Location = new System.Drawing.Point(9, 46);
            this.rdbChatInWindow.Name = "rdbChatInWindow";
            this.rdbChatInWindow.Size = new System.Drawing.Size(188, 17);
            this.rdbChatInWindow.TabIndex = 1;
            this.rdbChatInWindow.TabStop = true;
            this.rdbChatInWindow.Text = "Open conversation in new window";
            this.rdbChatInWindow.UseVisualStyleBackColor = true;
            // 
            // rdbChatInTab
            // 
            this.rdbChatInTab.AutoSize = true;
            this.rdbChatInTab.Location = new System.Drawing.Point(9, 23);
            this.rdbChatInTab.Name = "rdbChatInTab";
            this.rdbChatInTab.Size = new System.Drawing.Size(167, 17);
            this.rdbChatInTab.TabIndex = 0;
            this.rdbChatInTab.TabStop = true;
            this.rdbChatInTab.Text = "Open conversation in new tab";
            this.rdbChatInTab.UseVisualStyleBackColor = true;
            // 
            // tabPageMessages
            // 
            this.tabPageMessages.Controls.Add(this.groupBoxInstantMessages);
            this.tabPageMessages.Controls.Add(this.groupBoxFont);
            this.tabPageMessages.Location = new System.Drawing.Point(4, 40);
            this.tabPageMessages.Name = "tabPageMessages";
            this.tabPageMessages.Padding = new System.Windows.Forms.Padding(4, 6, 6, 6);
            this.tabPageMessages.Size = new System.Drawing.Size(312, 306);
            this.tabPageMessages.TabIndex = 1;
            this.tabPageMessages.Text = "Messages";
            this.tabPageMessages.UseVisualStyleBackColor = true;
            // 
            // groupBoxInstantMessages
            // 
            this.groupBoxInstantMessages.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxInstantMessages.Controls.Add(this.chkShowEmoticons);
            this.groupBoxInstantMessages.Controls.Add(this.chkEmotTextToImage);
            this.groupBoxInstantMessages.Controls.Add(this.rdbMessageToTaskbar);
            this.groupBoxInstantMessages.Controls.Add(this.rdbMessageToForeground);
            this.groupBoxInstantMessages.Controls.Add(this.chkAddDateToTimeStamp);
            this.groupBoxInstantMessages.Controls.Add(this.chkShowTimeStamp);
            this.groupBoxInstantMessages.Location = new System.Drawing.Point(7, 9);
            this.groupBoxInstantMessages.Name = "groupBoxInstantMessages";
            this.groupBoxInstantMessages.Padding = new System.Windows.Forms.Padding(6);
            this.groupBoxInstantMessages.Size = new System.Drawing.Size(296, 167);
            this.groupBoxInstantMessages.TabIndex = 0;
            this.groupBoxInstantMessages.TabStop = false;
            this.groupBoxInstantMessages.Text = "Message Window";
            // 
            // chkShowEmoticons
            // 
            this.chkShowEmoticons.AutoSize = true;
            this.chkShowEmoticons.Location = new System.Drawing.Point(9, 22);
            this.chkShowEmoticons.Name = "chkShowEmoticons";
            this.chkShowEmoticons.Size = new System.Drawing.Size(199, 17);
            this.chkShowEmoticons.TabIndex = 0;
            this.chkShowEmoticons.Text = "Show emoticons in instant messages";
            this.chkShowEmoticons.UseVisualStyleBackColor = true;
            // 
            // chkEmotTextToImage
            // 
            this.chkEmotTextToImage.AutoSize = true;
            this.chkEmotTextToImage.Location = new System.Drawing.Point(9, 45);
            this.chkEmotTextToImage.Name = "chkEmotTextToImage";
            this.chkEmotTextToImage.Size = new System.Drawing.Size(185, 17);
            this.chkEmotTextToImage.TabIndex = 1;
            this.chkEmotTextToImage.Text = "Replace emoticon text with image";
            this.chkEmotTextToImage.UseVisualStyleBackColor = true;
            // 
            // rdbMessageToTaskbar
            // 
            this.rdbMessageToTaskbar.AutoSize = true;
            this.rdbMessageToTaskbar.Location = new System.Drawing.Point(9, 137);
            this.rdbMessageToTaskbar.Name = "rdbMessageToTaskbar";
            this.rdbMessageToTaskbar.Size = new System.Drawing.Size(210, 17);
            this.rdbMessageToTaskbar.TabIndex = 5;
            this.rdbMessageToTaskbar.TabStop = true;
            this.rdbMessageToTaskbar.Text = "Minimize incoming messages to taskbar";
            this.rdbMessageToTaskbar.UseVisualStyleBackColor = true;
            // 
            // rdbMessageToForeground
            // 
            this.rdbMessageToForeground.AutoSize = true;
            this.rdbMessageToForeground.Location = new System.Drawing.Point(9, 114);
            this.rdbMessageToForeground.Name = "rdbMessageToForeground";
            this.rdbMessageToForeground.Size = new System.Drawing.Size(190, 17);
            this.rdbMessageToForeground.TabIndex = 4;
            this.rdbMessageToForeground.TabStop = true;
            this.rdbMessageToForeground.Text = "Set incoming messages foreground";
            this.rdbMessageToForeground.UseVisualStyleBackColor = true;
            // 
            // chkAddDateToTimeStamp
            // 
            this.chkAddDateToTimeStamp.AutoSize = true;
            this.chkAddDateToTimeStamp.Enabled = false;
            this.chkAddDateToTimeStamp.Location = new System.Drawing.Point(21, 91);
            this.chkAddDateToTimeStamp.Name = "chkAddDateToTimeStamp";
            this.chkAddDateToTimeStamp.Size = new System.Drawing.Size(141, 17);
            this.chkAddDateToTimeStamp.TabIndex = 3;
            this.chkAddDateToTimeStamp.Text = "Show date in time stamp";
            this.chkAddDateToTimeStamp.UseVisualStyleBackColor = true;
            // 
            // chkShowTimeStamp
            // 
            this.chkShowTimeStamp.AutoSize = true;
            this.chkShowTimeStamp.Location = new System.Drawing.Point(9, 68);
            this.chkShowTimeStamp.Name = "chkShowTimeStamp";
            this.chkShowTimeStamp.Size = new System.Drawing.Size(194, 17);
            this.chkShowTimeStamp.TabIndex = 2;
            this.chkShowTimeStamp.Text = "Add time stamp to instant messages";
            this.chkShowTimeStamp.UseVisualStyleBackColor = true;
            this.chkShowTimeStamp.CheckedChanged += new System.EventHandler(this.chkShowTimeStamp_CheckedChanged);
            // 
            // groupBoxFont
            // 
            this.groupBoxFont.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxFont.Controls.Add(this.btnChangeFont);
            this.groupBoxFont.Controls.Add(this.lblFontPreview);
            this.groupBoxFont.Controls.Add(this.lblDefaultFont);
            this.groupBoxFont.Location = new System.Drawing.Point(7, 185);
            this.groupBoxFont.Name = "groupBoxFont";
            this.groupBoxFont.Padding = new System.Windows.Forms.Padding(6);
            this.groupBoxFont.Size = new System.Drawing.Size(296, 77);
            this.groupBoxFont.TabIndex = 1;
            this.groupBoxFont.TabStop = false;
            this.groupBoxFont.Text = "Font";
            // 
            // btnChangeFont
            // 
            this.btnChangeFont.Location = new System.Drawing.Point(9, 41);
            this.btnChangeFont.Name = "btnChangeFont";
            this.btnChangeFont.Size = new System.Drawing.Size(75, 23);
            this.btnChangeFont.TabIndex = 2;
            this.btnChangeFont.Text = "Change...";
            this.btnChangeFont.UseVisualStyleBackColor = true;
            this.btnChangeFont.Click += new System.EventHandler(this.btnChangeFont_Click);
            // 
            // lblFontPreview
            // 
            this.lblFontPreview.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblFontPreview.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblFontPreview.Location = new System.Drawing.Point(97, 19);
            this.lblFontPreview.Name = "lblFontPreview";
            this.lblFontPreview.Size = new System.Drawing.Size(190, 45);
            this.lblFontPreview.TabIndex = 1;
            this.lblFontPreview.Text = "Font Preview";
            this.lblFontPreview.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblDefaultFont
            // 
            this.lblDefaultFont.AutoSize = true;
            this.lblDefaultFont.Location = new System.Drawing.Point(9, 22);
            this.lblDefaultFont.Margin = new System.Windows.Forms.Padding(3);
            this.lblDefaultFont.Name = "lblDefaultFont";
            this.lblDefaultFont.Size = new System.Drawing.Size(68, 13);
            this.lblDefaultFont.TabIndex = 0;
            this.lblDefaultFont.Text = "Default Font:";
            this.lblDefaultFont.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tabPageHistory
            // 
            this.tabPageHistory.Controls.Add(this.groupBoxHistoryLocation);
            this.tabPageHistory.Controls.Add(this.groupBoxHistorySettings);
            this.tabPageHistory.Location = new System.Drawing.Point(4, 40);
            this.tabPageHistory.Name = "tabPageHistory";
            this.tabPageHistory.Padding = new System.Windows.Forms.Padding(4, 6, 6, 6);
            this.tabPageHistory.Size = new System.Drawing.Size(312, 306);
            this.tabPageHistory.TabIndex = 7;
            this.tabPageHistory.Text = "History";
            this.tabPageHistory.UseVisualStyleBackColor = true;
            // 
            // groupBoxHistoryLocation
            // 
            this.groupBoxHistoryLocation.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxHistoryLocation.Controls.Add(this.rdbCustomLogFile);
            this.groupBoxHistoryLocation.Controls.Add(this.rdbDefaultLogFile);
            this.groupBoxHistoryLocation.Controls.Add(this.btnSelectLogFile);
            this.groupBoxHistoryLocation.Controls.Add(this.txtLogFilePath);
            this.groupBoxHistoryLocation.Location = new System.Drawing.Point(7, 99);
            this.groupBoxHistoryLocation.Name = "groupBoxHistoryLocation";
            this.groupBoxHistoryLocation.Padding = new System.Windows.Forms.Padding(6);
            this.groupBoxHistoryLocation.Size = new System.Drawing.Size(296, 104);
            this.groupBoxHistoryLocation.TabIndex = 1;
            this.groupBoxHistoryLocation.TabStop = false;
            this.groupBoxHistoryLocation.Text = "History File Location";
            // 
            // rdbCustomLogFile
            // 
            this.rdbCustomLogFile.AutoSize = true;
            this.rdbCustomLogFile.Location = new System.Drawing.Point(9, 46);
            this.rdbCustomLogFile.Name = "rdbCustomLogFile";
            this.rdbCustomLogFile.Size = new System.Drawing.Size(100, 17);
            this.rdbCustomLogFile.TabIndex = 1;
            this.rdbCustomLogFile.TabStop = true;
            this.rdbCustomLogFile.Text = "Custom location";
            this.rdbCustomLogFile.UseVisualStyleBackColor = true;
            this.rdbCustomLogFile.CheckedChanged += new System.EventHandler(this.rdbCustomLogFile_CheckedChanged);
            // 
            // rdbDefaultLogFile
            // 
            this.rdbDefaultLogFile.AutoSize = true;
            this.rdbDefaultLogFile.Location = new System.Drawing.Point(9, 23);
            this.rdbDefaultLogFile.Name = "rdbDefaultLogFile";
            this.rdbDefaultLogFile.Size = new System.Drawing.Size(134, 17);
            this.rdbDefaultLogFile.TabIndex = 0;
            this.rdbDefaultLogFile.TabStop = true;
            this.rdbDefaultLogFile.Text = "System default location";
            this.rdbDefaultLogFile.UseVisualStyleBackColor = true;
            this.rdbDefaultLogFile.CheckedChanged += new System.EventHandler(this.rdbDefaultLogFile_CheckedChanged);
            // 
            // btnSelectLogFile
            // 
            this.btnSelectLogFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSelectLogFile.Enabled = false;
            this.btnSelectLogFile.Location = new System.Drawing.Point(261, 68);
            this.btnSelectLogFile.Name = "btnSelectLogFile";
            this.btnSelectLogFile.Size = new System.Drawing.Size(26, 23);
            this.btnSelectLogFile.TabIndex = 3;
            this.btnSelectLogFile.Text = "...";
            this.btnSelectLogFile.UseVisualStyleBackColor = true;
            this.btnSelectLogFile.Click += new System.EventHandler(this.btnSelectLogFile_Click);
            // 
            // txtLogFilePath
            // 
            this.txtLogFilePath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtLogFilePath.Enabled = false;
            this.txtLogFilePath.Location = new System.Drawing.Point(21, 70);
            this.txtLogFilePath.Name = "txtLogFilePath";
            this.txtLogFilePath.ReadOnly = true;
            this.txtLogFilePath.Size = new System.Drawing.Size(234, 20);
            this.txtLogFilePath.TabIndex = 2;
            // 
            // groupBoxHistorySettings
            // 
            this.groupBoxHistorySettings.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxHistorySettings.Controls.Add(this.btnClearHistory);
            this.groupBoxHistorySettings.Controls.Add(this.chkSaveHistory);
            this.groupBoxHistorySettings.Location = new System.Drawing.Point(7, 9);
            this.groupBoxHistorySettings.Name = "groupBoxHistorySettings";
            this.groupBoxHistorySettings.Padding = new System.Windows.Forms.Padding(6);
            this.groupBoxHistorySettings.Size = new System.Drawing.Size(296, 81);
            this.groupBoxHistorySettings.TabIndex = 0;
            this.groupBoxHistorySettings.TabStop = false;
            this.groupBoxHistorySettings.Text = "History Settings";
            // 
            // btnClearHistory
            // 
            this.btnClearHistory.Location = new System.Drawing.Point(9, 45);
            this.btnClearHistory.Name = "btnClearHistory";
            this.btnClearHistory.Size = new System.Drawing.Size(75, 23);
            this.btnClearHistory.TabIndex = 1;
            this.btnClearHistory.Text = "Clear";
            this.btnClearHistory.UseVisualStyleBackColor = true;
            this.btnClearHistory.Click += new System.EventHandler(this.btnClearHistory_Click);
            // 
            // chkSaveHistory
            // 
            this.chkSaveHistory.AutoSize = true;
            this.chkSaveHistory.Location = new System.Drawing.Point(9, 22);
            this.chkSaveHistory.Name = "chkSaveHistory";
            this.chkSaveHistory.Size = new System.Drawing.Size(174, 17);
            this.chkSaveHistory.TabIndex = 0;
            this.chkSaveHistory.Text = "Keep a history of conversations";
            this.chkSaveHistory.UseVisualStyleBackColor = true;
            // 
            // tabPageAlerts
            // 
            this.tabPageAlerts.Controls.Add(this.groupBoxSounds);
            this.tabPageAlerts.Controls.Add(this.groupBoxAlerts);
            this.tabPageAlerts.Location = new System.Drawing.Point(4, 40);
            this.tabPageAlerts.Name = "tabPageAlerts";
            this.tabPageAlerts.Padding = new System.Windows.Forms.Padding(4, 6, 6, 6);
            this.tabPageAlerts.Size = new System.Drawing.Size(312, 306);
            this.tabPageAlerts.TabIndex = 6;
            this.tabPageAlerts.Text = "Alerts";
            this.tabPageAlerts.UseVisualStyleBackColor = true;
            // 
            // groupBoxSounds
            // 
            this.groupBoxSounds.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxSounds.Controls.Add(this.chkSuspendSoundsOnDND);
            this.groupBoxSounds.Controls.Add(this.chkSuspendSoundsOnBusy);
            this.groupBoxSounds.Controls.Add(this.chkPlaySounds);
            this.groupBoxSounds.Location = new System.Drawing.Point(7, 117);
            this.groupBoxSounds.Name = "groupBoxSounds";
            this.groupBoxSounds.Padding = new System.Windows.Forms.Padding(6);
            this.groupBoxSounds.Size = new System.Drawing.Size(296, 98);
            this.groupBoxSounds.TabIndex = 1;
            this.groupBoxSounds.TabStop = false;
            this.groupBoxSounds.Text = "Sounds";
            // 
            // chkSuspendSoundsOnDND
            // 
            this.chkSuspendSoundsOnDND.AutoSize = true;
            this.chkSuspendSoundsOnDND.Location = new System.Drawing.Point(9, 68);
            this.chkSuspendSoundsOnDND.Name = "chkSuspendSoundsOnDND";
            this.chkSuspendSoundsOnDND.Size = new System.Drawing.Size(264, 17);
            this.chkSuspendSoundsOnDND.TabIndex = 6;
            this.chkSuspendSoundsOnDND.Text = "Suspend sounds when my status is Do Not Disturb";
            this.chkSuspendSoundsOnDND.UseVisualStyleBackColor = true;
            // 
            // chkSuspendSoundsOnBusy
            // 
            this.chkSuspendSoundsOnBusy.AutoSize = true;
            this.chkSuspendSoundsOnBusy.Location = new System.Drawing.Point(9, 45);
            this.chkSuspendSoundsOnBusy.Name = "chkSuspendSoundsOnBusy";
            this.chkSuspendSoundsOnBusy.Size = new System.Drawing.Size(217, 17);
            this.chkSuspendSoundsOnBusy.TabIndex = 5;
            this.chkSuspendSoundsOnBusy.Text = "Suspend sounds when my status is Busy";
            this.chkSuspendSoundsOnBusy.UseVisualStyleBackColor = true;
            // 
            // chkPlaySounds
            // 
            this.chkPlaySounds.AutoSize = true;
            this.chkPlaySounds.Location = new System.Drawing.Point(9, 22);
            this.chkPlaySounds.Name = "chkPlaySounds";
            this.chkPlaySounds.Size = new System.Drawing.Size(169, 17);
            this.chkPlaySounds.TabIndex = 0;
            this.chkPlaySounds.Text = "Provide feedback with sounds";
            this.chkPlaySounds.UseVisualStyleBackColor = true;
            // 
            // groupBoxAlerts
            // 
            this.groupBoxAlerts.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxAlerts.Controls.Add(this.chkSuspendAlertsOnDND);
            this.groupBoxAlerts.Controls.Add(this.chkSuspendAlertsOnBusy);
            this.groupBoxAlerts.Controls.Add(this.chkDisplayAlerts);
            this.groupBoxAlerts.Location = new System.Drawing.Point(7, 9);
            this.groupBoxAlerts.Name = "groupBoxAlerts";
            this.groupBoxAlerts.Padding = new System.Windows.Forms.Padding(6);
            this.groupBoxAlerts.Size = new System.Drawing.Size(296, 98);
            this.groupBoxAlerts.TabIndex = 0;
            this.groupBoxAlerts.TabStop = false;
            this.groupBoxAlerts.Text = "Alerts";
            // 
            // chkSuspendAlertsOnDND
            // 
            this.chkSuspendAlertsOnDND.AutoSize = true;
            this.chkSuspendAlertsOnDND.Location = new System.Drawing.Point(9, 68);
            this.chkSuspendAlertsOnDND.Name = "chkSuspendAlertsOnDND";
            this.chkSuspendAlertsOnDND.Size = new System.Drawing.Size(255, 17);
            this.chkSuspendAlertsOnDND.TabIndex = 2;
            this.chkSuspendAlertsOnDND.Text = "Suspend alerts when my status is Do Not Disturb";
            this.chkSuspendAlertsOnDND.UseVisualStyleBackColor = true;
            // 
            // chkSuspendAlertsOnBusy
            // 
            this.chkSuspendAlertsOnBusy.AutoSize = true;
            this.chkSuspendAlertsOnBusy.Location = new System.Drawing.Point(9, 45);
            this.chkSuspendAlertsOnBusy.Name = "chkSuspendAlertsOnBusy";
            this.chkSuspendAlertsOnBusy.Size = new System.Drawing.Size(208, 17);
            this.chkSuspendAlertsOnBusy.TabIndex = 1;
            this.chkSuspendAlertsOnBusy.Text = "Suspend alerts when my status is Busy";
            this.chkSuspendAlertsOnBusy.UseVisualStyleBackColor = true;
            // 
            // chkDisplayAlerts
            // 
            this.chkDisplayAlerts.AutoSize = true;
            this.chkDisplayAlerts.Location = new System.Drawing.Point(9, 22);
            this.chkDisplayAlerts.Name = "chkDisplayAlerts";
            this.chkDisplayAlerts.Size = new System.Drawing.Size(119, 17);
            this.chkDisplayAlerts.TabIndex = 0;
            this.chkDisplayAlerts.Text = "Display status alerts";
            this.chkDisplayAlerts.UseVisualStyleBackColor = true;
            // 
            // tabPageNetwork
            // 
            this.tabPageNetwork.Controls.Add(this.groupBoxConnection);
            this.tabPageNetwork.Controls.Add(this.lblStar);
            this.tabPageNetwork.Controls.Add(this.groupBoxBroadcast);
            this.tabPageNetwork.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabPageNetwork.Location = new System.Drawing.Point(4, 40);
            this.tabPageNetwork.Name = "tabPageNetwork";
            this.tabPageNetwork.Padding = new System.Windows.Forms.Padding(4, 6, 6, 6);
            this.tabPageNetwork.Size = new System.Drawing.Size(312, 306);
            this.tabPageNetwork.TabIndex = 2;
            this.tabPageNetwork.Text = "Network";
            this.tabPageNetwork.UseVisualStyleBackColor = true;
            // 
            // groupBoxConnection
            // 
            this.groupBoxConnection.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxConnection.Controls.Add(this.lblConnectionRetriesVal);
            this.groupBoxConnection.Controls.Add(this.lblConnectionTimeOutVals);
            this.groupBoxConnection.Controls.Add(this.txtConnectionRetries);
            this.groupBoxConnection.Controls.Add(this.lblConnectionRetries);
            this.groupBoxConnection.Controls.Add(this.lblConnectionTimeOut);
            this.groupBoxConnection.Controls.Add(this.txtConnectionTimeOut);
            this.groupBoxConnection.Location = new System.Drawing.Point(7, 8);
            this.groupBoxConnection.Name = "groupBoxConnection";
            this.groupBoxConnection.Padding = new System.Windows.Forms.Padding(6);
            this.groupBoxConnection.Size = new System.Drawing.Size(296, 80);
            this.groupBoxConnection.TabIndex = 0;
            this.groupBoxConnection.TabStop = false;
            this.groupBoxConnection.Text = "Connection";
            // 
            // lblConnectionRetriesVal
            // 
            this.lblConnectionRetriesVal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblConnectionRetriesVal.AutoSize = true;
            this.lblConnectionRetriesVal.Location = new System.Drawing.Point(225, 49);
            this.lblConnectionRetriesVal.Name = "lblConnectionRetriesVal";
            this.lblConnectionRetriesVal.Size = new System.Drawing.Size(31, 13);
            this.lblConnectionRetriesVal.TabIndex = 10;
            this.lblConnectionRetriesVal.Text = "(0-6)";
            // 
            // lblConnectionTimeOutVals
            // 
            this.lblConnectionTimeOutVals.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblConnectionTimeOutVals.AutoSize = true;
            this.lblConnectionTimeOutVals.Location = new System.Drawing.Point(225, 23);
            this.lblConnectionTimeOutVals.Name = "lblConnectionTimeOutVals";
            this.lblConnectionTimeOutVals.Size = new System.Drawing.Size(43, 13);
            this.lblConnectionTimeOutVals.TabIndex = 9;
            this.lblConnectionTimeOutVals.Text = "(5-120)";
            // 
            // txtConnectionRetries
            // 
            this.txtConnectionRetries.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtConnectionRetries.ErrorText = null;
            this.txtConnectionRetries.ErrorTitle = null;
            this.txtConnectionRetries.InputMask = "^\\d*$";
            this.txtConnectionRetries.Location = new System.Drawing.Point(187, 46);
            this.txtConnectionRetries.Margin = new System.Windows.Forms.Padding(3, 3, 0, 3);
            this.txtConnectionRetries.MaximumValue = "6";
            this.txtConnectionRetries.MinimumValue = "0";
            this.txtConnectionRetries.Name = "txtConnectionRetries";
            this.txtConnectionRetries.RangeValidation = true;
            this.txtConnectionRetries.RequiredField = true;
            this.txtConnectionRetries.Size = new System.Drawing.Size(35, 21);
            this.txtConnectionRetries.TabIndex = 2;
            this.txtConnectionRetries.ValidationExpression = "^\\d*$";
            this.txtConnectionRetries.ValidationMessage = null;
            // 
            // lblConnectionRetries
            // 
            this.lblConnectionRetries.AutoSize = true;
            this.lblConnectionRetries.Location = new System.Drawing.Point(9, 49);
            this.lblConnectionRetries.Name = "lblConnectionRetries";
            this.lblConnectionRetries.Size = new System.Drawing.Size(95, 13);
            this.lblConnectionRetries.TabIndex = 8;
            this.lblConnectionRetries.Text = "Number of retries:";
            // 
            // lblConnectionTimeOut
            // 
            this.lblConnectionTimeOut.AutoSize = true;
            this.lblConnectionTimeOut.Location = new System.Drawing.Point(9, 23);
            this.lblConnectionTimeOut.Name = "lblConnectionTimeOut";
            this.lblConnectionTimeOut.Size = new System.Drawing.Size(79, 13);
            this.lblConnectionTimeOut.TabIndex = 7;
            this.lblConnectionTimeOut.Text = "Time out (sec):";
            // 
            // txtConnectionTimeOut
            // 
            this.txtConnectionTimeOut.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtConnectionTimeOut.ErrorText = null;
            this.txtConnectionTimeOut.ErrorTitle = null;
            this.txtConnectionTimeOut.InputMask = "^\\d*$";
            this.txtConnectionTimeOut.Location = new System.Drawing.Point(187, 20);
            this.txtConnectionTimeOut.Margin = new System.Windows.Forms.Padding(3, 3, 0, 3);
            this.txtConnectionTimeOut.MaximumValue = "120";
            this.txtConnectionTimeOut.MinimumValue = "5";
            this.txtConnectionTimeOut.Name = "txtConnectionTimeOut";
            this.txtConnectionTimeOut.RangeValidation = true;
            this.txtConnectionTimeOut.RequiredField = true;
            this.txtConnectionTimeOut.Size = new System.Drawing.Size(35, 21);
            this.txtConnectionTimeOut.TabIndex = 1;
            this.txtConnectionTimeOut.ValidationExpression = "^\\d*$";
            this.txtConnectionTimeOut.ValidationMessage = null;
            // 
            // lblStar
            // 
            this.lblStar.AutoSize = true;
            this.lblStar.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStar.Location = new System.Drawing.Point(7, 206);
            this.lblStar.Name = "lblStar";
            this.lblStar.Size = new System.Drawing.Size(209, 13);
            this.lblStar.TabIndex = 1;
            this.lblStar.Text = "*Takes effect after you restart %TITLE%";
            // 
            // groupBoxBroadcast
            // 
            this.groupBoxBroadcast.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxBroadcast.Controls.Add(this.txtTCPPort);
            this.groupBoxBroadcast.Controls.Add(this.lblTCPPort);
            this.groupBoxBroadcast.Controls.Add(this.lblUDPPort);
            this.groupBoxBroadcast.Controls.Add(this.txtUDPPort);
            this.groupBoxBroadcast.Controls.Add(this.txtBroadcastAddress);
            this.groupBoxBroadcast.Controls.Add(this.lblBroadcastAddress);
            this.groupBoxBroadcast.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBoxBroadcast.Location = new System.Drawing.Point(7, 97);
            this.groupBoxBroadcast.Name = "groupBoxBroadcast";
            this.groupBoxBroadcast.Padding = new System.Windows.Forms.Padding(6);
            this.groupBoxBroadcast.Size = new System.Drawing.Size(296, 106);
            this.groupBoxBroadcast.TabIndex = 1;
            this.groupBoxBroadcast.TabStop = false;
            this.groupBoxBroadcast.Text = "Broadcast Settings";
            // 
            // txtTCPPort
            // 
            this.txtTCPPort.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTCPPort.ErrorText = null;
            this.txtTCPPort.ErrorTitle = null;
            this.txtTCPPort.InputMask = "^\\d*$";
            this.txtTCPPort.Location = new System.Drawing.Point(187, 72);
            this.txtTCPPort.MaximumValue = "65535";
            this.txtTCPPort.MinimumValue = "0";
            this.txtTCPPort.Name = "txtTCPPort";
            this.txtTCPPort.RangeValidation = true;
            this.txtTCPPort.RequiredField = true;
            this.txtTCPPort.Size = new System.Drawing.Size(55, 21);
            this.txtTCPPort.TabIndex = 3;
            this.txtTCPPort.ValidationExpression = "^(6553[0-5]|655[0-2]\\d|65[0-4]\\d\\d|6[0-4]\\d{3}|[1-5]\\d{4}|[1-9]\\d{0,3}|0)$";
            this.txtTCPPort.ValidationMessage = "Please enter a valid port number from 0-65535.";
            // 
            // lblTCPPort
            // 
            this.lblTCPPort.AutoSize = true;
            this.lblTCPPort.Location = new System.Drawing.Point(9, 75);
            this.lblTCPPort.Name = "lblTCPPort";
            this.lblTCPPort.Size = new System.Drawing.Size(59, 13);
            this.lblTCPPort.TabIndex = 6;
            this.lblTCPPort.Text = "TCP Port*:";
            // 
            // lblUDPPort
            // 
            this.lblUDPPort.AutoSize = true;
            this.lblUDPPort.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUDPPort.Location = new System.Drawing.Point(9, 49);
            this.lblUDPPort.Name = "lblUDPPort";
            this.lblUDPPort.Size = new System.Drawing.Size(60, 13);
            this.lblUDPPort.TabIndex = 4;
            this.lblUDPPort.Text = "UDP Port*:";
            // 
            // txtUDPPort
            // 
            this.txtUDPPort.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtUDPPort.ErrorText = null;
            this.txtUDPPort.ErrorTitle = null;
            this.txtUDPPort.InputMask = "^\\d*$";
            this.txtUDPPort.Location = new System.Drawing.Point(187, 46);
            this.txtUDPPort.MaximumValue = "65535";
            this.txtUDPPort.MinimumValue = "0";
            this.txtUDPPort.Name = "txtUDPPort";
            this.txtUDPPort.RangeValidation = true;
            this.txtUDPPort.RequiredField = true;
            this.txtUDPPort.Size = new System.Drawing.Size(55, 21);
            this.txtUDPPort.TabIndex = 2;
            this.txtUDPPort.ValidationExpression = "^(6553[0-5]|655[0-2]\\d|65[0-4]\\d\\d|6[0-4]\\d{3}|[1-5]\\d{4}|[1-9]\\d{0,3}|0)$";
            this.txtUDPPort.ValidationMessage = "Please enter a valid port number from 0-65535.";
            // 
            // txtBroadcastAddress
            // 
            this.txtBroadcastAddress.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtBroadcastAddress.ErrorText = null;
            this.txtBroadcastAddress.ErrorTitle = null;
            this.txtBroadcastAddress.InputMask = "^[0-9\\.]*$";
            this.txtBroadcastAddress.Location = new System.Drawing.Point(187, 20);
            this.txtBroadcastAddress.MaximumValue = null;
            this.txtBroadcastAddress.MinimumValue = null;
            this.txtBroadcastAddress.Name = "txtBroadcastAddress";
            this.txtBroadcastAddress.RequiredField = true;
            this.txtBroadcastAddress.Size = new System.Drawing.Size(100, 21);
            this.txtBroadcastAddress.TabIndex = 1;
            this.txtBroadcastAddress.ValidationExpression = "^(?:(?:25[0-5]|2[0-4]\\d|[01]\\d\\d|\\d?\\d)(?(?=\\.?\\d)\\.)){4}$";
            this.txtBroadcastAddress.ValidationMessage = "Please enter a valid IP address.";
            // 
            // lblBroadcastAddress
            // 
            this.lblBroadcastAddress.AutoSize = true;
            this.lblBroadcastAddress.Location = new System.Drawing.Point(9, 23);
            this.lblBroadcastAddress.Name = "lblBroadcastAddress";
            this.lblBroadcastAddress.Size = new System.Drawing.Size(101, 13);
            this.lblBroadcastAddress.TabIndex = 0;
            this.lblBroadcastAddress.Text = "Broadcast Address:";
            // 
            // tabPageFileTransfer
            // 
            this.tabPageFileTransfer.Controls.Add(this.lnkFileSecurityRisks);
            this.tabPageFileTransfer.Controls.Add(this.groupBoxReceivedFileFolder);
            this.tabPageFileTransfer.Controls.Add(this.groupBoxIncomingFileRequest);
            this.tabPageFileTransfer.Location = new System.Drawing.Point(4, 40);
            this.tabPageFileTransfer.Name = "tabPageFileTransfer";
            this.tabPageFileTransfer.Padding = new System.Windows.Forms.Padding(4, 6, 6, 6);
            this.tabPageFileTransfer.Size = new System.Drawing.Size(312, 306);
            this.tabPageFileTransfer.TabIndex = 4;
            this.tabPageFileTransfer.Text = "File Transfer";
            this.tabPageFileTransfer.UseVisualStyleBackColor = true;
            // 
            // lnkFileSecurityRisks
            // 
            this.lnkFileSecurityRisks.AutoSize = true;
            this.lnkFileSecurityRisks.Location = new System.Drawing.Point(7, 208);
            this.lnkFileSecurityRisks.Name = "lnkFileSecurityRisks";
            this.lnkFileSecurityRisks.Size = new System.Drawing.Size(143, 13);
            this.lnkFileSecurityRisks.TabIndex = 2;
            this.lnkFileSecurityRisks.TabStop = true;
            this.lnkFileSecurityRisks.Text = "Learn about file security risks";
            this.lnkFileSecurityRisks.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkFileSecurityRisks_LinkClicked);
            this.lnkFileSecurityRisks.MouseLeave += new System.EventHandler(this.lnkLabel_MouseLeave);
            // 
            // groupBoxReceivedFileFolder
            // 
            this.groupBoxReceivedFileFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxReceivedFileFolder.Controls.Add(this.btnViewFiles);
            this.groupBoxReceivedFileFolder.Controls.Add(this.btnChangeReceivedFileFolder);
            this.groupBoxReceivedFileFolder.Controls.Add(this.lblReceivedFileFolder);
            this.groupBoxReceivedFileFolder.Location = new System.Drawing.Point(7, 118);
            this.groupBoxReceivedFileFolder.Name = "groupBoxReceivedFileFolder";
            this.groupBoxReceivedFileFolder.Padding = new System.Windows.Forms.Padding(4, 6, 6, 6);
            this.groupBoxReceivedFileFolder.Size = new System.Drawing.Size(296, 84);
            this.groupBoxReceivedFileFolder.TabIndex = 1;
            this.groupBoxReceivedFileFolder.TabStop = false;
            this.groupBoxReceivedFileFolder.Text = "Store Received Files in this Folder";
            // 
            // btnViewFiles
            // 
            this.btnViewFiles.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnViewFiles.Location = new System.Drawing.Point(212, 48);
            this.btnViewFiles.Name = "btnViewFiles";
            this.btnViewFiles.Size = new System.Drawing.Size(75, 23);
            this.btnViewFiles.TabIndex = 2;
            this.btnViewFiles.Text = "View Files...";
            this.btnViewFiles.UseVisualStyleBackColor = true;
            this.btnViewFiles.Click += new System.EventHandler(this.btnViewFiles_Click);
            // 
            // btnChangeReceivedFileFolder
            // 
            this.btnChangeReceivedFileFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnChangeReceivedFileFolder.Location = new System.Drawing.Point(212, 18);
            this.btnChangeReceivedFileFolder.Name = "btnChangeReceivedFileFolder";
            this.btnChangeReceivedFileFolder.Size = new System.Drawing.Size(75, 23);
            this.btnChangeReceivedFileFolder.TabIndex = 1;
            this.btnChangeReceivedFileFolder.Text = "Change...";
            this.btnChangeReceivedFileFolder.UseVisualStyleBackColor = true;
            this.btnChangeReceivedFileFolder.Click += new System.EventHandler(this.btnChangeReceivedFileFolder_Click);
            // 
            // lblReceivedFileFolder
            // 
            this.lblReceivedFileFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblReceivedFileFolder.Location = new System.Drawing.Point(8, 23);
            this.lblReceivedFileFolder.Name = "lblReceivedFileFolder";
            this.lblReceivedFileFolder.Size = new System.Drawing.Size(200, 33);
            this.lblReceivedFileFolder.TabIndex = 0;
            this.lblReceivedFileFolder.Text = "<Default>";
            // 
            // groupBoxIncomingFileRequest
            // 
            this.groupBoxIncomingFileRequest.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxIncomingFileRequest.Controls.Add(this.chkAutoReceiveFile);
            this.groupBoxIncomingFileRequest.Controls.Add(this.rdbFileToTaskbar);
            this.groupBoxIncomingFileRequest.Controls.Add(this.rdbFileToForeground);
            this.groupBoxIncomingFileRequest.Location = new System.Drawing.Point(7, 9);
            this.groupBoxIncomingFileRequest.Name = "groupBoxIncomingFileRequest";
            this.groupBoxIncomingFileRequest.Padding = new System.Windows.Forms.Padding(4, 6, 6, 6);
            this.groupBoxIncomingFileRequest.Size = new System.Drawing.Size(296, 100);
            this.groupBoxIncomingFileRequest.TabIndex = 0;
            this.groupBoxIncomingFileRequest.TabStop = false;
            this.groupBoxIncomingFileRequest.Text = "Incoming File Request";
            // 
            // chkAutoReceiveFile
            // 
            this.chkAutoReceiveFile.AutoSize = true;
            this.chkAutoReceiveFile.Location = new System.Drawing.Point(21, 70);
            this.chkAutoReceiveFile.Name = "chkAutoReceiveFile";
            this.chkAutoReceiveFile.Size = new System.Drawing.Size(235, 17);
            this.chkAutoReceiveFile.TabIndex = 2;
            this.chkAutoReceiveFile.Text = "Accept and start receiving files automatically";
            this.chkAutoReceiveFile.UseVisualStyleBackColor = true;
            // 
            // rdbFileToTaskbar
            // 
            this.rdbFileToTaskbar.AutoSize = true;
            this.rdbFileToTaskbar.Location = new System.Drawing.Point(9, 46);
            this.rdbFileToTaskbar.Name = "rdbFileToTaskbar";
            this.rdbFileToTaskbar.Size = new System.Drawing.Size(252, 17);
            this.rdbFileToTaskbar.TabIndex = 1;
            this.rdbFileToTaskbar.TabStop = true;
            this.rdbFileToTaskbar.Text = "Minimize incoming file transfer request to taskbar";
            this.rdbFileToTaskbar.UseVisualStyleBackColor = true;
            // 
            // rdbFileToForeground
            // 
            this.rdbFileToForeground.AutoSize = true;
            this.rdbFileToForeground.Location = new System.Drawing.Point(9, 23);
            this.rdbFileToForeground.Name = "rdbFileToForeground";
            this.rdbFileToForeground.Size = new System.Drawing.Size(232, 17);
            this.rdbFileToForeground.TabIndex = 0;
            this.rdbFileToForeground.TabStop = true;
            this.rdbFileToForeground.Text = "Set incoming file transfer request foreground";
            this.rdbFileToForeground.UseVisualStyleBackColor = true;
            // 
            // tabPageHotKeys
            // 
            this.tabPageHotKeys.Controls.Add(this.groupBoxHotKeys);
            this.tabPageHotKeys.Location = new System.Drawing.Point(4, 40);
            this.tabPageHotKeys.Name = "tabPageHotKeys";
            this.tabPageHotKeys.Padding = new System.Windows.Forms.Padding(4, 6, 6, 6);
            this.tabPageHotKeys.Size = new System.Drawing.Size(312, 306);
            this.tabPageHotKeys.TabIndex = 3;
            this.tabPageHotKeys.Text = "Hot Keys";
            this.tabPageHotKeys.UseVisualStyleBackColor = true;
            // 
            // groupBoxHotKeys
            // 
            this.groupBoxHotKeys.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxHotKeys.Controls.Add(this.rdbMessageHotKeyMod);
            this.groupBoxHotKeys.Controls.Add(this.rdbMessageHotKey);
            this.groupBoxHotKeys.Controls.Add(this.lblSendMessageHotKey);
            this.groupBoxHotKeys.Location = new System.Drawing.Point(7, 9);
            this.groupBoxHotKeys.Name = "groupBoxHotKeys";
            this.groupBoxHotKeys.Padding = new System.Windows.Forms.Padding(6);
            this.groupBoxHotKeys.Size = new System.Drawing.Size(296, 72);
            this.groupBoxHotKeys.TabIndex = 0;
            this.groupBoxHotKeys.TabStop = false;
            this.groupBoxHotKeys.Text = "Messages and Conversations";
            // 
            // rdbMessageHotKeyMod
            // 
            this.rdbMessageHotKeyMod.AutoSize = true;
            this.rdbMessageHotKeyMod.Location = new System.Drawing.Point(95, 41);
            this.rdbMessageHotKeyMod.Margin = new System.Windows.Forms.Padding(12, 3, 3, 3);
            this.rdbMessageHotKeyMod.Name = "rdbMessageHotKeyMod";
            this.rdbMessageHotKeyMod.Size = new System.Drawing.Size(77, 17);
            this.rdbMessageHotKeyMod.TabIndex = 2;
            this.rdbMessageHotKeyMod.TabStop = true;
            this.rdbMessageHotKeyMod.Text = "Ctrl + Enter";
            this.rdbMessageHotKeyMod.UseVisualStyleBackColor = true;
            // 
            // rdbMessageHotKey
            // 
            this.rdbMessageHotKey.AutoSize = true;
            this.rdbMessageHotKey.Location = new System.Drawing.Point(30, 42);
            this.rdbMessageHotKey.Name = "rdbMessageHotKey";
            this.rdbMessageHotKey.Size = new System.Drawing.Size(50, 17);
            this.rdbMessageHotKey.TabIndex = 1;
            this.rdbMessageHotKey.TabStop = true;
            this.rdbMessageHotKey.Text = "Enter";
            this.rdbMessageHotKey.UseVisualStyleBackColor = true;
            // 
            // lblSendMessageHotKey
            // 
            this.lblSendMessageHotKey.AutoSize = true;
            this.lblSendMessageHotKey.Location = new System.Drawing.Point(9, 23);
            this.lblSendMessageHotKey.Margin = new System.Windows.Forms.Padding(3);
            this.lblSendMessageHotKey.Name = "lblSendMessageHotKey";
            this.lblSendMessageHotKey.Size = new System.Drawing.Size(113, 13);
            this.lblSendMessageHotKey.TabIndex = 0;
            this.lblSendMessageHotKey.Text = "Send messages using:";
            // 
            // fontDialog
            // 
            this.fontDialog.AllowScriptChange = false;
            this.fontDialog.AllowVerticalFonts = false;
            this.fontDialog.ScriptsOnly = true;
            this.fontDialog.ShowColor = true;
            // 
            // saveFileDialog
            // 
            this.saveFileDialog.DefaultExt = "db";
            this.saveFileDialog.Filter = "Database File|*.db";
            this.saveFileDialog.FileOk += new System.ComponentModel.CancelEventHandler(this.saveFileDialog_FileOk);
            // 
            // lblEveryText
            // 
            this.lblEveryText.AutoSize = true;
            this.lblEveryText.Location = new System.Drawing.Point(38, 45);
            this.lblEveryText.Name = "lblEveryText";
            this.lblEveryText.Size = new System.Drawing.Size(33, 13);
            this.lblEveryText.TabIndex = 6;
            this.lblEveryText.Text = "every";
            // 
            // lblForText
            // 
            this.lblForText.AutoSize = true;
            this.lblForText.Location = new System.Drawing.Point(52, 93);
            this.lblForText.Name = "lblForText";
            this.lblForText.Size = new System.Drawing.Size(19, 13);
            this.lblForText.TabIndex = 7;
            this.lblForText.Text = "for";
            // 
            // SettingsDialog
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(334, 394);
            this.Controls.Add(this.tableLayoutPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SettingsDialog";
            this.Padding = new System.Windows.Forms.Padding(8, 6, 6, 6);
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "%TITLE% - Options";
            this.tableLayoutPanel.ResumeLayout(false);
            this.tabControl.ResumeLayout(false);
            this.tabPageGeneral.ResumeLayout(false);
            this.groupBoxSystem.ResumeLayout(false);
            this.groupBoxSystem.PerformLayout();
            this.groupBoxUserList.ResumeLayout(false);
            this.groupBoxUserList.PerformLayout();
            this.tabPageAppearance.ResumeLayout(false);
            this.groupBoxThemes.ResumeLayout(false);
            this.groupBoxThemes.PerformLayout();
            this.groupBoxTabs.ResumeLayout(false);
            this.groupBoxTabs.PerformLayout();
            this.tabPageMessages.ResumeLayout(false);
            this.groupBoxInstantMessages.ResumeLayout(false);
            this.groupBoxInstantMessages.PerformLayout();
            this.groupBoxFont.ResumeLayout(false);
            this.groupBoxFont.PerformLayout();
            this.tabPageHistory.ResumeLayout(false);
            this.groupBoxHistoryLocation.ResumeLayout(false);
            this.groupBoxHistoryLocation.PerformLayout();
            this.groupBoxHistorySettings.ResumeLayout(false);
            this.groupBoxHistorySettings.PerformLayout();
            this.tabPageAlerts.ResumeLayout(false);
            this.groupBoxSounds.ResumeLayout(false);
            this.groupBoxSounds.PerformLayout();
            this.groupBoxAlerts.ResumeLayout(false);
            this.groupBoxAlerts.PerformLayout();
            this.tabPageNetwork.ResumeLayout(false);
            this.tabPageNetwork.PerformLayout();
            this.groupBoxConnection.ResumeLayout(false);
            this.groupBoxConnection.PerformLayout();
            this.groupBoxBroadcast.ResumeLayout(false);
            this.groupBoxBroadcast.PerformLayout();
            this.tabPageFileTransfer.ResumeLayout(false);
            this.tabPageFileTransfer.PerformLayout();
            this.groupBoxReceivedFileFolder.ResumeLayout(false);
            this.groupBoxIncomingFileRequest.ResumeLayout(false);
            this.groupBoxIncomingFileRequest.PerformLayout();
            this.tabPageHotKeys.ResumeLayout(false);
            this.groupBoxHotKeys.ResumeLayout(false);
            this.groupBoxHotKeys.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPageGeneral;
        private System.Windows.Forms.TabPage tabPageMessages;
        private System.Windows.Forms.TabPage tabPageNetwork;
        private System.Windows.Forms.TabPage tabPageHotKeys;
        private System.Windows.Forms.CheckBox chkLaunchAtStartup;
        private System.Windows.Forms.CheckBox chkSilentMode;
        private System.Windows.Forms.GroupBox groupBoxUserList;
        private System.Windows.Forms.CheckBox chkAutoRefreshUser;
        private System.Windows.Forms.Label lblSeconds;
        private LANMedia.CustomControls.TextBoxEx txtUserRefreshTime;
        private System.Windows.Forms.GroupBox groupBoxFont;
        private System.Windows.Forms.Label lblFontPreview;
        private System.Windows.Forms.Label lblDefaultFont;
        private System.Windows.Forms.Button btnChangeFont;
        private System.Windows.Forms.GroupBox groupBoxSystem;
        private System.Windows.Forms.GroupBox groupBoxBroadcast;
        private System.Windows.Forms.Label lblUDPPort;
        private LANMedia.CustomControls.TextBoxEx txtUDPPort;
        private LANMedia.CustomControls.TextBoxEx txtBroadcastAddress;
        private System.Windows.Forms.Label lblBroadcastAddress;
        private System.Windows.Forms.GroupBox groupBoxHotKeys;
        private System.Windows.Forms.RadioButton rdbMessageHotKeyMod;
        private System.Windows.Forms.RadioButton rdbMessageHotKey;
        private System.Windows.Forms.Label lblSendMessageHotKey;
        private System.Windows.Forms.FontDialog fontDialog;
        private System.Windows.Forms.Label lblStar;
        private LANMedia.CustomControls.TextBoxEx txtTCPPort;
        private System.Windows.Forms.Label lblTCPPort;
        private System.Windows.Forms.TabPage tabPageFileTransfer;
        private System.Windows.Forms.GroupBox groupBoxIncomingFileRequest;
        private System.Windows.Forms.RadioButton rdbFileToTaskbar;
        private System.Windows.Forms.RadioButton rdbFileToForeground;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private System.Windows.Forms.GroupBox groupBoxReceivedFileFolder;
        private System.Windows.Forms.Label lblReceivedFileFolder;
        private System.Windows.Forms.Button btnChangeReceivedFileFolder;
        private System.Windows.Forms.CheckBox chkAutoReceiveFile;
        private System.Windows.Forms.CheckBox chkShowNotifications;
        private System.Windows.Forms.LinkLabel lnkLabelSilentMode;
        private System.Windows.Forms.ToolTip toolTipInfo;
        private System.Windows.Forms.LinkLabel lnkFileSecurityRisks;
        private System.Windows.Forms.TabPage tabPageAppearance;
        private System.Windows.Forms.GroupBox groupBoxTabs;
        private System.Windows.Forms.RadioButton rdbChatInWindow;
        private System.Windows.Forms.RadioButton rdbChatInTab;
        private System.Windows.Forms.GroupBox groupBoxThemes;
        private System.Windows.Forms.Label lblUseTheme;
        private System.Windows.Forms.ComboBox cboTheme;
        private System.Windows.Forms.TabPage tabPageAlerts;
        private System.Windows.Forms.GroupBox groupBoxAlerts;
        private System.Windows.Forms.CheckBox chkSuspendAlertsOnBusy;
        private System.Windows.Forms.CheckBox chkDisplayAlerts;
        private System.Windows.Forms.GroupBox groupBoxSounds;
        private System.Windows.Forms.CheckBox chkPlaySounds;
        private System.Windows.Forms.CheckBox chkSuspendSoundsOnBusy;
        private System.Windows.Forms.GroupBox groupBoxConnection;
        private LANMedia.CustomControls.TextBoxEx txtConnectionRetries;
        private System.Windows.Forms.Label lblConnectionRetries;
        private System.Windows.Forms.Label lblConnectionTimeOut;
        private LANMedia.CustomControls.TextBoxEx txtConnectionTimeOut;
        private System.Windows.Forms.Label lblConnectionRetriesVal;
        private System.Windows.Forms.Label lblConnectionTimeOutVals;
        private System.Windows.Forms.CheckBox chkUseThemes;
        private System.Windows.Forms.Button btnViewFiles;
        private System.Windows.Forms.GroupBox groupBoxInstantMessages;
        private System.Windows.Forms.CheckBox chkShowTimeStamp;
        private System.Windows.Forms.CheckBox chkAddDateToTimeStamp;
        private System.Windows.Forms.TabPage tabPageHistory;
        private System.Windows.Forms.GroupBox groupBoxHistorySettings;
        private System.Windows.Forms.Button btnClearHistory;
        private System.Windows.Forms.CheckBox chkSaveHistory;
        private System.Windows.Forms.RadioButton rdbMessageToTaskbar;
        private System.Windows.Forms.RadioButton rdbMessageToForeground;
        private System.Windows.Forms.GroupBox groupBoxHistoryLocation;
        private System.Windows.Forms.TextBox txtLogFilePath;
        private System.Windows.Forms.Button btnSelectLogFile;
        private System.Windows.Forms.RadioButton rdbCustomLogFile;
        private System.Windows.Forms.RadioButton rdbDefaultLogFile;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.CheckBox chkEmotTextToImage;
        private System.Windows.Forms.CheckBox chkShowEmoticons;
        private System.Windows.Forms.CheckBox chkIdleTime;
        private System.Windows.Forms.Label label1;
        private LANMedia.CustomControls.TextBoxEx txtIdleTime;
        private System.Windows.Forms.CheckBox chkSuspendSoundsOnDND;
        private System.Windows.Forms.CheckBox chkSuspendAlertsOnDND;
        private System.Windows.Forms.Label lblEveryText;
        private System.Windows.Forms.Label lblForText;
    }
}