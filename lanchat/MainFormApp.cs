using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Net;
using System.IO;
using LANMedia.CustomControls;

namespace LANChat
{
    /// <summary>
    /// All the application related functionalities are defined here.
    /// </summary>
    partial class MainForm
    {
        #region  Application level events
        private void Default_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            UpdateSettings(e.PropertyName);
        }

        public void Application_EventSignalled()
        {
            ShowWindow(true, true);
        }

        public void Application_TermEventSignalled()
        {
            //  Quit application.
            Exit();
        }
        #endregion

        /// <summary>
        /// Initializes application level data.
        /// </summary>
        private void InitApplication()
        {
            Properties.Settings.Default.PropertyChanged += new PropertyChangedEventHandler(Default_PropertyChanged);
            this.EventSignalled += new Event.EventSignalHandler(this.Application_EventSignalled);
            this.TermEventSignalled += new Event.EventSignalHandler(this.Application_TermEventSignalled);
        }

        private void StartApplication()
        {
            //  Load settings from application settings.
            if (!LoadSettings()) {
                Application.Exit();
                return;
            }

            //  Check if any parameters have been specified in the command line arguments.
            GetOverrideParams();
            //  Perform tasks that should be done the first time application runs.
            DoFirstRun();
        }

        /// <summary>
        /// If any parameters are specified as command line argument, they take
        /// precedence over values loaded from settings file.
        /// </summary>
        private void GetOverrideParams()
        {
            //  Parse command line arguments.
            foreach (string commandLineArg in Environment.GetCommandLineArgs()) {
                //  Check if a port number is specified with /port switch.
                //  Syntax /port:PortNumber. PortNumber should be a valid port number.
                if (commandLineArg.StartsWith("/port:")) {
                    string[] subArgs = commandLineArg.Split(":".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                    if (subArgs.Length == 2) {
                        int port;
                        if (int.TryParse(subArgs[1], out port)) {
                            if (port >= 0 && port <= 65535)
                                portNumber = port;
                        }
                    }
                }
            }
        }

        private void SetStatus(string status)
        {
            localStatus = status;
            UpdateUserStatusImages(localUserKey, localStatus);
            SendBroadcast(DatagramTypes.Notify, localStatus);
        }

        private void SetSilentMode(bool bSilent)
        {
            bSilentMode = bSilent;
            mnuNotifySilentMode.Checked = bSilentMode;
        }

        private void LoadChatControlSettings(ChatControl chatControl)
        {
            chatControl.HotKeyMod = Properties.Settings.Default.MessageHotKeyMod;
            chatControl.MessageToForeground = Properties.Settings.Default.MessageToForeground;
            chatControl.SilentMode = Properties.Settings.Default.SilentMode;
            chatControl.ShowEmoticons = Properties.Settings.Default.ShowEmoticons;
            chatControl.EmotTextToImage = Properties.Settings.Default.EmotTextToImage;
            chatControl.ShowTimeStamp = Properties.Settings.Default.ShowTimeStamp;
            chatControl.AddDateToTimeStamp = Properties.Settings.Default.AddDateToTimeStamp;
        }

        /// <summary>
        /// Load chat settings and apply to all the Chat controls inside inside tab pages and chat windows.
        /// </summary>
        private void LoadChatSettings()
        {
            for (int index = 0; index < tabList.Count; index++) {
                TabPageEx tabPage = tabList[index];
                ChatControl chatControl = (ChatControl)tabPage.Controls["ChatControl"];
                LoadChatControlSettings(chatControl);
            }
            for (int index = 0; index < windowList.Count; index++) {
                ChatForm chatForm = windowList[index];
                ChatControl chatControl = (ChatControl)chatForm.Controls["ChatControl"];
                LoadChatControlSettings(chatControl);
            }
        }

        /// <summary>
        /// Load settings from the settings file. This method also checks if all files needed by
        /// the application is present.
        /// </summary>
        /// <returns></returns>
        private bool LoadSettings()
        {
            try {
                int iConfigVersion = Properties.Settings.Default.ConfigVersion;

                //  The broadcast address.
                broadcastAddress = IPAddress.Parse(Properties.Settings.Default.BroadcastAddress);
                
                //  The port to be used for sending and receiving. Stored in settings.
                //  Do not change port number once Sockets have been bound.
                if (receiveUdpClient == null)
                    portNumber = Properties.Settings.Default.UDPPort;

                //  Flag to determine if system tray notifications should be displayed.
                bShowNotifications = Properties.Settings.Default.ShowSysTrayNotify;

                //  Set silent mode according to the stored settings.
                SetSilentMode(Properties.Settings.Default.SilentMode);

                //  Status of local user.
                localStatus = Properties.Settings.Default.UserStatus;

                bBkgndMessageShown = !Properties.Settings.Default.ShowMinimizeMessage;

                //  Flag to determine if new conversations should be opened in tabs or windows.
                bChatWindowed = Properties.Settings.Default.ChatWindowed;

                //  Set visual style according to theme.
                SetTheme(Properties.Settings.Default.UseThemes, Properties.Settings.Default.ThemeFile);

                //  Flag to determine window behavior for new incoming message.
                bMessageToForeground = Properties.Settings.Default.MessageToForeground;

                //  Flags for alerts and sounds.
                bDisplayAlerts = Properties.Settings.Default.DisplayAlerts;
                bSuspendAlertsOnBusy = Properties.Settings.Default.SuspendAlertsOnBusy;
                bSuspendAlertsOnDND = Properties.Settings.Default.SuspendAlertsOnDND;
                bPlaySounds = Properties.Settings.Default.PlaySounds;
                bSuspendSoundsOnBusy = Properties.Settings.Default.SuspendSoundsOnBusy;
                bSuspendSoundsOnDND = Properties.Settings.Default.SuspendSoundsOnDND;

                //  Connection time out period in seconds.
                connectionTimeout = Properties.Settings.Default.ConnectionTimeOut;

                //  Number of retries if connection dropped.
                connectionRetries = Properties.Settings.Default.ConnectionRetries;

                //  Time period before the user list is updated automatically.
                userRefreshPeriod = Properties.Settings.Default.UserRefreshPeriod;
                timerUserUpdate.Interval = userRefreshPeriod * 1000;

                //  Time period after which system is considered idle.
                //  Convert property value from minutes to milliseconds.
                idleTimeMax = Properties.Settings.Default.IdleTimeMins * 60 * 1000;

                //  Default font used for messaging.
                defaultFont = Properties.Settings.Default.DefaultFont;
                if (defaultFont == null)
                    Properties.Settings.Default.DefaultFont = Definitions.GetPresetFont(FormatTypes.Message);

                //  Color of default font.
                defaultFontColor = Properties.Settings.Default.DefaultFontColor;

                //  Folder for storing received files. If it is not set, assign default value.
                string receivedFileFolder = Properties.Settings.Default.ReceivedFileFolder;
                if (receivedFileFolder.Equals(string.Empty))
                    Properties.Settings.Default.ReceivedFileFolder = AppInfo.ReceivedFilePath;

                //  Load chat settings and apply to all the Chat controls inside tab pages/chat windows.
                LoadChatSettings();

                return true;
            }
            catch (Exception ex) {
                ErrorHandler.ShowError(ex);
                return false;
            }
        }

        private void UpdateChatControlSettings(ChatControl chatControl, string propertyName)
        {
            switch (propertyName) {
                case "MessageHotKeyMod":
                    chatControl.HotKeyMod = Properties.Settings.Default.MessageHotKeyMod;
                    break;
                case "MessageToForeground":
                    chatControl.MessageToForeground = Properties.Settings.Default.MessageToForeground;
                    break;
                case "SilentMode":
                    chatControl.SilentMode = Properties.Settings.Default.SilentMode;
                    break;
                case "ShowEmoticons":
                    chatControl.ShowEmoticons = Properties.Settings.Default.ShowEmoticons;
                    break;
                case "EmotTextToImage":
                    chatControl.EmotTextToImage = Properties.Settings.Default.EmotTextToImage;
                    break;
                case "ShowTimeStamp":
                    chatControl.ShowTimeStamp = Properties.Settings.Default.ShowTimeStamp;
                    break;
                case "AddDateToTimeStamp":
                    chatControl.AddDateToTimeStamp = Properties.Settings.Default.AddDateToTimeStamp;
                    break;
            }
        }

        private void UpdateChatSettings(string propertyName)
        {
            for (int index = 0; index < tabList.Count; index++) {
                TabPageEx tabPage = tabList[index];
                ChatControl chatControl = (ChatControl)tabPage.Controls["ChatControl"];
                UpdateChatControlSettings(chatControl, propertyName);
            }
            for (int index = 0; index < windowList.Count; index++) {
                ChatForm chatForm = windowList[index];
                ChatControl chatControl = (ChatControl)chatForm.Controls["ChatControl"];
                UpdateChatControlSettings(chatControl, propertyName);
            }
        }

        private void UpdateSettings(string propertyName)
        {
            switch (propertyName) {
                case "BroadcastAddress":
                    //  The broadcast address.
                    broadcastAddress = IPAddress.Parse(Properties.Settings.Default.BroadcastAddress);
                    break;
                case "ShowSysTrayNotify":
                    //  Flag to determine if system tray notifications should be displayed.
                    bShowNotifications = Properties.Settings.Default.ShowSysTrayNotify;
                    break;
                case "SilentMode":
                    //  Set silent mode according to the stored settings.
                    SetSilentMode(Properties.Settings.Default.SilentMode);
                    break;
                case "ChatWindowed":
                    //  Flag to determine if new conversations should be opened in tabs or windows.
                    SwitchWindowMode(Properties.Settings.Default.ChatWindowed);
                    break;
                case "UseThemes":
                case "ThemeFile":
                    //  Set visual style according to theme.
                    SetTheme(Properties.Settings.Default.UseThemes, Properties.Settings.Default.ThemeFile);
                    break;
                case "MessageToForeground":
                    //  Flag to determine window behavior for new incoming message.
                    bMessageToForeground = Properties.Settings.Default.MessageToForeground;
                    break;
                case "DisplayAlerts":
                    //  Flag to determing if user status alerts should be shown.
                    bDisplayAlerts = Properties.Settings.Default.DisplayAlerts;
                    break;
                case "SuspendAlertsOnBusy":
                    bSuspendAlertsOnBusy = Properties.Settings.Default.SuspendAlertsOnBusy;
                    break;
                case "SuspendAlertsOnDND":
                    bSuspendAlertsOnDND = Properties.Settings.Default.SuspendAlertsOnDND;
                    break;
                case "PlaySounds":
                    bPlaySounds = Properties.Settings.Default.PlaySounds;
                    break;
                case "SuspendSoundsOnBusy":
                    bSuspendSoundsOnBusy = Properties.Settings.Default.SuspendSoundsOnBusy;
                    break;
                case "SuspendSoundsOnDND":
                    bSuspendSoundsOnDND = Properties.Settings.Default.SuspendSoundsOnDND;
                    break;
                case "ConnectionTimeOut":
                    //  Connection time out period in seconds.
                    connectionTimeout = Properties.Settings.Default.ConnectionTimeOut;
                    break;
                case "ConnectionRetries":
                    //  Number of retries if connection dropped.
                    connectionRetries = Properties.Settings.Default.ConnectionRetries;
                    break;
                case "UserRefreshPeriod":
                    //  Time period before the user list is updated automatically.
                    userRefreshPeriod = Properties.Settings.Default.UserRefreshPeriod;
                    timerUserUpdate.Interval = userRefreshPeriod * 1000;
                    break;
                case "IdleTimeMins":
                    //  Time period after which system is considered idle.
                    //  Convert property value from minutes to milliseconds.
                    idleTimeMax = Properties.Settings.Default.IdleTimeMins * 60 * 1000;
                    break;
                case "DefaultFont":
                    //  Default font used for messaging.
                    defaultFont = Properties.Settings.Default.DefaultFont;
                    break;
                case "DefaultFontColor":
                    //  Color of default font.
                    defaultFontColor = Properties.Settings.Default.DefaultFontColor;
                    break;
            }

            //  Propogate any relevant settings to the Chat controls inside tab pages.
            UpdateChatSettings(propertyName);
        }

        private void SaveSettings()
        {
            Properties.Settings.Default.UserStatus = localStatus;
            Properties.Settings.Default.Save();
            Properties.Settings.Default.Synchronize();
            SaveGroupData();
        }

        private void SaveHistory()
        {
            //  Save history if that option is selected.
            if (Properties.Settings.Default.SaveHistory) {
                string path = AppInfo.LogPath;
                if (!System.IO.Directory.Exists(path))
                    System.IO.Directory.CreateDirectory(path);

                for (int index = 0; index < tabList.Count; index++) {
                    TabPageEx tabPage = tabList[index];
                    ChatControl chatControl = (ChatControl)tabPage.Controls["ChatControl"];
                    chatControl.SaveHistory(path);
                }
            }
        }

        private void LoadGroupData()
        {
            //  Get list of user groups from file.
            string path = Path.Combine(AppInfo.DataPath, "groups.bin");
            if (File.Exists(path)) {
                try {
                    using (Stream stream = File.Open(path, FileMode.Open)) {
                        System.Runtime.Serialization.Formatters.Binary.BinaryFormatter formatter =
                            new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();

                        try {
                            groupList = (List<string>)formatter.Deserialize(stream);
                        }
                        catch {
                            //  Could not read group data from file. Use default list.
                            groupList.Clear();
                            groupList.Add(Properties.Settings.Default.DefaultGroupName);
                        }

                        try {
                            userGroupLookup = (Dictionary<string, string>)formatter.Deserialize(stream);
                        }
                        catch {
                            //  Could not retrieve user group lookup table from file.
                            userGroupLookup.Clear();
                        }
                    }
                }
                catch (Exception ex) {
                    ErrorHandler.ShowError(ex);
                }
            }
            else {
                //  File does not exist. Create default group list.
                groupList.Clear();
                groupList.Add(Properties.Settings.Default.DefaultGroupName);
            }
        }

        private void SaveGroupData()
        {
            //  Save list of user groups to file.
            string path = Path.Combine(AppInfo.DataPath, "groups.bin");
            try {
                using (Stream stream = File.Open(path, FileMode.Create)) {
                    System.Runtime.Serialization.Formatters.Binary.BinaryFormatter formatter =
                        new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();

                    formatter.Serialize(stream, groupList);
                    formatter.Serialize(stream, userGroupLookup);
                }
            }
            catch (Exception ex) {
                ErrorHandler.ShowError(ex);
            }
        }

        private void DoFirstRun()
        {
            if (!Properties.Settings.Default.FirstRun)
                return;

            //  Replace value of ConfigVersion property with the default value defined in the application.
            Properties.Settings.Default.PropertyValues["ConfigVersion"].PropertyValue = 
                Properties.Settings.Default.Properties["ConfigVersion"].DefaultValue;

            History.ConvertLegacy();
            Properties.Settings.Default.FirstRun = false;
            Properties.Settings.Default.Save();
        }

        private void DeleteTemp()
        {
            try {
                string path = AppInfo.TempPath;
                if (Directory.Exists(path))
                    Directory.Delete(path, true);
            }
            catch {
            }
        }

        /// <summary>
        /// Exits the application.
        /// </summary>
        private void Exit()
        {
            //  Broadcast a depart message to all remote users.
            SendBroadcast(DatagramTypes.Depart, string.Empty);
            //  Save chat history.
            SaveHistory();
            //  Save all settings.
            SaveSettings();
            //  Delete all temp files
            DeleteTemp();
            //  Exit the application message loop.
            System.Windows.Forms.Application.Exit();
        }
    }
}
