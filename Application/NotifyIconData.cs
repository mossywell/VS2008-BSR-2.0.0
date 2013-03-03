using System;
using System.Collections.Generic;

namespace Mossywell.BSR
{
    public class NotifyIconData
    {
        #region Class Fields
        // Web Page Getter Stuff
        public AbbreviatedStatus[] AbbreviatedStatusArray;
        public long[] ElapsedMillisecondsArray;

        // Router Stats Stuff
        public string Uptime;
        public string Speed;
        public string SNR;
        #endregion

        #region Constructor
        public NotifyIconData(int websitecount)
        {
            // Set array sizes
            AbbreviatedStatusArray = new AbbreviatedStatus[websitecount];
            ElapsedMillisecondsArray = new long[websitecount];
        }
        #endregion

        #region Properties
        public string GetCompleteTooltipText
        {
            get
            {
                string s = DateTime.Now.ToLongTimeString() + " (";
                s += Uptime + ")" + Environment.NewLine;
                for (int i = 0; i < AbbreviatedStatusArray.Length; i++)
                {
                    s += (i + 1).ToString() + " ";
                    if (Properties.Settings.Default.url_check_disabled)
                    {
                        s += GlobalConstants.STRING_DISABLED;
                    }
                    else if (AbbreviatedStatusArray[i] == AbbreviatedStatus.GETTING)
                    {
                        s += GlobalConstants.STRING_GETTING;
                    }
                    else
                    {
                        s += AbbreviatedStatusArray[i].ToString() + " " + ElapsedMillisecondsArray[i].ToString();
                    }
                    s += Environment.NewLine;
                }
                s += Speed + GlobalConstants.STRING_KBPS + " " + SNR + GlobalConstants.STRING_DB;

                // Cut it to size if necessary
                if (s.Length > 63)
                {
                    s = s.Substring(0, 63);
                }
                return s;
            }
        }
        #endregion
    }
}
