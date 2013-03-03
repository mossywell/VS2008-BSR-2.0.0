using System;
using System.Windows.Forms;
using System.Reflection; // AssemblyInfo
using System.Drawing;

namespace Mossywell
{
    namespace BSR
    {
        public partial class FormAbout : Form
        {
            #region Constructor
            public FormAbout()
            {
                InitializeComponent();
            }
            #endregion

            #region Events
            private void FormAbout_Load(object sender, EventArgs e)
            {
                object[] attrs0 = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCompanyAttribute), false);
                object[] attrs1 = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
                this.labelTitle.Text = ((AssemblyCompanyAttribute)attrs0[0]).Company + "'s " + GlobalConstants.ASSEMBLY_TITLE + " - " + ((AssemblyCopyrightAttribute)attrs1[0]).Copyright;
                this.labelVersionData.Text = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
                this.labelExeLocationData.Text = GlobalMethods.GetAssemblyDirectory();

                Bitmap bmp = (Bitmap)this.pictureBox.Image;
                bmp.MakeTransparent(Color.Red);

                this.buttonOK.Select();
                this.buttonOK.Focus(); // MSDN says we shouldn't use focus!
            }

            private void buttonOK_Click(object sender, EventArgs e)
            {
                this.Close();
            }
            #endregion
        }
    }
}