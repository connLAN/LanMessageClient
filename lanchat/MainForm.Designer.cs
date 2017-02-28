namespace LANChat
{
    partial class MainForm
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

            //  Close the connections.
            if (sendUdpClient != null && sendUdpClient.Client != null)
                sendUdpClient.Close();
            if (receiveUdpClient != null && receiveUdpClient.Client != null)
                receiveUdpClient.Close();
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.tableLayoutPanelMain = new System.Windows.Forms.TableLayoutPanel();
            this.splitContainerMain = new System.Windows.Forms.SplitContainer();
            this.tableLayoutPanelLeft = new System.Windows.Forms.TableLayoutPanel();
            this.lblContactsHeader = new LANMedia.CustomControls.LabelEx();
            this.tvUsers = new LANMedia.CustomControls.TreeViewEx();
            this.imageListSmall = new System.Windows.Forms.ImageList(this.components);
            this.tableLayoutPanelUserHeader = new LANMedia.CustomControls.TableLayoutPanelEx();
            this.picBoxUserStatus = new System.Windows.Forms.PictureBox();
            this.lblUserName = new System.Windows.Forms.Label();
            this.lblUserStatus = new LANMedia.CustomControls.LabelEx();
            this.tabControlChat = new LANMedia.CustomControls.TabControlEx();
            this.lblInfoText = new System.Windows.Forms.Label();
            this.lblInfo = new System.Windows.Forms.Label();
            this.notifyIconSysTray = new System.Windows.Forms.NotifyIcon(this.components);
            this.timerUserUpdate = new System.Windows.Forms.Timer(this.components);
            this.timerStatusUpdate = new System.Windows.Forms.Timer(this.components);
            this.mnuNotify = new System.Windows.Forms.ContextMenu();
            this.mnuNotifyShow = new System.Windows.Forms.MenuItem();
            this.mnuNotifySep1 = new System.Windows.Forms.MenuItem();
            this.mnuNotifyStatus = new System.Windows.Forms.MenuItem();
            this.mnuNotifySilentMode = new System.Windows.Forms.MenuItem();
            this.mnuNotifySep2 = new System.Windows.Forms.MenuItem();
            this.mnuNotifyHistory = new System.Windows.Forms.MenuItem();
            this.mnuNotifyFileTransfers = new System.Windows.Forms.MenuItem();
            this.menuNotifySep3 = new System.Windows.Forms.MenuItem();
            this.mnuNotifyOptions = new System.Windows.Forms.MenuItem();
            this.mnuNotifyAbout = new System.Windows.Forms.MenuItem();
            this.mnuNotifySep4 = new System.Windows.Forms.MenuItem();
            this.mnuNotifyExit = new System.Windows.Forms.MenuItem();
            this.imageMenuEx = new LANMedia.CustomControls.ImageMenuEx(this.components);
            this.mnuUser = new System.Windows.Forms.ContextMenu();
            this.mnuUserConversation = new System.Windows.Forms.MenuItem();
            this.mnuUserSendFile = new System.Windows.Forms.MenuItem();
            this.mnuUserSep1 = new System.Windows.Forms.MenuItem();
            this.mnuUserBroadcast = new System.Windows.Forms.MenuItem();
            this.mnuUserSep2 = new System.Windows.Forms.MenuItem();
            this.mnuUserInformation = new System.Windows.Forms.MenuItem();
            this.mnuUserSep3 = new System.Windows.Forms.MenuItem();
            this.mnuUserMove = new System.Windows.Forms.MenuItem();
            this.mnuGroup = new System.Windows.Forms.ContextMenu();
            this.mnuGroupAdd = new System.Windows.Forms.MenuItem();
            this.mnuGroupRename = new System.Windows.Forms.MenuItem();
            this.mnuGroupDelete = new System.Windows.Forms.MenuItem();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.mnuStatus = new System.Windows.Forms.ContextMenu();
            this.tableLayoutPanelMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerMain)).BeginInit();
            this.splitContainerMain.Panel1.SuspendLayout();
            this.splitContainerMain.Panel2.SuspendLayout();
            this.splitContainerMain.SuspendLayout();
            this.tableLayoutPanelLeft.SuspendLayout();
            this.tableLayoutPanelUserHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxUserStatus)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageMenuEx)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanelMain
            // 
            this.tableLayoutPanelMain.ColumnCount = 1;
            this.tableLayoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanelMain.Controls.Add(this.splitContainerMain, 0, 0);
            this.tableLayoutPanelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelMain.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanelMain.Name = "tableLayoutPanelMain";
            this.tableLayoutPanelMain.RowCount = 1;
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelMain.Size = new System.Drawing.Size(584, 414);
            this.tableLayoutPanelMain.TabIndex = 0;
            // 
            // splitContainerMain
            // 
            this.splitContainerMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerMain.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainerMain.Location = new System.Drawing.Point(1, 1);
            this.splitContainerMain.Margin = new System.Windows.Forms.Padding(1);
            this.splitContainerMain.Name = "splitContainerMain";
            // 
            // splitContainerMain.Panel1
            // 
            this.splitContainerMain.Panel1.Controls.Add(this.tableLayoutPanelLeft);
            this.splitContainerMain.Panel1MinSize = 150;
            // 
            // splitContainerMain.Panel2
            // 
            this.splitContainerMain.Panel2.Controls.Add(this.tabControlChat);
            this.splitContainerMain.Panel2.Controls.Add(this.lblInfoText);
            this.splitContainerMain.Panel2.Controls.Add(this.lblInfo);
            this.splitContainerMain.Panel2.Padding = new System.Windows.Forms.Padding(0, 0, 2, 1);
            this.splitContainerMain.Panel2MinSize = 375;
            this.splitContainerMain.Size = new System.Drawing.Size(582, 412);
            this.splitContainerMain.SplitterDistance = 150;
            this.splitContainerMain.TabIndex = 0;
            // 
            // tableLayoutPanelLeft
            // 
            this.tableLayoutPanelLeft.ColumnCount = 1;
            this.tableLayoutPanelLeft.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelLeft.Controls.Add(this.lblContactsHeader, 0, 1);
            this.tableLayoutPanelLeft.Controls.Add(this.tvUsers, 0, 2);
            this.tableLayoutPanelLeft.Controls.Add(this.tableLayoutPanelUserHeader, 0, 0);
            this.tableLayoutPanelLeft.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelLeft.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanelLeft.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanelLeft.Name = "tableLayoutPanelLeft";
            this.tableLayoutPanelLeft.Padding = new System.Windows.Forms.Padding(2, 2, 0, 0);
            this.tableLayoutPanelLeft.RowCount = 3;
            this.tableLayoutPanelLeft.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanelLeft.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanelLeft.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelLeft.Size = new System.Drawing.Size(150, 412);
            this.tableLayoutPanelLeft.TabIndex = 0;
            // 
            // lblContactsHeader
            // 
            this.lblContactsHeader.AutoSize = true;
            this.lblContactsHeader.BackColor = System.Drawing.Color.Empty;
            this.lblContactsHeader.BorderColor = System.Drawing.Color.Empty;
            this.lblContactsHeader.BorderStyle = LANMedia.CustomControls.LabelStyle.Standard;
            this.lblContactsHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblContactsHeader.Image = null;
            this.lblContactsHeader.Location = new System.Drawing.Point(2, 42);
            this.lblContactsHeader.Margin = new System.Windows.Forms.Padding(0);
            this.lblContactsHeader.Name = "lblContactsHeader";
            this.lblContactsHeader.Padding = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.lblContactsHeader.SelectedColor = System.Drawing.SystemColors.Highlight;
            this.lblContactsHeader.Size = new System.Drawing.Size(148, 20);
            this.lblContactsHeader.TabIndex = 0;
            this.lblContactsHeader.Text = "Online Now";
            this.lblContactsHeader.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tvUsers
            // 
            this.tvUsers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvUsers.DrawMode = System.Windows.Forms.TreeViewDrawMode.OwnerDrawAll;
            this.tvUsers.HeaderColor = System.Drawing.Color.Lavender;
            this.tvUsers.HeaderLevel = 1;
            this.tvUsers.HighlightColor = System.Drawing.Color.LemonChiffon;
            this.tvUsers.ImageIndex = 0;
            this.tvUsers.ImageList = this.imageListSmall;
            this.tvUsers.Indent = 8;
            this.tvUsers.Location = new System.Drawing.Point(2, 62);
            this.tvUsers.Margin = new System.Windows.Forms.Padding(0);
            this.tvUsers.Name = "tvUsers";
            this.tvUsers.SelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(247)))), ((int)(((byte)(170)))));
            this.tvUsers.SelectedImageIndex = 0;
            this.tvUsers.ShowLines = false;
            this.tvUsers.Size = new System.Drawing.Size(148, 350);
            this.tvUsers.TabIndex = 1;
            this.tvUsers.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.tvUsers_NodeMouseClick);
            this.tvUsers.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.tvUsers_NodeMouseDoubleClick);
            this.tvUsers.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tvUsers_KeyDown);
            // 
            // imageListSmall
            // 
            this.imageListSmall.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.imageListSmall.ImageSize = new System.Drawing.Size(20, 20);
            this.imageListSmall.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // tableLayoutPanelUserHeader
            // 
            this.tableLayoutPanelUserHeader.ColumnCount = 2;
            this.tableLayoutPanelUserHeader.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tableLayoutPanelUserHeader.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelUserHeader.Controls.Add(this.picBoxUserStatus, 0, 0);
            this.tableLayoutPanelUserHeader.Controls.Add(this.lblUserName, 1, 0);
            this.tableLayoutPanelUserHeader.Controls.Add(this.lblUserStatus, 1, 1);
            this.tableLayoutPanelUserHeader.DefaultColor = System.Drawing.SystemColors.Window;
            this.tableLayoutPanelUserHeader.Divider = false;
            this.tableLayoutPanelUserHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelUserHeader.HighlightColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(249)))), ((int)(((byte)(199)))));
            this.tableLayoutPanelUserHeader.HotTracking = false;
            this.tableLayoutPanelUserHeader.Location = new System.Drawing.Point(2, 2);
            this.tableLayoutPanelUserHeader.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanelUserHeader.Name = "tableLayoutPanelUserHeader";
            this.tableLayoutPanelUserHeader.RowCount = 3;
            this.tableLayoutPanelUserHeader.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelUserHeader.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanelUserHeader.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 2F));
            this.tableLayoutPanelUserHeader.Selected = true;
            this.tableLayoutPanelUserHeader.SelectedColor = System.Drawing.SystemColors.Control;
            this.tableLayoutPanelUserHeader.Size = new System.Drawing.Size(148, 40);
            this.tableLayoutPanelUserHeader.TabIndex = 2;
            // 
            // picBoxUserStatus
            // 
            this.picBoxUserStatus.BackColor = System.Drawing.Color.Transparent;
            this.picBoxUserStatus.Dock = System.Windows.Forms.DockStyle.Top;
            this.picBoxUserStatus.Location = new System.Drawing.Point(0, 0);
            this.picBoxUserStatus.Margin = new System.Windows.Forms.Padding(0);
            this.picBoxUserStatus.Name = "picBoxUserStatus";
            this.tableLayoutPanelUserHeader.SetRowSpan(this.picBoxUserStatus, 2);
            this.picBoxUserStatus.Size = new System.Drawing.Size(24, 24);
            this.picBoxUserStatus.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.picBoxUserStatus.TabIndex = 0;
            this.picBoxUserStatus.TabStop = false;
            // 
            // lblUserName
            // 
            this.lblUserName.AutoSize = true;
            this.lblUserName.BackColor = System.Drawing.Color.Transparent;
            this.lblUserName.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblUserName.Location = new System.Drawing.Point(24, 0);
            this.lblUserName.Margin = new System.Windows.Forms.Padding(0);
            this.lblUserName.Name = "lblUserName";
            this.lblUserName.Padding = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblUserName.Size = new System.Drawing.Size(4, 18);
            this.lblUserName.TabIndex = 1;
            this.lblUserName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblUserName.SystemColorsChanged += new System.EventHandler(this.label_SystemColorsChanged);
            // 
            // lblUserStatus
            // 
            this.lblUserStatus.AutoSize = true;
            this.lblUserStatus.BackColor = System.Drawing.Color.Transparent;
            this.lblUserStatus.BorderColor = System.Drawing.Color.Transparent;
            this.lblUserStatus.Image = null;
            this.lblUserStatus.Location = new System.Drawing.Point(24, 18);
            this.lblUserStatus.Margin = new System.Windows.Forms.Padding(0);
            this.lblUserStatus.Name = "lblUserStatus";
            this.lblUserStatus.Padding = new System.Windows.Forms.Padding(2);
            this.lblUserStatus.SelectedColor = System.Drawing.SystemColors.Highlight;
            this.lblUserStatus.Size = new System.Drawing.Size(0, 20);
            this.lblUserStatus.TabIndex = 2;
            this.lblUserStatus.SystemColorsChanged += new System.EventHandler(this.label_SystemColorsChanged);
            // 
            // tabControlChat
            // 
            this.tabControlChat.BorderColor = System.Drawing.SystemColors.ControlDark;
            this.tabControlChat.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlChat.HighlightMod = 0.08F;
            this.tabControlChat.ImageList = this.imageListSmall;
            this.tabControlChat.ItemSize = new System.Drawing.Size(120, 22);
            this.tabControlChat.Location = new System.Drawing.Point(0, 0);
            this.tabControlChat.Margin = new System.Windows.Forms.Padding(0);
            this.tabControlChat.Name = "tabControlChat";
            this.tabControlChat.Padding = new System.Drawing.Point(22, 3);
            this.tabControlChat.SelectedIndex = 0;
            this.tabControlChat.ShowToolTips = true;
            this.tabControlChat.Size = new System.Drawing.Size(426, 411);
            this.tabControlChat.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tabControlChat.TabIndex = 3;
            this.tabControlChat.Visible = false;
            this.tabControlChat.TabClosed += new LANMedia.CustomControls.TabControlEx.TabClosedEventHandler(this.tabControlChat_TabClosed);
            this.tabControlChat.Selected += new System.Windows.Forms.TabControlEventHandler(this.tabControlChat_Selected);
            // 
            // lblInfoText
            // 
            this.lblInfoText.BackColor = System.Drawing.SystemColors.Info;
            this.lblInfoText.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInfoText.ForeColor = System.Drawing.SystemColors.InfoText;
            this.lblInfoText.Location = new System.Drawing.Point(63, 103);
            this.lblInfoText.Name = "lblInfoText";
            this.lblInfoText.Padding = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.lblInfoText.Size = new System.Drawing.Size(296, 23);
            this.lblInfoText.TabIndex = 2;
            this.lblInfoText.Text = "Double click on a name to start chatting.";
            // 
            // lblInfo
            // 
            this.lblInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblInfo.BackColor = System.Drawing.SystemColors.Info;
            this.lblInfo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInfo.ForeColor = System.Drawing.SystemColors.InfoText;
            this.lblInfo.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblInfo.Location = new System.Drawing.Point(7, 58);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Padding = new System.Windows.Forms.Padding(6, 0, 0, 0);
            this.lblInfo.Size = new System.Drawing.Size(410, 110);
            this.lblInfo.TabIndex = 1;
            this.lblInfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // notifyIconSysTray
            // 
            this.notifyIconSysTray.BalloonTipTitle = "%TITLE%";
            this.notifyIconSysTray.Text = "%TITLE%";
            this.notifyIconSysTray.Visible = true;
            this.notifyIconSysTray.MouseClick += new System.Windows.Forms.MouseEventHandler(this.notifyIconSysTray_MouseClick);
            this.notifyIconSysTray.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIconSysTray_MouseDoubleClick);
            // 
            // timerUserUpdate
            // 
            this.timerUserUpdate.Enabled = true;
            this.timerUserUpdate.Interval = 5000;
            this.timerUserUpdate.Tick += new System.EventHandler(this.timerUserUpdate_Tick);
            // 
            // timerStatusUpdate
            // 
            this.timerStatusUpdate.Enabled = true;
            this.timerStatusUpdate.Interval = 1000;
            this.timerStatusUpdate.Tick += new System.EventHandler(this.timerStatusUpdate_Tick);
            // 
            // mnuNotify
            // 
            this.mnuNotify.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.mnuNotifyShow,
            this.mnuNotifySep1,
            this.mnuNotifyStatus,
            this.mnuNotifySilentMode,
            this.mnuNotifySep2,
            this.mnuNotifyHistory,
            this.mnuNotifyFileTransfers,
            this.menuNotifySep3,
            this.mnuNotifyOptions,
            this.mnuNotifyAbout,
            this.mnuNotifySep4,
            this.mnuNotifyExit});
            // 
            // mnuNotifyShow
            // 
            this.mnuNotifyShow.DefaultItem = true;
            this.mnuNotifyShow.Index = 0;
            this.mnuNotifyShow.Text = "Show %TITLE%";
            this.mnuNotifyShow.Click += new System.EventHandler(this.mnuNotifyShow_Click);
            // 
            // mnuNotifySep1
            // 
            this.mnuNotifySep1.Index = 1;
            this.mnuNotifySep1.Text = "-";
            // 
            // mnuNotifyStatus
            // 
            this.mnuNotifyStatus.Index = 2;
            this.mnuNotifyStatus.Text = "Change Status";
            // 
            // mnuNotifySilentMode
            // 
            this.mnuNotifySilentMode.Index = 3;
            this.mnuNotifySilentMode.Text = "Enable Silent Mode";
            this.mnuNotifySilentMode.Click += new System.EventHandler(this.mnuNotifySilentMode_Click);
            // 
            // mnuNotifySep2
            // 
            this.mnuNotifySep2.Index = 4;
            this.mnuNotifySep2.Text = "-";
            // 
            // mnuNotifyHistory
            // 
            this.mnuNotifyHistory.Index = 5;
            this.mnuNotifyHistory.Text = "History";
            this.mnuNotifyHistory.Click += new System.EventHandler(this.mnuNotifyHistory_Click);
            // 
            // mnuNotifyFileTransfers
            // 
            this.mnuNotifyFileTransfers.Index = 6;
            this.mnuNotifyFileTransfers.Text = "File Transfers";
            this.mnuNotifyFileTransfers.Click += new System.EventHandler(this.mnuNotifyFileTransfers_Click);
            // 
            // menuNotifySep3
            // 
            this.menuNotifySep3.Index = 7;
            this.menuNotifySep3.Text = "-";
            // 
            // mnuNotifyOptions
            // 
            this.mnuNotifyOptions.Index = 8;
            this.mnuNotifyOptions.Text = "Options...";
            this.mnuNotifyOptions.Click += new System.EventHandler(this.mnuNotifyOptions_Click);
            // 
            // mnuNotifyAbout
            // 
            this.mnuNotifyAbout.Index = 9;
            this.mnuNotifyAbout.Text = "About";
            this.mnuNotifyAbout.Click += new System.EventHandler(this.mnuNotifyAbout_Click);
            // 
            // mnuNotifySep4
            // 
            this.mnuNotifySep4.Index = 10;
            this.mnuNotifySep4.Text = "-";
            // 
            // mnuNotifyExit
            // 
            this.mnuNotifyExit.Index = 11;
            this.mnuNotifyExit.Text = "Exit";
            this.mnuNotifyExit.Click += new System.EventHandler(this.mnuNotifyExit_Click);
            // 
            // imageMenuEx
            // 
            this.imageMenuEx.ContainerControl = this;
            // 
            // mnuUser
            // 
            this.mnuUser.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.mnuUserConversation,
            this.mnuUserSendFile,
            this.mnuUserSep1,
            this.mnuUserBroadcast,
            this.mnuUserSep2,
            this.mnuUserInformation,
            this.mnuUserSep3,
            this.mnuUserMove});
            // 
            // mnuUserConversation
            // 
            this.mnuUserConversation.Index = 0;
            this.mnuUserConversation.Text = "Conversation";
            this.mnuUserConversation.Click += new System.EventHandler(this.mnuUserConversation_Click);
            // 
            // mnuUserSendFile
            // 
            this.mnuUserSendFile.Index = 1;
            this.mnuUserSendFile.Text = "Send File";
            this.mnuUserSendFile.Click += new System.EventHandler(this.mnuUserSendFile_Click);
            // 
            // mnuUserSep1
            // 
            this.mnuUserSep1.Index = 2;
            this.mnuUserSep1.Text = "-";
            // 
            // mnuUserBroadcast
            // 
            this.mnuUserBroadcast.Index = 3;
            this.mnuUserBroadcast.Text = "Send Broadcast...";
            this.mnuUserBroadcast.Click += new System.EventHandler(this.mnuUserBroadcast_Click);
            // 
            // mnuUserSep2
            // 
            this.mnuUserSep2.Index = 4;
            this.mnuUserSep2.Text = "-";
            // 
            // mnuUserInformation
            // 
            this.mnuUserInformation.Index = 5;
            this.mnuUserInformation.Text = "Information";
            this.mnuUserInformation.Click += new System.EventHandler(this.mnuUserInformation_Click);
            // 
            // mnuUserSep3
            // 
            this.mnuUserSep3.Index = 6;
            this.mnuUserSep3.Text = "-";
            // 
            // mnuUserMove
            // 
            this.mnuUserMove.Index = 7;
            this.mnuUserMove.Text = "Move This User to Group";
            // 
            // mnuGroup
            // 
            this.mnuGroup.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.mnuGroupAdd,
            this.mnuGroupRename,
            this.mnuGroupDelete});
            // 
            // mnuGroupAdd
            // 
            this.mnuGroupAdd.Index = 0;
            this.mnuGroupAdd.Text = "Add Group...";
            this.mnuGroupAdd.Click += new System.EventHandler(this.mnuGroupAdd_Click);
            // 
            // mnuGroupRename
            // 
            this.mnuGroupRename.Index = 1;
            this.mnuGroupRename.Text = "Rename...";
            this.mnuGroupRename.Click += new System.EventHandler(this.mnuGroupRename_Click);
            // 
            // mnuGroupDelete
            // 
            this.mnuGroupDelete.Index = 2;
            this.mnuGroupDelete.Text = "Delete Group";
            this.mnuGroupDelete.Click += new System.EventHandler(this.mnuGroupDelete_Click);
            // 
            // MainForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(584, 414);
            this.Controls.Add(this.tableLayoutPanelMain);
            this.MinimumSize = new System.Drawing.Size(600, 400);
            this.Name = "MainForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "%TITLE%";
            this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.Resize += new System.EventHandler(this.MainForm_Resize);
            this.tableLayoutPanelMain.ResumeLayout(false);
            this.splitContainerMain.Panel1.ResumeLayout(false);
            this.splitContainerMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerMain)).EndInit();
            this.splitContainerMain.ResumeLayout(false);
            this.tableLayoutPanelLeft.ResumeLayout(false);
            this.tableLayoutPanelUserHeader.ResumeLayout(false);
            this.tableLayoutPanelUserHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxUserStatus)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageMenuEx)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelMain;
        private System.Windows.Forms.SplitContainer splitContainerMain;
        private System.Windows.Forms.NotifyIcon notifyIconSysTray;
        private System.Windows.Forms.ImageList imageListSmall;
        private System.Windows.Forms.Timer timerUserUpdate;
        private System.Windows.Forms.Label lblInfo;
        private System.Windows.Forms.Label lblInfoText;
        private System.Windows.Forms.Timer timerStatusUpdate;
        private LANMedia.CustomControls.TabControlEx tabControlChat;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelLeft;
        private LANMedia.CustomControls.LabelEx lblContactsHeader;
        private LANMedia.CustomControls.TreeViewEx tvUsers;
        private System.Windows.Forms.ContextMenu mnuNotify;
        private System.Windows.Forms.MenuItem mnuNotifyShow;
        private System.Windows.Forms.MenuItem mnuNotifyExit;
        private LANMedia.CustomControls.ImageMenuEx imageMenuEx;
        private System.Windows.Forms.MenuItem mnuNotifySilentMode;
        private System.Windows.Forms.MenuItem mnuNotifySep2;
        private System.Windows.Forms.MenuItem mnuNotifyAbout;
        private System.Windows.Forms.MenuItem mnuNotifySep1;
        private System.Windows.Forms.MenuItem mnuNotifyOptions;
        private System.Windows.Forms.MenuItem mnuNotifyHistory;
        private System.Windows.Forms.MenuItem menuNotifySep3;
        private System.Windows.Forms.MenuItem mnuNotifyStatus;
        private System.Windows.Forms.MenuItem mnuNotifySep4;
        private System.Windows.Forms.MenuItem mnuNotifyFileTransfers;
        private System.Windows.Forms.ContextMenu mnuUser;
        private System.Windows.Forms.MenuItem mnuUserConversation;
        private System.Windows.Forms.MenuItem mnuUserSendFile;
        private System.Windows.Forms.MenuItem mnuUserSep1;
        private System.Windows.Forms.MenuItem mnuUserInformation;
        private System.Windows.Forms.MenuItem mnuUserSep3;
        private System.Windows.Forms.MenuItem mnuUserMove;
        private System.Windows.Forms.ContextMenu mnuGroup;
        private System.Windows.Forms.MenuItem mnuGroupAdd;
        private System.Windows.Forms.MenuItem mnuGroupDelete;
        private System.Windows.Forms.MenuItem mnuGroupRename;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.MenuItem mnuUserBroadcast;
        private System.Windows.Forms.MenuItem mnuUserSep2;
        private LANMedia.CustomControls.TableLayoutPanelEx tableLayoutPanelUserHeader;
        private System.Windows.Forms.PictureBox picBoxUserStatus;
        private System.Windows.Forms.Label lblUserName;
        private LANMedia.CustomControls.LabelEx lblUserStatus;
        private System.Windows.Forms.ContextMenu mnuStatus;
    }
}

