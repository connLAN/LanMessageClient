using System;
using System.Xml;
using System.IO;
using System.Drawing;

namespace LANChat
{
    internal static class Theme
    {
        public static string GetThemeName(string themeFile)
        {
            string themeName = string.Empty;
            XmlReader reader = null;

            try {
                reader = XmlReader.Create(Path.Combine(AppInfo.ThemePath, themeFile));
                reader.ReadToFollowing("Theme");
                reader.MoveToAttribute("name");
                themeName = reader.Value;
            }
            catch {
            }
            finally {
                if (reader != null)
                    reader.Close();
            }

            return themeName;
        }

        public static Color GetThemeColor(string themeFile, string category, string subCategory, string property)
        {
            if (themeFile.Equals(string.Empty))
                return GetDefaultThemeColor(category, subCategory, property);

            Color color = Color.Transparent;
            XmlReader reader = null;

            try {
                reader = XmlReader.Create(Path.Combine(AppInfo.ThemePath, themeFile));
                reader.ReadToFollowing(category);
                if (subCategory != null)
                    reader.ReadToFollowing(subCategory);
                reader.ReadToFollowing(property);
                string value = reader.ReadElementContentAsString();
                string[] colorComps = value.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                int r = int.Parse(colorComps[0]);
                int g = int.Parse(colorComps[1]);
                int b = int.Parse(colorComps[2]);
                color = Color.FromArgb(r, g, b);
            }
            catch {
                color = GetDefaultThemeColor(category, subCategory, property);
            }
            finally {
                if (reader != null)
                    reader.Close();
            }

            return color;
        }

        public static dynamic GetThemeValue(string themeFile, string category, string subCategory, string property, Type type)
        {
            if (themeFile.Equals(string.Empty))
                return GetDefaultThemeValue(category, subCategory, property, type);

            XmlReader reader = null;

            try {
                reader = XmlReader.Create(Path.Combine(AppInfo.ThemePath, themeFile));
                reader.ReadToFollowing(category);
                if (subCategory != null)
                    reader.ReadToFollowing(subCategory);
                reader.ReadToFollowing(property);
                string value = reader.ReadElementContentAsString();
                return Convert.ChangeType(value, type);
            }
            catch {
                return GetDefaultThemeValue(category, subCategory, property, type);
            }
            finally {
                if (reader != null)
                    reader.Close();
            }
        }

        public static string GetDefaultThemeName()
        {
            XmlReader reader = XmlReader.Create(new StringReader(
                        (string)LANChat.Resources.Properties.Resources.ResourceManager.GetObject("theme_default")));
            reader.ReadToFollowing("Theme");
            reader.MoveToAttribute("name");
            return reader.Value;
        }

        public static Color GetDefaultThemeColor(string category, string subCategory, string property)
        {
            XmlReader reader = XmlReader.Create(new StringReader(
                        (string)LANChat.Resources.Properties.Resources.ResourceManager.GetObject("theme_default")));
            reader.ReadToFollowing(category);
            if (subCategory != null)
                reader.ReadToFollowing(subCategory);
            reader.ReadToFollowing(property);
            string value = reader.ReadElementContentAsString();
            string[] colorComps = value.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            int r = int.Parse(colorComps[0]);
            int g = int.Parse(colorComps[1]);
            int b = int.Parse(colorComps[2]);
            return Color.FromArgb(r, g, b);
        }

        public static dynamic GetDefaultThemeValue(string category, string subCategory, string property, Type type)
        {
            XmlReader reader = XmlReader.Create(new StringReader(
                        (string)LANChat.Resources.Properties.Resources.ResourceManager.GetObject("theme_default")));
            reader.ReadToFollowing(category);
            if (subCategory != null)
                reader.ReadToFollowing(subCategory);
            reader.ReadToFollowing(property);
            string value = reader.ReadElementContentAsString();
            return Convert.ChangeType(value, type);
        }
    }
}
