using System;
using System.Windows.Forms;

namespace LANChat
{
    internal static class ErrorHandler
    {
        public static void ShowError(Exception ex)
        {
            ShowError(ex, string.Empty);
        }

        public static void ShowError(Exception ex, string message)
        {
            if (Program.DebugMode)
                MessageBox.Show(ex.Message + Environment.NewLine + ex.StackTrace);
            else {
                if (!message.Equals(string.Empty))
                    MessageBox.Show(message, AppInfo.Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
