using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Text;
using LANMedia.CustomControls;

namespace LANChat
{
    public partial class ChatControl : UserControl
    {
        /// <summary>
        /// Class that that encapsulates arguments for send event raised 
        /// by Chat Control.
        /// </summary>
        public class ChatArgs : System.EventArgs
        {
            private DatagramTypes datagramType;
            private string key;
            private string message;

            //  Constructor.
            public ChatArgs(DatagramTypes datagramType, string key, string message)
            {
                this.datagramType = datagramType;
                this.key = key;
                this.message = message;
            }

            //  Read-only properties.
            public DatagramTypes DatagramType { get { return datagramType; } }
            public string Key { get { return key; } }
            public string Message { get { return message; } }
        }

        public enum WindowModeEvents
        {
            PopOut = 0,
            PopIn
        }

        /// <summary>
        /// Class that that encapsulates arguments for window mode change
        /// event raised by Chat Control.
        /// </summary>
        public class WindowModeArgs : System.EventArgs
        {
            private string key;
            private WindowModeEvents windowEvent;

            public WindowModeArgs(string key, WindowModeEvents windowEvent)
            {
                this.key = key;
                this.windowEvent = windowEvent;
            }

            //  Read-only properties.
            public string Key { get { return key; } }
            public WindowModeEvents Event { get { return windowEvent; } }
        }

        private class Emoticon
        {
            public int Id { get; set; }
            public string Description { get; set; }
            public string Code { get; set; }
            public string ImageKey { get; set; }

            public Emoticon(int id, string code, string description, string imageKey)
            {
                Id = id;
                Description = description;
                Code = code;
                ImageKey = imageKey;
            }
        }

        //  Properties of the Chat Control.
        private bool bMessageHotKeyMod;
        private bool bMessageToForeground;
        private bool bEnabled;
        private bool bWindowed;
        private bool bCanSave;
        private DateTime lastTypeTime;
        private string lastActionCode;
        private FormatTypes lastFormatType;
        private string key; //  Unique key of the remote user.
        private string localUserName;    //  User name of local user.
        private string localAddress;     //  IP address of local mcahine.
        private string remoteUserName;  //  User name of remote user.
        private string remoteAddress;    //  IP address of the other user.
        private string subnetMask;  //  Subnet mask for the network.
        private Font messageFont;
        private Color messageFontColor;
        private bool showEmoticons;
        private List<string> fontList;
        private List<string> colorList;
        private List<Emoticon> emotList;
        private List<KeyValuePair<int, string>> emotRtf;
        private List<KeyValuePair<int, string>> emotMapper;

        #region Properties
        //  Get Set accessors for properties.
        public bool Enabled
        {
            get { return bEnabled; }
            set
            {
                bEnabled = value;
                btnSendMessage.Enabled = bEnabled;
                tbBtnSendFile.Enabled = bEnabled;
            }
        }

        public string Key
        {
            get { return key; }
            set { key = value; }
        }

        public string LocalUserName
        {
            get { return localUserName; }
            set { localUserName = value; }
        }

        public string LocalAddress
        {
            get { return localAddress; }
            set { localAddress = value; }
        }

        public string RemoteUserName
        {
            get { return remoteUserName; }
            set { remoteUserName = value; }
        }

        public string RemoteAddress
        {
            get { return remoteAddress; }
            set { remoteAddress = value; }
        }

        public string SubnetMask
        {
            get { return subnetMask; }
            set { subnetMask = value; }
        }

        public Font MessageFont
        {
            get { return messageFont; }
            set { messageFont = value; }
        }

        public Color MessageFontColor
        {
            get { return messageFontColor; }
            set { messageFontColor = value; }
        }

        public bool HotKeyMod
        {
            get { return bMessageHotKeyMod; }
            set { bMessageHotKeyMod = value; }
        }

        public bool MessageToForeground { get; set; }

        public bool SilentMode { get; set; }

        public bool ShowEmoticons
        {
            get { return showEmoticons; }
            set { showEmoticons = value; ChangeEmoticonMode(); }
        }

        public bool EmotTextToImage { get; set; }

        public bool ShowTimeStamp { get; set; }

        public bool AddDateToTimeStamp { get; set; }

        public bool Windowed
        {
            get { return bWindowed; }
            set { bWindowed = value; SetToolbarButton(); }
        }
        #endregion

        //  Events and delegates.
        public delegate void SendEventHandler(object sender, ChatArgs e);
        public event SendEventHandler Sending;
        public delegate void TypingEventHandler(object sender, ChatArgs e);
        public event TypingEventHandler Typing;
        public delegate void WindowModeChangeEventHandler(object sender, WindowModeArgs e);
        public event WindowModeChangeEventHandler WindowModeChange;

        public ChatControl()
        {
            InitializeComponent();
            InitUI();

            rtbMessage.ContextMenu = mnuMessageBox;
            rtbMessage.AllowDrop = true;
            rtbMessage.DragEnter += new DragEventHandler(TextBox_DragEnter);
            rtbMessage.DragDrop += new DragEventHandler(TextBox_DragDrop);
            rtbMessageHistory.AllowDrop = true;
            rtbMessageHistory.DragEnter += new DragEventHandler(TextBox_DragEnter);
            rtbMessageHistory.DragDrop += new DragEventHandler(TextBox_DragDrop);

            fontList = new List<string>();
            colorList = new List<string>();
            bEnabled = true;
            bWindowed = false;
            bCanSave = false;
            lastTypeTime = DateTime.MinValue;
            lastFormatType = FormatTypes.Default;
        }

        /// <summary>
        /// Sets input focus to the message box.
        /// </summary>
        public void SetFocus()
        {
            rtbMessage.Focus();
        }

        /// <summary>
        /// Adds an incoming message to the chat history box of the control.
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="text"></param>
        public void ReceiveMessage(MessageTypes type, string timeStamp, string userName, string text)
        {
            switch (type) {
                case MessageTypes.Message:
                    AddMessage(timeStamp, userName, text);
                    ShowActionMessage(userName, "Received", timeStamp);
                    ShowParentWindow();
                    break;
                case MessageTypes.Failed:
                    UpdateChatHistory("The following message was not delivered:", FormatTypes.Status);
                    UpdateChatHistory(userName + ": ", FormatTypes.UserName);
                    UpdateChatHistory(text, FormatTypes.Message);
                    break;
                case MessageTypes.Status:
                    ShowStatusMessage(userName, text);
                    break;
                case MessageTypes.UserAction:
                    ShowActionMessage(userName, text, string.Empty);
                    break;
                case MessageTypes.OldVersion:
                    UpdateChatHistory(remoteUserName + " is using an older version of " + AppInfo.Title, FormatTypes.Status);
                    UpdateChatHistory(". Some features may not be available.\n", FormatTypes.Status);
                    break;
            }
        }

        /// <summary>
        /// Save the chat history to a file in the given path.
        /// </summary>
        /// <param name="path"></param>
        public void SaveHistory(string path)
        {
            //  If there is no message in the chat history box, no need to save.
            //  History with just status messages is not really history.
            if (!bCanSave)
                return;

            try {
                History.Save(remoteUserName, DateTime.UtcNow, rtbMessageHistory.Rtf);
            }
            catch {
            }
        }

        private void InitUI()
        {
            SetControlFonts();
            HideStatusMessage();
            //  Hide the send button.
            tableLayoutPanelMain.ColumnStyles[2].Width = 0;

            //  Control images.
            btnSendMessage.Image = (Image)LANChat.Resources.Properties.Resources.ResourceManager.GetObject("btnSendMessage");

            //  Edit menu.
            imageMenuEx.SetImage(mnuMBCopy, (Image)LANChat.Resources.Properties.Resources.ResourceManager.GetObject("mnuMBCopy"));
            imageMenuEx.SetImage(mnuMBCut, (Image)LANChat.Resources.Properties.Resources.ResourceManager.GetObject("mnuMBCut"));
            imageMenuEx.SetImage(mnuMBDelete, (Image)LANChat.Resources.Properties.Resources.ResourceManager.GetObject("mnuMBDelete"));
            imageMenuEx.SetImage(mnuMBPaste, (Image)LANChat.Resources.Properties.Resources.ResourceManager.GetObject("mnuMBPaste"));
            imageMenuEx.SetImage(mnuMBUndo, (Image)LANChat.Resources.Properties.Resources.ResourceManager.GetObject("mnuMBUndo"));
            imageMenuEx.SetImage(mnuMBRedo, (Image)LANChat.Resources.Properties.Resources.ResourceManager.GetObject("mnuMBRedo"));

            //  Image list.
            imageListToolBar.ImageSize = new Size(16, 16);
            imageListToolBar.Images.Add("Font", (Image)LANChat.Resources.Properties.Resources.ResourceManager.GetObject("il_Font"));
            imageListToolBar.Images.Add("Smiley", (Image)LANChat.Resources.Properties.Resources.ResourceManager.GetObject("il_Smiley"));
            imageListToolBar.Images.Add("SendFile", (Image)LANChat.Resources.Properties.Resources.ResourceManager.GetObject("il_SendFile"));
            imageListToolBar.Images.Add("Save", (Image)LANChat.Resources.Properties.Resources.ResourceManager.GetObject("il_Save"));
            imageListToolBar.Images.Add("PopOut", (Image)LANChat.Resources.Properties.Resources.ResourceManager.GetObject("il_PopOut"));
            imageListToolBar.Images.Add("PopIn", (Image)LANChat.Resources.Properties.Resources.ResourceManager.GetObject("il_PopIn"));

            //  Toolbar buttons.
            tbBtnFont.ImageKey = "Font";
            tbBtnSmiley.ImageKey = "Smiley";
            tbBtnSendFile.ImageKey = "SendFile";
            tbBtnSave.ImageKey = "Save";
            tbBtnPop.ImageKey = "PopOut";

            //  Smiley menu
            LoadEmoticons();
            int count = 0;
            int maxRow = (int)Math.Floor(Math.Sqrt(emotList.Count));
            int maxCol = (int)Math.Ceiling((float)emotList.Count / (float)maxRow);
            //  This is to avoid the popup menu looking like a square block.
            //  Rectangular shape is more ergonomic and aesthetic.
            if (maxRow == maxCol) {
                maxRow--;
                maxCol = (int)Math.Ceiling((float)emotList.Count / (float)maxRow);
            }
            
            int offset = 0;
            bool newRow = false;
            while (offset < maxCol) {
                int index = offset;
                while (index < emotList.Count) {
                    AddEmoticon(index, newRow);
                    newRow = false;
                    index += maxCol;
                }
                newRow = true;
                offset++;
            }
        }

        private void SetControlFonts()
        {
            this.Font = Helper.SystemFont;
            rtbMessageHistory.Font = Definitions.GetPresetFont(FormatTypes.Message);
            rtbMessage.Font = Definitions.GetPresetFont(FormatTypes.Message);
            btnSendMessage.Font = new Font(Helper.SystemFont.FontFamily, 11.25f, FontStyle.Bold);
            lblUserAction.Font = SystemFonts.StatusFont;
        }

        private void AddEmoticon(int index, bool newRow)
        {
            Emoticon emoticon = emotList[index];
            MenuItemEx item = new MenuItemEx((Image)LANChat.Resources.Properties.Resources.ResourceManager.GetObject(emoticon.ImageKey));
            item.ToolTipText = emoticon.Description;
            item.Tag = emoticon.Id;
            item.Click += mnuSmiley_ItemClick;
            item.BarBreak = newRow;
            mnuSmiley.MenuItems.Add(item);
        }

        private void LoadEmoticons()
        {
            System.Xml.XmlReader reader = System.Xml.XmlReader.Create(new System.IO.StringReader(
                        (string)LANChat.Resources.Properties.Resources.ResourceManager.GetObject("emoticon_defs")));

            emotList = new List<Emoticon>();
            emotMapper = new List<KeyValuePair<int, string>>();
            
            int id = -1;

            while (reader.Read()) {
                switch (reader.NodeType) {
                    case System.Xml.XmlNodeType.Element:
                        if (reader.Name.Equals("Emoticon")) {
                            id++;
                            //  Get emoticon code.
                            reader.MoveToNextAttribute();
                            string code = reader.Value;
                            //  Get emoticon description.
                            reader.MoveToNextAttribute();
                            string description = reader.Value;
                            //  Get emoticon image key.
                            reader.MoveToNextAttribute();
                            string imageKey = reader.Value;

                            emotList.Add(new Emoticon(id, code, description, imageKey));
                        }
                        break;
                    case System.Xml.XmlNodeType.Text:
                        string text = reader.Value;

                        emotMapper.Add(new KeyValuePair<int,string>(id, text));
                        break;
                }
            }
            if (reader != null)
                reader.Close();
        }

        private void ShowParentWindow()
        {
            if (this.SilentMode == true)
                return;

            Control control = this.Parent;
            while (!(control is BaseForm))
                control = control.Parent;

            BaseForm parentWindow = (BaseForm)control;
            parentWindow.ShowWindow(this.MessageToForeground, false);
        }

        private void SetToolbarButton()
        {
            tbBtnPop.ImageKey = bWindowed ? "PopIn" : "PopOut";
            tbBtnPop.ToolTipText = bWindowed ? "Pop in" : "Pop out";

            tbChat.ShowToolTips = true;
            tbPop.ShowToolTips = true;
        }

        /// <summary>
        /// Show messages according to the user's actions.
        /// </summary>
        /// <param name="userName">Name of user who performed the action.</param>
        /// <param name="actionCode">The code of a particualr action.</param>
        /// <param name="tag">Additional data related to the action.</param>
        private void ShowActionMessage(string userName, string actionCode, object tag)
        {
            timer.Stop();
            switch (actionCode) {
                case "Typing":
                    lblUserAction.Text = userName + " is typing a message...";
                    //  Start the timer which triggers an event after 5 seconds.
                    timer.Start();
                    break;
                case "Received":
                    DateTime messageTimeStamp = DateTime.FromBinary(long.Parse((string)tag)).ToLocalTime();
                    lblUserAction.Text = "Last message received on " + messageTimeStamp.ToString("d-MMM-yy") + " at " +
                        messageTimeStamp.ToString("h:mm tt") + ".";
                    break;
                default:
                    lblUserAction.Text = string.Empty;
                    break;
            }
            lastActionCode = actionCode;
        }

        private void ShowStatusMessage(string userName, string statusCode)
        {
            //  Show the status panel.
            tableLayoutPanelMain.RowStyles[2].Height = 20;
            lblStatus.Image = (Image)LANChat.Resources.Properties.Resources.ResourceManager.GetObject("lblTip");
            switch (statusCode) {
                case "Offline":
                    lblStatus.Text = userName + " is offline. You may not be able to send messages.";
                    lblStatus.ForeColor = Color.FromArgb(91, 91, 91);
                    break;
                case "Away":
                    lblStatus.Text = userName + " is away.";
                    lblStatus.ForeColor = Color.FromArgb(255, 100, 0);
                    break;
                case "Busy":
                case "DoNotDisturb":
                    lblStatus.Text = userName + " is busy. You may be interrupting.";
                    lblStatus.ForeColor = Color.FromArgb(192, 0, 0);
                    break;
                default:
                    HideStatusMessage();
                    break;
            }
            rtbMessageHistory.ScrollToBottom();
        }

        private void HideStatusMessage()
        {
            // Hide the status panel, but show it as a line.
            tableLayoutPanelMain.RowStyles[2].Height = 1;
            lblStatus.Text = string.Empty;
            lblStatus.Image = null;
        }

        private string GetEmoticonString(string emotCode)
        {
            string emoticonString = string.Empty;

            foreach (Emoticon emoticon in emotList) {
                if (emoticon.Code.Equals(emotCode)) {
                    if (showEmoticons)
                        emoticonString = rtbMessageHistory.ImageToRtf(
                            (Image)LANChat.Resources.Properties.Resources.ResourceManager.GetObject(emoticon.ImageKey));
                    else
                        emoticonString = "(" + emoticon.Description + ")";
                    break;
                }
            }

            return emoticonString;
        }

        private void ChangeEmoticonMode()
        {
            string rtfText = StripEmoticons(rtbMessageHistory.Rtf);
            rtfText = DecodeEmoticons(rtfText);
            rtbMessageHistory.Rtf = rtfText;
        }

        private string StripEmoticons(string rtfText)
        {
            StringBuilder rtf = new StringBuilder();

            int index = 0;
            do {
                int loc = index;
                loc = rtfText.IndexOf(@"\v ", index);
                if (loc == -1) loc = rtfText.IndexOf(@"\v\", index);

                if (loc > -1) {
                    int endLoc = rtfText.IndexOf(@"\v0", loc);
                    endLoc += @"\v0".Length;

                    int emotLoc;
                    if (showEmoticons) emotLoc = rtfText.LastIndexOf(@"(", loc - 1);
                    else emotLoc = rtfText.LastIndexOf(@"{\pict", loc - 1);
                    if (emotLoc > 0)
                        rtf.Append(rtfText.Substring(index, emotLoc - index));
                    rtf.Append(rtfText.Substring(loc, endLoc - loc));
                    loc = endLoc;
                }
                else {
                    loc = rtfText.Length - 1;
                    rtf.Append(rtfText.Substring(index, loc - index));
                    loc++;
                }

                index = loc;
            } while (index < rtfText.Length);

            return rtf.ToString();
        }

        /// <summary>
        /// Replace special emoticon control word with the actual Rtf control data.
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        private string DecodeEmoticons(string rtfText)
        {
            StringBuilder rtf = new StringBuilder();
            int index = 0;
            do {
                int loc = index;
                loc = rtfText.IndexOf(@"\v ", index);
                if (loc == -1) loc = rtfText.IndexOf(@"\v\", index);

                if (loc > -1) {
                    rtf.Append(rtfText.Substring(index, loc - index));

                    int startLoc = rtfText.IndexOf(@"emot", loc);
                    int endLoc = rtfText.IndexOf(@";", startLoc);
                    string emotCode = rtfText.Substring(startLoc, endLoc - startLoc);

                    endLoc = rtfText.IndexOf(@"\v0", endLoc);
                    endLoc += @"\v0".Length;

                    rtf.Append(GetEmoticonString(emotCode));
                    rtf.Append(rtfText.Substring(loc, endLoc - loc));

                    loc = endLoc;
                }
                else {
                    loc = rtfText.Length - 1;
                    rtf.Append(rtfText.Substring(index, loc - index));
                    loc++;
                }

                index = loc;
            } while (index < rtfText.Length);

            return rtf.ToString();
        }

        /// <summary>
        /// Replace emoticons in the messagebox with special control word.
        /// This is to reduce the size of the message sent across the network.
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        private string EncodeEmoticons(string rtfText)
        {
            StringBuilder rtf = new StringBuilder();

            //  Remove all orphan hidden tags.
            int index = 0;
            do {
                int loc = index;
                loc = rtfText.IndexOf(@"\v ", index);

                if (loc > -1) {
                    rtf.Append(rtfText.Substring(index, loc - index));

                    int endLoc = rtfText.IndexOf(@"\v0", loc);
                    endLoc += @"\v0".Length;

                    //  Check if this hidden tag is preceded by a pict tag.
                    //  Otherwise it should be ignored.
                    int picLoc = loc - 1;
                    //  See if the character just before \v is a closing brace '}'.
                    if (rtfText.Substring(picLoc, 1).Equals(@"}")) {
                        //  Closing brace found. Now get its corresponding opening brace '{'.
                        picLoc = rtfText.LastIndexOf(@"{", picLoc);
                        //  Check if it is a pict tag.
                        if (rtfText.IndexOf(@"{\pict", picLoc) == picLoc) {
                            //  Valid hidden tag.
                            rtf.Append(rtfText.Substring(loc, endLoc - loc));
                        }
                    }

                    loc = endLoc;
                }
                else {
                    loc = rtfText.Length - 1;
                    rtf.Append(rtfText.Substring(index, loc - index));
                    loc++;
                }

                index = loc;
            } while (index < rtfText.Length);

            //  All the orphan hidden tags have been removed. Copy this "clean" rtf to a string.
            string rtfSource = rtf.ToString();
            rtf.Clear();

            //  Now remove all the pict tags so that only the corresponding hidden tags
            //  are left in the rtf string.
            index = 0;
            do {
                int loc = index;
                loc = rtfSource.IndexOf(@"{\pict", index);

                if (loc > -1) {
                    rtf.Append(rtfSource.Substring(index, loc - index));
                    loc = rtfSource.IndexOf(@"}", loc);
                    loc++;
                }
                else {
                    loc = rtfSource.Length - 1;
                    rtf.Append(rtfSource.Substring(index, loc - index));
                    loc++;
                }

                index = loc;
            } while (index < rtfSource.Length);

            return rtf.ToString();
        }

        /// <summary>
        /// Adds an entry to the chat history box.
        /// </summary>
        /// <param name="text">The text to be added.</param>
        private void UpdateChatHistory(string text, FormatTypes formatType)
        {
            //  This code block ensures optimum line spacing between different types of formatted texts.
            switch (lastFormatType) {
                case FormatTypes.Status:
                    if (formatType != FormatTypes.Status)
                        rtbMessageHistory.AppendTextAsRtf("\n");
                    break;
                case FormatTypes.Message:
                    if (formatType != FormatTypes.UserName)
                        rtbMessageHistory.AppendTextAsRtf("\n");
                    break;
            }
            lastFormatType = formatType;

            //  Ensure there is no more than one blank line between messages.
            if (text.StartsWith(Environment.NewLine)) {
                if (rtbMessageHistory.Text.EndsWith("\n\n")) {
                    text = text.TrimStart(Environment.NewLine.ToCharArray());
                }
            }

            switch (formatType) {
                case FormatTypes.UserName:
                case FormatTypes.Status:
                    rtbMessageHistory.AppendTextAsRtf(text, Definitions.GetPresetFont(formatType), Definitions.GetPresetColor(formatType));
                    break;
                case FormatTypes.Message:
                    string rtfText = DecodeEmoticons(text);
                    rtfText = rtfText.Insert(rtfText.LastIndexOf(@"\f0"), @"\li240");
                    rtbMessageHistory.AppendRtf(rtfText);

                    //  Enable button that saves chat history.
                    if (!bCanSave && rtbMessageHistory.TextLength > 0) {
                        tbBtnSave.Enabled = true;
                        bCanSave = true;
                    }
                    break;
                default:
                    rtbMessageHistory.AppendTextAsRtf(text);
                    break;
            }

            //  For auto scrolling chat history box.
            rtbMessageHistory.ScrollToBottom();
        }

        private string FormatMessage()
        {
            string rtbText = rtbMessage.Text.Trim();
            //  If text contains no characters, return the empty string
            //  to indicate that the message should not be sent.
            if (rtbText.Length == 0)
                return rtbText;

            if (EmotTextToImage) {
                //  Replace all emoticon texts with the actual images.
                foreach (KeyValuePair<int, string> map in emotMapper) {
                    string text = map.Value;
                    int index = 0;
                    while ((index < rtbMessage.TextLength) &&
                        ((index = rtbMessage.Find(text, index, RichTextBoxFinds.None)) > -1)) {
                        int selLength = text.Length;
                        rtbMessage.Select(index, selLength);
                        rtbMessage.InsertImage((Image)LANChat.Resources.Properties.Resources.ResourceManager.GetObject(emotList[map.Key].ImageKey));
                        rtbMessage.InsertInnerRtf(@"\v " + emotList[map.Key].Code + @";\v0");
                        index++;
                    }
                }
            }

            //  Replace the emoticon images with special control words.
            return EncodeEmoticons(rtbMessage.Rtf);
        }

        /// <summary>
        /// Sends the current typed message by invoking the SendHandler event,
        /// adds it to the chat history box, then clears the message type box.
        /// </summary>
        private void SendMessage()
        {
            if (bEnabled == false)
                return;

            rtbMessage.Enabled = false;
            string rtfText = FormatMessage();

            //  Send message only if its not empty.
            if (rtfText.Equals(string.Empty)) {
                rtbMessage.Enabled = true;
                return;
            }

            ChatArgs e = new ChatArgs(DatagramTypes.Message, key, rtfText);
            Sending(this, e);

            //  Add the message to the chat history box.
            DateTime messageTimeStamp = DateTime.UtcNow;
            string timeStamp = messageTimeStamp.ToBinary().ToString();
            AddMessage(timeStamp, localUserName, rtfText);

            rtbMessage.Clear();
            rtbMessage.Enabled = true;
        }

        private void AddMessage(string timeStamp, string userName, string text)
        {
            DateTime messageTimeStamp = DateTime.FromBinary(long.Parse(timeStamp)).ToLocalTime();
            if (ShowTimeStamp) {
                string messageTime = messageTimeStamp.ToString("h:mm tt");
                if (AddDateToTimeStamp)
                    messageTime = messageTimeStamp.ToString("d-MMM-yy") + " " + messageTime;
                userName += " [" + messageTime + "]";
            }
            userName += ":\n";
            UpdateChatHistory(userName, FormatTypes.UserName);
            UpdateChatHistory(text, FormatTypes.Message);
        }

        private void rtbMessage_KeyDown(object sender, KeyEventArgs e)
        {
            //  Check if user pressed the Enter key. If Enter key was pressed
            //  clear contents of type box and transfer it to top chat box.
            if (e.KeyCode == Keys.Enter && e.Control == bMessageHotKeyMod) {
                //  We do not want the enter key press to be part of the chat
                //  text, so it is suppressed.
                e.SuppressKeyPress = true;
                lastTypeTime = DateTime.MinValue;
                SendMessage();
                rtbMessage.Focus();
            }
            else {
                TimeSpan timeSpan = DateTime.Now.Subtract(lastTypeTime);
                //  Raise event only if the last event was triggered more than 4 seconds ago.
                if (timeSpan.TotalSeconds > 4) {
                    lastTypeTime = DateTime.Now;
                    if (Sending != null) {
                        ChatArgs chatArgs = new ChatArgs(DatagramTypes.UserAction, key, UserActions.Typing.GetStringValue());
                        Sending(this, chatArgs);
                    }
                }
            }
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            SendMessage();
            rtbMessage.Focus();
        }

        private void ChatControl_Load(object sender, EventArgs e)
        {
            rtbMessage.Font = messageFont;
            rtbMessage.ForeColor = messageFontColor;
            rtbMessage.Focus();
        }

        private void rtbMessageHistory_Resize(object sender, EventArgs e)
        {
            //  For auto scrolling chat history box.
            rtbMessageHistory.SelectionStart = rtbMessageHistory.TextLength;
            rtbMessageHistory.ScrollToCaret();
        }

        private void mnuMessageBox_Popup(object sender, EventArgs e)
        {
            mnuMBUndo.Enabled = rtbMessage.CanUndo;
            mnuMBRedo.Enabled = rtbMessage.CanRedo;
            mnuMBCut.Enabled = (rtbMessage.SelectionLength > 0);
            mnuMBCopy.Enabled = (rtbMessage.SelectionLength > 0);
            mnuMBPaste.Enabled = Clipboard.ContainsText();
            mnuMBDelete.Enabled = (rtbMessage.SelectionLength > 0);
            mnuMBSelectAll.Enabled = (rtbMessage.TextLength > 0);
        }

        private void mnuMBUndo_Click(object sender, EventArgs e)
        {
            rtbMessage.Undo();
        }

        private void mnuMBRedo_Click(object sender, EventArgs e)
        {
            rtbMessage.Redo();
        }

        private void mnuMBCut_Click(object sender, EventArgs e)
        {
            rtbMessage.Cut();
        }

        private void mnuMBCopy_Click(object sender, EventArgs e)
        {
            rtbMessage.Copy();
        }

        private void mnuMBPaste_Click(object sender, EventArgs e)
        {
            rtbMessage.Paste();
        }

        private void mnuMBDelete_Click(object sender, EventArgs e)
        {
            rtbMessage.SelectedText = string.Empty;
        }

        private void mnuMBSelectAll_Click(object sender, EventArgs e)
        {
            rtbMessage.SelectAll();
        }

        private void tbChat_ButtonClick(object sender, ToolBarButtonClickEventArgs e)
        {
            switch (e.Button.Name) {
                case "tbBtnFont":
                    fontDialog.Font = rtbMessage.Font;
                    fontDialog.Color = rtbMessage.ForeColor;
                    if (fontDialog.ShowDialog() == DialogResult.OK) {
                        rtbMessage.Font = fontDialog.Font;
                        rtbMessage.ForeColor = fontDialog.Color;
                    }
                    break;
                case "tbBtnSmiley":
                    tbBtnSmiley.Pushed = true;
                    Point pos = new Point(tbBtnSmiley.Rectangle.Left, tbBtnSmiley.Rectangle.Bottom);
                    mnuSmiley.Show(tbChat, pos, tbBtnSmiley.Rectangle);
                    tbBtnSmiley.Pushed = false;
                    break;
                case "tbBtnSave":
                    saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                    saveFileDialog.FileName = string.Empty;
                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                        rtbMessageHistory.SaveFile(saveFileDialog.FileName);
                    break;
                case "tbBtnSendFile":
                    //openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                    openFileDialog.FileName = string.Empty;
                    if (openFileDialog.ShowDialog() == DialogResult.OK) {
                        ChatArgs chatArgs = new ChatArgs(DatagramTypes.File, key, openFileDialog.FileName);
                        Sending(this, chatArgs);
                    }
                    break;
                case "tbBtnPop":
                    WindowModeEvents windowEvent = bWindowed ? WindowModeEvents.PopIn : WindowModeEvents.PopOut;
                    WindowModeArgs windowModeArgs = new WindowModeArgs(key, windowEvent);
                    tbChat.ShowToolTips = false;
                    tbPop.ShowToolTips = false;
                    WindowModeChange(this, windowModeArgs);
                    break;
            }
            rtbMessage.Focus();
        }

        private void mnuSmiley_ItemClick(object sender, EventArgs e)
        {
            MenuItemEx item = (MenuItemEx)sender;
            rtbMessage.InsertImage(item.Image);
            int emotId = (int)item.Tag;
            rtbMessage.InsertInnerRtf(@"\v " + emotList[emotId].Code + @";\v0");
            rtbMessage.Focus();
        }

        private void rtbMessageHistory_MouseDown(object sender, MouseEventArgs e)
        {
            Win32.HideCaret(rtbMessageHistory.Handle);
        }

        private void rtbMessageHistory_KeyUp(object sender, KeyEventArgs e)
        {
            Win32.HideCaret(rtbMessageHistory.Handle);
        }

        private void TextBox_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy;
            else
                e.Effect = DragDropEffects.None;
        }

        private void TextBox_DragDrop(object sender, DragEventArgs e)
        {
            string[] fileNames = (string[])e.Data.GetData(DataFormats.FileDrop);
            foreach (string fileName in fileNames) {
                ChatArgs chatArgs = new ChatArgs(DatagramTypes.File, key, fileName);
                Sending(this, chatArgs);
                break;
            }
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            //  If a user typing message was posted 5 seconds ago, clear it.
            if (lastActionCode.Equals("Typing"))
                lblUserAction.Text = string.Empty;
        }
    }
}
