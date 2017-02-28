using System;
using System.Reflection;
using System.Management;
using System.IO;

namespace LANChat
{
    /// <summary>
    /// Static class that provides information about the application.
    /// </summary>
    internal static class AppInfo
    {
        #region Assembly Attribute Accessors

        public static string AssemblyName
        {
            get
            {
                return Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().CodeBase);
            }
        }

        public static string Title
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
                if (attributes.Length > 0) {
                    AssemblyTitleAttribute titleAttribute = (AssemblyTitleAttribute)attributes[0];
                    if (titleAttribute.Title != "") {
                        return titleAttribute.Title;
                    }
                }
                return Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().CodeBase);
            }
        }

        public static string Version
        {
            get
            {
                return Assembly.GetExecutingAssembly().GetName().Version.ToString();
            }
        }

        public static string Description
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);
                if (attributes.Length == 0) {
                    return "";
                }
                return ((AssemblyDescriptionAttribute)attributes[0]).Description;
            }
        }

        public static string ProductName
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyProductAttribute), false);
                if (attributes.Length == 0) {
                    return "";
                }
                return ((AssemblyProductAttribute)attributes[0]).Product;
            }
        }

        public static string Copyright
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
                if (attributes.Length == 0) {
                    return "";
                }
                return ((AssemblyCopyrightAttribute)attributes[0]).Copyright;
            }
        }

        public static string Company
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCompanyAttribute), false);
                if (attributes.Length == 0) {
                    return "";
                }
                return ((AssemblyCompanyAttribute)attributes[0]).Company;
            }
        }
        #endregion

        /// <summary>
        /// Friendly name of the operating system.
        /// </summary>
        public static string OSName
        {
            get
            {
                string osName = string.Empty;
                using (ManagementObjectSearcher searcher =
                    new ManagementObjectSearcher("Select Caption From Win32_OperatingSystem")) {
                        foreach (ManagementObject obj in searcher.Get()) {
                            osName = obj["Caption"].ToString();
                            break;
                        }
                }
                return osName;
            }
        }

        /// <summary>
        /// Path where all the user data is stored.
        /// </summary>
        public static string DataPath
        {
            get
            {
                string path = Environment.GetFolderPath(
                    Environment.SpecialFolder.LocalApplicationData,
                    Environment.SpecialFolderOption.Create);
                string company = AppInfo.Company;
                string title = AppInfo.Title;
                path = Path.Combine(path, company, title);

                return path;
            }
        }

        /// <summary>
        /// Default path for storing log files.
        /// </summary>
        public static string LogPath
        {
            get
            {
                string path = Path.Combine(DataPath, "Logs");
                return path;
            }
        }

        /// <summary>
        /// Default path for storing theme files.
        /// </summary>
        public static string ThemePath
        {
            get
            {
                string path = Path.Combine(DataPath, "Themes");
                return path;
            }
        }

        /// <summary>
        /// Default path for storing received files.
        /// </summary>
        public static string ReceivedFilePath
        {
            get
            {
                string path = Environment.GetFolderPath(
                    Environment.SpecialFolder.MyDocuments,
                    Environment.SpecialFolderOption.DoNotVerify);
                path = Path.Combine(path, "Received Files");

                return path;
            }
        }

        /// <summary>
        /// Path of the current temporary folder.
        /// </summary>
        public static string TempPath
        {
            get
            {
                string path = Path.Combine(Path.GetTempPath(), AssemblyName + ".tmp");
                return path;
            }
        }
    }
}
