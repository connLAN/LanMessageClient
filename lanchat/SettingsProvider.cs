using System.Collections;
using System.Collections.Specialized;
using System.Configuration;
using System.Xml;
using System.IO;

namespace LANChat.Properties
{
    /// <summary>
    /// A custom settings provider class for reading and writing configuration file.
    /// </summary>
    internal class LocalSettingsProvider : SettingsProvider
    {
        const string CONFIGROOT = "configuration";
        const string HEADERROOT = "configHeader";
        const string HEADERPATH = CONFIGROOT + "/" + HEADERROOT;
        const string SECTIONROOT = "userSettings";
        const string SECTIONPATH = CONFIGROOT + "/" + SECTIONROOT;

        public override void Initialize(string name, NameValueCollection config)
        {
            base.Initialize(this.ApplicationName, config);
        }

        public override string ApplicationName
        {
            get { return AppInfo.Title; }
            set { }
        }

        public override SettingsPropertyValueCollection GetPropertyValues(SettingsContext context, SettingsPropertyCollection collection)
        {
            SettingsPropertyValueCollection values = new SettingsPropertyValueCollection();

            foreach (SettingsProperty property in collection) {
                SettingsPropertyValue value = new SettingsPropertyValue(property);
                value.IsDirty = false;
                value.SerializedValue = GetValue(property);
                values.Add(value);
            }

            return values;
        }

        public override void SetPropertyValues(SettingsContext context, SettingsPropertyValueCollection collection)
        {
            foreach (SettingsPropertyValue value in collection) {
                SetValue(value);
            }

            try {
                if (!Directory.Exists(AppInfo.DataPath))
                    Directory.CreateDirectory(AppInfo.DataPath);

                SettingsXml.Save(Path.Combine(AppInfo.DataPath, "user.config"));
            }
            catch (System.Exception ex) {
                throw ex;
            }
        }

        private XmlDocument settingsXml = null;

        private XmlDocument SettingsXml
        {
            get
            {
                if (settingsXml == null) {
                    settingsXml = new XmlDocument();
                    try {
                        settingsXml.Load(Path.Combine(AppInfo.DataPath, "user.config"));
                    }
                    catch {
                        XmlDeclaration xmlDecl = settingsXml.CreateXmlDeclaration("1.0", "utf-8", string.Empty);
                        settingsXml.AppendChild(xmlDecl);

                        XmlNode rootNode = settingsXml.CreateNode(XmlNodeType.Element, CONFIGROOT, string.Empty);
                        settingsXml.AppendChild(rootNode);
                        XmlNode headerNode = settingsXml.CreateNode(XmlNodeType.Element, HEADERROOT, string.Empty);
                        rootNode.AppendChild(headerNode);
                        XmlNode sectionNode = settingsXml.CreateNode(XmlNodeType.Element, SECTIONROOT, string.Empty);
                        rootNode.AppendChild(sectionNode);
                    }
                }
                return settingsXml;
            }
        }

        private string GetValue(SettingsProperty property)
        {
            string value = string.Empty;
            string propertyPath = IsAppScoped(property) ? HEADERPATH : SECTIONPATH;

            try {
                value = SettingsXml.SelectSingleNode(propertyPath + "/" + property.Name).InnerText;
            }
            catch {
                if (property.DefaultValue != null)
                    value = property.DefaultValue.ToString();
                else
                    value = string.Empty;
            }

            return value;
        }

        private void SetValue(SettingsPropertyValue value)
        {
            XmlElement propertyNode;
            string propertyPath = IsAppScoped(value.Property) ? HEADERPATH : SECTIONPATH;

            try {
                propertyNode = (XmlElement)SettingsXml.SelectSingleNode(propertyPath + "/" + value.Name);
            }
            catch {
                propertyNode = null;
            }

            if (propertyNode != null) {
                propertyNode.InnerText = value.SerializedValue.ToString();
            }
            else {
                propertyNode = SettingsXml.CreateElement(value.Name);
                propertyNode.InnerText = value.SerializedValue.ToString();
                SettingsXml.SelectSingleNode(propertyPath).AppendChild(propertyNode);
            }
        }

        private bool IsAppScoped(SettingsProperty property)
        {
            foreach (DictionaryEntry entry in property.Attributes) {
                System.Attribute attrib = (System.Attribute)entry.Value;
                if (attrib is System.Configuration.ApplicationScopedSettingAttribute)
                    return true;
            }
            return false;
        }
    }
}
