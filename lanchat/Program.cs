using System;
using System.Windows.Forms;
using System.Diagnostics;
using System.Management;
using System.IO;

namespace LANChat
{
    static class Program
    {
        static Event _event;        //  This event handles multiple instances.
        static Event _termEvent;    //  This event handles termination of application instance.

        public static bool DebugMode;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            foreach (string argument in Environment.GetCommandLineArgs()) {
                //  /sync switch sunchronizes application settings and their external dependencies.
                //  Returns 0.
                if (argument.Equals("/sync")) {
                    Properties.Settings.Default.RegisterSettings();
                    Environment.Exit(0);
                    return;
                }
                //  /inst switch checks if an instance of the application is already running.
                //  Returns 1 if yes, 0 otherwise.
                if (argument.Equals("/inst")) {
                    _event = new Event(GlobalEvents.Instance);
                    if (_event.Exists()) 
                        Environment.Exit(1);
                    else 
                        Environment.Exit(0);
                    return;
                }
                //  /term switch kills an instance of the application that is already running.
                //  Returns 0.
                if (argument.Equals("/term")) {
                    _event = new Event(GlobalEvents.Terminate);
                    if (_event.Exists()) 
                        _event.SignalEvent();
                    Environment.Exit(0);
                    return;
                }
                //  /? switch displays a message box listing all possible command line switches.
                //  Return 0.
                if (argument.Equals("/?")) {
                    string text = (string)Properties.Resources.ResourceManager.GetObject("switches");
                    MessageBox.Show(text, AppInfo.Title);
                    Environment.Exit(0);
                    return;
                }
                //  /debug switch starts the application in debug mode, if it is not running.
                //  Has no effect if an instance of the application is already running.
                if (argument.Equals("/debug")) {
                    DebugMode = true;
                }
                //  /nohistory switch clears existing history. This switch should be processed
                //  before /noconfig switch or the path to log file may not be found.
                if (argument.Equals("/nohistory")) {
                    History.Clear();
                }
                //  /noconfig switch deletes the preference file.
                if (argument.Equals("/noconfig")) {
                    File.Delete(Path.Combine(AppInfo.DataPath, "user.config"));
                }
                //  /quit switch closes the application once all command line params are processed.
                //  Returns 0.
                if (argument.Equals("/quit")) {
                    Environment.Exit(0);
                    return;
                }
            }

            if (IsUserDifferent()) {
                MessageBox.Show("A user is already running " + AppInfo.Title + ". Only one instance is possible at a time.", 
                    AppInfo.Title,
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                Environment.Exit(1);
                return;
            }

            if (!VerifyModules()) {
                Environment.Exit(1);
                return;
            }

            _event = new Event();
            _termEvent = new Event(GlobalEvents.Terminate);

            if (_event.Exists()) {
                _event.SignalEvent();
            }
            else {
                MainForm form = new MainForm();
                IntPtr hWnd = form.Handle;
                _event.SetObject(form);
                _termEvent.SetObject(form);
                Application.Run(form);
            }
            Environment.Exit(0);
        }

        /// <summary>
        /// This functions checks whether any previous instance of the application
        /// is run by a different user logged in to the machine.
        /// </summary>
        /// <returns></returns>
        static bool IsUserDifferent()
        {
            //  Get the user name of this process.
            string currentUser = GetProcessInfo(Process.GetCurrentProcess().Id);
            Process[] processes = Process.GetProcessesByName(Process.GetCurrentProcess().ProcessName);
            if (processes.Length > 1) {
                //  Get the user names of all instances of this application.
                foreach (Process process in processes) {
                    string processUser = GetProcessInfo(process.Id);
                    //  For some reason this value is null when user is different.
                    if (processUser == null)
                        return true;
                    //  If an process has a different user name, it means
                    //  another user is running the application. Return false.
                    if (!processUser.Equals(currentUser))
                        return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Gets the user name of the owner of a process.
        /// </summary>
        /// <param name="id">Process Id.</param>
        /// <returns>The user name.</returns>
        static string GetProcessInfo(int id)
        {
            string userName = string.Empty;
            ManagementObjectSearcher searcher = null;
            try {
                string query = "Select * From Win32_Process Where ProcessID = " + id;
                searcher = new ManagementObjectSearcher(query);
                ManagementObjectCollection processList = searcher.Get();
                foreach (ManagementObject obj in processList) {
                    string[] argList = new string[] { string.Empty };
                    int retVal = Convert.ToInt32(obj.InvokeMethod("GetOwner", argList));
                    userName = argList[0];
                    break;
                }
                searcher.Dispose();
                return userName;
            }
            catch {
                searcher.Dispose();
                return userName;
            }
        }

        /// <summary>
        /// Check if the dll files needed are present in application folder.
        /// </summary>
        /// <returns>True if present, False otherwise.</returns>
        static bool VerifyModules()
        {
            string appFolder = Path.GetDirectoryName(Application.ExecutablePath);
            if (!File.Exists(Path.Combine(appFolder, "lanres.dll")) ||
                !File.Exists(Path.Combine(appFolder, "lanui.dll"))) {
                ErrorHandler.ShowError(new FileNotFoundException(),
                    AppInfo.Title + " has failed to start because a component was not found.\n" + 
                    "Please re-install " + AppInfo.Title + " to fix this problem.");
                return false;
            }
            return true;
        }
    }
}