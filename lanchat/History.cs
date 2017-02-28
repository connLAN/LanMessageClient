using System;
using System.Text;
using System.IO;
using System.Collections.Generic;

namespace LANChat
{
    internal static class History
    {
        private const string fileName = "messages.db";
        private const short headerSize = 28;
        private const int dbVersion = 1;
        private const string dbMarker = "DB";
        private const string idMarker = "ID";
        private const string dtMarker = "DT";

        private struct Header
        {
            public string Marker;
            public short HeaderSize;
            public int Version;
            public int MessageCount;
            public long FirstIndexPosition;
            public long LastIndexPosition;
        }

        private static void Create(string path)
        {
            FileStream stream = null;
            try {
                stream = new FileStream(FilePath, FileMode.OpenOrCreate, FileAccess.ReadWrite);

                Header header = new Header();
                header.Marker = dbMarker;
                header.HeaderSize = headerSize;
                header.Version = dbVersion;
                header.MessageCount = 0;
                header.FirstIndexPosition = 0;
                header.LastIndexPosition = 0;

                WriteHeader(stream, header);
            }
            catch {
            }
            finally {
                if (stream != null)
                    stream.Close();
            }
        }

        private static Header ReadHeader(FileStream stream)
        {
            stream.Seek(0, SeekOrigin.Begin);

            BinaryReader reader = new BinaryReader(stream, Encoding.ASCII);
            Header header = new Header();
            byte[] dbMarkerBuffer = new byte[ASCIIEncoding.Default.GetByteCount(dbMarker)];
            reader.Read(dbMarkerBuffer, 0, dbMarkerBuffer.Length);
            header.Marker = ASCIIEncoding.Default.GetString(dbMarkerBuffer);
            header.HeaderSize = reader.ReadInt16();
            header.Version = reader.ReadInt32();
            header.MessageCount = reader.ReadInt32();
            header.FirstIndexPosition = reader.ReadInt64();
            header.LastIndexPosition = reader.ReadInt64();

            return header;
        }

        private static void WriteHeader(FileStream stream, Header header)
        {
            stream.Seek(0, SeekOrigin.Begin);
            
            BinaryWriter writer = new BinaryWriter(stream, Encoding.ASCII);
            //  These three are always obtained from global constants.
            header.Marker = dbMarker;
            header.HeaderSize = headerSize;
            header.Version = dbVersion;
            byte[] dbMarkerBuffer = ASCIIEncoding.Default.GetBytes(header.Marker);
            writer.Write(dbMarkerBuffer);
            writer.Write(header.HeaderSize);
            writer.Write(header.Version);
            writer.Write(header.MessageCount);
            writer.Write(header.FirstIndexPosition);
            writer.Write(header.LastIndexPosition);
        }

        private static void UpdateIndex(FileStream stream, long indexPosition, long nextIndexPosition)
        {
            stream.Seek(indexPosition, SeekOrigin.Begin);
            stream.Seek(ASCIIEncoding.Default.GetByteCount(idMarker), SeekOrigin.Current);

            BinaryWriter writer = new BinaryWriter(stream, Encoding.ASCII);
            writer.Write(nextIndexPosition);
        }

        private static long InsertIndex(FileStream stream, long dataPosition, string userName, DateTime timeStamp)
        {
            stream.Seek(0, SeekOrigin.End);
            long indexPosition = stream.Position;

            BinaryWriter writer = new BinaryWriter(stream, Encoding.ASCII);
            writer.Write(ASCIIEncoding.Default.GetBytes(idMarker)); //  Index Marker - "ID" (2 bytes).
            writer.Write(0l);               //  Position of next header node (8 bytes).
            writer.Write(dataPosition);     //  Position of message data (8 bytes).
            writer.Write(timeStamp.ToFileTimeUtc()); //  Time stamp of the message (8 bytes).
            writer.Write((short)ASCIIEncoding.Default.GetByteCount(userName));  //  Length of user name (2 bytes).
            writer.Write(ASCIIEncoding.Default.GetBytes(userName));     //  User name (<Length> bytes).

            return indexPosition;
        }

        private static long InsertData(FileStream stream, string messageData)
        {
            stream.Seek(0, SeekOrigin.End);
            long dataPosition = stream.Position;

            BinaryWriter writer = new BinaryWriter(stream, Encoding.ASCII);
            writer.Write(ASCIIEncoding.Default.GetBytes(dtMarker));  //  Data Marker - "DT" (2 bytes).
            byte[] messageDataBuffer = Security.EncryptString(messageData);
            writer.Write(messageDataBuffer.Length);       //  Length of the message (4 bytes).
            writer.Write(messageDataBuffer);          //  Message data (<Length> bytes).

            return dataPosition;
        }

        public static string FileName { get { return fileName; } }
        
        public static string FilePath
        {
            get
            {
                if (Properties.Settings.Default.UseDefaultLogFile)
                    return Path.Combine(AppInfo.DataPath, FileName);
                else
                    return Properties.Settings.Default.LogFile;
            }
        }

        public static int Save(string userName, DateTime timeStamp, string messageData)
        {
            FileStream stream = null;
            try {
                string path = FilePath;
                //  Create message database if it does not exist.
                if (!File.Exists(path))
                    Create(path);

                //  Open file as stream.
                stream = new FileStream(path, FileMode.OpenOrCreate, FileAccess.ReadWrite);

                Header header = ReadHeader(stream);

                //  Check if header begins with "DB", else data may be corrupt.
                if (!header.Marker.Equals(dbMarker)) {
                    throw new Exception("Data integrity exception in header.");
                }

                long newDataPosition = InsertData(stream, messageData);
                long newIndexPosition = InsertIndex(stream, newDataPosition, userName, timeStamp);

                UpdateIndex(stream, header.LastIndexPosition, newIndexPosition);
                header.MessageCount++;
                header.FirstIndexPosition = (header.FirstIndexPosition == 0) ? newIndexPosition : header.FirstIndexPosition;
                header.LastIndexPosition = newIndexPosition;

                WriteHeader(stream, header);
                return 0;
            }
            catch (Exception ex) {
                return -1;
            }
            finally {
                if (stream != null)
                    stream.Close();
            }
        }

        public static List<MessageInfo> GetList()
        {
            FileStream stream = null;
            List<MessageInfo> messageList = new List<MessageInfo>();
            try {
                string path = FilePath;
                //  Return empty list if file does not exist.
                if (!File.Exists(path))
                    return messageList;

                //  Open file as stream.
                stream = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Read);

                Header header = ReadHeader(stream);

                long nextIndexPosition = header.FirstIndexPosition;
                BinaryReader reader = new BinaryReader(stream, Encoding.ASCII);
                while (nextIndexPosition != 0) {
                    stream.Seek(nextIndexPosition, SeekOrigin.Begin);
                    byte[] idMarkerBuffer = new byte[ASCIIEncoding.Default.GetByteCount(idMarker)];
                    reader.Read(idMarkerBuffer, 0, idMarkerBuffer.Length);
                    string marker = ASCIIEncoding.Default.GetString(idMarkerBuffer);
                    //  Check if index begins with "ID", else data may be corrupt
                    if (!marker.Equals(idMarker))
                        throw new Exception("Data integrity exception in index.");
                    nextIndexPosition = reader.ReadInt64();
                    long dataPosition = reader.ReadInt64();
                    DateTime timeStamp = DateTime.FromFileTimeUtc(reader.ReadInt64()).ToLocalTime();
                    short userNameLength = reader.ReadInt16();
                    byte[] userNameBuffer = new byte[userNameLength];
                    reader.Read(userNameBuffer, 0, userNameBuffer.Length);
                    string userName = ASCIIEncoding.Default.GetString(userNameBuffer);

                    messageList.Add(new MessageInfo(userName, timeStamp, dataPosition));
                }
            }
            catch (Exception ex) {
                messageList.Clear();
                return messageList;
            }
            finally {
                if (stream != null)
                    stream.Close();
            }
            return messageList;
        }

        public static string GetMessage(MessageInfo messageInfo)
        {
            FileStream stream = null;
            string messageData = string.Empty;
            try {
                string path = FilePath;
                //  Return empty list if file does not exist.
                if (!File.Exists(path))
                    return messageData;

                //  Open file as stream.
                stream = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Read);

                stream.Seek(messageInfo.Offset, SeekOrigin.Begin);
                BinaryReader reader = new BinaryReader(stream, Encoding.ASCII);
                byte[] dtMarkerBuffer = new byte[ASCIIEncoding.Default.GetByteCount(dtMarker)];
                reader.Read(dtMarkerBuffer, 0, dtMarkerBuffer.Length);
                string marker = ASCIIEncoding.Default.GetString(dtMarkerBuffer);
                //  Check if index begins with "DT", else data may be corrupt
                if (!marker.Equals(dtMarker))
                    throw new Exception("Data integrity exception in data.");
                int messageDataLength = reader.ReadInt32();
                byte[] messageDataBuffer = new byte[messageDataLength];
                reader.Read(messageDataBuffer, 0, messageDataBuffer.Length);
                messageData = Security.DecryptString(messageDataBuffer);
            }
            catch {
                messageData = string.Empty;
                return messageData;
            }
            finally {
                if (stream != null)
                    stream.Close();
            }
            return messageData;
        }

        public static void Clear()
        {
            try {
                string path = FilePath;
                File.Delete(path);
            }
            catch {
            }
        }

        public static void ConvertLegacy()
        {            
            //  Get log folder path.
            string logPath = AppInfo.LogPath;

            if (!System.IO.Directory.Exists(logPath))
                return;

            //  Get a list of all files in the log folder.
            string[] fileNames = System.IO.Directory.GetFiles(logPath);

            //  Extract name and date from file names and create a list of messages.
            for (int index = 0; index < fileNames.Length; index++) {
                string path = fileNames[index];
                string fileName = Path.GetFileNameWithoutExtension(path);

                int startIndex = fileName.LastIndexOf("_");
                DateTime date = DateTime.FromFileTimeUtc(long.Parse(fileName.Substring(startIndex + 1)));
                fileName = fileName.Remove(startIndex);
                string name = fileName;

                string messageData = ASCIIEncoding.Default.GetString(File.ReadAllBytes(path));
                Save(name, date, messageData);
            }

            Directory.Delete(logPath, true);
        }
    }

    internal class MessageInfo
    {
        public string Name;
        public DateTime Date;
        public long Offset;

        public MessageInfo(string name, DateTime date, long messageOffset)
        {
            this.Name = name;
            this.Date = date;
            this.Offset = messageOffset;
        }
    }
}
