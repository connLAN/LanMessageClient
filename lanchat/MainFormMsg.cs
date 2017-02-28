using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace LANChat
{
    /// <summary>
    /// All the messenger related functions are defined here.
    /// </summary>
    partial class MainForm
    {
        /// <summary>
        /// Initializes messaging level data.
        /// </summary>
        private void InitMessaging()
        {
            //  Create a list for holding available users.
            userList = new List<User>();
            bDirtyList = false;

            //  Create a list for holding incoming messages.
            messageQueue = new List<ReceivedMessage>();

            //  Createa a list for holding status messages pending acknowledgement.
            queryQueue = new List<PendingQuery>();

            //  Create a list for holding file transfers.
            fileTransferQueue = new List<FileTransferInfo>();

            //  Get the version of local client.
            localClientVersion = AppInfo.Version;

            //  Get the user name of person currently logged in.
            localUserName = GetUserName();

            //  Create a unique key for the local user.
            string macAddress = GetMacAddress();
            localUserKey = CreateUserKey(macAddress, localUserName);
        }

        /// <summary>
        /// Adds a new item to the user list collection. Item is not
        /// added to collection if it already exists.
        /// </summary>
        /// <param name="key">Unique key for the user.</param>
        /// <param name="userName">Display name of the user.</param>
        private bool AddUser(string key, string version, string address, string name, string status)
        {
            try {
                //  Do not add any users if network connection is not available. So local user
                //  will not be added and shown as online even though there is no connection.
                //  Overridden when in debug mode to allow loopback.
                if (!bNetworkAvailable && !Program.DebugMode)
                    return false;

                //  Lock the list for thread safety.
                lock (userList) {
                    //  Check if this user is already present in the list.
                    foreach (User user in userList) {
                        if (user.Key == key)
                            return false;
                    }
                    //  Check if this user has an entry in the user group lookup.
                    string userGroup;
                    if (userGroupLookup.TryGetValue(key, out userGroup)) {
                        //  Check if this group exists in the group list. If it does not 
                        //  exist, change the group of the user to the default group.
                        if (!groupList.Contains(userGroup)) {
                            userGroup = Properties.Settings.Default.DefaultGroupName;
                            userGroupLookup[key] = userGroup;
                        }
                    }
                    else {
                        //  Add an entry to the lookup table for this user.
                        userGroup = Properties.Settings.Default.DefaultGroupName;
                        userGroupLookup.Add(key, userGroup);
                    }
                    //  Add user to the user list.
                    userList.Add(new User(key, version, address, name, status, userGroup));
                    string timeStamp = DateTime.UtcNow.ToBinary().ToString();
                    if (status != "Offline") {
                        AddMessageToQueue(key, MessageTypes.Online, timeStamp, name, address, string.Empty);
                        AddMessageToQueue(key, MessageTypes.Status, string.Empty, name, string.Empty, status);
                    }
                    //  Set the flag indicating that user list was modified.
                    bDirtyList = true;
                    //  Call the UpdateUserList method asynchronously since the controls
                    //  were created in another thread.
                    this.BeginInvoke(new InvokeDelegate(UpdateUserList));
                    return true;
                }
            }
            catch (Exception ex) {
                ErrorHandler.ShowError(ex);
                return false;
            }
        }

        /// <summary>
        /// Removes an existing user from the user list collection.
        /// </summary>
        /// <param name="key">Unique key for the user.</param>
        private void RemoveUser(string key)
        {
            //  Remove user and show notification if network is available.
            //  This ensures that no notification is displayed when user is
            //  timed out due to network connection loss.
            RemoveUser(key, bNetworkAvailable);
        }

        /// <summary>
        /// Removes an existing user from the user list collection.
        /// </summary>
        /// <param name="key">Unique key for the user.</param>
        /// <param name="notify">Indicates whether a notification should be displayed.</param>
        private void RemoveUser(string key, bool notify)
        {
            try {
                //  Lock the list for thread safety.
                lock (userList) {
                    foreach (User user in userList) {
                        if (user.Key == key) {
                            //  Remove user from the user list.
                            userList.Remove(user);
                            //  Remove all query messages pending acknowledgement from that user from status list.
                            RemoveQueryFromQueue(user.Key, DatagramTypes.Query);
                            //  Cancel all incomplete file transfer to/from this user.
                            CancelFileTransfers(user.Key);
                            string timeStamp = DateTime.UtcNow.ToBinary().ToString();
                            if (notify && user.Status != "Offline") {
                                AddMessageToQueue(user.Key, MessageTypes.Offline, timeStamp, user.Name, user.Address, string.Empty);
                                AddMessageToQueue(user.Key, MessageTypes.Status, string.Empty, user.Name, string.Empty, "Offline");
                            }
                            //  Set the flag indicating that user list was modified.
                            bDirtyList = true;
                            //  Call the UpdateUserList method asynchronously since the controls
                            //  were created in another thread.
                            this.BeginInvoke(new InvokeDelegate(UpdateUserList));
                            break;
                        }
                    }
                }
                //  Update status images for the removed user's conversation tab/window if present.
                if (bDirtyList)
                    this.BeginInvoke(new InvokeDelegateParam(UpdateUserStatusImages), new object[] { key });
            }
            catch (Exception ex) {
                ErrorHandler.ShowError(ex);
            }
        }

        /// <summary>
        /// Remove all users from the user list.
        /// </summary>
        private void RemoveAllUsers()
        {
            while (userList.Count > 0)
                RemoveUser(userList[0].Key, false);
        }

        /// <summary>
        /// Updates a user's status and show visual notification.
        /// </summary>
        /// <param name="key">Unique key of the user.</param>
        /// <param name="flags">Flags indicating which fields has to be updated.</param>
        /// <param name="value">The value of the field.</param>
        private void UpdateUser(string key, UserInfoFlags flags, object value)
        {
            lock (userList) {
                foreach (User user in userList) {
                    if (user.Key.Equals(key)) {
                        //  If Status flag is set, update user's status.
                        if ((flags & UserInfoFlags.Status) != 0) {
                            string status = (string)value;
                            //  Don't do anything if status is same as user's current status.
                            if (user.Status == status)
                                return;

                            string prevStatus = user.Status;
                            user.Status = status;
                            //  If user is changing status to/from Offline, UI has to be updated.
                            //  ie, user should now be hidden/displayed in the online users list.
                            if (user.Status == "Offline" || prevStatus == "Offline") {
                                //  Set the flag indicating that user list was modified.
                                bDirtyList = true;
                                //  Call the UpdateUserList method asynchronously.
                                this.BeginInvoke(new InvokeDelegate(UpdateUserList));

                                //  Show the online/offline notification when user changes status from/to "Offline".
                                if (user.Status == "Offline")
                                    AddMessageToQueue(user.Key, MessageTypes.Offline, string.Empty, user.Name, user.Address, string.Empty);
                                else
                                    AddMessageToQueue(user.Key, MessageTypes.Online, string.Empty, user.Name, user.Address, string.Empty);
                            }
                            //  Display status message in the conversation area.
                            AddMessageToQueue(key, MessageTypes.Status, string.Empty, user.Name, user.Address, status);
                            break;
                        }
                        //  If Address flag is set, update user's address.
                        if ((flags & UserInfoFlags.Address) != 0) {
                            string address = (string)value;
                            user.Address = address;

                            if (key.Equals(localUserKey))
                                userInfo.IPAddress = address;
                        }
                    }
                }
            }
            this.BeginInvoke(new InvokeDelegateParam(UpdateUserStatusImages), new object[] { key });
        }

        /// <summary>
        /// Add a query with pending acknowledgement to the query queue.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="text"></param>
        private void AddQueryToQueue(string key, DatagramTypes type, string timeStamp, string userName, string text, int retries)
        {
            try {
                //  Lock the list for thread safety.
                lock (queryQueue) {
                    queryQueue.Add(new PendingQuery(key, type, timeStamp, userName, text, retries));
                }
            }
            catch (Exception ex) {
                ErrorHandler.ShowError(ex);
            }
        }

        /// <summary>
        /// Removes a query that matches the supplied key and time stamp.
        /// </summary>
        /// <param name="key">Unique key of user.</param>
        /// <param name="timeStamp">Time stamp of status.</param>
        private void RemoveQueryFromQueue(string key, string timeStamp)
        {
            try {
                //  Lock the list for thread safety.
                lock (queryQueue) {
                    foreach (PendingQuery status in queryQueue) {
                        if (status.UserKey == key && status.TimeStamp == timeStamp) {
                            queryQueue.Remove(status);
                            break;
                        }
                    }
                }
            }
            catch (Exception ex) {
                ErrorHandler.ShowError(ex);
            }
        }

        private void RemoveQueryFromQueue(string key, DatagramTypes type)
        {
            try {
                //  Lock the list for thread safety.
                lock (queryQueue) {
                    List<PendingQuery> remList = new List<PendingQuery>();

                    foreach (PendingQuery status in queryQueue) {
                        if (status.UserKey == key && status.Type == type) {
                            remList.Add(status);
                        }
                    }

                    //  Remove all items that are common to both lists
                    queryQueue.RemoveAll(new Predicate<PendingQuery>(delegate(PendingQuery item) {
                        return remList.Contains(item);
                    }));
                }
            }
            catch (Exception ex) {
                ErrorHandler.ShowError(ex);
            }
        }

        /// <summary>
        /// Check query messages with pending acknowledgements to see if any have timed out.
        /// </summary>
        private void CheckPendingQueries()
        {
            try {
                List<string> remUser = new List<string>();
                //  Lock the list for thread safety.
                lock (queryQueue) {
                    List<PendingQuery> remList = new List<PendingQuery>();
                    List<KeyValuePair<string, int>> retryList = new List<KeyValuePair<string, int>>();

                    foreach (PendingQuery status in queryQueue) {
                        string userKey = status.UserKey;
                        string timeStamp = status.TimeStamp;
                        DateTime messageTimeStamp = DateTime.FromBinary(long.Parse(timeStamp));
                        TimeSpan span = DateTime.UtcNow.Subtract(messageTimeStamp);

                        //  If this status message is older than timeout period, it is deemed as failed.
                        if (span.TotalSeconds > connectionTimeout) {
                            //  If there is no response for a ping query, remove that user from user list.
                            if (status.Type == DatagramTypes.Query) {
                                //  If number of retries exceed max number of tries, user is deemed offline.
                                //  Else query is tried again.
                                if (status.Retries >= connectionRetries)
                                    remUser.Add(userKey);
                                else
                                    retryList.Add(new KeyValuePair<string, int>(userKey, status.Retries));
                            }
                            //  If there is no acknowledgement to a sent message, add an error message to message queue.
                            if (status.Type == DatagramTypes.Message)
                                AddMessageToQueue(status.UserKey, MessageTypes.Failed, timeStamp, status.UserName, localAddress.ToString(), status.Text);
                            //  Add all status messages that are to be removed to a new list.
                            remList.Add(status);
                        }
                    }

                    //  Remove all items that are common to both lists
                    queryQueue.RemoveAll(new Predicate<PendingQuery>(delegate(PendingQuery item) {
                        return remList.Contains(item);
                    }));

                    //  Send a query message again for all those that timed out.
                    foreach (KeyValuePair<string, int> retry in retryList) {
                        int numRetries = retry.Value + 1;
                        SendMessage(DatagramTypes.Query, retry.Key, numRetries.ToString());
                    }
                }

                foreach (string userKey in remUser) {
                    RemoveUser(userKey);
                }
            }
            catch (Exception ex) {
                ErrorHandler.ShowError(ex);
            }
        }

        /// <summary>
        /// Add an incoming mesage to the message queue.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="text"></param>
        private void AddMessageToQueue(string key, MessageTypes type, string timeStamp, string userName, string userAddress, string text)
        {
            try {
                //  Do not add the message if the sender is not in the user list.
                if (GetUser(key) == null) {
                    switch (type) {
                        case MessageTypes.Message:
                            return;
                            break;
                    }
                }

                //  Lock the list for thread safety.
                lock (messageQueue) {
                    messageQueue.Add(new ReceivedMessage(key, type, timeStamp, userName, userAddress, text));
                }
                //  Call the CheckNewMessages method asynchronously since the controls
                //  were created in another thread.
                this.BeginInvoke(new InvokeDelegate(CheckNewMessages));
            }
            catch (Exception ex) {
                ErrorHandler.ShowError(ex);
            }
        }

        /// <summary>
        /// Check for new messages in the message queue. Once accessed,
        /// new messages are removed from the queue.
        /// </summary>
        private void CheckNewMessages()
        {
            try {
                //  Lock the list for thread safety.
                lock (messageQueue) {
                    foreach (ReceivedMessage message in messageQueue) {
                        string key = message.UserKey;
                        string timeStamp = message.TimeStamp;
                        string userName = message.UserName;
                        string userAddress = message.UserAddress;
                        string messageText = message.Text;
                        MessageTypes messageType = message.Type;

                        //  Dispatch the message to the appropriate function.
                        switch (messageType) {
                            case MessageTypes.UserInfo:
                                DisplayUserInfo(userName, messageText);
                                break;
                            case MessageTypes.File:
                                ReceiveFile(key, userName, userAddress, messageText);
                                break;
                            case MessageTypes.Broadcast:
                                DisplayBroadcastMessage(userName, messageText);
                                break;
                            default:
                                //  Update the conversation with the message.
                                UpdateConversation(key, messageType, timeStamp, userName, messageText);
                                break;
                        }
                    }
                    messageQueue.Clear();
                }
            }
            catch (Exception ex) {
                ErrorHandler.ShowError(ex);
            }
        }

        private void AddFileTransferToQueue(FileTransferInfo fileInfo)
        {
            try {
                lock (fileTransferQueue) {
                    fileTransferQueue.Add(fileInfo);
                }
            }
            catch (Exception ex) {
                ErrorHandler.ShowError(ex);
            }
        }

        /// <summary>
        /// Cancel all file transfer to/from the user with given key.
        /// </summary>
        /// <param name="userKey"></param>
        private void CancelFileTransfers(string userKey)
        {
            try {
                lock (fileTransferQueue) {
                    foreach (FileTransferInfo fileInfo in fileTransferQueue) {
                        if (fileInfo.UserKey.Equals(userKey)) {
                            this.BeginInvoke(new InvokeDelegateParam(CancelFileTransfer), new object[] { fileInfo.Key });
                        }
                    }
                }
            }
            catch (Exception ex) {
                ErrorHandler.ShowError(ex);
            }
        }

        private void CancelFileTransfer(object key)
        {
            string transferKey = (string)key;
            FileTransferDialog fileTransferDialog = GetFileTransferDialog();
            fileTransferDialog.CancelFileTransfer(transferKey);
        }

        private void ReceiveFile(string userKey, string userName, string sourceAddress, string messageText)
        {
            string[] fileAttributes = messageText.Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            string key = fileAttributes[0];
            string fileName = fileAttributes[1];
            string fileSize = fileAttributes[2];
            FileTransferInfo fileInfo = new FileTransferInfo(FileTransfer.Modes.Receive, key, userKey, userName, sourceAddress,
                fileName, fileSize);

            FileTransferDialog fileTransferDialog = GetFileTransferDialog();
            fileTransferDialog.AddFileTransfer(fileInfo.Mode, fileInfo.UserName, fileInfo.Address, fileInfo.Key, 
                fileInfo.FileName, fileInfo.FileSize);
            PlaySound(NotifyEvents.NewFile);
        }

        private FileTransferInfo SendFile(string key, string userKey, string userName, string remoteAddress, string filePath)
        {
            System.IO.FileInfo sentFileInfo = new System.IO.FileInfo(filePath);
            //  Do not send files larger than max allowed size. 2 GB by default.
            if (sentFileInfo.Length < Properties.Settings.Default.MaxSendFileSize) {
                string fileSize = sentFileInfo.Length.ToString();
                string fileName = sentFileInfo.Name;
                FileTransferInfo fileInfo = new FileTransferInfo(FileTransfer.Modes.Send, key, userKey, userName, remoteAddress,
                    fileName, fileSize);

                FileTransferDialog fileTransferDialog = GetFileTransferDialog();
                fileTransferDialog.AddFileTransfer(fileInfo.Mode, fileInfo.UserName, fileInfo.Address, fileInfo.Key, 
                    filePath, fileInfo.FileSize);
                return fileInfo;
            }
            else {
                string fileSizeString = Helper.FormatSize(Properties.Settings.Default.MaxSendFileSize);
                MessageBox.Show("Please select a file with size less than " + fileSizeString + ".", 
                    AppInfo.Title, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return null;
            }
        }

        /// <summary>
        /// Ping all users in the user list.
        /// </summary>
        private void PingUsers()
        {
            System.Diagnostics.Debug.Print("Pinging users...");

            SendBroadcast(DatagramTypes.Announce, localStatus);
            lock (userList) {
                try {
                    foreach (User user in userList) {
                        SendMessage(DatagramTypes.Query, user.Key, "0");
                    }
                }
                catch (Exception ex) {
                    ErrorHandler.ShowError(ex);
                }
            }
        }

        private void UpdateUserGroupLookup(string key)
        {
            User user = GetUser(key);
            userGroupLookup[user.Key] = user.Group;
        }

        /// <summary>
        /// Get the user with the given key from the user list.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        private User GetUser(string key)
        {
            foreach (User user in userList) {
                if (user.Key == key)
                    return user;
            }

            return null;
        }

        private void GetUserInfo(string key)
        {
            User user = GetUser(key);
            SendMessage(DatagramTypes.UserQuery, user.Key, string.Empty);
        }
    }
}
