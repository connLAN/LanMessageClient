using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace LANChat
{
    public partial class BroadcastSender : BaseForm
    {
        public TreeNodeCollection UserList;
        public delegate void BroadcastCallback(string text, TreeNodeCollection userList);
        private BroadcastCallback callback;

        public BroadcastSender(BroadcastCallback callback)
        {
            InitializeComponent();
            InitUI();

            this.callback = callback;
            Properties.Settings.Default.PropertyChanged += new PropertyChangedEventHandler(Default_PropertyChanged);
            UserList = null;
        }

        public void Show()
        {
            ShowWindow(true, false);
            tvUsers.ExpandAll();
        }

        private void InitUI()
        {
            this.Icon = LANChat.Resources.Properties.Resources.BroadcastIcon;
            this.ClientSize = new Size(500, 230);
            txtMessage.ContextMenu = new ContextMenu();
            tvUsers.Font = Helper.SystemFont;
            //  Set visual style of window according to current theme.
            SetTheme(Properties.Settings.Default.UseThemes, Properties.Settings.Default.ThemeFile);
        }

        private void SetTheme(bool bUseThemes, string themeFile)
        {
            if (bUseThemes) {
                this.BackColor = Theme.GetThemeColor(themeFile, "Broadcast", null, "BackColor");
                lblMessageCaption.ForeColor = Theme.GetThemeColor(themeFile, "Broadcast", null, "TextColor");
                lblRecipients.ForeColor = Theme.GetThemeColor(themeFile, "Broadcast", null, "TextColor");
                tvUsers.UseSystemStyle = false;
                tvUsers.BackColor = Theme.GetThemeColor(themeFile, "Broadcast", "ContactList", "BackColor");
                tvUsers.ForeColor = Theme.GetThemeColor(themeFile, "Broadcast", "ContactList", "TextColor");
                tvUsers.SelectedColor = Theme.GetThemeColor(themeFile, "Broadcast", "ContactList", "SelectedColor");
                tvUsers.HighlightColor = Theme.GetThemeColor(themeFile, "Broadcast", "ContactList", "HighlightColor");
                tvUsers.HeaderColor = Theme.GetThemeColor(themeFile, "Broadcast", "ContactList", "HeaderColor");
                tvUsers.ColorGradientMax = Theme.GetThemeValue(themeFile, "Broadcast", "ContactList", "GradientMax", typeof(float));
                tvUsers.ColorGradientMin = Theme.GetThemeValue(themeFile, "Broadcast", "ContactList", "GradientMin", typeof(float));
                tvUsers.ItemHeight = 24;
            }
            else {
                this.BackColor = SystemColors.Control;
                lblMessageCaption.ForeColor = SystemColors.ControlText;
                lblRecipients.ForeColor = SystemColors.ControlText;
                tvUsers.UseSystemStyle = true;
                tvUsers.BackColor = SystemColors.Window;
                tvUsers.ForeColor = SystemColors.WindowText;
                tvUsers.ItemHeight = 22;
            }
            this.Invalidate(true);
        }

        private void PopulateUserList()
        {
            if (UserList == null)
                return;

            foreach (TreeNode node in UserList) {
                tvUsers.Nodes.Add((TreeNode)node.Clone());
            }
        }

        private void CloseWindow()
        {
            Properties.Settings.Default.PropertyChanged -= new PropertyChangedEventHandler(Default_PropertyChanged);
            this.Close();
        }

        private bool syncMode = false;
        private byte syncFlags = 0x00;
        /// <summary>
        /// Synchronizes the selection in the combo box and the checked state of tree nodes.
        /// </summary>
        private void Sync()
        {
            syncMode = true;
            switch (syncFlags) {
                case 0x01:  //  all checked
                    cboRecipients.SelectedIndex = 0;
                    break;
                case 0x10:  //  all unchecked
                    cboRecipients.SelectedIndex = 1;
                    break;
                default:    //  mixed
                    cboRecipients.SelectedIndex = -1;
                    break;
            }
            syncMode = false;
        }

        private void Default_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals("UseThemes") || e.PropertyName.Equals("ThemeFile")) {
                SetTheme(Properties.Settings.Default.UseThemes, Properties.Settings.Default.ThemeFile);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            CloseWindow();
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            if (callback != null)
                callback(txtMessage.Text.Trim(), tvUsers.Nodes);
            CloseWindow();
        }

        private void BroadcastSender_Load(object sender, EventArgs e)
        {
            PopulateUserList();
            cboRecipients.SelectedIndex = 0;
        }

        private void tvUsers_AfterCheck(object sender, TreeViewEventArgs e)
        {
            //  Check/uncheck all the child nodes inside the current node.
            foreach (TreeNode childNode in e.Node.Nodes) {
                childNode.Checked = e.Node.Checked;
            }

            if (e.Node.Checked) syncFlags |= 0x01;
            if (!e.Node.Checked) syncFlags |= 0x10;
            Sync();
        }

        private void cboRecipients_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (syncMode)
                return;

            syncFlags = 0x00;
            switch (cboRecipients.SelectedIndex) {
                case 0: //  Select All
                    foreach (TreeNode node in tvUsers.Nodes)
                        node.Checked = true;
                    break;
                case 1: //  Select None
                    foreach (TreeNode node in tvUsers.Nodes)
                        node.Checked = false;
                    break;
            }
        }
    }
}
