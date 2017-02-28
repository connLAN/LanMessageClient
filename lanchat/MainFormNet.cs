using System;
using System.Text;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;

namespace LANChat
{
    /// <summary>
    /// All the network related functions using UDP are defined here.
    /// </summary>
    partial class MainForm
    {
        #region Network level events
        private void Networking_AddressChanged(object sender, EventArgs e)
        {
            localAddress = GetIPAddress();
            subnetMask = GetSubnetMask();

            UpdateUser(localUserKey, UserInfoFlags.Address, localAddress.ToString());
        }

        private void Networking_AvailabilityChanged(object sender, NetworkAvailabilityEventArgs e)
        {
            bNetworkAvailable = e.IsAvailable;
            ShowNetworkStatus(bNetworkAvailable);

            //  Send an announce broadcast when network connection becomes available.
            if (bNetworkAvailable)
                SendBroadcast(DatagramTypes.Announce, localStatus);
        }
        #endregion

        /// <summary>
        /// Initializes network level data.
        /// </summary>
        private void InitNetworking()
        {
            NetworkChange.NetworkAvailabilityChanged += new NetworkAvailabilityChangedEventHandler(Networking_AvailabilityChanged);
            NetworkChange.NetworkAddressChanged += new NetworkAddressChangedEventHandler(Networking_AddressChanged);

            bNetworkAvailable = NetworkInterface.GetIsNetworkAvailable();

            //  Get the IP Address and Subnet mask.
            localAddress = GetIPAddress();
            subnetMask = GetSubnetMask();
        }

        private void StartNetworking()
        {
            SendBroadcast(DatagramTypes.Depart, string.Empty);
            ReceiveMessages();
            SendBroadcast(DatagramTypes.Announce, localStatus);
        }

        /// <summary>
        /// Parses a string and populate HeaderInfo structure.
        /// </summary>
        /// <param name="headerString">The string to parse.</param>
        /// <returns>The populated structure.</returns>
        private HeaderInfo ParseHeader(string headerString)
        {
            HeaderInfo headerInfo = new HeaderInfo();

            string[] headerAttributes = headerString.Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            headerInfo.Version = headerAttributes[(int)MessageHeader.Version];
            headerInfo.TimeStamp = headerAttributes[(int)MessageHeader.TimeStamp];
            headerInfo.Type = (DatagramTypes)Enum.Parse(
                    typeof(DatagramTypes), headerAttributes[(int)MessageHeader.Type]);
            headerInfo.Key = headerAttributes[(int)MessageHeader.Key];
            headerInfo.Address = headerAttributes[(int)MessageHeader.Address];
            headerInfo.UserName = headerAttributes[(int)MessageHeader.UserName];

            return headerInfo;
        }

        private void ReceiveCallback(IAsyncResult ar)
        {
            try {
                UdpClient udpClient = (UdpClient)((UdpState)ar.AsyncState).Client;
                IPEndPoint remoteEP = (IPEndPoint)((UdpState)ar.AsyncState).EndPoint;

                if (udpClient == null || udpClient.Client == null)
                    return;

                byte[] cipherMessage = udpClient.EndReceive(ar, ref remoteEP);
                string receiveString = Security.DecryptString(cipherMessage);

                int headerStart = receiveString.IndexOf(Environment.NewLine);
                int headerLength = int.Parse(receiveString.Substring(0, headerStart));
                string headerString = receiveString.Substring(headerStart, headerLength);
                int dataStart = headerStart + headerLength;
                string dataString = receiveString.Substring(dataStart);
                string[] dataAttributes = dataString.Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

                HeaderInfo headerInfo = ParseHeader(headerString);
                //  Unique key for this user - MacAddress:UserName eg: 001CBFC90A03:Dilip
                string userKey = headerInfo.Key;
                
                switch (headerInfo.Type) {
                    case DatagramTypes.Announce:
                        //  Add a user to the user list.
                        string messageText = dataAttributes[0];
                        string userStatus = messageText;
                        bool bNewUser = AddUser(userKey, headerInfo.Version, headerInfo.Address, headerInfo.UserName, userStatus);
                        if (bNewUser) {
                            SendBroadcast(DatagramTypes.Announce, localStatus);
                        }
                        UpdateUser(userKey, UserInfoFlags.Status, userStatus);
                        break;
                    case DatagramTypes.Depart:
                        //  Remove user from the user list.
                        RemoveUser(userKey);
                        RemoveQueryFromQueue(userKey, DatagramTypes.Query);
                        break;
                    case DatagramTypes.Broadcast:
                        //  Remote user has sent a broadcast message to everyone.
                        messageText = dataString;
                        AddMessageToQueue(userKey, MessageTypes.Broadcast, headerInfo.TimeStamp, headerInfo.UserName, headerInfo.Address, messageText);
                        break;
                    case DatagramTypes.Message:
                        //  Remote user has sent a message.
                        messageText = dataString;
                        AddMessageToQueue(userKey, MessageTypes.Message, headerInfo.TimeStamp, headerInfo.UserName, headerInfo.Address, messageText);
                        SendMessage(DatagramTypes.Status, userKey, headerInfo.TimeStamp);
                        break;
                    case DatagramTypes.File:
                        //  Remote user is requesting permission to send a file.
                        messageText = dataString;
                        AddMessageToQueue(userKey, MessageTypes.File, headerInfo.TimeStamp, headerInfo.UserName, headerInfo.Address, messageText);
                        break;
                    case DatagramTypes.Query:
                        //  Add this user if user is not present in the user list.
                        bNewUser = AddUser(userKey, headerInfo.Version, headerInfo.Address, headerInfo.UserName, "Available");
                        //  Remote user has sent a query which needs an acknowledgement.
                        SendMessage(DatagramTypes.Status, userKey, headerInfo.TimeStamp);
                        if (bNewUser) {
                            SendBroadcast(DatagramTypes.Announce, localStatus);
                        }
                        break;
                    case DatagramTypes.Status:
                        //  Remote user has sent an acknowledgement.
                        messageText = dataAttributes[0];
                        RemoveQueryFromQueue(userKey, messageText);
                        break;
                    case DatagramTypes.Notify:
                        //  Remote user has sent a ntofication to all users.
                        messageText = dataAttributes[0];
                        userStatus = messageText;
                        UpdateUser(userKey, UserInfoFlags.Status, userStatus);
                        break;
                    case DatagramTypes.UserQuery:
                        //  Remote user has sent a user info query which needs a response.
                        SendMessage(DatagramTypes.UserInfo, userKey, string.Empty);
                        break;
                    case DatagramTypes.UserInfo:
                        //  Remote user has sent a response to user info query.
                        messageText = dataString;
                        AddMessageToQueue(userKey, MessageTypes.UserInfo, headerInfo.TimeStamp, headerInfo.UserName, headerInfo.Address, messageText);
                        break;
                    case DatagramTypes.UserAction:
                        messageText = dataAttributes[0];
                        AddMessageToQueue(userKey, MessageTypes.UserAction, headerInfo.TimeStamp, headerInfo.UserName, headerInfo.Address, messageText);
                        break;
                }

                //  Keep listening for incoming messages.
                ReceiveMessages();
            }
            catch (Exception ex) {
                ErrorHandler.ShowError(ex);
            }
        }

        private void ReceiveMessages()
        {
            try {
                IPEndPoint localEP = new IPEndPoint(IPAddress.Any, portNumber);
                if (receiveUdpClient == null)
                    receiveUdpClient = new UdpClient(localEP);

                UdpState udpState = new UdpState(localEP, receiveUdpClient);

                receiveUdpClient.BeginReceive(ReceiveCallback, udpState);
            }
            catch (Exception ex) {
                ErrorHandler.ShowError(ex);
            }
        }

        private void SendCallback(IAsyncResult ar)
        {
            try {
                UdpClient udpClient = (UdpClient)((UdpState)ar.AsyncState).Client;
                if (udpClient != null)
                    udpClient.EndSend(ar);
            }
            catch (SocketException sex) {
                //  NoBufferSpaceAvailable error can occur when the system
                //  comes out of sleep or stand by. Ignore it.
                if (sex.SocketErrorCode != SocketError.NoBufferSpaceAvailable)
                    ErrorHandler.ShowError(sex);
            }
            catch (Exception ex) {
                ErrorHandler.ShowError(ex);
            }
        }

        private void SendBroadcast(DatagramTypes datagramType, string text)
        {
            try {
                string timeStamp = DateTime.UtcNow.ToBinary().ToString();

                IPEndPoint remoteEP = new IPEndPoint(broadcastAddress, portNumber);
                if (sendUdpClient == null)
                    sendUdpClient = new UdpClient();

                UdpState udpState = new UdpState(remoteEP, sendUdpClient);

                SendDatagram(remoteEP, udpState, timeStamp, datagramType, text);
            }
            catch (Exception ex) {
                ErrorHandler.ShowError(ex);
            }
        }

        private void SendMessage(DatagramTypes datagramType, string remoteUserKey, string text)
        {
            try {
                string timeStamp = DateTime.UtcNow.ToBinary().ToString();

                User remoteUser = GetUser(remoteUserKey);
                //  If user does not exist in user list, display a failure message.
                if (remoteUser == null) {
                    if (datagramType == DatagramTypes.Message) {
                        AddMessageToQueue(remoteUserKey, MessageTypes.Failed, timeStamp, localUserName, localAddress.ToString(), text);
                    }
                    return;
                }
                string remoteUserName = remoteUser.Name;
                IPAddress remoteAddress = IPAddress.Parse(remoteUser.Address);
                IPEndPoint remoteEP = new IPEndPoint(remoteAddress, portNumber);
                if (sendUdpClient == null)
                    sendUdpClient = new UdpClient();

                UdpState udpState = new UdpState(remoteEP, sendUdpClient);

                switch (datagramType) {
                    case DatagramTypes.Message:
                        AddQueryToQueue(remoteUserKey, DatagramTypes.Message, timeStamp, localUserName, text, 0);
                        break;
                    case DatagramTypes.File:
                        string filePath = text;
                        string key = System.Guid.NewGuid().ToString();
                        FileTransferInfo fileInfo = SendFile(key, remoteUserKey, remoteUserName, remoteAddress.ToString(), filePath);
                        //  Send message only if SendFile returned succesfully.
                        if (fileInfo != null) {
                            string fileName = fileInfo.FileName;
                            string fileSize = fileInfo.FileSize;
                            text = key + Environment.NewLine + fileName + Environment.NewLine + fileSize;
                        }
                        else {
                            //  Do not send the message.
                            return;
                        }
                        break;
                    case DatagramTypes.Query:
                        int retries = int.Parse(text);
                        AddQueryToQueue(remoteUserKey, DatagramTypes.Query, timeStamp, string.Empty, string.Empty, retries);
                        break;
                    case DatagramTypes.UserInfo:
                        text = userInfo.LogonName + Environment.NewLine + userInfo.MachineName + Environment.NewLine
                            + userInfo.IPAddress + Environment.NewLine + userInfo.OSName + Environment.NewLine
                            + userInfo.MessengerVersion;
                        break;
                }

                SendDatagram(remoteEP, udpState, timeStamp, datagramType, text);
            }
            catch (Exception ex) {
                ErrorHandler.ShowError(ex);
            }
        }

        private void SendDatagram(IPEndPoint remoteEP, UdpState udpState, 
            string timeStamp, DatagramTypes datagramType, string text)
        {
            try {
                StringBuilder sendMessage = new StringBuilder();
                sendMessage.AppendLine();
                sendMessage.AppendLine(timeStamp);
                sendMessage.AppendLine(localClientVersion);
                sendMessage.AppendLine(Enum.GetName(typeof(DatagramTypes), datagramType));
                sendMessage.AppendLine(localUserKey);
                sendMessage.AppendLine(localAddress.ToString());
                sendMessage.AppendLine(localUserName);

                int headerLength = sendMessage.Length;
                sendMessage.Insert(0, headerLength);

                sendMessage.AppendLine(text);

                byte[] cipherMessage = Security.EncryptString(sendMessage.ToString());
                sendUdpClient.BeginSend(cipherMessage, cipherMessage.Length, remoteEP, SendCallback, udpState);
            }
            catch (Exception ex) {
                ErrorHandler.ShowError(ex);
            }
        }
    }
}
