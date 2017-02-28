using Microsoft.Win32;
using System.Windows.Forms;

namespace LANChat.Properties {
    
    /// <summary>
    /// The primary purpose of this file is to set the default SettingsProvider for all settings.
    /// The default provider is set as LocalSettingsProvider. This avoids having to specify the
    /// SettingsProvider for each setting individually in the settings designer.
    /// </summary>
    [global::System.Configuration.SettingsProviderAttribute(typeof(LocalSettingsProvider))]
    internal sealed partial class Settings {
        
        public Settings() {
        }

        /// <summary>
        /// Synchronizes the values of settings with their external dependencies.
        /// </summary>
        public void Synchronize()
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Run", true);
            if (this.LaunchAtStartup == true)
                key.SetValue(AppInfo.Title, Application.ExecutablePath, RegistryValueKind.String);
            else
                key.DeleteValue(AppInfo.Title, false);
            key.Close();
        }

        /// <summary>
        /// Synchronizes registry key with application settings and clear FirstRun flag.
        /// </summary>
        public void RegisterSettings()
        {
            Synchronize();
            this.FirstRun = true;
            this.Save();
        }
    }
}
