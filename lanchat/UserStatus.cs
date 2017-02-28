using System;
using System.Collections.Generic;
using System.Xml;

namespace LANChat
{
    internal static class UserStatus
    {
        private static List<StatusInfo> statusList;

        public static List<StatusInfo> StatusList
        {
            get
            {
                if (statusList == null) {
                    statusList = new List<StatusInfo>();
                    System.Xml.XmlReader reader = System.Xml.XmlReader.Create(new System.IO.StringReader(
                                (string)LANChat.Resources.Properties.Resources.ResourceManager.GetObject("status_defs")));
                    int id = -1;
                    while (reader.Read()) {
                        switch (reader.NodeType) {
                            case System.Xml.XmlNodeType.Element:
                                if (reader.Name.Equals("Status")) {
                                    id++;
                                    //  Get status code.
                                    reader.MoveToNextAttribute();
                                    string code = reader.Value;
                                    //  Get status selectable attribute.
                                    reader.MoveToNextAttribute();
                                    bool selectable = bool.Parse(reader.Value);
                                    //  Get status description.
                                    reader.MoveToNextAttribute();
                                    string description = reader.Value;
                                    //  Get status presence image key.
                                    reader.MoveToNextAttribute();
                                    string statusImageKey = reader.Value;
                                    //  Get status chat image key.
                                    reader.MoveToNextAttribute();
                                    string chatImageKey = reader.Value;

                                    statusList.Add(new StatusInfo(id, code, selectable, description, 
                                        statusImageKey, chatImageKey));
                                }
                                break;
                        }
                    }
                    if (reader != null)
                        reader.Close();
                }
                return statusList;
            }
        }

        public static StatusInfo GetStatusInfo(string statusCode)
        {
            foreach (StatusInfo info in StatusList) {
                if (info.Code.Equals(statusCode))
                    return info;
            }
            return new StatusInfo(-1, string.Empty, false, string.Empty, string.Empty, string.Empty);
        }
    }

    internal struct StatusInfo
    {
        public int Id;
        public string Code;
        public bool Selectable;
        public string Description;
        public string StatusImageKey;
        public string ChatImageKey;

        public StatusInfo(int id, string code, bool selectable, string description, string statusImageKey, string chatImageKey)
        {
            Id = id;
            Code = code;
            Selectable = selectable;
            Description = description;
            StatusImageKey = statusImageKey;
            ChatImageKey = chatImageKey;
        }
    }
}
