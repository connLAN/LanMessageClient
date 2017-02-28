using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;

namespace LANChat
{
    public partial class FileTransferDialog : BaseForm
    {
        private class FileTransferControlList : List<FileTransferControl>
        {
            public FileTransferControl this[int index]
            {
                get { return base[index]; }
                set { base[index] = value; }
            }

            public FileTransferControl this[string key]
            {
                get
                {
                    foreach (FileTransferControl fileTransferControl in this) {
                        if (fileTransferControl.Key.Equals(key))
                            return fileTransferControl;
                    }
                    return null;
                }
            }

            public bool ContainsKey(string key)
            {
                foreach (FileTransferControl fileTransferControl in this) {
                    if (fileTransferControl.Key.Equals(key))
                        return true;
                }
                return false;
            }

            public int GetIndex(string key)
            {
                for (int index = 0; index < this.Count; index++) {
                    if (this[index].Key.Equals(key))
                        return index;
                }
                return -1;
            }
        }

        private class TcpState
        {
            public string Key;
            public object TcpObject;

            public TcpState(string key, object tcpObject)
            {
                this.Key = key;
                this.TcpObject = tcpObject;
            }
        }

        public static FileTransferDialog Instance;
        public static EventHandler SetForeground;

        private static int instanceCount;
        private TcpListener tcpListener;
        private int port;
        private bool bFileToForeground;
        private bool bAutoReceiveFile;
        private FileTransferControlList fileTransferList;

        private IPAddress localAddress;
        public IPAddress LocalAddress
        {
            get { return localAddress; }
            set { localAddress = value; }
        }

        private string localUserName;
        public string LocalUserName
        {
            get { return localUserName; }
            set { localUserName = value; }
        }

        public delegate void NotifyEventhandler(object sender, NotifyEventArgs e);
        public event NotifyEventhandler Notify;

        public FileTransferDialog()
        {
            instanceCount++;
            if (instanceCount > 1) {
                this.Dispose();
                return;
            }

            Instance = this;
            InitializeComponent();
            InitUI();

            Properties.Settings.Default.PropertyChanged += new PropertyChangedEventHandler(Default_PropertyChanged);
            fileTransferList = new FileTransferControlList();

            SetForeground += new EventHandler(FileTransferDialog_SetForeground);
        }

        private void InitUI()
        {
            this.ClientSize = new Size(430, 300);
            this.Icon = Helper.GetAssociatedIcon(Application.ExecutablePath);
            //  Set visual style of window according to current theme.
            SetTheme(Properties.Settings.Default.UseThemes, Properties.Settings.Default.ThemeFile);
        }

        private void SetTheme(bool bUseThemes, string themeFile)
        {
            if (bUseThemes) {
                this.BackColor = Theme.GetThemeColor(themeFile, "FileTransfer", null, "BackColor");
                panel.BackColor = Theme.GetThemeColor(themeFile, "FileTransfer", "FileList", "BackColor");
            }
            else {
                this.BackColor = SystemColors.Control;
                panel.BackColor = SystemColors.Window;
            }

            if (fileTransferList != null) {
                for (int index = 0; index < fileTransferList.Count; index++) {
                    //  This will cause the control to repaint itself.
                    fileTransferList[index].IsSelected = fileTransferList[index].IsSelected;
                    fileTransferList[index].UseSystemStyle = !bUseThemes;
                }
            }
            this.Invalidate(true);
        }

        /// <summary>
        /// Add a new File Transfer control to the form. A connection is established between
        /// the client and server. Depending on the transfer mode, the local machine acts as
        /// a server or a client.
        /// </summary>
        /// <param name="mode">The transfer mode. Can be Send or Receive.</param>
        /// <param name="remoteUserName">Remote user's name.</param>
        /// <param name="remoteAddress">IP address of remote user.</param>
        /// <param name="key">Unique key for this file transfer.</param>
        /// <param name="fileName">Name of file to be sent/received.</param>
        /// <param name="fileSize">Size of the file.</param>
        public void AddFileTransfer(FileTransfer.Modes mode, string remoteUserName, string remoteAddress, string key, string fileName, string fileSize)
        {
            //  Create a new File Transfer control and set its properties.
            FileTransferControl fileTransferControl = null;
            try {
                fileTransferControl = new FileTransferControl(mode, key, bAutoReceiveFile);
                fileTransferControl.UserName = remoteUserName;
                fileTransferControl.FileName = fileName;
                fileTransferControl.FileSize = long.Parse(fileSize);
                //  This will force the system to create a handle for the control.
                IntPtr dummy = fileTransferControl.Handle;
                fileTransferControl.Selected += new FileTransferControl.SelectedEventHandler(FileTransferControl_Selected);
                fileTransferControl.MouseClick += new FileTransferControl.MouseClickEventHandler(FileTransferControl_MouseClick);
                fileTransferControl.Notify += new FileTransferControl.NotifyEventhandler(FileTransferControl_Notify);
                fileTransferControl.LayoutChanged += new FileTransferControl.LayoutChangedEventHandler(FileTransferControl_LayoutChanged);
                fileTransferControl.InitControl();
                //  Add the control to the top of the File transfer list.
                fileTransferList.Insert(0, fileTransferControl);

                if (mode == FileTransfer.Modes.Send) {
                    ShowWindow(false, false);
                    BeginAccept(key);
                }
                else {
                    ShowWindow(bFileToForeground, false);
                    BeginConnect(key, remoteAddress);
                }

                //  Update the panel's controls.
                PopulateTransferList(0);
                //  Set the UI theme.
                SetTheme(Properties.Settings.Default.UseThemes, Properties.Settings.Default.ThemeFile);
                //  Make the new control the selected control.
                SelectControl(fileTransferControl);
            }
            catch (Exception ex) {
                fileTransferControl.Dispose();
            }
        }

        public void CancelFileTransfer(string key)
        {
            foreach (FileTransferControl control in fileTransferList) {
                if (control.Key.Equals(key))
                    control.CancelTransfer();
            }
        }

        private void PopulateTransferList(int startIndex)
        {
            if (fileTransferList.Count == 0 || startIndex < 0) {
                btnClear.Enabled = (fileTransferList.Count > 0);
                return;
            }

            int lastControlBottom = 0;
            if (startIndex > 0)
                lastControlBottom = fileTransferList[startIndex - 1].Bottom;
            for (int index = startIndex; index < fileTransferList.Count; index++) {
                if (!panel.Controls.Contains(fileTransferList[index])) {
                    panel.Controls.Add(fileTransferList[index]);
                    fileTransferList[index].Anchor = (AnchorStyles)(AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right);
                    fileTransferList[index].Left = 0;
                    fileTransferList[index].Width = panel.ClientSize.Width;
                }
                fileTransferList[index].Top = lastControlBottom;
                lastControlBottom = fileTransferList[index].Bottom;
            }
            btnClear.Enabled = (fileTransferList.Count > 0);
        }

        private void ClearTransferList()
        {
            FileTransferControlList remList = new FileTransferControlList();
            foreach (FileTransferControl control in fileTransferList) {
                if ((control.Status & FileTransfer.Status.Completed) != 0)
                    remList.Add(control);
            }
            fileTransferList.RemoveAll(new Predicate<FileTransferControl>(delegate(FileTransferControl item) {
                return remList.Contains(item);
            }));
            //  Dispose all controls removed from the list.
            for (int index = 0; index < remList.Count; index++)
                remList[index].Dispose();
        }

        private void RemoveTransferFromList(FileTransferControl control)
        {
            fileTransferList.Remove(control);
            control.Dispose();
        }

        private void SelectControl(FileTransferControl control)
        {
            foreach (FileTransferControl fileTransferControl in fileTransferList) {
                if (fileTransferControl.IsSelected) {
                    fileTransferControl.IsSelected = false;
                    break;
                }
            }
            control.IsSelected = true;
        }

        /// <summary>
        /// Start the TCP server and begin listening for incoming connections.
        /// </summary>
        private void StartListening()
        {
            try {
                if (tcpListener == null) {
                    IPEndPoint localEP = new IPEndPoint(localAddress, port);
                    tcpListener = new TcpListener(localEP);
                }
                tcpListener.Start();
            }
            catch (Exception ex) {
                ErrorHandler.ShowError(ex);
            }
        }

        /// <summary>
        /// Begin accepting an incoming connection asynchronously.
        /// </summary>
        /// <param name="key"></param>
        private void BeginAccept(string key)
        {
            TcpState tcpState = new TcpState(key, tcpListener);
            try {
                tcpListener.BeginAcceptTcpClient(AcceptCallback, tcpState);
            }
            catch (Exception ex) {
                ErrorHandler.ShowError(ex);
            }
        }

        /// <summary>
        /// This method is called when the asynchronous acceptance of incoming connection is complete.
        /// </summary>
        /// <param name="ar"></param>
        private void AcceptCallback(IAsyncResult ar)
        {
            try {
                TcpState tcpState = (TcpState)ar.AsyncState;
                string key = tcpState.Key;
                TcpListener tcpListener = (TcpListener)tcpState.TcpObject;

                TcpClient tcpClient = tcpListener.EndAcceptTcpClient(ar);

                SetTcpClient(tcpClient, key, FileTransfer.Modes.Send);
            }
            catch (Exception ex) {
                ErrorHandler.ShowError(ex);
            }
        }

        /// <summary>
        /// Begin connecting to a remote server asynchronously.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="address"></param>
        private void BeginConnect(string key, string address)
        {
            try {
                TcpClient tcpClient = new TcpClient();
                IPAddress ipAddress = IPAddress.Parse(address);

                TcpState tcpState = new TcpState(key, tcpClient);

                tcpClient.BeginConnect(ipAddress, port, ConnectCallback, tcpState);
            }
            catch (Exception ex) {
                ErrorHandler.ShowError(ex);
            }
        }

        /// <summary>
        /// This method is called when asynchronous connection to a remote server is complete.
        /// </summary>
        /// <param name="ar"></param>
        private void ConnectCallback(IAsyncResult ar)
        {
            try {
                TcpState tcpState = (TcpState)ar.AsyncState;
                string key = tcpState.Key;
                TcpClient tcpClient = (TcpClient)tcpState.TcpObject;
                
                tcpClient.EndConnect(ar);

                SetTcpClient(tcpClient, key, FileTransfer.Modes.Receive);
            }
            catch (Exception ex) {
                ErrorHandler.ShowError(ex);
            }
        }

        /// <summary>
        /// Send the TcpClient obtained when a connection is established to the correct
        /// File Transfer control.
        /// </summary>
        /// <param name="tcpClient"></param>
        /// <param name="key"></param>
        /// <param name="mode"></param>
        private void SetTcpClient(TcpClient tcpClient, string key, FileTransfer.Modes mode)
        {
            //  Find the File Transfer control with the matching key and transfer mode.
            //  Set the supplied TcpClient as the control's Tcp Client.
            foreach (FileTransferControl fileTransferControl in fileTransferList) {
                if (fileTransferControl.Key.Equals(key) && fileTransferControl.Mode == mode) {
                    fileTransferControl.SetTcpClient(tcpClient);
                    break;
                }
            }
        }

        /// <summary>
        /// Get the settings related to this form from the settings file.
        /// </summary>
        private void LoadSettings()
        {
            port = Properties.Settings.Default.TCPPort;
            bFileToForeground = Properties.Settings.Default.FileToForeground;
            bAutoReceiveFile = Properties.Settings.Default.AutoReceiveFile;
        }

        /// <summary>
        /// Bring the window to the foreground.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void FileTransferDialog_SetForeground(object sender, EventArgs e)
        {
            this.WindowState = prevWindowState;
            Win32.SetForegroundWindow(new HandleRef(this, this.Handle));
        }

        private void FileTransferDialog_Load(object sender, EventArgs e)
        {
            prevWindowState = FormWindowState.Normal;
            LoadSettings();
            StartListening();

            this.Hide();
        }

        private void FileTransferDialog_FormClosing(object sender, FormClosingEventArgs e)
        {
            //  If window was closed by user, close to system tray instead
            //  of terminating application.
            if (e.CloseReason == CloseReason.UserClosing) {
                e.Cancel = true;
                HideWindow();
            }
        }

        private void FileTransferDialog_Resize(object sender, EventArgs e)
        {
            //  Remember state of window before minimizing.
            if (WindowState != FormWindowState.Minimized)
                prevWindowState = WindowState;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            //  Clear all transfers that are not active.
            ClearTransferList();
            //  Update UI.
            PopulateTransferList(0);
        }

        private void Default_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals("FileToForeground")) {
                bFileToForeground = Properties.Settings.Default.FileToForeground;
            }
            if (e.PropertyName.Equals("AutoReceiveFile")) {
                bAutoReceiveFile = Properties.Settings.Default.AutoReceiveFile;
            }
            if (e.PropertyName.Equals("UseThemes") || e.PropertyName.Equals("ThemeFile")) {
                SetTheme(Properties.Settings.Default.UseThemes, Properties.Settings.Default.ThemeFile);
            }
        }

        private void FileTransferControl_Selected(object sender, MouseEventArgs e)
        {
            FileTransferControl control = (FileTransferControl)sender;
            SelectControl(control);
        }

        private void FileTransferControl_MouseClick(object sender, MouseEventArgs e)
        {
            FileTransferControl control = (FileTransferControl)sender;
            //  Show context menu if right mouse button was clicked.
            if (e.Button == System.Windows.Forms.MouseButtons.Right) {
                //  No need to show menu when waiting for receive confirmation.
                if (control.Status == FileTransfer.Status.Confirming)
                    return;

                Point pos = this.PointToClient(Cursor.Position);
                mnuFile.Tag = control;
                mnuFileOpen.Visible = ((control.Status & FileTransfer.Status.Completed) != 0);
                mnuFileOpen.Enabled = control.FileExists;
                mnuFileCancel.Visible = ((control.Status & FileTransfer.Status.Sending) != 0);
                mnuFileOpenFolder.Enabled = control.FileExists;
                mnuFileRemove.Enabled = ((control.Status & FileTransfer.Status.Completed) != 0);
                mnuFile.Show(this, pos);
            }
        }

        private void FileTransferControl_Notify(object sender, NotifyEventArgs e)
        {
            Notify(this, e);
        }

        private void FileTransferControl_LayoutChanged(object sender, EventArgs e)
        {
            FileTransferControl control = (FileTransferControl)sender;
            int startIndex = fileTransferList.GetIndex(control.Key);
            if (startIndex >= 0) {
                panel.SuspendLayout();
                int lastControlBottom = fileTransferList[startIndex].Bottom;
                for (int index = startIndex + 1; index < fileTransferList.Count; index++) {
                    fileTransferList[index].Top = lastControlBottom;
                    lastControlBottom = fileTransferList[index].Bottom;
                }
                panel.ResumeLayout();
            }
        }

        private void FileTransferControl_StatusChanged(object sender, EventArgs e)
        {
            FileTransferControl control = (FileTransferControl)sender;
        }

        private void mnuFileOpen_Click(object sender, EventArgs e)
        {
            FileTransferControl control = (FileTransferControl)mnuFile.Tag;
            control.OpenFile();
        }

        private void mnuFileOpenFolder_Click(object sender, EventArgs e)
        {
            FileTransferControl control = (FileTransferControl)mnuFile.Tag;
            control.OpenFileFolder();
        }

        private void mnuFileCancel_Click(object sender, EventArgs e)
        {
            FileTransferControl control = (FileTransferControl)mnuFile.Tag;
            control.CancelTransfer();
        }

        private void mnuFileRemove_Click(object sender, EventArgs e)
        {
            FileTransferControl control = (FileTransferControl)mnuFile.Tag;
            int index = fileTransferList.GetIndex(control.Key);
            RemoveTransferFromList(control);
            PopulateTransferList(index);
        }
    }
}
