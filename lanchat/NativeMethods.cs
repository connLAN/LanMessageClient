using System;
using System.Runtime.InteropServices;

namespace LANChat
{
    /// <summary>
    /// Defines all the native methods that are called through Platform Invoke.
    /// </summary>
    internal static class Win32
    {
        [StructLayout(LayoutKind.Sequential)]
        public struct FLASHWINFO
        {
            /// <summary>
            /// The size of the structure in bytes.
            /// </summary>
            public uint cbSize;
            /// <summary>
            /// A Handle to the Window to be Flashed. The window can be either opened or minimized.
            /// </summary>
            public IntPtr hwnd;
            /// <summary>
            /// The Flash Status.
            /// </summary>
            public uint dwFlags;
            /// <summary>
            /// The number of times to Flash the window.
            /// </summary>
            public uint uCount;
            /// <summary>
            /// The rate at which the Window is to be flashed, in milliseconds. If Zero, the function uses the default cursor blink rate.
            /// </summary>
            public uint dwTimeout;
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct SECURITY_DESCRIPTOR
        {
            public byte Revision;
            public byte Sbz1;
            public short Control;
            public IntPtr Owner;
            public IntPtr Group;
            public IntPtr Sacl;
            public IntPtr Dacl;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct SECURITY_ATTRIBUTES
        {
            public int nLength;
            public IntPtr lpSecurityDescriptor;
            public int bInheritHandle;
        }

        #region FlashWindow Flags
        /// <summary>
        /// Stop flashing. The system restores the window to its original state.
        /// </summary>
        public const uint FLASHW_STOP = 0;
        /// <summary>
        /// Flash the window caption.
        /// </summary>
        public const uint FLASHW_CAPTION = 1;
        /// <summary>
        /// Flash the taskbar button.
        /// </summary>
        public const uint FLASHW_TRAY = 2;
        /// <summary>
        /// Flash both the window caption and taskbar button.
        /// This is equivalent to setting the FLASHW_CAPTION | FLASHW_TRAY flags.
        /// </summary>
        public const uint FLASHW_ALL = 3;
        /// <summary>
        /// Flash continuously, until the FLASHW_STOP flag is set.
        /// </summary>
        public const uint FLASHW_TIMER = 4;
        /// <summary>
        /// Flash continuously until the window comes to the foreground.
        /// </summary>
        public const uint FLASHW_TIMERNOFG = 12;
        #endregion

        #region SetWindowPos Flags
        /// <summary>If the calling thread and the thread that owns the window are attached to different input queues, 
        /// the system posts the request to the thread that owns the window. This prevents the calling thread from 
        /// blocking its execution while other threads process the request.</summary>
        /// <remarks>SWP_ASYNCWINDOWPOS</remarks>
        public const uint SWP_ASYNCWINDOWPOS = 0x4000;
        /// <summary>Prevents generation of the WM_SYNCPAINT message.</summary>
        /// <remarks>SWP_DEFERERASE</remarks>
        public const uint SWP_DEFERERASE = 0x2000;
        /// <summary>Draws a frame (defined in the window's class description) around the window.</summary>
        /// <remarks>SWP_DRAWFRAME</remarks>
        public const uint SWP_DRAWFRAME = 0x0020;
        /// <summary>Applies new frame styles set using the SetWindowLong function. Sends a WM_NCCALCSIZE message to 
        /// the window, even if the window's size is not being changed. If this flag is not specified, WM_NCCALCSIZE 
        /// is sent only when the window's size is being changed.</summary>
        /// <remarks>SWP_FRAMECHANGED</remarks>
        public const uint SWP_FRAMECHANGED = 0x0020;
        /// <summary>Hides the window.</summary>
        /// <remarks>SWP_HIDEWINDOW</remarks>
        public const uint SWP_HIDEWINDOW = 0x0080;
        /// <summary>Does not activate the window. If this flag is not set, the window is activated and moved to the 
        /// top of either the topmost or non-topmost group (depending on the setting of the hWndInsertAfter 
        /// parameter).</summary>
        /// <remarks>SWP_NOACTIVATE</remarks>
        public const uint SWP_NOACTIVATE = 0x0010;
        /// <summary>Discards the entire contents of the client area. If this flag is not specified, the valid 
        /// contents of the client area are saved and copied back into the client area after the window is sized or 
        /// repositioned.</summary>
        /// <remarks>SWP_NOCOPYBITS</remarks>
        public const uint SWP_NOCOPYBITS = 0x0100;
        /// <summary>Retains the current position (ignores X and Y parameters).</summary>
        /// <remarks>SWP_NOMOVE</remarks>
        public const uint SWP_NOMOVE = 0x0002;
        /// <summary>Does not change the owner window's position in the Z order.</summary>
        /// <remarks>SWP_NOOWNERZORDER</remarks>
        public const uint SWP_NOOWNERZORDER = 0x0200;
        /// <summary>Does not redraw changes. If this flag is set, no repainting of any kind occurs. This applies to 
        /// the client area, the nonclient area (including the title bar and scroll bars), and any part of the parent 
        /// window uncovered as a result of the window being moved. When this flag is set, the application must 
        /// explicitly invalidate or redraw any parts of the window and parent window that need redrawing.</summary>
        /// <remarks>SWP_NOREDRAW</remarks>
        public const uint SWP_NOREDRAW = 0x0008;
        /// <summary>Same as the SWP_NOOWNERZORDER flag.</summary>
        /// <remarks>SWP_NOREPOSITION</remarks>
        public const uint SWP_NOREPOSITION = 0x0200;
        /// <summary>Prevents the window from receiving the WM_WINDOWPOSCHANGING message.</summary>
        /// <remarks>SWP_NOSENDCHANGING</remarks>
        public const uint SWP_NOSENDCHANGING = 0x0400;
        /// <summary>Retains the current size (ignores the cx and cy parameters).</summary>
        /// <remarks>SWP_NOSIZE</remarks>
        public const uint SWP_NOSIZE = 0x0001;
        /// <summary>Retains the current Z order (ignores the hWndInsertAfter parameter).</summary>
        /// <remarks>SWP_NOZORDER</remarks>
        public const uint SWP_NOZORDER = 0x0004;
        /// <summary>Displays the window.</summary>
        /// <remarks>SWP_SHOWWINDOW</remarks>
        public const uint SWP_SHOWWINDOW = 0x0040;
        #endregion

        #region Menu Flags
        public const uint MF_BYCOMMAND = 0x00000000;
        public const uint MF_BYPOSITION = 0x00000400;
        public const uint MF_POPUP = 0x00000010;
        public const uint MF_MENUBARBREAK = 0x00000020;
        public const uint MF_MENUBREAK = 0x00000040;
        #endregion        

        public const uint SECURITY_DESCRIPTOR_REVISION = 1;

        public static readonly IntPtr HWND_TOPMOST = new IntPtr(-1);
        public static readonly IntPtr HWND_NOTOPMOST = new IntPtr(-2);
        public static readonly IntPtr HWND_TOP = new IntPtr(0);
        public static readonly IntPtr HWND_BOTTOM = new IntPtr(1);

        [DllImport("advapi32.dll", SetLastError = true)]
        public static extern bool InitializeSecurityDescriptor(out SECURITY_DESCRIPTOR pSecurityDescriptor, uint dwRevision);
        [DllImport("advapi32.dll", SetLastError = true)]
        public static extern bool SetSecurityDescriptorDacl(ref SECURITY_DESCRIPTOR sd, bool daclPresent, IntPtr dacl, bool daclDefaulted);
        [DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern IntPtr CreateEvent(ref SECURITY_ATTRIBUTES lpEventAttributes, bool bManualReset, bool bInitialState, [MarshalAs(UnmanagedType.LPWStr)] string lpName);
        [DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern IntPtr OpenEvent(UInt32 dwDesiredAccess, bool bInheritable, [MarshalAs(UnmanagedType.LPWStr)] string lpName);
        [DllImport("kernel32.dll")]
        public static extern bool SetEvent(IntPtr hEvent);
        [DllImport("kernel32.dll")]
        public static extern uint WaitForSingleObject(IntPtr hHandle, uint dwMilliseconds);
        [DllImport("kernel32.dll")]
        public static extern bool CloseHandle(IntPtr hHandle);
        [DllImport("kernel32.dll")]
        public static extern bool ResetEvent(IntPtr hEvent);
        [DllImport("User32.dll")]
        public static extern bool SetForegroundWindow(HandleRef hWnd);
        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        public static extern bool AppendMenu(IntPtr hMenu, uint uFlags, IntPtr uIDNewItem, [MarshalAs(UnmanagedType.LPWStr)] string lpNewItem);
        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        public static extern bool ModifyMenu(IntPtr hMenu, uint uPosition, uint uFlags, IntPtr uIDNewItem, [MarshalAs(UnmanagedType.LPWStr)] string lpNewItem);
        [DllImport("User32.dll")]
        public static extern bool TrackPopupMenuEx(IntPtr hMenu, [MarshalAs(UnmanagedType.U4)] uint fuFlags, int x, int y, IntPtr hWnd, IntPtr lpTpm);
        [DllImport("user32.dll")]
        public static extern bool HideCaret(IntPtr hWnd);
        [DllImport("user32.dll")]
        public static extern bool ShowCaret(IntPtr hWnd);
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool FlashWindowEx(ref FLASHWINFO pwfi);
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);
        [DllImport("user32.dll")]
        public static extern IntPtr SetFocus(IntPtr hWnd);
        [DllImport("user32.dll")]
        public static extern IntPtr SetActiveWindow(IntPtr hWnd);
        [DllImport("user32.dll")]
        public static extern IntPtr GetTopWindow(IntPtr hWnd);
        [DllImport("winmm.dll", SetLastError = true)]
        public static extern bool PlaySound(byte[] ptrToSound, System.UIntPtr hmod, uint fdwSound);
        [DllImport("winmm.dll", SetLastError = true)]
        public static extern bool PlaySound(IntPtr ptrToSound, System.UIntPtr hmod, uint fdwSound);
        [DllImport("shell32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern IntPtr ExtractIcon(IntPtr hInst, string lpszExeFileName, int nIconIndex);
        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool DestroyIcon(IntPtr hIcon);

        public static FLASHWINFO CreateFlashWInfo(IntPtr handle, uint flags, uint count, uint timeout)
        {
            FLASHWINFO fi = new FLASHWINFO();
            fi.cbSize = Convert.ToUInt32(Marshal.SizeOf(fi));
            fi.hwnd = handle;
            fi.dwFlags = flags;
            fi.uCount = count;
            fi.dwTimeout = timeout;
            return fi;
        }

        /// <summary>
        /// Create security attributes with all access requests granted. This will
        /// enable sharing of events between processes running at different privileges.
        /// </summary>
        /// <returns></returns>
        public static SECURITY_ATTRIBUTES CreateSecurityAttributes()
        {
            SECURITY_DESCRIPTOR securityDescriptor;
            InitializeSecurityDescriptor(out securityDescriptor, SECURITY_DESCRIPTOR_REVISION);
            SetSecurityDescriptorDacl(ref securityDescriptor, true, IntPtr.Zero, false);
            
            IntPtr lpSecurityDescriptor = Marshal.AllocHGlobal(Marshal.SizeOf(securityDescriptor));
            Marshal.StructureToPtr(securityDescriptor, lpSecurityDescriptor, true);

            SECURITY_ATTRIBUTES securityAttributes = new SECURITY_ATTRIBUTES();
            securityAttributes.nLength = Marshal.SizeOf(securityAttributes);
            securityAttributes.lpSecurityDescriptor = lpSecurityDescriptor;

            return securityAttributes;
        }
    }
}
