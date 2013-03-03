using System;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Drawing;

namespace Mossywell
{
    namespace BSR
    {
        public partial class FormLogReader : Form
        {
            #region Class Fields
            bool _firstload;
            string _filename;
            long _oldlength;
            UTF8Encoding _utf8;
            FileStream _fs;    // Only used to open the file in a non-read only manner.
            StreamReader _sr;  // Used for the file io ops.
            #endregion

            #region Constructor
            public FormLogReader(string filename)
            {
                _filename = filename;
                InitializeComponent();
            }
            #endregion

            #region Events
            private void Form1_Load(object sender, EventArgs e)
            {
                // Set the form size
                Rectangle screen = Screen.GetWorkingArea(this);
                this.Width = (screen.Width > 900 ? 900 : screen.Width);
                this.Left = (screen.Width - this.Width) / 2;
                this.Height = (screen.Height > 600 ? 600 : screen.Height);
                this.Top = (screen.Height - this.Height) / 2;

                // Instanatiate objects
                _firstload = true;
                _oldlength = 0;
                _utf8 = new UTF8Encoding(true);
                _fs = new FileStream(_filename, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                _sr = new StreamReader(_fs, Encoding.UTF8);

                // Set the buttons - play is disabled as we start the timer
                buttonPlay.Enabled = false;
                buttonStop.Enabled = true;

                // Start the timer
                timer.Start();

                // Flash the foem if necessary
                GlobalMethods.FlashWindowEx(this);
            }

            private void timer_Tick(object sender, EventArgs e)
            {
                // Now check the length
                long l = _fs.Length;
                if (l != _oldlength)
                {
                    // File has grown. So, read to the end of the file. We don't
                    // care if the file has grown AGAIN since since measuring
                    // _fs.Length because the streamreader will move the pointer
                    // on to the end of the file anyway. To put it another way,
                    // we are using _oldlength to determine if the file has changed,
                    // but fundamentally _sr.ReadToEnd() will always read from the
                    // last read point to the end anyway, so no seeks need be done.
                    string s = _sr.ReadToEnd();
                    if (_firstload)
                    {
                        richTextBox.Text = "";
                        _firstload = false;
                    }
                    richTextBox.AppendText(s);

                }
                _oldlength = l;
            }

            private void Form1_FormClosing(object sender, FormClosingEventArgs e)
            {
                // Stop the timer
                timer.Stop();
                timer.Dispose();

                // Get rid of the streams
                _sr.Close();   // Closes the filestream as well
                _sr.Dispose(); // Disposes the filestream as well
                _sr = null;
                _fs = null;
            }

            private void buttonPlay_Click(object sender, EventArgs e)
            {
                buttonPlay.Enabled = false;
                buttonStop.Enabled = true;

                timer.Start();

                // Run the timer now to update things
                timer_Tick(null, null);
            }

            private void buttonStop_Click(object sender, EventArgs e)
            {
                buttonPlay.Enabled = true;
                buttonStop.Enabled = false;

                timer.Stop();
            }

            private void toolStripMenuItemCopy_Click(object sender, EventArgs e)
            {
                string text = richTextBox.SelectedText;

                if (text.Length > 0)
                {
                    // Get the end of lines into Windows format for notepad to work!
                    text = text.Replace("\n", "\r\n");

                    // Send to the clipboard
                    Clipboard.SetDataObject(text, true);
                }
            }
            #endregion
        }
    }
}