using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using LANMedia.CustomControls;

namespace LANChat
{
    /// <summary>
    /// The main window class.
    /// </summary>
    public partial class MainForm : BaseForm
    {
        public MainForm()
        {
            InitializeComponent();
            
            InitNetworking();
            InitMessaging();
            InitApplication();
            InitUI();

            //  Flag indicating whether any modal dialog is open.
            bModalOpen = false;

            //  Create a list for holding all the opened and closed tab pages.
            tabList = new TabPageList();

            //  Create a list for holding all opened and closed chat windows.
            windowList = new WindowList();

            //  Create a list for holding all the user groups.
            groupList = new List<string>();

            //  A dictionary that acts as lookup for a user's group.
            userGroupLookup = new Dictionary<string, string>();

            //  Initialize this to current time.
            userRefreshTime = DateTime.UtcNow;

            //  Initialize idle time (millisecs) to 0.
            idleTime = 0;
            bSystemIdle = false;
        }

        [System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name = "FullTrust")]
        protected override void WndProc(ref Message m)
        {
            const int WM_INITMENUPOPUP = 0x0117;
            switch (m.Msg) {
                case WM_INITMENUPOPUP:
                    if (m.WParam == mnuNotify.Handle)
                        imageMenuEx.MenuItem_Popup(mnuNotify, null);
                    break;
                default:
                    base.WndProc(ref m);
                    break;
            }
        }

        #region UI Events
        private void MainForm_Load(object sender, EventArgs e)
        {            
            prevWindowState = FormWindowState.Normal;
            bFirstTime = true;

            if (Program.DebugMode) {
                this.Text += " - Debug Mode";
            }

            //  Start application hidden. Only a systray icon will be visible.
            this.Hide();

            //  A structure that holds information about the local user.
            userInfo = new UserInfo(Environment.UserName, Environment.MachineName, localAddress.ToString(),
                AppInfo.OSName, AppInfo.Version, string.Empty);

            //  Get the list of user groups and add to the tree view control.
            LoadGroupData();
            PopulateGroups();

            StartApplication();
            StartNetworking();

            //  Check the appropriate menu item in the status menu.
            foreach (MenuItem menuItem in mnuStatus.MenuItems) {
                if (localStatus == (string)menuItem.Tag)
                    menuItem.Checked = true;
            }
            //  Show the status image in the presence area.
            UpdateUserStatusImages(localUserKey, localStatus);

            //  Global hooks for monitoring keyboard and mouse input.
            activityMonitor = new ActivityMonitor();
            activityMonitor.OnMouseActivity += new MouseEventHandler(ActivityMonitor_Event);
            activityMonitor.KeyDown += new KeyEventHandler(ActivityMonitor_Event);
            activityMonitor.KeyPress += new KeyPressEventHandler(ActivityMonitor_Event);
            activityMonitor.KeyUp += new KeyEventHandler(ActivityMonitor_Event);
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            //  Remember state of window before minimizing.
            if (WindowState != FormWindowState.Minimized)
                prevWindowState = WindowState;
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            //  If window was closed by user, close to system tray instead
            //  of terminating application.
            if (e.CloseReason == CloseReason.UserClosing) {
                e.Cancel = true;
                HideWindow();
            }
            else if (e.CloseReason == CloseReason.ApplicationExitCall) {
                //  Do nothing as this is handled elsewhere.
            }
            else {
                //  The application has been sent a terminate message.
                Exit();
            }
        }

        private void FileTransferDialog_Notify(object sender, NotifyEventArgs e)
        {
            if (bShowNotifications && !bSilentMode)
                DisplayBalloonMessage(e.Title, e.Text);
            PlaySound(NotifyEvents.FileComplete);
        }

        private void tvUsers_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right) {
                tvUsers.SelectedNode = e.Node;
                if (e.Node.Level < tvUsers.HeaderLevel) {
                    mnuGroupRename.Enabled = !e.Node.Name.Equals(Properties.Settings.Default.DefaultGroupName);
                    mnuGroupDelete.Enabled = !e.Node.Name.Equals(Properties.Settings.Default.DefaultGroupName);
                    mnuGroup.Tag = e.Node;
                    mnuGroup.Show(tvUsers, new Point(e.X, e.Y));
                }
                else {
                    User user = GetUser(e.Node.Name);
                    mnuUserConversation.Enabled = !user.Key.Equals(localUserKey);
                    mnuUserSendFile.Enabled = !user.Key.Equals(localUserKey);
                    mnuUserMove.MenuItems.Clear();
                    foreach (string groupName in groupList) {
                        MenuItem menuItem = new MenuItem(groupName, mnuUserMove_Click);
                        menuItem.Enabled = !groupName.Equals(user.Group);
                        mnuUserMove.MenuItems.Add(menuItem);
                    }
                    mnuUser.Tag = e.Node;
                    mnuUser.Show(tvUsers, new Point(e.X, e.Y));
                }
            }
        }

        private void tvUsers_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            NodeActivated(e.Node);
        }

        private void tvUsers_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && tvUsers.SelectedNode != null) {
                NodeActivated(tvUsers.SelectedNode);
            }
        }

        private void notifyIconSysTray_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            //  Restore window if no modal dialog is open.
            if (bModalOpen == false)
                ShowWindow(true, true);
        }

        private void notifyIconSysTray_MouseClick(object sender, MouseEventArgs e)
        {
            //  Show system tray menu if no modal dialog is open.
            if (e.Button == System.Windows.Forms.MouseButtons.Right && bModalOpen == false) {
                Win32.SetForegroundWindow(new HandleRef(this, this.Handle));
                Win32.TrackPopupMenuEx(mnuNotify.Handle, 0x0, Cursor.Position.X, Cursor.Position.Y, this.Handle, (IntPtr)null);
            }
        }

        private void mnuNotifyShow_Click(object sender, EventArgs e)
        {
            //  Restore Window.
            ShowWindow(true, true);
        }

        private void mnuNotifyHistory_Click(object sender, EventArgs e)
        {
            try {
                this.Cursor = Cursors.WaitCursor;
                //  Check if an instance of this window already exists. In that
                //  case just display the existing window.
                if (HistoryViewer.Instance != null) {
                    HistoryViewer.Instance.Show();
                    HistoryViewer.Instance.Invoke(HistoryViewer.SetForeground);
                }
                else {
                    //  Window does not exist, create a new instance and display it.
                    HistoryViewer historyViewer = new HistoryViewer();
                    historyViewer.Show();
                }
            }
            catch {
            }
            finally {
                this.Cursor = Cursors.Default;
            }
        }

        private void mnuNotifyFileTransfers_Click(object sender, EventArgs e)
        {
            FileTransferDialog fileTransferDialog = GetFileTransferDialog();
            fileTransferDialog.Show();
            fileTransferDialog.Invoke(FileTransferDialog.SetForeground);
        }

        private void mnuNotifySilentMode_Click(object sender, EventArgs e)
        {
            SetSilentMode(!bSilentMode);
            Properties.Settings.Default.SilentMode = bSilentMode;
        }

        private void mnuNotifyOptions_Click(object sender, EventArgs e)
        {
            SettingsDialog settingsDialog = null;
            try {
                settingsDialog = new SettingsDialog();
                if (this.WindowState == FormWindowState.Minimized)
                    settingsDialog.StartPosition = FormStartPosition.CenterScreen;
                bModalOpen = true;
                //  Show the settings dialog.
                DialogResult result = settingsDialog.ShowDialog(this);
                bModalOpen = false;
                settingsDialog.Dispose();
            }
            catch {
                settingsDialog.Dispose();
                bModalOpen = false;
            }
        }

        private void mnuNotifyAbout_Click(object sender, EventArgs e)
        {
            AboutDialog aboutDialog = null;
            try {
                aboutDialog = new AboutDialog();
                if (this.WindowState == FormWindowState.Minimized)
                    aboutDialog.StartPosition = FormStartPosition.CenterScreen;
                bModalOpen = true;
                aboutDialog.ShowDialog(this);
                aboutDialog.Dispose();
                bModalOpen = false;
            }
            catch {
                aboutDialog.Dispose();
                bModalOpen = false;
            }
        }

        private void mnuNotifyExit_Click(object sender, EventArgs e)
        {
            //  Quit application.
            Exit();
        }

        private void mnuStatus_Click(object sender, EventArgs e)
        {
            MenuItem menuItem = (MenuItem)sender;

            foreach (MenuItem item in menuItem.Parent.MenuItems)
                item.Checked = false;

            menuItem.Checked = true;
            SetStatus((string)menuItem.Tag);
        }

        private void ChatControl_Sending(object sender, ChatControl.ChatArgs e)
        {
            ChatControl control = (ChatControl)sender;
            SendMessage(e.DatagramType, e.Key, e.Message);
        }

        private void ChatControl_WindowModeChange(object sender, ChatControl.WindowModeArgs e)
        {
            ChatControl control = (ChatControl)sender;
            control.Parent = null;
            string key = e.Key;
            string text = string.Empty;
            bool enabled = true;
            if (e.Event == ChatControl.WindowModeEvents.PopIn) {
                foreach (ChatForm chatForm in windowList) {
                    if (chatForm.Name.Equals(key)) {
                        text = chatForm.Text;
                        enabled = chatForm.Enabled;
                        windowList.Remove(chatForm);
                        chatForm.Dispose();
                        break;
                    }
                }
                TabPageEx newTabPage = CreateChatTab(key, text);
                if (newTabPage != null) {
                    newTabPage.Enabled = enabled;
                    newTabPage.Controls.Add(control);
                    control.Windowed = false;
                }
            }
            else {
                if (tabControlChat.TabPages.ContainsKey(key))
                    tabControlChat.TabPages.RemoveByKey(key);
                foreach (TabPageEx tabPage in tabList) {   
                    if (tabPage.Name.Equals(key)) {
                        text = tabPage.Text;
                        enabled = tabPage.Enabled;
                        tabList.Remove(tabPage);
                        tabPage.Dispose();
                        break;
                    }
                }
                ChatForm newChatForm = CreateChatForm(key, text, true);
                if (newChatForm != null) {
                    newChatForm.Enabled = enabled;
                    newChatForm.Controls.Add(control);
                    control.Windowed = true;
                }
            }
            tabControlChat.Visible = (tabControlChat.TabCount > 0);
        }

        private void timerUserUpdate_Tick(object sender, EventArgs e)
        {
            //  Periodically ping all users in the user list.
            PingUsers();
            userRefreshTime = DateTime.UtcNow;
        }

        private void timerStatusUpdate_Tick(object sender, EventArgs e)
        {
            //  Check the status queue for any timed out acknowledgements.
            this.BeginInvoke(new InvokeDelegate(CheckPendingQueries));

            //  In case the user list has not been refreshed timely, do it now.
            TimeSpan timeSpan = DateTime.UtcNow.Subtract(userRefreshTime);
            if (timeSpan.TotalSeconds > userRefreshPeriod) {
                PingUsers();
                userRefreshTime = DateTime.UtcNow;
            }

            //  Increment idle time if no input event has occured.
            idleTime += timerStatusUpdate.Interval;

            if (idleTime > idleTimeMax) {
                //  Limit max value of idleTime variable to prevent it
                //  from exceeding max value of long.
                idleTime = idleTimeMax;
                SetIdle(true);
            }
        }

        private void label_SystemColorsChanged(object sender, EventArgs e)
        {
            if (!Properties.Settings.Default.UseThemes) {
                if (Application.RenderWithVisualStyles)
                    ((Label)sender).ForeColor = SystemColors.WindowText;
                else
                    ((Label)sender).ForeColor = SystemColors.HighlightText;
            }
        }

        private void tabControlChat_Selected(object sender, TabControlEventArgs e)
        {
            if (e.TabPage != null && e.Action == TabControlAction.Selected) {
                TabPageEx tabPage = (TabPageEx)e.TabPage;
                ChatControl chatControl = (ChatControl)tabPage.Controls["ChatControl"];
                chatControl.SetFocus();
            }
        }

        private void tabControlChat_TabClosed(object sender, TabControlEventArgs e)
        {
            if (tabControlChat.TabCount == 0)
                tabControlChat.Visible = false;
        }
        
        private void mnuGroupAdd_Click(object sender, EventArgs e)
        {
            string groupName = InputBox.Show("Enter a group name", "Add new group", string.Empty, Cursor.Position).Trim();
            if (groupName != string.Empty) {

                //  Check if a group with this name exists.
                if (groupList.Contains(groupName)) {
                    MessageBox.Show("A group named '" + groupName + "' already exists. Please specify a different name",
                            AppInfo.Title, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                
                TreeNode groupNode = new TreeNode(groupName + string.Empty);
                groupNode.Name = groupName;
                tvUsers.Nodes.Add(groupNode);
                SetNodeLook(groupNode, true);
                groupList.Add(groupName);
            }
        }

        private void mnuGroupDelete_Click(object sender, EventArgs e)
        {
            TreeNode nodeToDelete = (TreeNode)mnuGroup.Tag;
            //  Move all the child nodes of this node to the General node.
            TreeNode generalNode = tvUsers.Nodes[Properties.Settings.Default.DefaultGroupName];
            foreach (TreeNode childNode in nodeToDelete.Nodes) {
                TreeNode cloneNode = (TreeNode)childNode.Clone();
                generalNode.Nodes.Add(cloneNode);
                //  Update the user's Group attribute to reflect the change.
                User user = GetUser(cloneNode.Name);
                user.Group = Properties.Settings.Default.DefaultGroupName;
                //  Update the lookup dictionary to match the change.
                UpdateUserGroupLookup(user.Key);
            }
            nodeToDelete.Nodes.Clear();
            nodeToDelete.Remove();
            generalNode.Expand();
            groupList.Remove(nodeToDelete.Text);
        }

        private void mnuGroupRename_Click(object sender, EventArgs e)
        {
            TreeNode nodeToRename = (TreeNode)mnuGroup.Tag;
            string oldGroupName = nodeToRename.Text;
            string groupName = InputBox.Show("Enter a new name", "Rename group", oldGroupName, Cursor.Position).Trim();

            if (groupName != string.Empty) {

                //  Check if a group with this name exists.
                if (groupList.Contains(groupName)) {
                    MessageBox.Show("A group named '" + groupName + "' already exists. Please specify a different name",
                            AppInfo.Title, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
    
                nodeToRename.Text = groupName + string.Empty;
                nodeToRename.Name = groupName;
                groupList[groupList.IndexOf(oldGroupName)] = groupName;
            }
        }

        private void mnuUserConversation_Click(object sender, EventArgs e)
        {
            NodeActivated((TreeNode)mnuUser.Tag);
        }

        private void mnuUserSendFile_Click(object sender, EventArgs e)
        {
            TreeNode userNode = (TreeNode)mnuUser.Tag;
            User user = GetUser(userNode.Name);
            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK) {
                SendMessage(DatagramTypes.File, user.Key, openFileDialog.FileName);
            }
        }

        private void mnuUserMove_Click(object sender, EventArgs e)
        {
            //  Get the destination group name.
            MenuItem menuItem = (MenuItem)sender;
            string destinationGroup = menuItem.Text;

            //  Get the node to move and create a copy of it.
            //  This copy is added to the new group, the original
            //  node is then deleted.
            TreeNode userNode = (TreeNode)mnuUser.Tag;
            TreeNode cloneNode = (TreeNode)userNode.Clone();

            //  Loop through root nodes of tree view until the
            //  specified destination node is found.
            foreach (TreeNode groupNode in tvUsers.Nodes) {
                if (groupNode.Name.Equals(destinationGroup)) {
                    groupNode.Nodes.Add(cloneNode);
                    break;
                }
            }

            //  Remove the original node from tree view control.
            userNode.Remove();

            //  Update the user's Group attribute to reflect the change.
            User user = GetUser(cloneNode.Name);
            user.Group = destinationGroup;
            //  Update the lookup dictionary to match the change.
            UpdateUserGroupLookup(user.Key);

            tvUsers.SelectedNode = cloneNode;
        }

        private void mnuUserInformation_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            TreeNode userNode = (TreeNode)mnuUser.Tag;
            GetUserInfo(userNode.Name);
        }
        
        private void mnuUserBroadcast_Click(object sender, EventArgs e)
        {
            BroadcastSender broadcastSender = new BroadcastSender(BroadcastCallback);
            broadcastSender.UserList = tvUsers.Nodes;
            broadcastSender.Show();
        }

        private void ActivityMonitor_Event(object sender, EventArgs e)
        {
            //  Reset idle time to zero whenever a keyboard or mouse event occurs.
            idleTime = 0;
            SetIdle(false);
        }
        #endregion
    }
}
