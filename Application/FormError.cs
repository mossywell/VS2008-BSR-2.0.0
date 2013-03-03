using System;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace Mossywell.BSR
{
    public partial class FormError : Form
    {
        #region Class fields
        Exception _ex;
        string _msg;
        #endregion

        #region Constructor
        public FormError(Exception ex, string msg)
        {
            InitializeComponent();
            _ex = ex;
            _msg = msg;
        }
        #endregion

        #region Events
        private void buttonOK_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FormError_Load(object sender, EventArgs e)
        {
            this.Text = GlobalConstants.ASSEMBLY_TITLE + " " + GlobalConstants.STRING_ERROR;
            this.textBoxLabel.Text = GlobalConstants.STRING_ERROR + GlobalConstants.LOG_SEPARATOR_STRING + _msg;
            this.textBoxError.Text = _ex.Message;
            this.buttonOK.Select();
            this.buttonOK.Focus(); // MSDN says we shouldn't use this!

            GlobalMethods.FlashWindowEx(this);
        }
        #endregion 
    }
}