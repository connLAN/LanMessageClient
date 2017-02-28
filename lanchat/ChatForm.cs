using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace LANChat
{
    public partial class ChatForm : BaseForm
    {
        int themeId = 0;

        public ChatForm()
        {
            InitializeComponent();
            InitUI();

            Properties.Settings.Default.PropertyChanged += new PropertyChangedEventHandler(Default_PropertyChanged);
        }

        #region Properties
        bool bEnabled = true;
        public bool Enabled
        {
            get { return bEnabled; }
            set { bEnabled = value; SetBackColor(); }
        }

        bool bUseSystemStyle = true;
        public bool UseSystemStyle
        {
            get { return bUseSystemStyle; }
            set { bUseSystemStyle = value; }
        }

        private string text;
        public string Text
        {
            get { return text; }
            set { text = value; base.Text = text + " - Conversation"; }
        }

        private Color defaultColor;
        public Color DefaultColor
        {
            get { return defaultColor; }
            set { defaultColor = value; }
        }

        private Color disabledColor;
        public Color DisabledColor
        {
            get { return disabledColor; }
            set { disabledColor = value; }
        }
        #endregion

        private void InitUI()
        {
            this.Icon = Helper.GetAssociatedIcon(Application.ExecutablePath);
            this.ClientSize = new Size(490, 450);
            this.Location = new Point((SystemInformation.WorkingArea.Width - this.Width) / 2,
                                    (SystemInformation.WorkingArea.Height - this.Height) / 2);

            //  Set visual style of window according to current theme.
            SetTheme(Properties.Settings.Default.UseThemes, Properties.Settings.Default.ThemeFile);
        }

        private void SetBackColor()
        {
            this.BackColor = bEnabled ? DefaultColor : DisabledColor;
        }

        private void SetTheme(bool bUseThemes, string themeFile)
        {
            if (bUseThemes) {
                this.DefaultColor = Theme.GetThemeColor(themeFile, "Main", "ChatPane", "SelectedTabColor");
                this.DisabledColor = Theme.GetThemeColor(themeFile, "Main", "ChatPane", "DisabledTabColor");
            }
            else {
                this.DefaultColor = GetSysColorDefault();
                this.DisabledColor = GetSysColorDisabled();
            }
            SetBackColor();
            //  Cause form and child controls to redraw.
            this.Invalidate(true);
        }

        private Color GetSysColorDefault()
        {
            if (TabRenderer.IsSupported)
                return SystemColors.Window;
            else
                return SystemColors.Control;
        }

        private Color GetSysColorDisabled()
        {
            if (TabRenderer.IsSupported)
                return SystemColors.ScrollBar;
            else
                return SystemColors.Control;
        }

        private void Default_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals("UseThemes") || e.PropertyName.Equals("ThemeFile")) {
                SetTheme(Properties.Settings.Default.UseThemes, Properties.Settings.Default.ThemeFile);
            }
        }

        private void ChatForm_Load(object sender, EventArgs e)
        {
            prevWindowState = FormWindowState.Normal;
        }

        private void ChatForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            //  If window was closed by user, close to system tray instead
            //  of terminating application.
            if (e.CloseReason == CloseReason.UserClosing) {
                e.Cancel = true;
                HideWindow();
            }
        }

        private void ChatForm_Resize(object sender, EventArgs e)
        {
            //  Remember state of window before minimizing.
            if (WindowState != FormWindowState.Minimized)
                prevWindowState = WindowState;
        }
    }
}
