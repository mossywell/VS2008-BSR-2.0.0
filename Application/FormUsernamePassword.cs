using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Mossywell.BSR
{
    public partial class FormUsernamePassword : Form
    {
        #region Constructor
        public FormUsernamePassword()
        {
            InitializeComponent();
        }
        #endregion

        #region Events
        private void FormUsernamePassword_Load(object sender, EventArgs e)
        {
            Bitmap bmp = (Bitmap)this.pictureBox.Image;
            bmp.MakeTransparent(Color.Red);
            this.buttonClose.Select();
            this.buttonClose.Focus(); // MSDN says we shouldn't use focus!

            System.Media.SystemSounds.Asterisk.Play();
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonCopyToClipboard_Click(object sender, EventArgs e)
        {
            Clipboard.SetDataObject(textBoxUsername.Text + Environment.NewLine + textBoxPassword.Text, true);
        }
        #endregion
    }
}