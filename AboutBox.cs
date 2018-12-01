using System.Diagnostics;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

namespace UnderwaterVideo
{
    public partial class AboutBox : Form
    {
        #region Properties

        public string Title
        {
            get
            {
                return titleLbl.Text;
            }
            set
            {
                this.Text = string.Format("About {0}", value);
                titleLbl.Text = value;
            }
        }

        public string Version
        {
            get
            {
                return versionLbl.Text;
            }
            set
            {
                versionLbl.Text = value;
            }
        }

        public string Copyright
        {
            get
            {
                return copyrightLbl.Text;
            }
            set
            {
                copyrightLbl.Text = value;
            }
        }

        public string Description
        {
            get
            {
                return descriptionTxb.Text;
            }
            set
            {
                descriptionTxb.Text = value;
            }
        }

        public string Weblink
        {
            get
            {
                return weblinkLbl.Text;
            }
            set
            {
                weblinkLbl.Text = value;
            }
        }

        public Bitmap Logo
        {
            get
            {
                return (Bitmap)logoBox.Image;
            }
            set
            {
                logoBox.Image = value;
            }
        }

        #endregion

        #region Constrcutor

        public AboutBox()
        {
            InitializeComponent();
        }
        
        #endregion

        #region Methods

        public void ApplyAssembly(Assembly assembly)
        {
            Title = GetAssemblyTitle(assembly);
            Version = string.Format("v{0}", GetAssemblyVersion(assembly));
            Copyright = GetAssemblyCopyright(assembly);
            Description = GetAssemblyDescription(assembly);
        }

        public string GetAssemblyTitle(Assembly assembly)
        {
            object[] attributes = assembly.GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
            if (attributes.Length > 0)
            {
                AssemblyTitleAttribute titleAttribute = (AssemblyTitleAttribute)attributes[0];
                if (!string.IsNullOrEmpty(titleAttribute.Title))
                    return titleAttribute.Title;
            }

            return System.IO.Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().CodeBase);
        }

        public string GetAssemblyVersion(Assembly assembly)
        {
            return assembly.GetName().Version.ToString();
        }

        public string GetAssemblyDescription(Assembly assembly)
        {
            object[] attributes = assembly.GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);
            if (attributes.Length == 0)
                return "";
            else
                return ((AssemblyDescriptionAttribute)attributes[0]).Description;
        }

        public string GetAssemblyProduct(Assembly assembly)
        {
            object[] attributes = assembly.GetCustomAttributes(typeof(AssemblyProductAttribute), false);
            if (attributes.Length == 0)
                return "";
            else
                return ((AssemblyProductAttribute)attributes[0]).Product;
        }

        public string GetAssemblyCopyright(Assembly assembly)
        {
            object[] attributes = assembly.GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
            if (attributes.Length == 0)
                return "";
            else
                return ((AssemblyCopyrightAttribute)attributes[0]).Copyright;
        }

        public string GetAssemblyCompany(Assembly assembly)
        {
            object[] attributes = assembly.GetCustomAttributes(typeof(AssemblyCompanyAttribute), false);
            if (attributes.Length == 0)
                return "";
            return ((AssemblyCompanyAttribute)attributes[0]).Company;
        }

        #endregion

        #region Handlers

        private void weblinkLbl_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(weblinkLbl.Text);
        }

        #endregion
    }
}
