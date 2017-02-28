using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace LANChat
{
    public partial class BroadcastViewer : BaseForm
    {
        private int autoCloseTime = 30;

        public BroadcastViewer()
        {
            InitializeComponent();
            InitUI();

            Properties.Settings.Default.PropertyChanged += new PropertyChangedEventHandler(Default_PropertyChanged);
        }

        /// <summary>
        /// Show the form.
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="messageText"></param>
        public void Show(string userName, string messageText)
        {
            this.Text = "Broadcast from " + userName;
            txtMessage.Text = messageText;
            lblCloseMessage.Text = "This window will close in " + autoCloseTime.ToString() + " seconds.";
            ShowWindow(true, false);
        }

        private void InitUI()
        {
            this.Icon = LANChat.Resources.Properties.Resources.BroadcastIcon;
            this.ClientSize = new Size(330, 230);
            txtMessage.ContextMenu = new ContextMenu();
            //  Set visual style of window according to current theme.
            SetTheme(Properties.Settings.Default.UseThemes, Properties.Settings.Default.ThemeFile);
        }

        private void SetTheme(bool bUseThemes, string themeFile)
        {
            if (bUseThemes) {
                this.BackColor = Theme.GetThemeColor(themeFile, "Broadcast", null, "BackColor");
                lblMessageCaption.ForeColor = Theme.GetThemeColor(themeFile, "Broadcast", null, "TextColor");
                lblCloseMessage.ForeColor = Theme.GetThemeColor(themeFile, "Broadcast", null, "TextColor");
            }
            else {
                this.BackColor = SystemColors.Control;
                lblMessageCaption.ForeColor = SystemColors.ControlText;
                lblCloseMessage.ForeColor = SystemColors.ControlText;
            }
            this.Invalidate(true);
        }

        private void CloseWindow()
        {
            Properties.Settings.Default.PropertyChanged -= new PropertyChangedEventHandler(Default_PropertyChanged);
            this.Close();
        }

        private void Default_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals("UseThemes") || e.PropertyName.Equals("ThemeFile")) {
                SetTheme(Properties.Settings.Default.UseThemes, Properties.Settings.Default.ThemeFile);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            CloseWindow();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            autoCloseTime--;
            if (autoCloseTime < 1) {
                CloseWindow();
            }

            lblCloseMessage.Text = "This window will close in " + autoCloseTime.ToString() + " seconds.";
        }

        private void BroadcastDialog_Load(object sender, EventArgs e)
        {
            prevWindowState = FormWindowState.Normal;
        }
    }
}
