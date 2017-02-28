using System;
using System.Collections.Generic;
using System.Drawing;
using System.Net;
using System.Net.Sockets;
using System.Net.NetworkInformation;
using LANMedia.CustomControls;

namespace LANChat
{
    /// <summary>
    /// All classes, properties and helper methods used internally by MainForm are defined here.
    /// </summary>
    partial class MainForm
    {
        private bool bModalOpen;
        private bool bSilentMode;
        private bool bShowNotifications;
        private bool bMessageToForeground;
        private bool bDisplayAlerts;
        private bool bSuspendAlertsOnBusy;
        private bool bSuspendAlertsOnDND;
        private bool bPlaySounds;
        private bool bSuspendSoundsOnBusy;
        private bool bSuspendSoundsOnDND;
        private bool bChatWindowed;
        private bool bNetworkAvailable;
        private int connectionTimeout;
        private int connectionRetries;
        private int userRefreshPeriod;
        private DateTime userRefreshTime;
        private Font defaultFont;
        private Color defaultFontColor;
        private string localUserKey;
        private string localUserName;
        private string localClientVersion;
        private string localStatus;
        private IPAddress broadcastAddress;
        private IPAddress localAddress;
        private IPAddress subnetMask;
        private int portNumber;
        private UdpClient receiveUdpClient;
        private UdpClient sendUdpClient;
        private List<User> userList;
        private bool bDirtyList;
        private List<ReceivedMessage> messageQueue;
        private List<PendingQuery> queryQueue;
        private List<FileTransferInfo> fileTransferQueue;
        private bool bBkgndMessageShown;
        private bool bFirstTime;
        private TabPageList tabList;
        private WindowList windowList;
        private List<string> groupList;
        private Dictionary<string, string> userGroupLookup;
        private UserInfo userInfo;
        private ActivityMonitor activityMonitor;
        private long idleTime;   //  System idle time in milliseconds.
        private long idleTimeMax;   //  Time period after which system is considered idle.
        private bool bSystemIdle;
        private string oldLocalStatus;

        private delegate void InvokeDelegate();
        private delegate void InvokeDelegateParam(object param);

        public Event.EventSignalHandler EventSignalled;
        public Event.EventSignalHandler TermEventSignalled;

        private class UdpState
        {
            public IPEndPoint EndPoint;
            public UdpClient Client;

            public UdpState(IPEndPoint localEP, UdpClient udpClient)
            {
                this.EndPoint = localEP;
                this.Client = udpClient;
            }
        }

        private class User
        {
            public string Key;      //  Unique key of the user.
            public string Version;  //  Version of client used by user.
            public string Address;  //  User's IP Address.
            public string Name;     //  User name.
            public string Status;   //  User's messenger status.
            public string Group;    //  Group user belongs to.

            public User(string key, string version, string address, string name, string status, string group)
            {
                this.Key = key;
                this.Version = version;
                this.Address = address;
                this.Name = name;
                this.Status = status;
                this.Group = group;     // The default group.
            }
        }

        private class ReceivedMessage
        {
            public string UserKey;
            public MessageTypes Type;
            public string TimeStamp;
            public string UserName;
            public string UserAddress;
            public string Text;

            public ReceivedMessage(string userKey, MessageTypes type, string timeStamp, string userName, string userAddress, string text)
            {
                UserKey = userKey;
                Type = type;
                TimeStamp = timeStamp;
                UserName = userName;
                UserAddress = userAddress;
                Text = text;
            }
        }

        private class FileTransferInfo
        {
            public FileTransfer.Modes Mode;
            public string Key;
            public string UserKey;
            public string UserName;
            public string Address;
            public string FileName;
            public string FileSize;

            public FileTransferInfo(FileTransfer.Modes mode, string key, string userKey, string userName, string address, string fileName, string fileSize)
            {
                Mode = mode;
                Key = key;
                UserKey = userKey;
                UserName = userName;
                Address = address;
                FileName = fileName;
                FileSize = fileSize;
            }
        }

        private class PendingQuery
        {
            public string UserKey;
            public DatagramTypes Type;
            public string TimeStamp;
            public string UserName;
            public string Text;
            public int Retries;

            public PendingQuery(string userKey, DatagramTypes type, string timeStamp, string userName, string text, int retries)
            {
                UserKey = userKey;
                Type = type;
                TimeStamp = timeStamp;
                UserName = userName;
                Text = text;
                Retries = retries;
            }
        }

        private struct HeaderInfo
        {
            public string TimeStamp;
            public string Version;
            public DatagramTypes Type;
            public string Key;
            public string Address;
            public string UserName;
        }

        private class TabPageList : List<TabPageEx>
        {
            public TabPageEx this[int index]
            {
                get { return base[index]; }
                set { base[index] = value; }
            }

            public TabPageEx this[string key]
            {
                get {
                    foreach (TabPageEx tabPage in this) {
                        if (tabPage.Name.Equals(key))
                            return tabPage;
                    }
                    return null;
                }
            }

            public bool ContainsKey(string key)
            {
                foreach (TabPageEx tabPage in this) {
                    if (tabPage.Name.Equals(key))
                        return true;
                }
                return false;
            }
        }

        private class WindowList : List<ChatForm>
        {
            public ChatForm this[int index]
            {
                get { return base[index]; }
                set { base[index] = value; }
            }

            public ChatForm this[string key]
            {
                get
                {
                    foreach (ChatForm chatForm in this) {
                        if (chatForm.Name.Equals(key))
                            return chatForm;
                    }
                    return null;
                }
            }

            public bool ContainsKey(string key)
            {
                foreach (ChatForm chatForm in this) {
                    if (chatForm.Name.Equals(key))
                        return true;
                }
                return false;
            }
        }

        #region Helper methods
        /// <summary>
        /// Get the user name of person currently logged in.
        /// </summary>
        /// <returns>User name.</returns>
        private string GetUserName()
        {
            return Environment.UserName;
        }

        /// <summary>
        /// Get the IP Address of the local machine.
        /// </summary>
        /// <returns>IP Address.</returns>
        private IPAddress GetIPAddress()
        {
            foreach (NetworkInterface networkInterface in NetworkInterface.GetAllNetworkInterfaces())
                if (networkInterface.OperationalStatus == OperationalStatus.Up)
                    foreach (UnicastIPAddressInformation ipAddressInfo in networkInterface.GetIPProperties().UnicastAddresses)
                        if (ipAddressInfo.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                            return ipAddressInfo.Address;

            return IPAddress.None;
        }

        /// <summary>
        /// Get the MAC Address of the local machine.
        /// </summary>
        /// <returns></returns>
        private string GetMacAddress()
        {
            foreach (NetworkInterface networkInterface in NetworkInterface.GetAllNetworkInterfaces())
                if (networkInterface.OperationalStatus == OperationalStatus.Up) {
                    PhysicalAddress macAddress = networkInterface.GetPhysicalAddress();
                    return macAddress.ToString();
                }

            return string.Empty;
        }

        /// <summary>
        /// Get the Subnet Mask for IP Address of local machine.
        /// </summary>
        /// <returns>Subnet Mask.</returns>
        private IPAddress GetSubnetMask()
        {
            foreach (NetworkInterface networkInterface in NetworkInterface.GetAllNetworkInterfaces())
                if (networkInterface.OperationalStatus == OperationalStatus.Up)
                    foreach (UnicastIPAddressInformation ipAddressInfo in networkInterface.GetIPProperties().UnicastAddresses)
                        if (ipAddressInfo.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                            return ipAddressInfo.IPv4Mask;

            return IPAddress.None;
        }

        /// <summary>
        /// Creates a unique key for a user by combining IP Address and User name.
        /// </summary>
        /// <param name="userAddress"></param>
        /// <param name="userName"></param>
        /// <returns>User key</returns>
        private string CreateUserKey(string userAddress, string userName)
        {
            return userAddress + ":" + userName;
        }

        /// <summary>
        /// Check if the remote version is older than version of local client.
        /// </summary>
        /// <param name="remoteVersion"></param>
        /// <returns></returns>
        private bool IsVersionOlder(string remoteVersion, string localVersion)
        {
            Version local = new Version(localVersion);
            Version remote = new Version(remoteVersion);

            return remote.CompareTo(local) < 0 ? true : false;
        }
        #endregion
    }
}
