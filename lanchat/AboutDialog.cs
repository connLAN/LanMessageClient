using System;
using System.Drawing;
using System.Windows.Forms;

namespace LANChat
{
    partial class AboutDialog : Form
    {
        public AboutDialog()
        {
            InitializeComponent();
            InitUI();
            this.Text = String.Format("About {0}", AppInfo.Title);
            this.labelProductName.Text = AppInfo.ProductName;
            this.labelVersion.Text = String.Format("Version {0}", AppInfo.Version);
            this.labelCopyright.Text = AppInfo.Copyright;
            this.labelCompanyName.Text = AppInfo.Company;
            this.textBoxDescription.Text = AppInfo.Description;
        }

        private void InitUI()
        {
            picBoxLogo.Image = (Image)LANChat.Resources.Properties.Resources.ResourceManager.GetObject("picBoxLogo");
        }
    }
}
