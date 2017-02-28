using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Runtime.InteropServices;
using System.Linq;

namespace LANChat
{
    public partial class HistoryViewer : Form
    {
        public static Form Instance;
        public static EventHandler RefreshMessages;
        public static EventHandler SetForeground;

        private static int instanceCount;
        private FormWindowState prevWindowState;
        private List<MessageInfo> messageList;
        private List<KeyValuePair<string, string>> groupList;
        private int sortColumn;

        public HistoryViewer()
        {
            instanceCount++;
            if (instanceCount > 1) {
                this.Dispose();
                return;
            }

            Instance = this;
            InitializeComponent();
            InitUI();

            RefreshMessages += new EventHandler(HistoryViewer_RefreshMessages);
            SetForeground += new EventHandler(HistoryViewer_SetForeground);
            Properties.Settings.Default.PropertyChanged += new PropertyChangedEventHandler(Default_PropertyChanged);

            lvMessages.Sorting = SortOrder.Descending;
            
            sortColumn = 1;
            messageList = new List<MessageInfo>();
            CreateGroups();
        }

        public void HistoryViewer_RefreshMessages(object sender, EventArgs e)
        {
            LoadMessages();
        }

        public void HistoryViewer_SetForeground(object sender, EventArgs e)
        {
            this.WindowState = prevWindowState;
            Win32.SetForegroundWindow(new HandleRef(this, this.Handle));
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void HistoryViewer_Load(object sender, EventArgs e)
        {
            prevWindowState = this.WindowState;

            LoadMessages();
        }

        private void InitUI()
        {
            this.Icon = LANChat.Resources.Properties.Resources.HistoryIcon;
            this.ClientSize = new System.Drawing.Size(645, 480);
            lblTip.Image = (Image)LANChat.Resources.Properties.Resources.ResourceManager.GetObject("lblTip");
            rtbMessageHistory.Font = Helper.SystemFont;

            //  Set visual style of window according to current theme.
            SetTheme(Properties.Settings.Default.UseThemes, Properties.Settings.Default.ThemeFile);
        }

        private void LoadMessages()
        {
            //  Create a list of messages.
            CreateMessageList();

            //  Populates list view control according to the sort column and sort order.
            PopulateListView();

            SetTileSize();
            SetColumnWidth();

            DisplayMessage();
        }

        private void SetTileSize()
        {
            //  Avoid horizontal scrollbar by limiting tile size 
            //  within client rectangle of the list view control.
            Size tileSize = lvMessages.ClientSize;
            tileSize.Height = 33;
            lvMessages.TileSize = tileSize;
        }

        private void SetColumnWidth()
        {
            colHdrDate.Width = lvMessages.ClientSize.Width / 2;
            colHdrName.Width = lvMessages.ClientSize.Width - colHdrDate.Width;
        }

        private void DisplayMessage()
        {
            try {
                rtbMessageHistory.Clear();
                lblName.Text = "No message selected";
                lblDate.Text = string.Empty;

                if (lvMessages.SelectedIndices.Count > 0) {
                    ListViewItem item = lvMessages.SelectedItems[0];
                    MessageInfo info = new MessageInfo(string.Empty, DateTime.Now, (long)item.Tag);
                    rtbMessageHistory.Rtf = History.GetMessage(info);
                    if (rtbMessageHistory.Text.Equals(string.Empty))
                        rtbMessageHistory.Text = "Could not retrieve chat history.";
                    lblName.Text = "Chat with " + item.Text;
                    lblDate.Text = item.SubItems[1].Text;
                }
            }
            catch {
            }
        }

        private void CreateMessageList()
        {
            messageList = History.GetList();
        }

        private void PopulateListView()
        {
            lvMessages.BeginUpdate();

            //  Sort the message list.
            SortList();

            //  Add groups to the list view control.
            AddGroups();

            //  Add messages to the list view control.
            AddItems();

            lvMessages.EndUpdate();
        }

        /// <summary>
        /// Sort message list according to list view sort order.
        /// </summary>
        private void SortList()
        {
            if (lvMessages.Sorting == SortOrder.Ascending) {
                switch (sortColumn) {
                    case 0:
                        messageList.Sort(delegate(MessageInfo m1, MessageInfo m2) { 
                            int result = m1.Name.CompareTo(m2.Name); return result == 0 ? m2.Date.CompareTo(m1.Date) : result; });
                        break;
                    case 1:
                        messageList.Sort(delegate(MessageInfo m1, MessageInfo m2) { return m1.Date.CompareTo(m2.Date); });
                        break;
                }
            }
            else {
                switch (sortColumn) {
                    case 0:
                        messageList.Sort(delegate(MessageInfo m1, MessageInfo m2) { 
                            int result = m2.Name.CompareTo(m1.Name); return result == 0 ? m2.Date.CompareTo(m1.Date) : result; });
                        break;
                    case 1:
                        messageList.Sort(delegate(MessageInfo m1, MessageInfo m2) { return m2.Date.CompareTo(m1.Date); });
                        break;
                }
            }
        }

        /// <summary>
        /// Check if a group with the specified header exists.
        /// </summary>
        /// <param name="header"></param>
        private bool GroupExists(string header)
        {
            foreach (ListViewGroup group in lvMessages.Groups) {
                if (group.Header.Equals(header))
                    return true;
            }
            return false;
        }

        private void CreateGroups()
        {
            //  Create groups that fall into different time frames.
            groupList = new List<KeyValuePair<string, string>>();
            
            DateTime today = DateTime.Today;
            groupList.Add(new KeyValuePair<string,string>(today.ToFileTime().ToString(), "Today"));
            DateTime yesterday = today.AddDays(-1);
            groupList.Add(new KeyValuePair<string, string>(yesterday.ToFileTime().ToString(), "Yesterday"));
            
            DateTime thisWeek = today.AddDays(-(int)today.DayOfWeek);
            if (thisWeek.CompareTo(yesterday) < 0)
                groupList.Add(new KeyValuePair<string, string>(thisWeek.ToFileTime().ToString(), "Earlier this week"));

            DateTime lastWeek = thisWeek.AddDays(-7);
            groupList.Add(new KeyValuePair<string, string>(lastWeek.ToFileTime().ToString(), "Last week"));

            DateTime thisMonth = new DateTime(today.Year, today.Month, 1);
            if (thisMonth.CompareTo(yesterday) < 0 && thisMonth.CompareTo(thisWeek) < 0 && thisMonth.CompareTo(lastWeek) < 0)
                groupList.Add(new KeyValuePair<string, string>(thisMonth.ToFileTime().ToString(), "Earlier this month"));

            DateTime thisYear = new DateTime(today.Year, 1, 1);
            if (thisYear.CompareTo(yesterday) < 0 && thisYear.CompareTo(lastWeek) < 0 && thisYear.CompareTo(thisMonth) < 0)
                groupList.Add(new KeyValuePair<string, string>(thisYear.ToFileTime().ToString(), "Earlier this year"));

            DateTime oldest = new DateTime(1971, 1, 1);
            groupList.Add(new KeyValuePair<string, string>(oldest.ToFileTime().ToString(), "A long time ago"));
        }

        /// <summary>
        /// Add sort column dependent groups to list view control.
        /// </summary>
        private void AddGroups()
        {
            lvMessages.Groups.Clear();

            switch (sortColumn) {
                case 0:
                    //  Add a group for each unique name in the list.
                    foreach (MessageInfo message in messageList) {
                        string name = message.Name;
                        if (!GroupExists(name)) {
                            lvMessages.Groups.Add(name, name);
                        }
                    }
                    break;
                case 1:
                    if (lvMessages.Sorting == SortOrder.Ascending) {
                        for (int index = groupList.Count - 1; index >= 0; index--)
                            lvMessages.Groups.Add(groupList[index].Key, groupList[index].Value);
                    }
                    else {
                        for (int index = 0; index < groupList.Count; index++)
                            lvMessages.Groups.Add(groupList[index].Key, groupList[index].Value);
                    }
                    break;
            }
        }

        /// <summary>
        /// Add the messages in the message list as items into ListView controls.
        /// </summary>
        private void AddItems()
        {
            lvMessages.Items.Clear();

            foreach (MessageInfo message in messageList) {
                string name = message.Name;
                string date = message.Date.ToString("d-MMM-yyyy h:mm tt");
                ListViewItem item = new ListViewItem(new string[] { name, date });
                item.Tag = message.Offset;

                switch (sortColumn) {
                    case 0:
                        item.Group = lvMessages.Groups[message.Name];
                        break;
                    case 1:
                        foreach (ListViewGroup group in lvMessages.Groups) {
                            DateTime groupTime = DateTime.FromFileTime(long.Parse(group.Name));
                            if (message.Date.CompareTo(groupTime) > 0) {
                                item.Group = group;
                                if (lvMessages.Sorting == SortOrder.Descending)
                                    break;
                            }
                        }
                        break;
                }

                //  Make the first item selected.
                if (lvMessages.Items.Count == 0)
                    item.Selected = true;

                lvMessages.Items.Add(item);
            }
        }

        private void SetTheme(bool bUseThemes, string themeFile)
        {
            if (bUseThemes) {
                this.BackColor = Theme.GetThemeColor(themeFile, "History", null, "BackColor");
                lblName.ForeColor = Theme.GetThemeColor(themeFile, "History", null, "TextColor");
                lblDate.ForeColor = Theme.GetThemeColor(themeFile, "History", null, "TextColor");
                lvMessages.UseSystemStyle = false;
                lvMessages.BackColor = Theme.GetThemeColor(themeFile, "History", "MessageList", "BackColor");
                lvMessages.ForeColor = Theme.GetThemeColor(themeFile, "History", "MessageList", "TextColor");
                lvMessages.SelectedColor = Theme.GetThemeColor(themeFile, "History", "MessageList", "SelectedColor");
                lvMessages.HighLightColor = Theme.GetThemeColor(themeFile, "History", "MessageList", "HighlightColor");
                lvMessages.HeaderColor = Theme.GetThemeColor(themeFile, "History", "MessageList", "HeaderColor");
                lvMessages.ColorGradientMax = Theme.GetThemeValue(themeFile, "History", "MessageList", "GradientMax", typeof(float));
                lvMessages.ColorGradientMin = Theme.GetThemeValue(themeFile, "History", "MessageList", "GradientMin", typeof(float));
            }
            else {
                this.BackColor = SystemColors.Control;
                lblName.ForeColor = SystemColors.ControlText;
                lblDate.ForeColor = SystemColors.GrayText;
                lvMessages.UseSystemStyle = true;
                lvMessages.BackColor = SystemColors.Window;
                lvMessages.ForeColor = SystemColors.WindowText;
            }
            //  Cause form and child controls to redraw.
            this.Invalidate(true);
        }

        private void lvMessages_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (e.Column != sortColumn) {
                sortColumn = e.Column;
                lvMessages.Sorting = SortOrder.Ascending;
            }
            else {
                if (lvMessages.Sorting == SortOrder.Ascending)
                    lvMessages.Sorting = SortOrder.Descending;
                else
                    lvMessages.Sorting = SortOrder.Ascending;
            }

            PopulateListView();
        }

        private void lvMessages_Resize(object sender, EventArgs e)
        {
            SetTileSize();
            SetColumnWidth();
        }

        private void Default_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals("UseThemes") || e.PropertyName.Equals("ThemeFile")) {
                SetTheme(Properties.Settings.Default.UseThemes, Properties.Settings.Default.ThemeFile);
            }
        }

        private void lvMessages_SelectedIndexChanged(object sender, EventArgs e)
        {
            DisplayMessage();
        }

        private void rtbMessageHistory_Enter(object sender, EventArgs e)
        {
            Win32.HideCaret(rtbMessageHistory.Handle);
        }

        private void rtbMessageHistory_MouseDown(object sender, MouseEventArgs e)
        {
            Win32.HideCaret(rtbMessageHistory.Handle);
        }

        private void rtbMessageHistory_KeyUp(object sender, KeyEventArgs e)
        {
            Win32.HideCaret(rtbMessageHistory.Handle);
        }

        private void HistoryViewer_Resize(object sender, EventArgs e)
        {
            //  Remember state of window before minimizing.
            if (this.WindowState != FormWindowState.Minimized)
                prevWindowState = this.WindowState;
        }
    }
}
