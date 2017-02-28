namespace LANChat
{
    partial class ChatControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.tableLayoutPanelMain = new System.Windows.Forms.TableLayoutPanel();
            this.rtbMessageHistory = new LANMedia.CustomControls.RichTextBoxEx();
            this.btnSendMessage = new System.Windows.Forms.Button();
            this.tableLayoutPanelMessage = new System.Windows.Forms.TableLayoutPanel();
            this.rtbMessage = new LANMedia.CustomControls.RichTextBoxEx();
            this.panelToolBar = new System.Windows.Forms.Panel();
            this.tbPop = new System.Windows.Forms.ToolBar();
            this.tbBtnPop = new System.Windows.Forms.ToolBarButton();
            this.imageListToolBar = new System.Windows.Forms.ImageList(this.components);
            this.tbChat = new System.Windows.Forms.ToolBar();
            this.tbBtnFont = new System.Windows.Forms.ToolBarButton();
            this.tbBtnSmiley = new System.Windows.Forms.ToolBarButton();
            this.tbBtnSep1 = new System.Windows.Forms.ToolBarButton();
            this.tbBtnSendFile = new System.Windows.Forms.ToolBarButton();
            this.tbBtnSep2 = new System.Windows.Forms.ToolBarButton();
            this.tbBtnSave = new System.Windows.Forms.ToolBarButton();
            this.lblStatus = new LANMedia.CustomControls.LabelEx();
            this.lblUserAction = new LANMedia.CustomControls.LabelEx();
            this.fontDialog = new System.Windows.Forms.FontDialog();
            this.mnuMessageBox = new System.Windows.Forms.ContextMenu();
            this.mnuMBUndo = new System.Windows.Forms.MenuItem();
            this.mnuMBRedo = new System.Windows.Forms.MenuItem();
            this.mnuMBSep2 = new System.Windows.Forms.MenuItem();
            this.mnuMBCut = new System.Windows.Forms.MenuItem();
            this.mnuMBCopy = new System.Windows.Forms.MenuItem();
            this.mnuMBPaste = new System.Windows.Forms.MenuItem();
            this.mnuMBDelete = new System.Windows.Forms.MenuItem();
            this.mnuMBSep3 = new System.Windows.Forms.MenuItem();
            this.mnuMBSelectAll = new System.Windows.Forms.MenuItem();
            this.mnuSmiley = new LANMedia.CustomControls.ContextMenuEx();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.imageMenuEx = new LANMedia.CustomControls.ImageMenuEx(this.components);
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.lblHistoryTopBar = new System.Windows.Forms.Label();
            this.lblHistoryLeftBar = new System.Windows.Forms.Label();
            this.lblHistoryRightBar = new System.Windows.Forms.Label();
            this.lblMessageTopBar = new System.Windows.Forms.Label();
            this.lblMessageLeftBar = new System.Windows.Forms.Label();
            this.lblMessageRightBar = new System.Windows.Forms.Label();
            this.tableLayoutPanelMain.SuspendLayout();
            this.tableLayoutPanelMessage.SuspendLayout();
            this.panelToolBar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imageMenuEx)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanelMain
            // 
            this.tableLayoutPanelMain.ColumnCount = 4;
            this.tableLayoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 1F));
            this.tableLayoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 88F));
            this.tableLayoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 1F));
            this.tableLayoutPanelMain.Controls.Add(this.lblMessageRightBar, 3, 4);
            this.tableLayoutPanelMain.Controls.Add(this.rtbMessageHistory, 1, 1);
            this.tableLayoutPanelMain.Controls.Add(this.btnSendMessage, 2, 4);
            this.tableLayoutPanelMain.Controls.Add(this.tableLayoutPanelMessage, 1, 5);
            this.tableLayoutPanelMain.Controls.Add(this.panelToolBar, 0, 3);
            this.tableLayoutPanelMain.Controls.Add(this.lblStatus, 0, 2);
            this.tableLayoutPanelMain.Controls.Add(this.lblUserAction, 0, 6);
            this.tableLayoutPanelMain.Controls.Add(this.lblHistoryTopBar, 0, 0);
            this.tableLayoutPanelMain.Controls.Add(this.lblHistoryLeftBar, 0, 1);
            this.tableLayoutPanelMain.Controls.Add(this.lblHistoryRightBar, 3, 1);
            this.tableLayoutPanelMain.Controls.Add(this.lblMessageTopBar, 0, 4);
            this.tableLayoutPanelMain.Controls.Add(this.lblMessageLeftBar, 0, 5);
            this.tableLayoutPanelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelMain.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanelMain.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanelMain.Name = "tableLayoutPanelMain";
            this.tableLayoutPanelMain.RowCount = 7;
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 1F));
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 1F));
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 65F));
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 19F));
            this.tableLayoutPanelMain.Size = new System.Drawing.Size(400, 279);
            this.tableLayoutPanelMain.TabIndex = 0;
            // 
            // rtbMessageHistory
            // 
            this.rtbMessageHistory.BackColor = System.Drawing.SystemColors.Window;
            this.rtbMessageHistory.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tableLayoutPanelMain.SetColumnSpan(this.rtbMessageHistory, 2);
            this.rtbMessageHistory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbMessageHistory.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtbMessageHistory.ForeColor = System.Drawing.SystemColors.WindowText;
            this.rtbMessageHistory.Location = new System.Drawing.Point(1, 1);
            this.rtbMessageHistory.Margin = new System.Windows.Forms.Padding(0);
            this.rtbMessageHistory.Name = "rtbMessageHistory";
            this.rtbMessageHistory.ReadOnly = true;
            this.rtbMessageHistory.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.rtbMessageHistory.Size = new System.Drawing.Size(398, 147);
            this.rtbMessageHistory.TabIndex = 2;
            this.rtbMessageHistory.TabStop = false;
            this.rtbMessageHistory.Text = "";
            this.rtbMessageHistory.KeyUp += new System.Windows.Forms.KeyEventHandler(this.rtbMessageHistory_KeyUp);
            this.rtbMessageHistory.MouseDown += new System.Windows.Forms.MouseEventHandler(this.rtbMessageHistory_MouseDown);
            this.rtbMessageHistory.Resize += new System.EventHandler(this.rtbMessageHistory_Resize);
            // 
            // btnSendMessage
            // 
            this.btnSendMessage.BackColor = System.Drawing.SystemColors.Control;
            this.btnSendMessage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnSendMessage.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSendMessage.Location = new System.Drawing.Point(314, 194);
            this.btnSendMessage.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.btnSendMessage.Name = "btnSendMessage";
            this.tableLayoutPanelMain.SetRowSpan(this.btnSendMessage, 2);
            this.btnSendMessage.Size = new System.Drawing.Size(85, 66);
            this.btnSendMessage.TabIndex = 1;
            this.btnSendMessage.Text = "&Send";
            this.btnSendMessage.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSendMessage.UseVisualStyleBackColor = true;
            this.btnSendMessage.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // tableLayoutPanelMessage
            // 
            this.tableLayoutPanelMessage.ColumnCount = 1;
            this.tableLayoutPanelMessage.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelMessage.Controls.Add(this.rtbMessage, 0, 0);
            this.tableLayoutPanelMessage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelMessage.Location = new System.Drawing.Point(1, 195);
            this.tableLayoutPanelMessage.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanelMessage.Name = "tableLayoutPanelMessage";
            this.tableLayoutPanelMessage.RowCount = 1;
            this.tableLayoutPanelMessage.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelMessage.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanelMessage.Size = new System.Drawing.Size(310, 65);
            this.tableLayoutPanelMessage.TabIndex = 0;
            // 
            // rtbMessage
            // 
            this.rtbMessage.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtbMessage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbMessage.Location = new System.Drawing.Point(0, 0);
            this.rtbMessage.Margin = new System.Windows.Forms.Padding(0);
            this.rtbMessage.Name = "rtbMessage";
            this.rtbMessage.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical;
            this.rtbMessage.Size = new System.Drawing.Size(310, 65);
            this.rtbMessage.TabIndex = 0;
            this.rtbMessage.Text = "";
            this.rtbMessage.KeyDown += new System.Windows.Forms.KeyEventHandler(this.rtbMessage_KeyDown);
            // 
            // panelToolBar
            // 
            this.panelToolBar.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanelMain.SetColumnSpan(this.panelToolBar, 4);
            this.panelToolBar.Controls.Add(this.tbPop);
            this.panelToolBar.Controls.Add(this.tbChat);
            this.panelToolBar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelToolBar.Location = new System.Drawing.Point(0, 168);
            this.panelToolBar.Margin = new System.Windows.Forms.Padding(0);
            this.panelToolBar.Name = "panelToolBar";
            this.panelToolBar.Padding = new System.Windows.Forms.Padding(2);
            this.panelToolBar.Size = new System.Drawing.Size(400, 26);
            this.panelToolBar.TabIndex = 5;
            // 
            // tbPop
            // 
            this.tbPop.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.tbPop.Appearance = System.Windows.Forms.ToolBarAppearance.Flat;
            this.tbPop.AutoSize = false;
            this.tbPop.Buttons.AddRange(new System.Windows.Forms.ToolBarButton[] {
            this.tbBtnPop});
            this.tbPop.Divider = false;
            this.tbPop.Dock = System.Windows.Forms.DockStyle.None;
            this.tbPop.DropDownArrows = true;
            this.tbPop.ImageList = this.imageListToolBar;
            this.tbPop.Location = new System.Drawing.Point(375, 2);
            this.tbPop.Margin = new System.Windows.Forms.Padding(0);
            this.tbPop.Name = "tbPop";
            this.tbPop.ShowToolTips = true;
            this.tbPop.Size = new System.Drawing.Size(24, 24);
            this.tbPop.TabIndex = 7;
            this.tbPop.TextAlign = System.Windows.Forms.ToolBarTextAlign.Right;
            this.tbPop.ButtonClick += new System.Windows.Forms.ToolBarButtonClickEventHandler(this.tbChat_ButtonClick);
            // 
            // tbBtnPop
            // 
            this.tbBtnPop.ImageKey = "(none)";
            this.tbBtnPop.Name = "tbBtnPop";
            this.tbBtnPop.ToolTipText = "Pop out";
            // 
            // imageListToolBar
            // 
            this.imageListToolBar.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.imageListToolBar.ImageSize = new System.Drawing.Size(16, 16);
            this.imageListToolBar.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // tbChat
            // 
            this.tbChat.Appearance = System.Windows.Forms.ToolBarAppearance.Flat;
            this.tbChat.AutoSize = false;
            this.tbChat.Buttons.AddRange(new System.Windows.Forms.ToolBarButton[] {
            this.tbBtnFont,
            this.tbBtnSmiley,
            this.tbBtnSep1,
            this.tbBtnSendFile,
            this.tbBtnSep2,
            this.tbBtnSave});
            this.tbChat.Divider = false;
            this.tbChat.Dock = System.Windows.Forms.DockStyle.None;
            this.tbChat.DropDownArrows = true;
            this.tbChat.ImageList = this.imageListToolBar;
            this.tbChat.Location = new System.Drawing.Point(2, 2);
            this.tbChat.Margin = new System.Windows.Forms.Padding(0);
            this.tbChat.Name = "tbChat";
            this.tbChat.ShowToolTips = true;
            this.tbChat.Size = new System.Drawing.Size(250, 24);
            this.tbChat.TabIndex = 6;
            this.tbChat.TextAlign = System.Windows.Forms.ToolBarTextAlign.Right;
            this.tbChat.Wrappable = false;
            this.tbChat.ButtonClick += new System.Windows.Forms.ToolBarButtonClickEventHandler(this.tbChat_ButtonClick);
            // 
            // tbBtnFont
            // 
            this.tbBtnFont.ImageKey = "(none)";
            this.tbBtnFont.Name = "tbBtnFont";
            this.tbBtnFont.Text = "Font";
            this.tbBtnFont.ToolTipText = "Select font";
            // 
            // tbBtnSmiley
            // 
            this.tbBtnSmiley.ImageKey = "(none)";
            this.tbBtnSmiley.Name = "tbBtnSmiley";
            this.tbBtnSmiley.ToolTipText = "Insert smiley";
            // 
            // tbBtnSep1
            // 
            this.tbBtnSep1.Name = "tbBtnSep1";
            this.tbBtnSep1.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // tbBtnSendFile
            // 
            this.tbBtnSendFile.ImageKey = "(none)";
            this.tbBtnSendFile.Name = "tbBtnSendFile";
            this.tbBtnSendFile.Text = "Send File";
            this.tbBtnSendFile.ToolTipText = "Send a file to this user";
            // 
            // tbBtnSep2
            // 
            this.tbBtnSep2.Name = "tbBtnSep2";
            this.tbBtnSep2.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // tbBtnSave
            // 
            this.tbBtnSave.Enabled = false;
            this.tbBtnSave.ImageKey = "(none)";
            this.tbBtnSave.Name = "tbBtnSave";
            this.tbBtnSave.Text = "Save As";
            this.tbBtnSave.ToolTipText = "Save the conversation";
            // 
            // lblStatus
            // 
            this.lblStatus.BackColor = System.Drawing.SystemColors.Window;
            this.lblStatus.BorderStyle = LANMedia.CustomControls.LabelStyle.Standard;
            this.tableLayoutPanelMain.SetColumnSpan(this.lblStatus, 4);
            this.lblStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblStatus.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatus.Image = null;
            this.lblStatus.Location = new System.Drawing.Point(0, 148);
            this.lblStatus.Margin = new System.Windows.Forms.Padding(0);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.SelectedColor = System.Drawing.SystemColors.Highlight;
            this.lblStatus.Size = new System.Drawing.Size(400, 20);
            this.lblStatus.TabIndex = 6;
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblUserAction
            // 
            this.lblUserAction.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblUserAction.BackColor = System.Drawing.Color.Transparent;
            this.lblUserAction.BorderStyle = LANMedia.CustomControls.LabelStyle.RoundedBottom;
            this.tableLayoutPanelMain.SetColumnSpan(this.lblUserAction, 4);
            this.lblUserAction.Image = null;
            this.lblUserAction.Location = new System.Drawing.Point(0, 260);
            this.lblUserAction.Margin = new System.Windows.Forms.Padding(0);
            this.lblUserAction.Name = "lblUserAction";
            this.lblUserAction.Padding = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.lblUserAction.SelectedColor = System.Drawing.SystemColors.Highlight;
            this.lblUserAction.Size = new System.Drawing.Size(400, 19);
            this.lblUserAction.TabIndex = 7;
            this.lblUserAction.Text = "Press Enter to send your message.";
            this.lblUserAction.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // fontDialog
            // 
            this.fontDialog.AllowScriptChange = false;
            this.fontDialog.AllowVerticalFonts = false;
            this.fontDialog.ScriptsOnly = true;
            this.fontDialog.ShowColor = true;
            // 
            // mnuMessageBox
            // 
            this.mnuMessageBox.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.mnuMBUndo,
            this.mnuMBRedo,
            this.mnuMBSep2,
            this.mnuMBCut,
            this.mnuMBCopy,
            this.mnuMBPaste,
            this.mnuMBDelete,
            this.mnuMBSep3,
            this.mnuMBSelectAll});
            this.mnuMessageBox.Popup += new System.EventHandler(this.mnuMessageBox_Popup);
            // 
            // mnuMBUndo
            // 
            this.mnuMBUndo.Index = 0;
            this.mnuMBUndo.Shortcut = System.Windows.Forms.Shortcut.CtrlZ;
            this.mnuMBUndo.Text = "&Undo";
            this.mnuMBUndo.Click += new System.EventHandler(this.mnuMBUndo_Click);
            // 
            // mnuMBRedo
            // 
            this.mnuMBRedo.Index = 1;
            this.mnuMBRedo.Text = "Redo";
            this.mnuMBRedo.Click += new System.EventHandler(this.mnuMBRedo_Click);
            // 
            // mnuMBSep2
            // 
            this.mnuMBSep2.Index = 2;
            this.mnuMBSep2.Text = "-";
            // 
            // mnuMBCut
            // 
            this.mnuMBCut.Index = 3;
            this.mnuMBCut.Shortcut = System.Windows.Forms.Shortcut.CtrlX;
            this.mnuMBCut.Text = "Cu&t";
            this.mnuMBCut.Click += new System.EventHandler(this.mnuMBCut_Click);
            // 
            // mnuMBCopy
            // 
            this.mnuMBCopy.Index = 4;
            this.mnuMBCopy.Shortcut = System.Windows.Forms.Shortcut.CtrlC;
            this.mnuMBCopy.Text = "&Copy";
            this.mnuMBCopy.Click += new System.EventHandler(this.mnuMBCopy_Click);
            // 
            // mnuMBPaste
            // 
            this.mnuMBPaste.Index = 5;
            this.mnuMBPaste.Shortcut = System.Windows.Forms.Shortcut.CtrlV;
            this.mnuMBPaste.Text = "&Paste";
            this.mnuMBPaste.Click += new System.EventHandler(this.mnuMBPaste_Click);
            // 
            // mnuMBDelete
            // 
            this.mnuMBDelete.Index = 6;
            this.mnuMBDelete.Text = "&Delete";
            this.mnuMBDelete.Click += new System.EventHandler(this.mnuMBDelete_Click);
            // 
            // mnuMBSep3
            // 
            this.mnuMBSep3.Index = 7;
            this.mnuMBSep3.Text = "-";
            // 
            // mnuMBSelectAll
            // 
            this.mnuMBSelectAll.Index = 8;
            this.mnuMBSelectAll.Shortcut = System.Windows.Forms.Shortcut.CtrlA;
            this.mnuMBSelectAll.Text = "Select &All";
            this.mnuMBSelectAll.Click += new System.EventHandler(this.mnuMBSelectAll_Click);
            // 
            // saveFileDialog
            // 
            this.saveFileDialog.DefaultExt = "rtf";
            this.saveFileDialog.Filter = "Rich Text Format (RTF)|*.rtf| All files|*.*";
            // 
            // imageMenuEx
            // 
            this.imageMenuEx.ContainerControl = this;
            // 
            // timer
            // 
            this.timer.Interval = 5000;
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // lblHistoryTopBar
            // 
            this.lblHistoryTopBar.AutoSize = true;
            this.lblHistoryTopBar.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.tableLayoutPanelMain.SetColumnSpan(this.lblHistoryTopBar, 4);
            this.lblHistoryTopBar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblHistoryTopBar.Location = new System.Drawing.Point(0, 0);
            this.lblHistoryTopBar.Margin = new System.Windows.Forms.Padding(0);
            this.lblHistoryTopBar.Name = "lblHistoryTopBar";
            this.lblHistoryTopBar.Size = new System.Drawing.Size(400, 1);
            this.lblHistoryTopBar.TabIndex = 8;
            // 
            // lblHistoryLeftBar
            // 
            this.lblHistoryLeftBar.AutoSize = true;
            this.lblHistoryLeftBar.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.lblHistoryLeftBar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblHistoryLeftBar.Location = new System.Drawing.Point(0, 1);
            this.lblHistoryLeftBar.Margin = new System.Windows.Forms.Padding(0);
            this.lblHistoryLeftBar.Name = "lblHistoryLeftBar";
            this.lblHistoryLeftBar.Size = new System.Drawing.Size(1, 147);
            this.lblHistoryLeftBar.TabIndex = 9;
            // 
            // lblHistoryRightBar
            // 
            this.lblHistoryRightBar.AutoSize = true;
            this.lblHistoryRightBar.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.lblHistoryRightBar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblHistoryRightBar.Location = new System.Drawing.Point(399, 1);
            this.lblHistoryRightBar.Margin = new System.Windows.Forms.Padding(0);
            this.lblHistoryRightBar.Name = "lblHistoryRightBar";
            this.lblHistoryRightBar.Size = new System.Drawing.Size(1, 147);
            this.lblHistoryRightBar.TabIndex = 10;
            // 
            // lblMessageTopBar
            // 
            this.lblMessageTopBar.AutoSize = true;
            this.lblMessageTopBar.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.tableLayoutPanelMain.SetColumnSpan(this.lblMessageTopBar, 2);
            this.lblMessageTopBar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblMessageTopBar.Location = new System.Drawing.Point(0, 194);
            this.lblMessageTopBar.Margin = new System.Windows.Forms.Padding(0);
            this.lblMessageTopBar.Name = "lblMessageTopBar";
            this.lblMessageTopBar.Size = new System.Drawing.Size(311, 1);
            this.lblMessageTopBar.TabIndex = 11;
            // 
            // lblMessageLeftBar
            // 
            this.lblMessageLeftBar.AutoSize = true;
            this.lblMessageLeftBar.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.lblMessageLeftBar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblMessageLeftBar.Location = new System.Drawing.Point(0, 195);
            this.lblMessageLeftBar.Margin = new System.Windows.Forms.Padding(0);
            this.lblMessageLeftBar.Name = "lblMessageLeftBar";
            this.lblMessageLeftBar.Size = new System.Drawing.Size(1, 65);
            this.lblMessageLeftBar.TabIndex = 12;
            // 
            // lblMessageRightBar
            // 
            this.lblMessageRightBar.AutoSize = true;
            this.lblMessageRightBar.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.lblMessageRightBar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblMessageRightBar.Location = new System.Drawing.Point(399, 194);
            this.lblMessageRightBar.Margin = new System.Windows.Forms.Padding(0);
            this.lblMessageRightBar.Name = "lblMessageRightBar";
            this.tableLayoutPanelMain.SetRowSpan(this.lblMessageRightBar, 2);
            this.lblMessageRightBar.Size = new System.Drawing.Size(1, 66);
            this.lblMessageRightBar.TabIndex = 14;
            // 
            // ChatControl
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.tableLayoutPanelMain);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "ChatControl";
            this.Size = new System.Drawing.Size(400, 279);
            this.Load += new System.EventHandler(this.ChatControl_Load);
            this.tableLayoutPanelMain.ResumeLayout(false);
            this.tableLayoutPanelMain.PerformLayout();
            this.tableLayoutPanelMessage.ResumeLayout(false);
            this.panelToolBar.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.imageMenuEx)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelMain;
        private LANMedia.CustomControls.RichTextBoxEx rtbMessageHistory;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelMessage;
        private System.Windows.Forms.FontDialog fontDialog;
        private LANMedia.CustomControls.RichTextBoxEx rtbMessage;
        private System.Windows.Forms.ContextMenu mnuMessageBox;
        private System.Windows.Forms.MenuItem mnuMBUndo;
        private System.Windows.Forms.MenuItem mnuMBSep2;
        private System.Windows.Forms.MenuItem mnuMBCut;
        private System.Windows.Forms.MenuItem mnuMBCopy;
        private System.Windows.Forms.MenuItem mnuMBPaste;
        private System.Windows.Forms.MenuItem mnuMBSep3;
        private System.Windows.Forms.MenuItem mnuMBSelectAll;
        private System.Windows.Forms.MenuItem mnuMBDelete;
        private LANMedia.CustomControls.ImageMenuEx imageMenuEx;
        private System.Windows.Forms.Button btnSendMessage;
        private System.Windows.Forms.ImageList imageListToolBar;
        private System.Windows.Forms.Panel panelToolBar;
        private System.Windows.Forms.ToolBar tbChat;
        private System.Windows.Forms.ToolBarButton tbBtnFont;
        private System.Windows.Forms.ToolBarButton tbBtnSmiley;
        private System.Windows.Forms.ToolBarButton tbBtnSep1;
        private System.Windows.Forms.ToolBarButton tbBtnSave;
        private LANMedia.CustomControls.ContextMenuEx mnuSmiley;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.ToolBarButton tbBtnSendFile;
        private System.Windows.Forms.ToolBarButton tbBtnSep2;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.ToolBar tbPop;
        private System.Windows.Forms.ToolBarButton tbBtnPop;
        private System.Windows.Forms.MenuItem mnuMBRedo;
        private LANMedia.CustomControls.LabelEx lblStatus;
        private LANMedia.CustomControls.LabelEx lblUserAction;
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.Label lblHistoryTopBar;
        private System.Windows.Forms.Label lblHistoryLeftBar;
        private System.Windows.Forms.Label lblHistoryRightBar;
        private System.Windows.Forms.Label lblMessageTopBar;
        private System.Windows.Forms.Label lblMessageLeftBar;
        private System.Windows.Forms.Label lblMessageRightBar;
    }
}
