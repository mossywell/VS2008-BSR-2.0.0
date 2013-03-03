using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Mossywell.BSR
{
    static class MainEntryPoint
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {
                System.Threading.Mutex m = new System.Threading.Mutex(true, @"Global\" + System.Reflection.Assembly.GetExecutingAssembly().FullName);
                if (m.WaitOne(10, false))
                {
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    Application.Run(new NotifyIconForm());
                    m.ReleaseMutex();
                }
                else
                {
                    System.Windows.Forms.MessageBox.Show("You can only run me once on any computer and I'm already running on this one!\nLook down in the System Tray - you should see me there.\nIf not, do a Ctrl-Alt-Del and look for Mossywell.BSR.exe.", GlobalConstants.ASSEMBLY_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            catch (UnauthorizedAccessException)
            {
                System.Windows.Forms.MessageBox.Show("You can only run me once on any computer and I'm already running on this one under a different user login!\nThis error can occur on multi-user systems, including Fast User Switching on Windows XP.", GlobalConstants.ASSEMBLY_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex);
                System.Windows.Forms.MessageBox.Show("A critical error has occurred in the application and it is being closed by the operating system.\n" + ex.TargetSite.Name.ToString() + ": " + ex.Message, GlobalConstants.ASSEMBLY_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
    }
}