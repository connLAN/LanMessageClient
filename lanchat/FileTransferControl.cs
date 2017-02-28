using System;
using System.Drawing;
using System.Windows.Forms;
using System.Net.Sockets;
using System.IO;
using System.Threading;

namespace LANChat
{
    public partial class FileTransferControl : UserControl
    {
        #region Properties
        //  Control properties.
        private FileTransfer.Modes mode;
        /// <summary>
        /// File transfer mode.
        /// </summary>
        public FileTransfer.Modes Mode
        {
            get { return mode; }
            set { mode = value; SetTableLayout(); }
        }

        private FileTransfer.Status status;
        /// <summary>
        /// File transfer status.
        /// </summary>
        public FileTransfer.Status Status
        {
            get { return status; }
        }

        private string userName = string.Empty;
        /// <summary>
        /// Name of remote user.
        /// </summary>
        public string UserName
        {
            get { return userName; }
            set { userName = value; }
        }

        private string key = string.Empty;
        /// <summary>
        /// Unique id of this file transfer.
        /// </summary>
        public string Key
        {
            get { return key; }
            set { key = value; }
        }

        private string fileName = string.Empty;
        /// <summary>
        /// Name of file to be sent/received.
        /// </summary>
        public string FileName
        {
            get { return fileName; }
            set { fileName = value; SetIcon(fileName); }
        }

        private long fileSize = 0;
        /// <summary>
        /// Size of the file.
        /// </summary>
        public long FileSize
        {
            get { return fileSize; }
            set { fileSize = value; fileSizeString = Helper.FormatSize(fileSize); }
        }

        private bool isSelected = false;
        /// <summary>
        /// Selected state of this control.
        /// </summary>
        public bool IsSelected
        {
            get { return isSelected; }
            set { isSelected = value; SetSelectedLook(); }
        }

        private bool useSystemStyle = false;
        /// <summary>
        /// Indicates whether the control uses native Windows style.
        /// </summary>
        public bool UseSystemStyle
        {
            get { return useSystemStyle; }
            set { useSystemStyle = value; SetTheme(); }
        }

        /// <summary>
        /// Indicates whether the file is present in its path.
        /// </summary>
        public bool FileExists
        {
            get { return File.Exists(filePath); }
        }
        #endregion

        //  Events and delegates.
        public delegate void SelectedEventHandler(object sender, MouseEventArgs e);
        public event SelectedEventHandler Selected;
        public delegate void MouseClickEventHandler(object sender, MouseEventArgs e);
        public event MouseClickEventHandler MouseClick;
        public delegate void NotifyEventhandler(object sender, NotifyEventArgs e);
        public event NotifyEventhandler Notify;
        public delegate void StatusChangedEventHandler(object sender, EventArgs e);
        public event StatusChangedEventHandler StatusChanged;
        public delegate void LayoutChangedEventHandler(object sender, EventArgs e);
        public event LayoutChangedEventHandler LayoutChanged;

        private bool bCancelTransfer;   //  Flag indicating file transfer cancel.
        private bool bAutoReceive;  //  Flag indicating whether file should be accepted automatically.
        private string fileSizeString = string.Empty;  //  File size represented as a string.
        private string filePath = string.Empty; //  This will hold the full path of the file.
        private TcpClient sendClient;
        private TcpClient receiveClient;
        private NetworkStream sendStream;
        private NetworkStream receiveStream;
        System.Threading.Timer progressTimer;
        private long position = 0;
        private const long bufferSize = 65536;

        private delegate void InvokeDelegate();
        private delegate void UpdateProgressDelegate(long value);
        private delegate void DisplayProgressDelegate(string text);
        private delegate void UpdateStatusDelegate(FileTransfer.Status status);
        private delegate void DisplayStatusDelegate(bool notify);
        private delegate void SetIconDelegate(string filePath);

        private const byte CTRL_ACCEPT = 0x00;
        private const byte CTRL_DECLINE = 0x01;
        private const byte CTRL_CANCEL = 0x02;
        private const byte ACK_OK = 0x03;
        private const byte ACK_FAIL = 0x04;
        private const byte ACK_BAD = 0x05;
        private const byte DT_DATA = 0xFF;
        private const byte DT_END = 0xFE;

        public FileTransferControl(FileTransfer.Modes mode, string key, bool autoReceive)
        {
            this.key = key;
            this.mode = mode;
            this.bAutoReceive = autoReceive;

            this.bCancelTransfer = false;
            this.isSelected = false;

            InitializeComponent();
            InitUI();

            foreach (Control control in tableLayoutPanel.Controls) {
                control.MouseDown += new MouseEventHandler(FileTransferControl_MouseDown);
                control.MouseClick += new MouseEventHandler(FileTransferControl_MouseClick);
                if (control is Label)
                    control.MouseDoubleClick += new MouseEventHandler(FileTransferControl_MouseDoubleClick);
            }
        }

        /// <summary>
        /// Set the TcpClient to be used by this File Transfer control.
        /// </summary>
        /// <param name="tcpClient"></param>
        public void SetTcpClient(TcpClient tcpClient)
        {
            if (this.mode == FileTransfer.Modes.Send) {
                sendClient = tcpClient;
                sendClient.SendBufferSize = (int)bufferSize;
                InitSendFile();
            }
            else {
                receiveClient = tcpClient;
                receiveClient.ReceiveBufferSize = (int)bufferSize;
                InitReceiveFile();
            }
        }

        /// <summary>
        /// Opens the file with its associated program.
        /// </summary>
        [System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name = "FullTrust")]
        public void OpenFile()
        {
            try {
                System.Diagnostics.Process.Start(filePath);
            }
            catch (Exception ex) {
            }
        }

        /// <summary>
        /// Opens the containing folder of the file.
        /// </summary>
        [System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name = "FullTrust")]
        public void OpenFileFolder()
        {
            try {
                string argument = "/select, \"" + filePath + "\"";
                System.Diagnostics.Process.Start("explorer.exe", argument);
            }
            catch (Exception ex) {
            }
        }

        /// <summary>
        /// Cancels the file transfer.
        /// </summary>
        public void CancelTransfer()
        {
            //  Set the cancel flag to true.
            this.bCancelTransfer = true;
        }

        private void InitUI()
        {
            foreach (Control control in tableLayoutPanel.Controls) {
                if (control is Label) {
                    control.BackColor = Color.Transparent;
                    control.Font = Helper.SystemFont;
                }
            }
            lblUserName.Font = new System.Drawing.Font(Helper.SystemFont, FontStyle.Bold);
            picBoxTransfer.Image = (Image)LANChat.Resources.Properties.Resources.ResourceManager.GetObject("FileIcon");
            
            SetSelectedLook();
        }

        private void SetIcon(string filePath)
        {
            Icon icon = Helper.GetAssociatedIcon(filePath);
            if (icon != null) {
                this.picBoxTransfer.Image = icon.ToBitmap();
                icon.Dispose();
            }
        }

        private void SetTheme()
        {
            tableLayoutPanel.UseSystemStyle = useSystemStyle;
            if (Properties.Settings.Default.UseThemes) {
                tableLayoutPanel.DefaultColor = Theme.GetThemeColor(Properties.Settings.Default.ThemeFile, "FileTransfer", "FileList", "BackColor");
                tableLayoutPanel.SelectedColor = Theme.GetThemeColor(Properties.Settings.Default.ThemeFile, "FileTransfer", "FileList", "SelectedColor");
                tableLayoutPanel.HighlightColor = Theme.GetThemeColor(Properties.Settings.Default.ThemeFile, "FileTransfer", "FileList", "HighlightColor");
                tableLayoutPanel.ColorGradientMax = Theme.GetThemeValue(Properties.Settings.Default.ThemeFile, "FileTransfer", "FileList", "GradientMax", typeof(float));
                tableLayoutPanel.ColorGradientMin = Theme.GetThemeValue(Properties.Settings.Default.ThemeFile, "FileTransfer", "FileList", "GradientMin", typeof(float));
                Color foreColor = Theme.GetThemeColor(Properties.Settings.Default.ThemeFile, "FileTransfer", "FileList", "TextColor");
                SetLabelForeColor(foreColor);
            }
        }

        private void SetSelectedLook()
        {
            tableLayoutPanel.BackColor = SystemColors.Control;
            tableLayoutPanel.Selected = isSelected;
            Color foreColor = SystemColors.WindowText;
            if (this.isSelected && !Properties.Settings.Default.UseThemes && !Application.RenderWithVisualStyles)
                foreColor = SystemColors.HighlightText;
            if (Properties.Settings.Default.UseThemes)
                foreColor = Theme.GetThemeColor(Properties.Settings.Default.ThemeFile, "FileTransfer", "FileList", "TextColor");
            SetLabelForeColor(foreColor);
        }

        private void SetLabelForeColor(Color foreColor)
        {
            foreach (Control control in tableLayoutPanel.Controls) {
                if (control is Label)
                    control.ForeColor = foreColor;
            }
            lblStatus.ForeColor = Helper.ModulateColor(foreColor, 0.35f);
        }

        /// <summary>
        /// Set the layout of the controls based on the current transfer status.
        /// </summary>
        private void SetTableLayout()
        {
            switch (this.status) {
                case FileTransfer.Status.Confirming:
                    tableLayoutPanel.RowStyles[3].Height = 0;
                    tableLayoutPanel.RowStyles[4].Height = 0;
                    tableLayoutPanel.RowStyles[5].Height = 100.0f;
                    progressBar.Visible = false;
                    btnCancel.Visible = false;
                    btnAccept.Visible = true;
                    btnDecline.Visible = true;
                    this.Height = 58;
                    break;
                case FileTransfer.Status.Waiting:
                    tableLayoutPanel.RowStyles[3].Height = 100.0f;
                    tableLayoutPanel.RowStyles[4].Height = 0;
                    tableLayoutPanel.RowStyles[5].Height = 0;
                    progressBar.Visible = true;
                    btnCancel.Visible = true;
                    btnCancel.Enabled = false;
                    btnAccept.Visible = false;
                    btnDecline.Visible = false;
                    this.Height = 52;
                    break;
                case FileTransfer.Status.Sending:
                case FileTransfer.Status.Receiving:
                    tableLayoutPanel.RowStyles[3].Height = 41.67f;
                    tableLayoutPanel.RowStyles[4].Height = 58.33f;
                    tableLayoutPanel.RowStyles[5].Height = 0;
                    progressBar.Visible = true;
                    btnCancel.Visible = true;
                    btnCancel.Enabled = true;
                    btnAccept.Visible = false;
                    btnDecline.Visible = false;
                    this.Height = 73;
                    break;
                default:
                    tableLayoutPanel.RowStyles[3].Height = 100.0f;
                    tableLayoutPanel.RowStyles[4].Height = 0;
                    tableLayoutPanel.RowStyles[5].Height = 0;
                    progressBar.Visible = false;
                    btnCancel.Visible = false;
                    btnCancel.Enabled = false;
                    btnAccept.Visible = false;
                    btnDecline.Visible = false;
                    this.Height = 52;
                    break;
            }

            if (LayoutChanged != null)
                LayoutChanged(this, new EventArgs());
        }

        /// <summary>
        /// Display a message based on the current transfer status.
        /// </summary>
        private void DisplayStatus()
        {
            DisplayStatus(true);
        }

        /// <summary>
        /// Display a message based on the current transfer status.
        /// </summary>
        /// <param name="notify">If true, raises a notification event.</param>
        private void DisplayStatus(bool notify)
        {
            switch (this.status) {
                case FileTransfer.Status.Waiting:
                    lblStatus.Text = "Waiting for " + userName + " to accept...";
                    break;
                case FileTransfer.Status.Confirming:
                    lblStatus.Text = "Awaiting confirmation...";
                    break;
                case FileTransfer.Status.Sending:
                    lblStatus.Text = "Sending file...";
                    break;
                case FileTransfer.Status.Receiving:
                    lblStatus.Text = "Receiving file...";
                    break;
                case FileTransfer.Status.Completed:
                    lblStatus.Text = "";
                    if (notify)
                        ShowNotification();
                    break;
                case FileTransfer.Status.Declined:
                    lblStatus.Text = "Declined";
                    break;
                case FileTransfer.Status.Cancelled:
                case FileTransfer.Status.Failed:
                    lblStatus.Text = "Canceled";
                    break;
                default:
                    lblStatus.Text = "Status: Unknown";
                    break;
            }
        }

        private string TruncateString(Label control, string text)
        {
            string truncText = text;
            int truncPos = text.Length / 2;
            float width = (float)(control.ClientSize.Width);

            Graphics graphics = control.CreateGraphics();
            while (truncPos > 1) {
                if (graphics.MeasureString(truncText, control.Font).Width > width) {
                    truncPos--;
                    truncText = text.Substring(0, truncPos) + "..." + text.Substring(text.Length - truncPos, truncPos);
                    continue;
                }
                break;
            }
            
            return truncText;
        }

        /// <summary>
        /// Populates the labels with appropriate details.
        /// </summary>
        private void DisplayInfo()
        {
            string prefix = (mode == FileTransfer.Modes.Send) ? "To" : "From";
            lblUserName.Text = prefix + ": " + userName;
            lblFileInfo.Text = TruncateString(lblFileInfo, fileName + " (" + fileSizeString + ")");
        }

        /// <summary>
        /// Update the current status of the file transfer.
        /// </summary>
        /// <param name="status"></param>
        private void UpdateStatus(FileTransfer.Status status)
        {
            this.status = status;
            if (StatusChanged != null)
                StatusChanged(this, new EventArgs());
        }

        /// <summary>
        /// Update the progress bar to show the file trasnfer progress.
        /// </summary>
        /// <param name="position">Total bytes transferred.</param>
        private void UpdateProgress(long position)
        {
            float fProgress = (((float)position / (float)fileSize) * 100.0f);
            int progress = Convert.ToInt32(fProgress);
            progressBar.Value = progress;
        }

        /// <summary>
        /// Creates a timer that tracks transfer progress.
        /// </summary>
        private void CreateTimer()
        {
            progressTimer = new System.Threading.Timer(TimerCallback, null, 250, 1000);
        }

        private long lastPosition = 0;
        private long lastSpeed = 0;
        /// <summary>
        /// Callback invoked preiodically by the timer.
        /// </summary>
        /// <param name="state"></param>
        private void TimerCallback(Object state)
        {
            //  Check if transfer is complete, canceled, declined, or in any other final state.
            if ((status & FileTransfer.Status.Completed) != 0) {
                progressTimer.Change(Timeout.Infinite, Timeout.Infinite);
                progressTimer.Dispose();
                lblStatus.BeginInvoke(new DisplayStatusDelegate(this.DisplayStatus), new object[] { false });
                return;
            }

            long speed = (position - lastPosition);
            speed = (speed + lastSpeed) / 2;
            lastSpeed = speed;
            string timeString;
            try {
                long time = (fileSize - position) / speed;
                timeString = Helper.FormatTime(time);
            }
            catch {
                timeString = "Calculating time";
            }
            string speedString = Helper.FormatSize(speed);
            string sizeTransferredString = Helper.FormatSize(position);
            lastPosition = position;
            string progress = timeString + " left - " + 
                sizeTransferredString + " of " + fileSizeString + 
                " (" + speedString + "/sec)";

            lblStatus.BeginInvoke(new DisplayProgressDelegate(this.DisplayProgress), new object[] { progress });
        }

        /// <summary>
        /// Update the status field with the progress.
        /// </summary>
        /// <param name="text"></param>
        private void DisplayProgress(string text)
        {
            lblStatus.Text = text;
        }

        /// <summary>
        /// Replace any invalid character in file name with a valid symbol.
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        private string GetValidFileName(string fileName)
        {
            //  Replace invalid characters with pilcrow sign(¶)
            char[] invalidChars = Path.GetInvalidFileNameChars();
            foreach (char invalidChar in invalidChars)
                fileName = fileName.Replace(invalidChar, Convert.ToChar(0xB6));
            return fileName;
        }

        /// <summary>
        /// Create an unused file name based on the supplied file name, if the supplied
        /// file name already exists.
        /// eg: If Test.txt exists, function returns Test[1].txt
        /// </summary>
        /// <param name="fileName">The file name to verify.</param>
        /// <returns>Unused file name.</returns>
        private string GetFreeFileName(string fileName)
        {
            string freeFileName = fileName;

            string fileDir = Properties.Settings.Default.ReceivedFileFolder;
            string filePath = Path.Combine(fileDir, fileName);

            fileName = Path.GetFileNameWithoutExtension(filePath);
            string fileExt = Path.GetExtension(filePath);

            int fileCount = 0;
            while (File.Exists(filePath)) {
                fileCount++;
                freeFileName = fileName + "[" + fileCount.ToString() + "]" + fileExt;
                filePath = Path.Combine(fileDir, freeFileName);
            }

            return freeFileName;
        }

        /// <summary>
        /// Populate all the labels and set the layout of the controls. This method
        /// should always be called before starting the file transfer.
        /// </summary>
        public void InitControl()
        {
            if (this.mode == FileTransfer.Modes.Send) {
                UpdateStatus(FileTransfer.Status.Waiting);
                this.filePath = this.fileName;
                this.fileName = Path.GetFileName(this.filePath);
            }
            else {
                //  File Receive mode.
                UpdateStatus(FileTransfer.Status.Confirming);
                this.fileName = GetValidFileName(this.fileName);
            }
            DisplayInfo();
            DisplayStatus();
            SetTableLayout();
        }

        /// <summary>
        /// Gets the network stream used for receiving the file.
        /// </summary>
        private void InitReceiveFile()
        {
            try {
                receiveStream = receiveClient.GetStream();

                //  Start receiving the file if automatic file receiving is set.
                if (bAutoReceive)
                    ReceiveFile();
            }
            catch {
                UpdateStatus(FileTransfer.Status.Receiving);
                this.BeginInvoke(new InvokeDelegate(DisplayStatus));
                this.BeginInvoke(new InvokeDelegate(SetTableLayout));
            }
        }

        /// <summary>
        /// Start receiving the file on a new thread.
        /// </summary>
        private void ReceiveFile()
        {
            //  Send a control byte to sender indicating that file has been accepted.
            receiveStream.WriteByte(CTRL_ACCEPT);

            UpdateStatus(FileTransfer.Status.Receiving);
            this.BeginInvoke(new InvokeDelegate(DisplayStatus));
            this.BeginInvoke(new InvokeDelegate(SetTableLayout));

            this.fileName = GetFreeFileName(this.fileName);
            this.BeginInvoke(new InvokeDelegate(DisplayInfo));

            //  Execute the file reception on a separate thread.
            //  This will ensure that the UI is responsive during file transfer.
            Thread thread = new Thread(new ThreadStart(ReceiveStream));
            thread.Start();
        }

        /// <summary>
        /// This method handles the actual file reception. It reads data from the
        /// network stream and writes to the file.
        /// </summary>
        private void ReceiveStream()
        {
            FileStream fs = null;
            try {
                //  Read byte stream from network stream.
                //  Write to file stream.

                string path = Properties.Settings.Default.ReceivedFileFolder;
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                filePath = Path.Combine(path, fileName);
                fs = new FileStream(filePath, FileMode.Create, FileAccess.Write);
                byte[] buffer = new byte[bufferSize];
                long unsentCount = fileSize;
                position = 0;

                //  Create timer to update transfer progress.
                CreateTimer();

                while (unsentCount > 0) {
                    int bytesReceived = receiveStream.Read(buffer, 0, (int)bufferSize);
                    fs.Write(buffer, 0, bytesReceived);
                    position += bytesReceived;
                    unsentCount = fileSize - position;
                    progressBar.BeginInvoke(new UpdateProgressDelegate(this.UpdateProgress), new object[] { position });

                    if (bytesReceived == 0)
                        this.bCancelTransfer = true;

                    if (this.bCancelTransfer)
                        break;
                }

                if (this.bCancelTransfer)
                    this.BeginInvoke(new UpdateStatusDelegate(this.UpdateStatus), new object[] { FileTransfer.Status.Cancelled });
                else {
                    this.BeginInvoke(new UpdateStatusDelegate(this.UpdateStatus), new object[] { FileTransfer.Status.Completed });
                    //  Get the actual icon once the file has finished downloading.
                    this.BeginInvoke(new SetIconDelegate(this.SetIcon), new object[] { filePath });
                }
            }
            catch (Exception ex) {
                this.BeginInvoke(new UpdateStatusDelegate(this.UpdateStatus), new object[] { FileTransfer.Status.Failed });
                ErrorHandler.ShowError(ex);
            }
            finally {
                fs.Close();

                receiveClient.Close();
                receiveStream.Close();

                if (this.bCancelTransfer)
                    File.Delete(filePath);
                
                this.BeginInvoke(new InvokeDelegate(DisplayStatus));
                this.BeginInvoke(new InvokeDelegate(SetTableLayout));
            }
        }

        /// <summary>
        /// Gets the network stream used for sending the file. It checks if the 
        /// recipient has accepted the file. If recipient has accepted, file transfer 
        /// begins else transfer is cancelled.
        /// </summary>
        private void InitSendFile()
        {
            try {
                sendStream = sendClient.GetStream();

                int controlByte = sendStream.ReadByte();
                switch (controlByte) {
                    case CTRL_ACCEPT:
                        SendFile();
                        break;
                    case CTRL_DECLINE:
                        sendClient.Close();
                        sendStream.Close();

                        UpdateStatus(FileTransfer.Status.Declined);
                        this.BeginInvoke(new InvokeDelegate(DisplayStatus));
                        this.BeginInvoke(new InvokeDelegate(SetTableLayout));
                        break;
                }
            }
            catch {
                UpdateStatus(FileTransfer.Status.Failed);
                this.BeginInvoke(new InvokeDelegate(DisplayStatus));
                this.BeginInvoke(new InvokeDelegate(SetTableLayout));
            }
        }

        /// <summary>
        /// Start sending the file on a new thread.
        /// </summary>
        private void SendFile()
        {
            UpdateStatus(FileTransfer.Status.Sending);
            this.BeginInvoke(new InvokeDelegate(DisplayStatus));
            this.BeginInvoke(new InvokeDelegate(SetTableLayout));

            //  Execute the file transmission on a separate thread.
            //  This will ensure that the UI is responsive during file transfer.
            Thread thread = new Thread(new ThreadStart(SendStream));
            thread.Start();
        }

        /// <summary>
        /// This method handles the actual file transmission. It reads data from the
        /// file and writes to the network stream.
        /// </summary>
        private void SendStream()
        {
            FileStream fs = null;
            try {
                //  Open file as stream.
                //  Write file stream to network stream.

                fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);                
                byte[] buffer = new byte[bufferSize];
                long unsentCount = fileSize;
                position = 0;

                //  Create timer to update transfer progress.
                CreateTimer();

                while (unsentCount > 0) {
                    if (this.bCancelTransfer)
                        break;

                    long bytesSent = (bufferSize < unsentCount) ? bufferSize : unsentCount;
                    fs.Read(buffer, 0, (int)bytesSent);
                    sendStream.Write(buffer, 0, (int)bytesSent);
                    position += bytesSent;
                    unsentCount = fileSize - position;
                    progressBar.BeginInvoke(new UpdateProgressDelegate(this.UpdateProgress), new object[] { position });
                }

                if (this.bCancelTransfer)
                    this.BeginInvoke(new UpdateStatusDelegate(this.UpdateStatus), new object[] { FileTransfer.Status.Cancelled });
                else
                    this.BeginInvoke(new UpdateStatusDelegate(this.UpdateStatus), new object[] { FileTransfer.Status.Completed });
            }
            catch (Exception ex) {
                this.BeginInvoke(new UpdateStatusDelegate(this.UpdateStatus), new object[] { FileTransfer.Status.Failed });
                ErrorHandler.ShowError(ex);
            }
            finally {
                fs.Close();

                sendClient.Close();
                sendStream.Close();

                this.BeginInvoke(new InvokeDelegate(DisplayStatus));
                this.BeginInvoke(new InvokeDelegate(SetTableLayout));
            }
        }

        private void ShowNotification()
        {
            string action = this.mode == FileTransfer.Modes.Send ? "sent" : "received";
            NotifyEventArgs e = new NotifyEventArgs("File Transfer Complete",
                "'" + fileName + "' has been " + action + " succesfully.");
            if (Notify != null)
                Notify(this, e);
        }

        /// <summary>
        /// Cancels the file transfer and displays appropriate message.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            CancelTransfer();
        }

        /// <summary>
        /// Declines the file transfer. A message is sent to the sender notifying the decline.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDecline_Click(object sender, EventArgs e)
        {
            try {
                //  Send a control byte to sender indicating that file has been declined.
                receiveStream.WriteByte(CTRL_DECLINE);

                UpdateStatus(FileTransfer.Status.Declined);
                DisplayStatus();
                SetTableLayout();
            }
            catch {
                UpdateStatus(FileTransfer.Status.Failed);
                DisplayStatus();
                SetTableLayout();
            }
        }

        /// <summary>
        /// Accept the file transfer. Start receiving the file.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAccept_Click(object sender, EventArgs e)
        {
            try {
                ReceiveFile();
            }
            catch {
                UpdateStatus(FileTransfer.Status.Failed);
                DisplayStatus();
                SetTableLayout();
            }
        }

        private void FileTransferControl_Resize(object sender, EventArgs e)
        {
            DisplayInfo();
        }

        private void FileTransferControl_MouseDown(object sender, MouseEventArgs e)
        {
            if (Selected != null) {
                Selected(this, e);
            }
        }

        private void FileTransferControl_MouseClick(object sender, MouseEventArgs e)
        {
            if (MouseClick != null)
                MouseClick(this, e);
        }

        private void FileTransferControl_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            //  Open the file.
            OpenFile();
        }

        private void FileTransferControl_SystemColorsChanged(object sender, EventArgs e)
        {
            if (isSelected)
                SetSelectedLook();
        }
    }

    /// <summary>
    /// Class that defines the enumerations used for file transfer.
    /// </summary>
    public static class FileTransfer
    {
        public enum Modes
        {
            Send = 0,
            Receive,
            Max
        }

        public enum Status
        {
            None = 0,           //  0 0000 0000
            Waiting = 1,        //  0 0000 0001
            Confirming = 3,     //  0 0000 0011
            Sending = 4,        //  0 0000 0100
            Receiving = 12,     //  0 0000 1100
            Completed = 16,     //  0 0001 0000
            Declined = 48,      //  0 0011 0000
            Cancelled = 80,     //  0 0101 0000
            Failed = 144,       //  0 1001 0000
            Max = 256           //  1 0000 0000
        }
    }
}
