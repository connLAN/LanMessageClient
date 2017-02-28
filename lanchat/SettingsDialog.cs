using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Reflection;
using System.IO;
using Microsoft.Win32;
using System.Security.Permissions;
using System.Security;

namespace LANChat
{
    public partial class SettingsDialog : Form
    {
        private List<KeyValuePair<string, string>> themeList;

        public SettingsDialog()
        {
            InitializeComponent();
            InitUI();
            
            lnkLabelSilentMode.LostFocus += new EventHandler(lnkLabel_LostFocus);
            lnkFileSecurityRisks.LostFocus += new EventHandler(lnkLabel_LostFocus);

            this.chkLaunchAtStartup.Checked = Properties.Settings.Default.LaunchAtStartup;
            this.chkShowNotifications.Checked = Properties.Settings.Default.ShowSysTrayNotify;
            this.chkSilentMode.Checked = Properties.Settings.Default.SilentMode;
            this.chkAutoRefreshUser.Checked = Properties.Settings.Default.AutoRefreshUser;
            this.txtUserRefreshTime.Text = Properties.Settings.Default.UserRefreshPeriod.ToString();
            this.chkIdleTime.Checked = Properties.Settings.Default.IdleStatusChange;
            this.txtIdleTime.Text = Properties.Settings.Default.IdleTimeMins.ToString();
            this.rdbChatInTab.Checked = !Properties.Settings.Default.ChatWindowed;
            this.rdbChatInWindow.Checked = Properties.Settings.Default.ChatWindowed;
            this.chkUseThemes.Checked = Properties.Settings.Default.UseThemes;
            PopulateThemeList();
            this.chkShowEmoticons.Checked = Properties.Settings.Default.ShowEmoticons;
            this.chkEmotTextToImage.Checked = Properties.Settings.Default.EmotTextToImage;
            this.chkShowTimeStamp.Checked = Properties.Settings.Default.ShowTimeStamp;
            this.chkAddDateToTimeStamp.Checked = Properties.Settings.Default.AddDateToTimeStamp;
            this.rdbMessageToTaskbar.Checked = !Properties.Settings.Default.MessageToForeground;
            this.rdbMessageToForeground.Checked = Properties.Settings.Default.MessageToForeground;
            this.lblFontPreview.Font = Properties.Settings.Default.DefaultFont;
            this.lblFontPreview.ForeColor = Properties.Settings.Default.DefaultFontColor;
            this.lblFontPreview.Text = Properties.Settings.Default.DefaultFont.Name;
            this.chkSaveHistory.Checked = Properties.Settings.Default.SaveHistory;
            //  Set value of text box before setting checked state of radio buttons.
            //  This will enable value of text box to be changed by radio button checked event.
            this.txtLogFilePath.Text = Properties.Settings.Default.LogFile;
            this.rdbDefaultLogFile.Checked = Properties.Settings.Default.UseDefaultLogFile;
            this.rdbCustomLogFile.Checked = !Properties.Settings.Default.UseDefaultLogFile;
            this.chkDisplayAlerts.Checked = Properties.Settings.Default.DisplayAlerts;
            this.chkSuspendAlertsOnBusy.Checked = Properties.Settings.Default.SuspendAlertsOnBusy;
            this.chkSuspendAlertsOnDND.Checked = Properties.Settings.Default.SuspendAlertsOnDND;
            this.chkPlaySounds.Checked = Properties.Settings.Default.PlaySounds;
            this.chkSuspendSoundsOnBusy.Checked = Properties.Settings.Default.SuspendSoundsOnBusy;
            this.chkSuspendSoundsOnDND.Checked = Properties.Settings.Default.SuspendSoundsOnDND;
            this.txtConnectionTimeOut.Text = Properties.Settings.Default.ConnectionTimeOut.ToString();
            this.txtConnectionRetries.Text = Properties.Settings.Default.ConnectionRetries.ToString();
            this.txtBroadcastAddress.Text = Properties.Settings.Default.BroadcastAddress;
            this.txtUDPPort.Text = Properties.Settings.Default.UDPPort.ToString();
            this.txtTCPPort.Text = Properties.Settings.Default.TCPPort.ToString();
            this.rdbFileToTaskbar.Checked = !Properties.Settings.Default.FileToForeground;
            this.rdbFileToForeground.Checked = Properties.Settings.Default.FileToForeground;
            this.chkAutoReceiveFile.Checked = Properties.Settings.Default.AutoReceiveFile;
            this.lblReceivedFileFolder.Text = Properties.Settings.Default.ReceivedFileFolder;
            this.rdbMessageHotKey.Checked = !Properties.Settings.Default.MessageHotKeyMod;
            this.rdbMessageHotKeyMod.Checked = Properties.Settings.Default.MessageHotKeyMod;
        }

        private void InitUI()
        {
            this.ClientSize = new Size(340, 420);
            this.Font = Helper.SystemFont;
            SetControlCaptions(this);
        }

        private string ReplaceText(string text)
        {
            return text.Replace("%TITLE%", AppInfo.Title);
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
                if (component is LANMedia.CustomControls.TextBoxEx) {
                    LANMedia.CustomControls.TextBoxEx textBoxEx = (LANMedia.CustomControls.TextBoxEx)component;
                    textBoxEx.ErrorTitle = "Unacceptable Character";
                    textBoxEx.ErrorText = "You can only type a number here.";
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

        private void PopulateThemeList()
        {
            //  Initialize theme list and add the default theme.
            themeList = new List<KeyValuePair<string, string>>();
            themeList.Add(new KeyValuePair<string, string>(Theme.GetDefaultThemeName(), string.Empty));

            cboTheme.Items.Add(themeList[0].Key);
            cboTheme.SelectedIndex = 0;

            string themePath = AppInfo.ThemePath;
            if (!Directory.Exists(themePath))
                return;

            string[] filePaths = System.IO.Directory.GetFiles(themePath);
            foreach (string filePath in filePaths) {
                string fileName = Path.GetFileName(filePath);
                string themeName = Theme.GetThemeName(fileName);
                if (!themeName.Equals(string.Empty)) {
                    themeList.Add(new KeyValuePair<string, string>(themeName, fileName));
                    cboTheme.Items.Add(themeList[themeList.Count - 1].Key);
                    if (fileName.Equals(Properties.Settings.Default.ThemeFile))
                        cboTheme.SelectedIndex = cboTheme.Items.Count - 1;
                }
            }
        }

        private void CancelValidation(Control control)
        {
            control.CausesValidation = false;
            foreach (Control childControl in control.Controls) {
                CancelValidation(childControl);
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            const int WM_KEYDOWN = 0x0100;

            if (msg.Msg == WM_KEYDOWN && keyData == Keys.Escape)
                CancelValidation(this);
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            //  Do this before changing any other setting. If an error occurs, do not save settings.
            if (string.Compare(txtLogFilePath.Text, History.FilePath, true) != 0) {
                try {
                    if (File.Exists(History.FilePath))
                        File.Move(History.FilePath, txtLogFilePath.Text);
                }
                catch {
                    MessageBox.Show("Please select a different location for history file.",
                        "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    tabControl.SelectedTab = tabPageHistory;
                    //  Prevent dialog from closing.
                    DialogResult = DialogResult.None;
                    return;
                }
            }

            if (Properties.Settings.Default.LaunchAtStartup != chkLaunchAtStartup.Checked)
                Properties.Settings.Default.LaunchAtStartup = chkLaunchAtStartup.Checked;
            if (Properties.Settings.Default.ShowSysTrayNotify != chkShowNotifications.Checked)
                Properties.Settings.Default.ShowSysTrayNotify = chkShowNotifications.Checked;
            if (Properties.Settings.Default.SilentMode != chkSilentMode.Checked)
                Properties.Settings.Default.SilentMode = chkSilentMode.Checked;
            if (Properties.Settings.Default.AutoRefreshUser != chkAutoRefreshUser.Checked)
                Properties.Settings.Default.AutoRefreshUser = chkAutoRefreshUser.Checked;
            if (Properties.Settings.Default.UserRefreshPeriod != int.Parse(txtUserRefreshTime.Text))
                Properties.Settings.Default.UserRefreshPeriod = int.Parse(txtUserRefreshTime.Text);
            if (Properties.Settings.Default.IdleStatusChange != chkIdleTime.Checked)
                Properties.Settings.Default.IdleStatusChange = chkIdleTime.Checked;
            if (Properties.Settings.Default.IdleTimeMins != int.Parse(txtIdleTime.Text))
                Properties.Settings.Default.IdleTimeMins = int.Parse(txtIdleTime.Text);
            if (Properties.Settings.Default.ChatWindowed != rdbChatInWindow.Checked)
                Properties.Settings.Default.ChatWindowed = rdbChatInWindow.Checked;
            if (Properties.Settings.Default.UseThemes != chkUseThemes.Checked)
                Properties.Settings.Default.UseThemes = chkUseThemes.Checked;
            if (Properties.Settings.Default.ThemeFile != themeList[cboTheme.SelectedIndex].Value)
                Properties.Settings.Default.ThemeFile = themeList[cboTheme.SelectedIndex].Value;
            if (Properties.Settings.Default.ShowEmoticons != chkShowEmoticons.Checked)
                Properties.Settings.Default.ShowEmoticons = chkShowEmoticons.Checked;
            if (Properties.Settings.Default.EmotTextToImage != chkEmotTextToImage.Checked)
                Properties.Settings.Default.EmotTextToImage = chkEmotTextToImage.Checked;
            if (Properties.Settings.Default.ShowTimeStamp != chkShowTimeStamp.Checked)
                Properties.Settings.Default.ShowTimeStamp = chkShowTimeStamp.Checked;
            if (Properties.Settings.Default.AddDateToTimeStamp != chkAddDateToTimeStamp.Checked)
                Properties.Settings.Default.AddDateToTimeStamp = chkAddDateToTimeStamp.Checked;
            if (Properties.Settings.Default.MessageToForeground != rdbMessageToForeground.Checked)
                Properties.Settings.Default.MessageToForeground = rdbMessageToForeground.Checked;
            if (Properties.Settings.Default.DefaultFont != lblFontPreview.Font)
                Properties.Settings.Default.DefaultFont = lblFontPreview.Font;
            if (Properties.Settings.Default.DefaultFontColor != lblFontPreview.ForeColor)
                Properties.Settings.Default.DefaultFontColor = lblFontPreview.ForeColor;
            if (Properties.Settings.Default.SaveHistory != chkSaveHistory.Checked)
                Properties.Settings.Default.SaveHistory = chkSaveHistory.Checked;
            if (Properties.Settings.Default.UseDefaultLogFile != rdbDefaultLogFile.Checked)
                Properties.Settings.Default.UseDefaultLogFile = rdbDefaultLogFile.Checked;
            if (Properties.Settings.Default.LogFile != txtLogFilePath.Text)
                Properties.Settings.Default.LogFile = txtLogFilePath.Text;
            if (Properties.Settings.Default.DisplayAlerts != chkDisplayAlerts.Checked)
                Properties.Settings.Default.DisplayAlerts = chkDisplayAlerts.Checked;
            if (Properties.Settings.Default.SuspendAlertsOnBusy != chkSuspendAlertsOnBusy.Checked)
                Properties.Settings.Default.SuspendAlertsOnBusy = chkSuspendAlertsOnBusy.Checked;
            if (Properties.Settings.Default.SuspendAlertsOnDND != chkSuspendAlertsOnDND.Checked)
                Properties.Settings.Default.SuspendAlertsOnDND = chkSuspendAlertsOnDND.Checked;
            if (Properties.Settings.Default.PlaySounds != chkPlaySounds.Checked)
                Properties.Settings.Default.PlaySounds = chkPlaySounds.Checked;
            if (Properties.Settings.Default.SuspendSoundsOnBusy != chkSuspendSoundsOnBusy.Checked)
                Properties.Settings.Default.SuspendSoundsOnBusy = chkSuspendSoundsOnBusy.Checked;
            if (Properties.Settings.Default.SuspendSoundsOnDND != chkSuspendSoundsOnDND.Checked)
                Properties.Settings.Default.SuspendSoundsOnDND = chkSuspendSoundsOnDND.Checked;
            if (Properties.Settings.Default.ConnectionTimeOut != int.Parse(txtConnectionTimeOut.Text))
                Properties.Settings.Default.ConnectionTimeOut = int.Parse(txtConnectionTimeOut.Text);
            if (Properties.Settings.Default.ConnectionRetries != int.Parse(txtConnectionRetries.Text))
                Properties.Settings.Default.ConnectionRetries = int.Parse(txtConnectionRetries.Text);
            if (Properties.Settings.Default.BroadcastAddress != txtBroadcastAddress.Text)
                Properties.Settings.Default.BroadcastAddress = txtBroadcastAddress.Text;
            if (Properties.Settings.Default.UDPPort != int.Parse(txtUDPPort.Text))
                Properties.Settings.Default.UDPPort = int.Parse(txtUDPPort.Text);
            if (Properties.Settings.Default.TCPPort != int.Parse(txtTCPPort.Text))
                Properties.Settings.Default.TCPPort = int.Parse(txtTCPPort.Text);
            if (Properties.Settings.Default.FileToForeground != rdbFileToForeground.Checked)
                Properties.Settings.Default.FileToForeground = rdbFileToForeground.Checked;
            if (Properties.Settings.Default.AutoReceiveFile != chkAutoReceiveFile.Checked)
                Properties.Settings.Default.AutoReceiveFile = chkAutoReceiveFile.Checked;
            if (Properties.Settings.Default.ReceivedFileFolder != lblReceivedFileFolder.Text)
                Properties.Settings.Default.ReceivedFileFolder = lblReceivedFileFolder.Text;
            if (Properties.Settings.Default.MessageHotKeyMod != rdbMessageHotKeyMod.Checked)
                Properties.Settings.Default.MessageHotKeyMod = rdbMessageHotKeyMod.Checked;
            Properties.Settings.Default.Save();

            Properties.Settings.Default.Synchronize();
        }

        private void btnChangeFont_Click(object sender, EventArgs e)
        {
            fontDialog.Font = lblFontPreview.Font;
            fontDialog.Color = lblFontPreview.ForeColor;
            if (fontDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK) {
                lblFontPreview.Font = fontDialog.Font;
                lblFontPreview.ForeColor = fontDialog.Color;
            }
            lblFontPreview.Text = lblFontPreview.Font.Name;
        }

        private void btnClearHistory_Click(object sender, EventArgs e)
        {
            btnClearHistory.Enabled = false;
            History.Clear();
            if (HistoryViewer.Instance != null) {
                HistoryViewer.Instance.Invoke(HistoryViewer.RefreshMessages);
            }
            btnClearHistory.Enabled = true;
        }

        private void btnChangeReceivedFileFolder_Click(object sender, EventArgs e)
        {
            folderBrowserDialog.Description = "Choose a folder for storing received files";
            folderBrowserDialog.SelectedPath = lblReceivedFileFolder.Text;
            if (folderBrowserDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK) {
                lblReceivedFileFolder.Text = folderBrowserDialog.SelectedPath;
            }
        }

        private void DisplayLinkMessage(object sender, string text)
        {
            Control control = (Control)sender;
            Point point = (control.PointToClient(Cursor.Position));
            if (control.DisplayRectangle.Contains(point))
                point.Y += 20;
            else
                point = new Point(control.DisplayRectangle.Left, control.DisplayRectangle.Bottom + 4);
            toolTipInfo.Show(text, (IWin32Window)sender, point);
        }

        private void lnkLabel_MouseLeave(object sender, EventArgs e)
        {
            toolTipInfo.Hide((IWin32Window)sender);
        }

        private void lnkLabel_LostFocus(object sender, EventArgs e)
        {
            toolTipInfo.Hide((IWin32Window)sender);
        }

        private void lnkLabelSilentMode_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string text =   "When Silent mode is enabled notifications and\n" +
                            "conversations are not displayed on the screen.\n" +
                            "You can still view conversations by opening the\n" +
                            "window from system tray.";
            DisplayLinkMessage(sender, text);
        }
        
        private void lnkFileSecurityRisks_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string text =   "Files can contain viruses and other malicious software. If\n" +
                            "you are not expecting a file, verify with the sender that\n"+
                            "the file was sent intentionally and that it is safe. If the file\n"+
                            "is from an unknown source, exercise caution. Always use\n"+
                            "an antivirus program to scan files that you receive.";
            DisplayLinkMessage(sender, text);
        }

        private void chkUseThemes_CheckedChanged(object sender, EventArgs e)
        {
            cboTheme.Enabled = chkUseThemes.Checked;
        }

        private void btnViewFiles_Click(object sender, EventArgs e)
        {
            try {
                string path = lblReceivedFileFolder.Text;
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                System.Diagnostics.Process.Start(path);
            }
            catch {
            }
        }

        private void chkShowTimeStamp_CheckedChanged(object sender, EventArgs e)
        {
            chkAddDateToTimeStamp.Enabled = chkShowTimeStamp.Checked;
        }

        private void rdbDefaultLogFile_CheckedChanged(object sender, EventArgs e)
        {
            txtLogFilePath.Text = Path.Combine(AppInfo.DataPath, History.FileName);
        }

        private void rdbCustomLogFile_CheckedChanged(object sender, EventArgs e)
        {
            txtLogFilePath.Enabled = rdbCustomLogFile.Checked;
            btnSelectLogFile.Enabled = rdbCustomLogFile.Checked;
        }

        private void btnSelectLogFile_Click(object sender, EventArgs e)
        {
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
                txtLogFilePath.Text = saveFileDialog.FileName;
        }

        private void saveFileDialog_FileOk(object sender, CancelEventArgs e)
        {
            if (!HasAccessPermission(saveFileDialog.FileName)) {
                MessageBox.Show("You do not have permission to save here.\nPlease select a different location.", 
                    saveFileDialog.Title, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Cancel = true;
            }
        }

        private bool HasAccessPermission(string path)
        {
            FileIOPermission accessPermission = new FileIOPermission(FileIOPermissionAccess.Read | FileIOPermissionAccess.Write, path);
            if (!SecurityManager.IsGranted(accessPermission))
                return false;

            try {
                using (FileStream fStream = new FileStream(path, FileMode.Create)) {
                    using (TextWriter writer = new StreamWriter(fStream))
                        writer.WriteLine("Test");
                }
                File.Delete(path);
                return true;
            }
            catch (UnauthorizedAccessException ex) {
                return false;
            }
        }

        private void chkIdleTime_CheckedChanged(object sender, EventArgs e)
        {
            txtIdleTime.Enabled = chkIdleTime.Checked;
        }

        private void chkAutoRefreshUser_CheckedChanged(object sender, EventArgs e)
        {
            txtUserRefreshTime.Enabled = chkAutoRefreshUser.Checked;
        }
    }
}