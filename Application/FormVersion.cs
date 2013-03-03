using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Mossywell.BSR
{
    public partial class FormVersion : Form
    {
        #region Class Fields
        string _thisversion;
        string _latestversion;
        string _newurl;
        #endregion

        #region Constructor
        public FormVersion(string thisversion, string latestversion, string newurl)
        {
            InitializeComponent();

            _thisversion = thisversion;
            _latestversion = latestversion;
            _newurl = newurl;
        }
        #endregion

        #region Events
        private void FormVersion_Load(object sender, EventArgs e)
        {
            this.Text = GlobalConstants.ASSEMBLY_TITLE + GlobalConstants.FORMVERSION_TITLE_TEXT;
            this.textBoxThisVersion.Text = _thisversion;
            this.textBoxLatestVersion.Text = _latestversion;
            this.textBoxQuestion.Text = GlobalConstants.FORMVERSION_TEXTBOX_QUESTION_1 + GlobalConstants.ASSEMBLY_TITLE + GlobalConstants.FORMVERSION_TEXTBOX_QUESTION_2;

            Bitmap bmp = (Bitmap)this.pictureBox.Image;
            bmp.MakeTransparent(Color.Red);
            this.buttonNo.Select();
            this.buttonNo.Focus(); // MSDN says we shouldn't use focus!

            // Make it flash
            GlobalMethods.FlashWindowEx(this);

            System.Media.SystemSounds.Asterisk.Play();
        }

        private void buttonNo_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonYes_Click(object sender, EventArgs e)
        {
            WebBrowser browser = new WebBrowser();
            browser.Navigate(_newurl);
            this.Close();
        }
        #endregion
    }
}