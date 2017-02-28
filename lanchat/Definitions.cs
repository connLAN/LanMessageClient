using System;
using System.Collections.Generic;
using System.Reflection;
using System.Drawing;
using System.IO;

namespace LANChat
{
    /// <summary>
    /// The type of named event that can be set.
    /// </summary>
    public enum GlobalEvents
    {
        Instance = 0,
        Terminate
    }

    /// <summary>
    /// Specifies the different types of datagrams that can be sent or received.
    /// </summary>
    public enum DatagramTypes
    {
        Blank = 0,
        Announce,
        Depart,
        Broadcast,
        Message,
        File,
        Query,
        Status,
        Notify,
        UserQuery,
        UserInfo,
        UserAction,
        Max
    }

    /// <summary>
    /// Specifies the different types of messages that can be sent or received.
    /// </summary>
    public enum MessageTypes
    {
        None = 0,
        Message,
        Offline,
        Online,
        Failed,
        Error,
        Status,
        OldVersion,
        File,
        UserInfo,
        UserAction,
        Broadcast,
        Max
    }

    /// <summary>
    /// Specifies the different components of a message header.
    /// </summary>
    internal enum MessageHeader
    {
        TimeStamp = 0,
        Version,
        Type,
        Key,
        Address,
        UserName,
        Max
    }

    /// <summary>
    /// Specifies the different components of data portion of a message.
    /// </summary>
    internal enum MessageData
    {
        Text = 0,
        Max
    }

    /// <summary>
    /// Specifies the different actions the user performs.
    /// </summary>
    internal enum UserActions
    {
        [StringValue("Typing")]
        Typing = 0,
        [StringValue("Received")]
        Received
    }

    internal enum UserInfoFlags : uint
    {
        Key = 0x0000,
        Version = 0x0001,
        Address = 0x0002,
        Name = 0x0004,
        Status = 0x0008,
        Group = 0x0010
    }

    /// <summary>
    /// Specifies the different types of notifications given to the user.
    /// </summary>
    internal enum NotifyEvents
    {
        Online = 0,
        Offline,
        NewMessage,
        NewFile,
        Complete,
        FileComplete
    }

    /// <summary>
    /// Specifies the different types of formatting applied to messages when displayed.
    /// </summary>
    internal enum FormatTypes
    {
        Default = 0,
        UserName,
        Status,
        Message,
        Max
    }

    /// <summary>
    /// Contains definitions of the different formattings that are applied to rtf text.
    /// </summary>
    internal static class Definitions
    {
        private static List<KeyValuePair<FormatTypes, Font>> presetFonts;
        private static List<KeyValuePair<FormatTypes, Color>> presetColors;

        static Definitions()
        {
            AddPresetFonts();
            AddPresetColors();
        }

        private static void AddPresetFonts()
        {
            presetFonts = new List<KeyValuePair<FormatTypes, Font>>();
            presetFonts.Add(new KeyValuePair<FormatTypes,Font>(FormatTypes.UserName, Helper.GetValidFont("Meiryo UI", 9.75f)));
            presetFonts.Add(new KeyValuePair<FormatTypes, Font>(FormatTypes.Status, Helper.GetValidFont("Segoe UI", 9.0f)));
            presetFonts.Add(new KeyValuePair<FormatTypes, Font>(FormatTypes.Message, Helper.GetValidFont("Calibri", 10.5f)));
        }

        private static void AddPresetColors()
        {
            presetColors = new List<KeyValuePair<FormatTypes, Color>>();
            presetColors.Add(new KeyValuePair<FormatTypes, Color>(FormatTypes.UserName, Color.FromArgb(43, 96, 222)));
            presetColors.Add(new KeyValuePair<FormatTypes, Color>(FormatTypes.Status, Color.FromArgb(128, 128, 128)));
            presetColors.Add(new KeyValuePair<FormatTypes, Color>(FormatTypes.Message, Color.FromArgb(0, 0, 0)));
        }

        public static List<KeyValuePair<FormatTypes, Font>> PresetFonts
        {
            get { return presetFonts; }
        }

        public static List<KeyValuePair<FormatTypes, Color>> PresetColors
        {
            get { return presetColors; }
        }

        public static Font GetPresetFont(FormatTypes formatType)
        {
            foreach (KeyValuePair<FormatTypes, Font> font in presetFonts) {
                if (font.Key == formatType)
                    return font.Value;
            }
            return null;
        }

        public static Color GetPresetColor(FormatTypes formatType)
        {
            foreach (KeyValuePair<FormatTypes, Color> color in presetColors) {
                if (color.Key == formatType)
                    return color.Value;
            }
            return Color.Transparent;
        }
    }

    internal static class Helper
    {
        public static Font SystemFont { get { return SystemFonts.MenuFont; } }

        public static string GetStringValue(this Enum value)
        {
            //  Get the type.
            Type type = value.GetType();

            //  Get field info for this type.
            FieldInfo fieldInfo = type.GetField(value.ToString());

            //  Get the string value attributes.
            StringValueAttribute[] attribs = fieldInfo.GetCustomAttributes(
                typeof(StringValueAttribute), false) as StringValueAttribute[];

            //  Return the first if there was a match.
            return attribs.Length > 0 ? attribs[0].StringValue : null;
        }

        public static System.Drawing.Color ModulateColor(System.Drawing.Color color, float mod)
        {
            float h = color.GetHue() / 360.0f;
            float s = color.GetSaturation();
            float l = color.GetBrightness();

            l += mod;
            l = Math.Min(l, 1.0f);

            double r = 0, g = 0, b = 0;
            double temp1, temp2;

            temp2 = ((l <= 0.5) ? l * (1.0 + s) : l + s - (l * s));
            temp1 = 2.0 * l - temp2;

            double[] t3 = new double[] { h + 1.0 / 3.0, h, h - 1.0 / 3.0 };
            double[] clr = new double[] { 0, 0, 0 };
            for (int i = 0; i < 3; i++) {
                if (t3[i] < 0)
                    t3[i] += 1.0;
                if (t3[i] > 1)
                    t3[i] -= 1.0;

                if (6.0 * t3[i] < 1.0)
                    clr[i] = temp1 + (temp2 - temp1) * t3[i] * 6.0;
                else if (2.0 * t3[i] < 1.0)
                    clr[i] = temp2;
                else if (3.0 * t3[i] < 2.0)
                    clr[i] = (temp1 + (temp2 - temp1) * ((2.0 / 3.0) - t3[i]) * 6.0);
                else
                    clr[i] = temp1;
            }

            clr[0] = Math.Max(Math.Min(clr[0], 1.0), 0.0);
            clr[1] = Math.Max(Math.Min(clr[1], 1.0), 0.0);
            clr[2] = Math.Max(Math.Min(clr[2], 1.0), 0.0);

            r = clr[0] * 255.0;
            g = clr[1] * 255.0;
            b = clr[2] * 255.0;

            return System.Drawing.Color.FromArgb(color.A, (int)r, (int)g, (int)b);
        }

        public static Icon GetAssociatedIcon(string filePath)
        {
            Icon icon = null;

            try {
                if (File.Exists(filePath)) {
                    icon = Icon.ExtractAssociatedIcon(filePath);
                }
                else {
                    string tempPath = AppInfo.TempPath;
                    if (!Directory.Exists(tempPath))
                        Directory.CreateDirectory(tempPath);
                    string tempFileName = Path.GetFileName(filePath);
                    string tempFilePath = Path.Combine(tempPath, tempFileName);
                    FileStream fStream = File.Create(tempFilePath);
                    fStream.Close();
                    icon = Icon.ExtractAssociatedIcon(tempFilePath);
                    File.Delete(tempFilePath);
                }
            }
            catch {
            }

            return icon;
        }

        /// <summary>
        /// Create a formatted string that describes the file size in a user friendly manner.
        /// </summary>
        /// <param name="fileSize">File size in bytes.</param>
        /// <returns>The formatted string.</returns>
        public static string FormatSize(long fileSize)
        {
            long gigaByte = 1073741824;
            long megaByte = 1048576;
            long kiloByte = 1024;
            float fSize;
            string size;
            if (fileSize > gigaByte) {
                fSize = (float)fileSize / (float)gigaByte;
                size = " GB";
            }
            else if (fileSize > megaByte) {
                fSize = (float)fileSize / (float)megaByte;
                size = " MB";
            }
            else if (fileSize > kiloByte) {
                fSize = (float)fileSize / (float)kiloByte;
                size = " KB";
            }
            else {
                fSize = (float)fileSize;
                size = " bytes";
            }
            size = size.Insert(0, fSize.ToString("0.##"));
            return size;
        }

        /// <summary>
        /// Create a formatted string that describes the timespan in a user friendly manner.
        /// </summary>
        /// <param name="timeSpan">Timespan in seconds.</param>
        /// <returns>The formatted string.</returns>
        public static string FormatTime(long timeSpan)
        {
            //  Convert seconds to ticks and initialize a TimeSpan object.
            TimeSpan span = new TimeSpan(timeSpan * 10000000);
            string time = string.Empty;

            if (span.Days > 0)
                time += span.Days.ToString() + " days ";
            if (span.Hours > 0)
                time += span.Hours.ToString() + " hrs ";
            if (span.Minutes > 0)
                time += span.Minutes.ToString() + " mins ";
            if (span.Seconds > 0)
                time += span.Seconds.ToString() + " secs ";

            if (time.Equals(string.Empty))
                time = "0 secs ";

            return time.Trim();
        }

        public static Font GetValidFont(string familyName, float emSize)
        {
            Font font = new Font(familyName, emSize);
            if (!font.Name.Equals(familyName)) {
                font.Dispose();
                font = new Font(Helper.SystemFont.Name, emSize);
            }
            return font;
        }
    }

    internal class Win32Window : System.Windows.Forms.IWin32Window
    {
        private IntPtr hWnd;

        public Win32Window(IntPtr handle)
        {
            hWnd = handle;
        }

        public IntPtr Handle { get { return hWnd; } }
    }

    internal class StringValueAttribute : System.Attribute
    {
        public string StringValue { get; protected set; }

        public StringValueAttribute(string value)
        {
            StringValue = value;
        }
    }

    /// <summary>
    /// Struct for holding user information. This is different from the user class.
    /// </summary>
    internal struct UserInfo
    {
        public string LogonName;
        public string MachineName;
        public string IPAddress;
        public string OSName;
        public string MessengerVersion;
        public string Message;

        public UserInfo(string logonName, string machineName, string ipAddress, string osName, 
            string messengerVersion, string message)
        {
            this.LogonName = logonName;
            this.MachineName = machineName;
            this.IPAddress = ipAddress;
            this.OSName = osName;
            this.MessengerVersion = messengerVersion;
            this.Message = message;
        }
    }

    /// <summary>
    /// Class that contains event data that is needed by the notify event.
    /// </summary>
    public class NotifyEventArgs : EventArgs
    {
        private string title;
        private string text;

        public NotifyEventArgs(string title, string text)
        {
            this.title = title;
            this.text = text;
        }

        //  Read only properties.
        public string Title { get { return title; } }
        public string Text { get { return text; } }
    }
}
