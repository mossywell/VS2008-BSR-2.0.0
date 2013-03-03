using System;

namespace Mossywell
{
    namespace BSR
    {
        /// <summary>
        /// Class to do the end of install stuff
        /// </summary>
        internal class CustomActionsInstall
        {
            #region Constructor
            public CustomActionsInstall()
            {
            }
            #endregion

            #region Main
            static void Main()
            {
                try
                {
                    // The installer passes TARGETDIR to this exe so that it knows
                    // where the app was installed.
                    // Trim the trailing double quotes if they are they (which they usually are)
                    string targetdir = Environment.GetCommandLineArgs()[1].TrimEnd(new char[] { '\"' });
                    System.Diagnostics.Process.Start(targetdir + @"\README.TXT");
                }
                catch { }
            }
            #endregion
        }
    }
}

