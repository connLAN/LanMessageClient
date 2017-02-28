using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Reflection;
using System.IO;
using LANMedia.CustomControls;

namespace LANChat
{
    /// <summary>
    /// All the UI related functions (other than event handlers) are defined here.
    /// </summary>
    partial class MainForm
    {
        /// <summary>
        /// Restores the window to its normal state.
        /// </summary>
        /// <param name="bRestore">If true, restores window to previous state, else window is minimized.</param>
        public override void ShowWindow(bool bRestore, bool bActivated)
        {
            base.ShowWindow(bRestore, bActivated);

            //  If this is the first time window is shown to user.
            if (bFirstTime) {
                bFirstTime = false;
                tvUsers.SelectedNode = null;
                tvUsers.ExpandAll();
            }
        }

        /// <summary>
        /// Hides the window and show a notification.
        /// </summary>
        public override void HideWindow()
        {
            base.HideWindow();

            if (!bBkgndMessageShown) {
                string appName = AppInfo.Title;
                DisplayBalloonMessage(appName,
                    appName + " is still running in the background.\nYou can open it by double clicking this icon.");
                bBkgndMessageShown = true;
                Properties.Settings.Default.ShowMinimizeMessage = !bBkgndMessageShown;
            }
        }

        /// <summary>
        /// This method flashes the window 3 times or until the window receives focus.
        /// </summary>
        public override void FlashWindow()
        {
            base.FlashWindow();
        }

        /// <summary>
        /// Set the images and properties for various UI elements.
        /// </summary>
        private void InitUI()
        {
            this.Icon = Helper.GetAssociatedIcon(Application.ExecutablePath);
            this.ClientSize = new System.Drawing.Size(645, 480);

            notifyIconSysTray.Icon = this.Icon;

            this.Font = Helper.SystemFont;
            lblUserName.Font = new System.Drawing.Font(Helper.SystemFont, FontStyle.Bold);
            lblUserName.Text = localUserName;
            lblUserStatus.Font = Helper.SystemFont;
            lblUserStatus.Text = UserStatus.GetStatusInfo(localStatus).Description;

            lblContactsHeader.Font = new Font(Helper.SystemFont, FontStyle.Bold);
            tvUsers.Font = Helper.SystemFont;
            tabControlChat.Font = Helper.SystemFont;

            //  Label images.
            lblInfo.Image = (Image)LANChat.Resources.Properties.Resources.ResourceManager.GetObject("lblInfo");

            //  Create a popup menu that contains all the statuses.
            foreach (StatusInfo statusInfo in UserStatus.StatusList) {
                if (!statusInfo.Selectable)
                    continue;
                MenuItem item = new MenuItem(statusInfo.Description, mnuStatus_Click);
                item.RadioCheck = true;
                item.Tag = statusInfo.Code;
                mnuStatus.MenuItems.Add(item);
            }

            //  Add the popup menu as a submenu to the system tray menu.
            Win32.ModifyMenu(mnuNotify.Handle, 2, Win32.MF_BYPOSITION | Win32.MF_POPUP, mnuStatus.Handle, "Change Status");
            //  Add the same popup menu as the context menu of status label in presence area.
            lblUserStatus.ContextMenu = mnuStatus;

            //  Notify menu
            imageMenuEx.SetImage(mnuNotifyShow, (Image)LANChat.Resources.Properties.Resources.ResourceManager.GetObject("mnuNotifyShow"));
            imageMenuEx.SetImage(mnuNotifyHistory, (Image)LANChat.Resources.Properties.Resources.ResourceManager.GetObject("mnuNotifyHistory"));
            imageMenuEx.SetImage(mnuNotifyFileTransfers, (Image)LANChat.Resources.Properties.Resources.ResourceManager.GetObject("mnuNotifyFileTransfers"));
            imageMenuEx.SetImage(mnuNotifyOptions, (Image)LANChat.Resources.Properties.Resources.ResourceManager.GetObject("mnuNotifyOptions"));
            imageMenuEx.SetImage(mnuNotifyAbout, (Image)LANChat.Resources.Properties.Resources.ResourceManager.GetObject("mnuNotifyAbout"));
            imageMenuEx.SetImage(mnuNotifyExit, (Image)LANChat.Resources.Properties.Resources.ResourceManager.GetObject("mnuNotifyExit"));

            //  Image list.
            imageListSmall.ImageSize = new Size(20, 20);
            imageListSmall.Images.Add("Blank", (Image)LANChat.Resources.Properties.Resources.ResourceManager.GetObject("il_Blank"));
            //imageListSmall.Images.Add("Online_Blue", (Image)LANChat.Resources.Properties.Resources.ResourceManager.GetObject("il_Online_Blue"));
            //imageListSmall.Images.Add("Online_Red", (Image)LANChat.Resources.Properties.Resources.ResourceManager.GetObject("il_Online_Red"));
            //imageListSmall.Images.Add("Online_Yellow", (Image)LANChat.Resources.Properties.Resources.ResourceManager.GetObject("il_Online_Yellow"));
            //imageListSmall.Images.Add("Offline_Gray", (Image)LANChat.Resources.Properties.Resources.ResourceManager.GetObject("il_Offline_Gray"));
            //imageListSmall.Images.Add("Bubble_Blue", (Image)LANChat.Resources.Properties.Resources.ResourceManager.GetObject("il_Bubble_Blue"));
            //imageListSmall.Images.Add("Bubble_Red", (Image)LANChat.Resources.Properties.Resources.ResourceManager.GetObject("il_Bubble_Red"));
            //imageListSmall.Images.Add("Bubble_Yellow", (Image)LANChat.Resources.Properties.Resources.ResourceManager.GetObject("il_Bubble_Yellow"));
            //imageListSmall.Images.Add("Bubble_Gray", (Image)LANChat.Resources.Properties.Resources.ResourceManager.GetObject("il_Bubble_Gray"));
            foreach (StatusInfo statusInfo in UserStatus.StatusList) {
                imageListSmall.Images.Add(statusInfo.StatusImageKey, 
                    (Image)LANChat.Resources.Properties.Resources.ResourceManager.GetObject(statusInfo.StatusImageKey));
                imageListSmall.Images.Add(statusInfo.ChatImageKey, 
                    (Image)LANChat.Resources.Properties.Resources.ResourceManager.GetObject(statusInfo.ChatImageKey));
            }

            SetControlCaptions(this);
            SetMenuCaptions(mnuNotify);
            ShowNetworkStatus(bNetworkAvailable);
        }

        /// <summary>
        /// Show a balloon message in the notification area.
        /// </summary>
        /// <param name="caption">Caption of message.</param>
        /// <param name="text">Message text.</param>
        private void DisplayBalloonMessage(string caption, string text)
        {
            notifyIconSysTray.ShowBalloonTip(1000, caption, text, ToolTipIcon.Info);
        }

        private void DisplayAlert(string caption, string text)
        {
            //  Do not display alerts when busy and suspend alerts on busy is selected.
            if (!bDisplayAlerts || (localStatus == "Busy" && bSuspendAlertsOnBusy)
                || (localStatus == "DoNotDisturb" && bSuspendAlertsOnDND))
                return;
            
            DisplayBalloonMessage(caption, text);
        }

        private void PlaySound(NotifyEvents notifyEvent)
        {
            //  Do not play sounds when busy and suspend sounds on busy is selected.
            if (!bPlaySounds || (localStatus == "Busy" && bSuspendSoundsOnBusy)
                || (localStatus == "DoNotDisturb" && bSuspendSoundsOnDND))
                return;
            
            try {
                Stream waveStream = null;
                switch (notifyEvent) {
                    case NotifyEvents.Online:
                        waveStream = LANChat.Resources.Properties.Resources.ResourceManager.GetStream("online");
                        break;
                    case NotifyEvents.NewMessage:
                        waveStream = LANChat.Resources.Properties.Resources.ResourceManager.GetStream("newmessage");
                        break;
                    case NotifyEvents.NewFile:
                        waveStream = LANChat.Resources.Properties.Resources.ResourceManager.GetStream("newalert");
                        break;
                    case NotifyEvents.FileComplete:
                        waveStream = LANChat.Resources.Properties.Resources.ResourceManager.GetStream("done");
                        break;
                }

                SoundPlayer.PlaySound(waveStream);
            }
            catch {
            }
        }

        /// <summary>
        /// Displays the information about a user.
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="messageText"></param>
        private void DisplayUserInfo(string userName, string messageText)
        {
            //  The message box is shown using a new thread so it won't hold up the rest of the program.
            string info = this.Handle.ToString() + Environment.NewLine + userName + Environment.NewLine + messageText;
            System.Threading.Thread thread = new System.Threading.Thread(
                new System.Threading.ParameterizedThreadStart(DisplayUserInfo));
            thread.Start(info);
            this.Cursor = Cursors.Default;
        }

        private void DisplayUserInfo(object data)
        {
            string info = (string)data;
            string[] userInfo = info.Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            IntPtr hWnd = new IntPtr(long.Parse(userInfo[0]));
            string text = "Logon Name:\t" + userInfo[2] + "\n"
                + "Computer:\t" + userInfo[3] + "\n"
                + "IP Address:\t" + userInfo[4] + "\n"
                + "Operating System:\t" + userInfo[5] + "\n"
                + "Messenger Version:\t" + userInfo[6];
            MessageBox.Show(new Win32Window(hWnd), text, "User Info: " + userInfo[1], MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// Displays an incoming broadcast message.
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="messageText"></param>
        private void DisplayBroadcastMessage(string userName, string messageText)
        {
            BroadcastViewer broadcastDialog = new BroadcastViewer();
            broadcastDialog.Show(userName, messageText);
        }

        /// <summary>
        /// Updates an existing conversation with a new incoming message.
        /// </summary>
        /// <param name="key">Unique key of the sender.</param>
        /// <param name="messageType">Type of the message.</param>
        /// <param name="timeStamp">Time stamp of the message.</param>
        /// <param name="userName">Name of the sender.</param>
        /// <param name="messageText">The message text.</param>
        private void UpdateConversation(string key, MessageTypes messageType, string timeStamp, string userName, string messageText)
        {
            //  Start a new conversation if a conversation with this user does not exist.
            switch (messageType) {
                case MessageTypes.Message:
                    StartConversation(key, userName, false);
                    PlaySound(NotifyEvents.NewMessage);
                    break;
                case MessageTypes.Online:
                    if (this.WindowState == FormWindowState.Minimized && bShowNotifications) {
                        if (key.Equals(localUserKey))
                            DisplayAlert(AppInfo.Title, "You are online.");
                        else
                            DisplayAlert(AppInfo.Title, userName + " is online.");
                    }
                    if (!key.Equals(localUserKey))
                        PlaySound(NotifyEvents.Online);
                    break;
                case MessageTypes.Offline:
                    if (this.WindowState == FormWindowState.Minimized && bShowNotifications) {
                        if (key.Equals(localUserKey))
                            DisplayAlert(AppInfo.Title, "You are offline.");
                        else
                            DisplayAlert(AppInfo.Title, userName + " is offline.");
                    }
                    break;
            }

            if (tabList.ContainsKey(key)) {
                TabPageEx tabPage = tabList[key];
                ChatControl chatControl = (ChatControl)tabPage.Controls["ChatControl"];
                chatControl.ReceiveMessage(messageType, timeStamp, userName, messageText);
            }
            else if (windowList.ContainsKey(key)) {
                ChatForm chatForm = windowList[key];
                ChatControl chatControl = (ChatControl)chatForm.Controls["ChatControl"];
                chatControl.ReceiveMessage(messageType, timeStamp, userName, messageText);
            }
        }

        /// <summary>
        /// Adds a new tab page to the Tab Control and specifies the text and
        /// key for the tab. If tab already exists, bring it to top.
        /// </summary>
        /// <param name="text"></param>
        private void StartConversation(string key, string text, bool activated)
        {
            bool tabSelected = false;
            //  Check if tab with given key exists.
            if (tabControlChat.TabPages.ContainsKey(key)) {
                //  Tab found, make it selected.
                TabPageEx selectedTabPage = (TabPageEx)tabControlChat.TabPages[key];
                tabControlChat.SelectedTab = selectedTabPage;
                tabSelected = true;
            }
            else if (tabList.ContainsKey(key)) {
                TabPageEx tabPage = tabList[key];
                tabControlChat.TabPages.Add(tabPage);
                tabControlChat.SelectedTab = tabPage;
                tabSelected = true;
            }
            else if (windowList.ContainsKey(key)) {
                if (activated) {
                    ChatForm chatForm = windowList[key];
                    chatForm.ShowWindow(true, true);
                }
            }
            else {
                TabPageEx newTabPage = null;
                ChatForm newChatForm = null;
                ChatControl newChatControl = null;
                try {
                    //  Create a Chat control and set its properties.
                    newChatControl = new ChatControl();
                    newChatControl.Name = "ChatControl";
                    newChatControl.Dock = DockStyle.Fill;
                    newChatControl.LocalUserName = localUserName;
                    newChatControl.LocalAddress = localAddress.ToString();

                    User remoteUser = GetUser(key);
                    newChatControl.Key = key;
                    newChatControl.RemoteUserName = remoteUser.Name;
                    newChatControl.RemoteAddress = remoteUser.Address;
                    newChatControl.MessageFont = defaultFont;
                    newChatControl.MessageFontColor = defaultFontColor;
                    newChatControl.HotKeyMod = Properties.Settings.Default.MessageHotKeyMod;
                    newChatControl.MessageToForeground = bMessageToForeground;
                    newChatControl.SilentMode = bSilentMode;
                    newChatControl.ShowEmoticons = Properties.Settings.Default.ShowEmoticons;
                    newChatControl.EmotTextToImage = Properties.Settings.Default.EmotTextToImage;
                    newChatControl.ShowTimeStamp = Properties.Settings.Default.ShowTimeStamp;
                    newChatControl.AddDateToTimeStamp = Properties.Settings.Default.AddDateToTimeStamp;
                    newChatControl.Windowed = bChatWindowed;
                    newChatControl.Sending += new ChatControl.SendEventHandler(ChatControl_Sending);
                    newChatControl.WindowModeChange += new ChatControl.WindowModeChangeEventHandler(ChatControl_WindowModeChange);

                    if (Properties.Settings.Default.ChatWindowed == true) {
                        newChatForm = CreateChatForm(key, text, activated);
                        newChatForm.Controls.Add(newChatControl);
                    }
                    else {
                        //  Create a new tab page and add the chat control to it.
                        newTabPage = CreateChatTab(key, text);
                        newTabPage.Controls.Add(newChatControl);
                        tabSelected = true;
                    }
                    //  Display status message.
                    newChatControl.ReceiveMessage(MessageTypes.Status, string.Empty, remoteUser.Name, remoteUser.Status);
                    //  Display a message if remote user's version is older.
                    if (IsVersionOlder(remoteUser.Version, localClientVersion))
                        newChatControl.ReceiveMessage(MessageTypes.OldVersion, string.Empty, remoteUser.Name, string.Empty);
                }
                catch {
                    if (newChatControl != null)
                        newChatControl.Dispose();
                    if (newTabPage != null)
                        newTabPage.Dispose();
                    if (newChatForm != null)
                        newChatForm.Dispose();
                    return;
                }
            }
            tabControlChat.Visible = (tabControlChat.TabCount > 0);
            //  Set keyboard focus to the message box inside the chat control.
            //  This is to be done after tab control becomes visible, else focus will not work.
            if (tabSelected && tabControlChat.SelectedIndex >= 0) {
                TabPageEx tabPage = (TabPageEx)tabControlChat.SelectedTab;
                ChatControl chatControl = (ChatControl)tabPage.Controls["ChatControl"];
                chatControl.SetFocus();
            }
        }

        /// <summary>
        /// Creates a chat tab and adds it to the tab control.
        /// </summary>
        /// <param name="key">Unique key of user.</param>
        /// <param name="text">Title of tab.</param>
        /// <returns></returns>
        private TabPageEx CreateChatTab(string key, string text)
        {
            return CreateChatTab(key, text, true);
        }

        /// <summary>
        /// Creates a chat tab and adds it to the tab control based on the parameter value.
        /// </summary>
        /// <param name="key">Unique key of user.</param>
        /// <param name="text">Title of tab.</param>
        /// <param name="show">Whether to show tab or not.</param>
        /// <returns></returns>
        private TabPageEx CreateChatTab(string key, string text, bool show)
        {
            return CreateChatTab(key, text, show, 0);
        }

        /// <summary>
        /// Creates a chat tab and adds it to the tab control based on the parameter value.
        /// </summary>
        /// <param name="key">Unique key of user.</param>
        /// <param name="text">Title of tab.</param>
        /// <param name="show">Whether to show tab or not.</param>
        /// <param name="retries">Number of retry.</param>
        /// <returns></returns>
        private TabPageEx CreateChatTab(string key, string text, bool show, int retries)
        {
            TabPageEx newTabPage = null;
            try {
                newTabPage = new TabPageEx(text);
                newTabPage.Name = key;
                newTabPage.ImageKey = "Blank";
                newTabPage.ToolTipText = text;
                SetTabLook(newTabPage);

                //  Add tab page to the tab list.
                tabList.Add(newTabPage);
                //  Set correct status images.
                UpdateUserStatusImages(key);
                //  Set correct tab look.
                UpdateTabWindowStatusLook(key);

                if (show) {
                    //  Add tab page to tab control.
                    tabControlChat.TabPages.Add(newTabPage);
                    // Make the new tab the selected tab.
                    tabControlChat.SelectedTab = newTabPage;
                }

                return newTabPage;
            }
            catch {
                if (newTabPage != null) {
                    if (tabList.Contains(newTabPage))
                        tabList.Remove(newTabPage);
                    newTabPage.Dispose();
                }
                //  If this is first attempt to create tab, try again.
                //  Else give up and return null.
                if (retries == 0)
                    return CreateChatTab(key, text, show, ++retries);
                else
                    return null;
            }
        }

        /// <summary>
        /// Creates a chat window and displays it.
        /// </summary>
        /// <param name="key">Unique key of user.</param>
        /// <param name="text">Title of window.</param>
        /// <param name="activated">Whether to restore window or not.</param>
        /// <returns></returns>
        private ChatForm CreateChatForm(string key, string text, bool activated)
        {
            return CreateChatForm(key, text, activated, true);
        }

        /// <summary>
        /// Creates a chat window and displays it based on the parameter value.
        /// </summary>
        /// <param name="key">Unique key of user.</param>
        /// <param name="text">Title of window.</param>
        /// <param name="activated">Whether to restore window or not.</param>
        /// <param name="show">Whether to bring window to foreground or not.</param>
        /// <returns></returns>
        private ChatForm CreateChatForm(string key, string text, bool activated, bool show)
        {
            ChatForm newChatForm = null;
            try {
                newChatForm = new ChatForm();
                newChatForm.Name = key;
                newChatForm.Text = text;

                //  Add window to window list.
                windowList.Add(newChatForm);
                //  Set correct window look.
                UpdateTabWindowStatusLook(key);

                //  Show the window if "show" is true.
                if (show) {
                    if (activated)
                        newChatForm.ShowWindow(true, true);
                    else
                        newChatForm.ShowWindow(bMessageToForeground, false);
                }

                return newChatForm;
            }
            catch {
                if (newChatForm != null) {
                    if (windowList.Contains(newChatForm))
                        windowList.Remove(newChatForm);
                    newChatForm.Dispose();
                }
                return null;
            }
        }

        /// <summary>
        /// This method is called when the user changes tab option in the Options dialog.
        /// All the chat tabs and windows that are currently in the closed state are
        /// switched to the new mode. This is the behaviour a user would expect.
        /// </summary>
        private void SwitchWindowMode(bool value)
        {
            //  This is a rather expensive operation, no need to do it if value
            //  is the same as bChatWindowed.
            if (bChatWindowed == value)
                return;

            bChatWindowed = value;
            if (bChatWindowed) {
                List<TabPageEx> remList = new List<TabPageEx>();
                foreach (TabPageEx tabPage in tabList) {
                    if (!tabControlChat.TabPages.ContainsKey(tabPage.Name)) {
                        ChatControl chatControl = (ChatControl)tabPage.Controls["ChatControl"];
                        chatControl.Parent = null;
                        string key = chatControl.Key;
                        string text = tabPage.Text;
                        ChatForm newChatForm = CreateChatForm(key, text, false, false);
                        newChatForm.Enabled = chatControl.Enabled;
                        newChatForm.Controls.Add(chatControl);
                        chatControl.Windowed = true;
                        remList.Add(tabPage);
                    }
                }
                //  Remove all tabpages that were added to rem list from tab list.
                tabList.RemoveAll(new Predicate<TabPageEx>(delegate(TabPageEx item) {
                    return remList.Contains(item);
                }));
                //  Dispose all the tab pages that were removed.
                foreach (TabPageEx tabPage in remList)
                    tabPage.Dispose();
            }
            else {
                List<ChatForm> remList = new List<ChatForm>();
                foreach (ChatForm chatForm in windowList) {
                    if (!chatForm.Visible) {
                        ChatControl chatControl = (ChatControl)chatForm.Controls["ChatControl"];
                        chatControl.Parent = null;
                        string key = chatControl.Key;
                        string text = chatForm.Text;
                        TabPageEx newTabPage = CreateChatTab(key, text, false);
                        newTabPage.Enabled = chatControl.Enabled;
                        newTabPage.Controls.Add(chatControl);
                        chatControl.Windowed = false;
                        remList.Add(chatForm);
                    }
                }
                //  Remove all chat forms that were added to rem list from window list.
                windowList.RemoveAll(new Predicate<ChatForm>(delegate(ChatForm item) {
                    return remList.Contains(item);
                }));
                //  Dispose all the chat windows that were removed.
                foreach (ChatForm chatForm in remList)
                    chatForm.Dispose();
            }
        }

        private void SetTheme(bool bUseThemes, string themeFile)
        {
            if (bUseThemes) {
                this.BackColor = Theme.GetThemeColor(themeFile, "Main", null, "BackColor");
                tableLayoutPanelUserHeader.UseSystemStyle = false;
                tableLayoutPanelUserHeader.SelectedColor = Theme.GetThemeColor(themeFile, "Main", "PresencePane", "BackColor");
                tableLayoutPanelUserHeader.ColorGradientMax = Theme.GetThemeValue(themeFile, "Main", "PresencePane", "GradientMax", typeof(float));
                tableLayoutPanelUserHeader.ColorGradientMin = Theme.GetThemeValue(themeFile, "Main", "PresencePane", "GradientMin", typeof(float));
                lblUserName.ForeColor = Theme.GetThemeColor(themeFile, "Main", "PresencePane", "TextColor");
                lblUserStatus.ForeColor = Theme.GetThemeColor(themeFile, "Main", "PresencePane", "TextColor");
                lblContactsHeader.BackColor = Theme.GetThemeColor(themeFile, "Main", "PresencePane", "HeaderColor");
                lblContactsHeader.BorderColor = Theme.GetThemeColor(themeFile, "Main", "PresencePane", "HeaderColor");
                lblContactsHeader.ForeColor = Theme.GetThemeColor(themeFile, "Main", "PresencePane", "HeaderTextColor");
                tabControlChat.UseSystemStyle = false;
                for (int index = 0; index < tabList.Count; index++) {
                    TabPageEx tabPage = tabList[index];
                    SetTabLook(tabPage);
                }
                tabControlChat.ColorGradientMax = Theme.GetThemeValue(themeFile, "Main", "ChatPane", "GradientMax", typeof(float));
                tabControlChat.ColorGradientMin = Theme.GetThemeValue(themeFile, "Main", "ChatPane", "GradientMin", typeof(float));
                tvUsers.UseSystemStyle = false;
                tvUsers.BackColor = Theme.GetThemeColor(themeFile, "Main", "ContactList", "BackColor");
                tvUsers.ForeColor = Theme.GetThemeColor(themeFile, "Main", "ContactList", "TextColor");
                tvUsers.SelectedColor = Theme.GetThemeColor(themeFile, "Main", "ContactList", "SelectedColor");
                tvUsers.HighlightColor = Theme.GetThemeColor(themeFile, "Main", "ContactList", "HighlightColor");
                tvUsers.HeaderColor = Theme.GetThemeColor(themeFile, "Main", "ContactList", "HeaderColor");
                tvUsers.ColorGradientMax = Theme.GetThemeValue(themeFile, "Main", "ContactList", "GradientMax", typeof(float));
                tvUsers.ColorGradientMin = Theme.GetThemeValue(themeFile, "Main", "ContactList", "GradientMin", typeof(float));
                tvUsers.ItemHeight = 24;
            }
            else {
                this.BackColor = SystemColors.Control;
                tableLayoutPanelUserHeader.UseSystemStyle = true;
                label_SystemColorsChanged(lblUserName, null);
                label_SystemColorsChanged(lblUserStatus, null);
                lblContactsHeader.BackColor = SystemColors.ActiveCaption;
                lblContactsHeader.BorderColor = SystemColors.ActiveCaption;
                lblContactsHeader.ForeColor = SystemColors.ActiveCaptionText;
                tabControlChat.UseSystemStyle = true;
                tvUsers.UseSystemStyle = true;
                tvUsers.BackColor = SystemColors.Window;
                tvUsers.ForeColor = SystemColors.WindowText;
                tvUsers.ItemHeight = 22;
            }
            //  Cause form and child controls to redraw.
            this.Invalidate(true);
        }

        private void SetTabLook(TabPageEx tabPage)
        {
            string themeFile = Properties.Settings.Default.ThemeFile;
            tabPage.BackColor = Theme.GetThemeColor(themeFile, "Main", "ChatPane", "BackColor");
            tabPage.DefaultTabColor = Theme.GetThemeColor(themeFile, "Main", "ChatPane", "DefaultTabColor");
            tabPage.DefaultBorderColor = Theme.GetThemeColor(themeFile, "Main", "ChatPane", "DefaultBorderColor");
            tabPage.DefaultForeColor = Theme.GetThemeColor(themeFile, "Main", "ChatPane", "DefaultTextColor");
            tabPage.SelectedTabColor = Theme.GetThemeColor(themeFile, "Main", "ChatPane", "SelectedTabColor");
            tabPage.SelectedBorderColor = Theme.GetThemeColor(themeFile, "Main", "ChatPane", "SelectedBorderColor");
            tabPage.SelectedForeColor = Theme.GetThemeColor(themeFile, "Main", "ChatPane", "SelectedTextColor");
            tabPage.DisabledTabColor = Theme.GetThemeColor(themeFile, "Main", "ChatPane", "DisabledTabColor");
            tabPage.DisabledBorderColor = Theme.GetThemeColor(themeFile, "Main", "ChatPane", "DisabledBorderColor");
            tabPage.DisabledForeColor = Theme.GetThemeColor(themeFile, "Main", "ChatPane", "DisabledTextColor");
            tabPage.Enabled = tabPage.Enabled;  //  To force correct repainting.
        }

        /// <summary>
        /// Sets the look of a tree node. This method should be called only after
        /// the node has been added to a tree view control.
        /// </summary>
        /// <param name="node"></param>
        /// <param name="bHeader"></param>
        private void SetNodeLook(TreeNode node, bool bHeader)
        {
            if (bHeader) {
                Font font = new Font(node.TreeView.Font, FontStyle.Bold);
                node.NodeFont = font;
            }
        }

        private void PopulateGroups()
        {
            try {
                lock (groupList) {
                    foreach (string groupName in groupList) {
                        TreeNode groupNode = new TreeNode(groupName + string.Empty);
                        groupNode.Name = groupName;
                        tvUsers.Nodes.Add(groupNode);
                        SetNodeLook(groupNode, true);
                    }
                }
            }
            catch (Exception ex) {
                ErrorHandler.ShowError(ex);
            }
        }

        /// <summary>
        /// Scan the local network and list all online users.
        /// </summary>
        private void UpdateUserList()
        {
            //  Lock the list for thread safety.
            lock (userList) {
                try {
                    //  Update contents of ListView control on left pane if user list was modified.
                    if (bDirtyList) {
                        //  List for holding all currently empty root nodes. If an
                        //  item is added to an empty root node, it has to be expanded.
                        List<TreeNode> nodesToExpand = new List<TreeNode>();

                        //  Clear the tree view control. The root nodes are retained. 
                        //  Only child nodes are removed.
                        foreach (TreeNode rootNode in tvUsers.Nodes) {
                            if (rootNode.Nodes.Count == 0)
                                nodesToExpand.Add(rootNode);
                            rootNode.Nodes.Clear();
                        }

                        //  Set all conversation tab pages and windows to disabled.
                        foreach (TabPageEx tabPage in tabList) {
                            ChatControl chatControl = (ChatControl)tabPage.Controls["ChatControl"];
                            chatControl.Enabled = false;
                            tabPage.Enabled = false;
                        }
                        foreach (ChatForm chatForm in windowList) {
                            ChatControl chatControl = (ChatControl)chatForm.Controls["ChatControl"];
                            chatControl.Enabled = false;
                            chatForm.Enabled = false;
                        }

                        //  Loop through user list and add to ListView control.
                        foreach (User user in userList) {
                            //  Do not display a user whose status is "Offline".
                            if (user.Status != "Offline") {
                                //  Do not add local user to list unless in debug mode.
                                if (!user.Key.Equals(localUserKey) || Program.DebugMode)
                                    tvUsers.Nodes[user.Group].Nodes.Add(user.Key, user.Name + string.Empty, 
                                        "Blank", "Blank");
                            }

                            if (tabList.ContainsKey(user.Key)) {
                                ChatControl chatControl = (ChatControl)tabList[user.Key].Controls["ChatControl"];
                                chatControl.Enabled = true;
                                //  Keep appearance as disabled if user status is Offline.
                                if (user.Status != "Offline")
                                    tabList[user.Key].Enabled = true;
                            }
                            if (windowList.ContainsKey(user.Key)) {
                                ChatControl chatControl = (ChatControl)windowList[user.Key].Controls["ChatControl"];
                                chatControl.Enabled = true;
                                //  Keep appearance as disabled if user status is Offline.
                                if (user.Status != "Offline")
                                    windowList[user.Key].Enabled = true;
                            }

                            //  Show appropriate status images in the UI.
                            UpdateUserStatusImages(user.Key);
                        }

                        //  Expand all previously empty root nodes.
                        foreach (TreeNode rootNode in nodesToExpand)
                            rootNode.ExpandAll();
                        //  Clear the list, it is no longer needed.
                        nodesToExpand.Clear();

                        //  Force the tab control to redraw.
                        tabControlChat.Invalidate();
                        //  Reset the user list modify flag.
                        bDirtyList = false;
                    }
                }
                catch (Exception ex) {
                    ErrorHandler.ShowError(ex);
                }
            }
        }

        /// <summary>
        /// Recursively search through the node hierarchy for the node with the given key and
        /// change its image key.
        /// </summary>
        /// <param name="nodes">Node collection to loop through.</param>
        /// <param name="userKey">Key of the node.</param>
        /// <param name="nodeImageKey">Image key to set.</param>
        private void UpdateNodeImage(TreeNodeCollection nodes, string userKey, string nodeImageKey)
        {
            foreach (TreeNode node in nodes) {
                if (node.Name.Equals(userKey)) {
                    node.ImageKey = nodeImageKey;
                    node.SelectedImageKey = nodeImageKey;
                    break;
                }
                if (node.Nodes.Count > 0) {
                    UpdateNodeImage(node.Nodes, userKey, nodeImageKey);
                }
            }
        }

        /// <summary>
        /// Update the images of tree node and tab to reflect the user's status.
        /// </summary>
        /// <param name="userKey"></param>
        /// <param name="nodeImageKey"></param>
        /// <param name="tabImageKey"></param>
        private void UpdateStatusImage(string userKey, string nodeImageKey, string tabImageKey)
        {
            UpdateNodeImage(tvUsers.Nodes, userKey, nodeImageKey);
            if (tabList.ContainsKey(userKey))
                tabList[userKey].ImageKey = tabImageKey;
            if (userKey.Equals(localUserKey)) {
                picBoxUserStatus.Image = imageListSmall.Images[nodeImageKey];
                lblUserStatus.Text = UserStatus.GetStatusInfo(localStatus).Description;
            }
        }

        private void UpdateUserStatusImages(object key)
        {
            string userKey = (string)key;
            try {
                User user = GetUser(userKey);
                //  If user is present in user list, set appropriate status images.
                //  If user is not present in user list, show Offline status images.
                if (user != null)
                    UpdateUserStatusImages(user.Key, user.Status);
                else
                    UpdateUserStatusImages(userKey, "Offline");
            }
            catch (Exception ex) {
                ErrorHandler.ShowError(ex);
            }
        }

        private void UpdateUserStatusImages(string userKey, string status)
        {
            StatusInfo statusInfo = UserStatus.GetStatusInfo(status);
            if (statusInfo.Code != string.Empty) {
                UpdateStatusImage(userKey, statusInfo.StatusImageKey, statusInfo.ChatImageKey);
            }
        }

        private void UpdateTabWindowStatusLook(object key)
        {
            string userKey = (string)key;
            try {
                User user = GetUser(userKey);
                //  If user is present in user list, set appropriate look.
                //  If user is not present in user list, set Offline look.
                if (user != null)
                    UpdateTabWindowStatusLook(user.Key, user.Status);
                else
                    UpdateTabWindowStatusLook(userKey, "Offline");
            }
            catch (Exception ex) {
                ErrorHandler.ShowError(ex);
            }
        }

        private void UpdateTabWindowStatusLook(string userKey, string status)
        {
            bool enabled = true;
            if (status == "Offline")
                enabled = false;

            if (tabList.ContainsKey(userKey))
                tabList[userKey].Enabled = enabled;
            if (windowList.ContainsKey(userKey))
                windowList[userKey].Enabled = enabled;
        }

        private FileTransferDialog GetFileTransferDialog()
        {
            if (FileTransferDialog.Instance != null) {
                FileTransferDialog fileTransferDialog = FileTransferDialog.Instance;
                return fileTransferDialog;
            }
            else {
                FileTransferDialog fileTransferDialog = new FileTransferDialog();
                fileTransferDialog.LocalAddress = localAddress;
                fileTransferDialog.LocalUserName = localUserName;
                fileTransferDialog.Notify += new FileTransferDialog.NotifyEventhandler(FileTransferDialog_Notify);
                //  Call Show() method to trigger form's load event.
                fileTransferDialog.Show();
                return fileTransferDialog;
            }
        }

        private void NodeActivated(TreeNode node)
        {
            //  If the item is not a header, open a new tab.
            if (node.Level > 0 && node.Nodes.Count == 0) {
                //  Open a new chat tab if selected user is not local user.
                if (!node.Name.Equals(localUserKey))
                    StartConversation(node.Name, node.Text, true);

                //  Allow loopback messaging in debug mode.
                if (node.Name.Equals(localUserKey) && Program.DebugMode)
                    StartConversation(node.Name, node.Text, true);
            }
        }

        private string ReplaceText(string text)
        {
            return text.Replace("%TITLE%", AppInfo.Title);
        }

        /// <summary>
        /// Loop through all menus inside a menu and format their text.
        /// </summary>
        /// <param name="menu">The parent menu.</param>
        private void SetMenuCaptions(Menu menu)
        {
            if (menu is MenuItem)
                ((MenuItem)menu).Text = ReplaceText(((MenuItem)menu).Text);
            if (menu.MenuItems != null) {
                foreach (MenuItem menuItem in menu.MenuItems)
                    SetMenuCaptions(menuItem);
            }
        }

        /// <summary>
        /// Loop through all controls and components inside a component and format their text.
        /// </summary>
        /// <param name="component">The parent component.</param>
        private void SetControlCaptions(Component component)
        {
            if (component is Control) {
                if (component is Form) {
                    Form form = (Form)component;
                    Type formType = form.GetType();
                    FieldInfo fieldInfo = formType.GetField("components", BindingFlags.Instance | BindingFlags.NonPublic);
                    IContainer container = (IContainer)fieldInfo.GetValue(form);
                    foreach (Component childComponent in container.Components) {
                        SetControlCaptions(childComponent);
                    }
                }
                Control control = (Control)component;
                control.Text = ReplaceText(control.Text);
                foreach (Control childControl in control.Controls) {
                    if (childControl.Controls != null)
                        SetControlCaptions(childControl);
                    else
                        childControl.Text = ReplaceText(childControl.Text);
                }
            }
            else if (component is NotifyIcon) {
                NotifyIcon notifyIcon = (NotifyIcon)component;
                notifyIcon.BalloonTipTitle = ReplaceText(notifyIcon.BalloonTipTitle);
                notifyIcon.Text = ReplaceText(notifyIcon.Text);
            }
        }

        private void ShowNetworkStatus(bool available)
        {
            string status = available ? string.Empty : " - Not Connected";
            notifyIconSysTray.Text = AppInfo.Title + status;
        }

        private void BroadcastCallback(string text, TreeNodeCollection userList)
        {
            if (string.IsNullOrEmpty(text))
                return;

            foreach (TreeNode groupNode in userList)
                foreach (TreeNode userNode in groupNode.Nodes)
                    if (userNode.Checked)
                        SendMessage(DatagramTypes.Broadcast, userNode.Name, text);
        }

        private void SetIdle(bool bIdle)
        {
            //  If status change on idle is disabled, return.
            //  No need to do anything if desired idle state is already in effect.
            if (!Properties.Settings.Default.IdleStatusChange || bSystemIdle == bIdle)
                return;

            bSystemIdle = bIdle;
            //  If user status is "Offline", do not change it to "Away".
            if (localStatus == "Offline")
                return;

            if (bIdle) {
                oldLocalStatus = localStatus;
                SetStatus("Away");
            }
            else {
                localStatus = oldLocalStatus;
                SetStatus(localStatus);
            }
        }
    }
}
