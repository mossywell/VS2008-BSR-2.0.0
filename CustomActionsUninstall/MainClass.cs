using System;
using System.Runtime.InteropServices; // DllImport

namespace Mossywell
{
    namespace BSR
    {
        /// <summary>
        /// Class to do the end of uninstall tidying up
        /// </summary>
        internal class CustomActionsUninstall
        {
            #region PInvoke - Checked for 64-bit Safety
            [DllImport("user32.dll")]
            public static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, string lpszClass, string lpszWindow);
            [DllImport("user32.dll")]
            public static extern IntPtr PostMessage(IntPtr hWnd, UInt32 Msg, IntPtr wParam, IntPtr lParam);
            #endregion

            #region Class Fields
            private const string MAIN_FORM_NAME = "BSRNotifyIconForm";
            private const int WM_CLOSE = 16;
            #endregion

            #region Constructor
            internal CustomActionsUninstall()
            {
            }
            #endregion

            #region Main
            static void Main()
            {
                // Tidy up code goes here

                // 1. Close the window if it is already running
                try
                {
                    IntPtr hWnd = FindWindowEx(IntPtr.Zero, IntPtr.Zero, null, MAIN_FORM_NAME);
                    if (hWnd.ToInt32() != 0)
                    {
                        IntPtr retval = PostMessage(hWnd, WM_CLOSE, IntPtr.Zero, IntPtr.Zero);
                    }
                }
                catch { }
            }
            #endregion
        }
    }
}
