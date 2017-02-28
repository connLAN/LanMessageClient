using System;
using System.Windows.Forms;

namespace LANChat
{
    public class BaseForm : Form
    {
        protected FormWindowState prevWindowState;
        
        /// <summary>
        /// Restores the window to its normal state.
        /// </summary>
        /// <param name="bRestore">If true, restores window to previous state, else window is minimized.</param>
        public virtual void ShowWindow(bool bRestore, bool bActivated)
        {
            //  If window is already visible, just set it to foreground.
            if (this.Visible && this.WindowState != FormWindowState.Minimized) {
                //  If form was activated by user action, bring it to foreground.
                if (bActivated) {
                    Win32.SetForegroundWindow(new System.Runtime.InteropServices.HandleRef(this, this.Handle));
                }
                else {
                    //  Form was shown programatically, no need to bring to foreground if already visible.
                    IntPtr mainHwnd = System.Diagnostics.Process.GetCurrentProcess().MainWindowHandle;
                    if (mainHwnd != this.Handle)
                        FlashWindow();
                }
                return;
            }

            this.Show();
            this.ShowInTaskbar = true;
            if (bRestore) {
                //  Restore window to its previous state.
                this.WindowState = prevWindowState;
                Win32.SetForegroundWindow(new System.Runtime.InteropServices.HandleRef(this, this.Handle));
            }
            else {
                //  Minimize the window, but flash it to alert the user.
                this.WindowState = FormWindowState.Minimized;
                FlashWindow();
            }
        }

        /// <summary>
        /// Hides the window and show a notification.
        /// </summary>
        public virtual void HideWindow()
        {
            this.WindowState = FormWindowState.Minimized;
            this.Hide();
        }

        /// <summary>
        /// This method flashes the window 3 times.
        /// </summary>
        public virtual void FlashWindow()
        {
            Win32.FLASHWINFO fi = Win32.CreateFlashWInfo(this.Handle, Win32.FLASHW_ALL, 3, 0);
            Win32.FlashWindowEx(ref fi);
        }

        /// <summary>
        /// Stops flashing the window.
        /// </summary>
        private void StopFlash()
        {
            Win32.FLASHWINFO fi = Win32.CreateFlashWInfo(this.Handle, Win32.FLASHW_STOP, uint.MaxValue, 0);
            Win32.FlashWindowEx(ref fi);
        }

        protected override void OnActivated(EventArgs e)
        {
            base.OnActivated(e);
            StopFlash();
        }
    }
}
