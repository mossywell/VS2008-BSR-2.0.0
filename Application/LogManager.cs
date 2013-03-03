using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Diagnostics;

namespace Mossywell.BSR
{
    static class LogManager
    {
        #region Class Fields
        static private bool _pruningenabled;
        #endregion

        #region Static Constructor
        static LogManager()
        {
            _pruningenabled = false;
        }
        #endregion

        #region Utilities
        public static void PruneLogDirectory()
        {
            // If we're not yet enabled, exit now. We are only enabled after the form_load
            // method has had a change to log its own start. (More cosmetic than anything else)
            if (!_pruningenabled)
            {
                return;
            }

            // Get a list of the logfile name date parts (yyyymmdd)
            List<int> list = new List<int>();
            foreach (string file in Directory.GetFiles(LogManager.GetLogFileDirectory()))
            {
                // NOTE: Accessing Groups[X], where X is out of range doesn't cause an exception. :)
                string s = System.Text.RegularExpressions.Regex.Match(file, GlobalConstants.LOG_FILE_NAME_RE).Groups[1].ToString();
                if (s != String.Empty)
                {
                    // We found a log file, so save the date as an int (the RE forces numbers, so it's safe)
                    list.Add(int.Parse(s));
                }
            }

            // Now add today if it's not already in the list, then sort the list
            int today = int.Parse(DateTime.Now.ToString("yyyyMMdd"));
            if (!list.Contains(today))
            {
                list.Add(today);
            }
            list.Sort(); // Default sort, which is numerical ascending

            // Now we need to ensure that the number of log files is less than the maximum allowed
            int deletableitems = list.Count - Properties.Settings.Default.max_logfile_count;
            if (deletableitems > 0)
            {
                // Remove from the top (old log files)
                list.RemoveRange(0, deletableitems);
            }

            // Finally, go through the log files again and remove those not now in the list
            foreach (string file in Directory.GetFiles(LogManager.GetLogFileDirectory()))
            {
                // NOTE: Accessing Groups[X], where X is out of range doesn't cause an exception. :)
                string s = System.Text.RegularExpressions.Regex.Match(file, GlobalConstants.LOG_FILE_NAME_RE).Groups[1].ToString();
                if(s != String.Empty)
                {
                    // We've found a log file
                    if (!list.Contains(int.Parse(s)))
                    {
                        // It's not in the list so delete it
                        try
                        {
                            File.Delete(file);
                            LogManager.Log(GlobalConstants.STRING_WARNING, GlobalConstants.NOTICE_LOG_FILE_PRUNED + file);
                        }
                        catch(Exception ex)
                        {
                            // Log the problem but move on
                            LogManager.Log(GlobalConstants.STRING_ERROR, GlobalConstants.NOTICE_LOG_FILE_PRUNED_ERROR + file + " - " + ex.Message);
                        }
                    }
                }
            }
        }

        public static string GetLogFileDirectory()
        {
            // This is the directory path in my documents and excludes a trailing \
            return Environment.GetFolderPath(Environment.SpecialFolder.Personal) + @"\" + GlobalConstants.ASSEMBLY_COMPANY + @"\" + GlobalConstants.ASSEMBLY_TITLE;
        }

        public static string GetLogFileFullName()
        {
            // This is run eveny time we log something. It's probably not the most efficient way to do
            // things as it would be better to watch for a day change and change the log file then.
            // However, it'll do for now!
            return LogManager.GetLogFileDirectory() + @"\" + Assembly.GetExecutingAssembly().GetName().Name + "." + DateTime.Now.ToString("yyyyMMdd") + ".log";
        }

        public static string GetExceptionLogFileFullName()
        {
            // This is run eveny time we log something. It's probably not the most efficient way to do
            // things as it would be better to watch for a day change and change the log file then.
            // However, it'll do for now!
            return LogManager.GetLogFileDirectory() + @"\" + Assembly.GetExecutingAssembly().GetName().Name + ".Exception.log";
        }

        public static void Log(string prefix, string msg)
        {
            // Check the logging level
            if (prefix == GlobalConstants.STRING_ERROR && Properties.Settings.Default.logging_level < 1 ||
               prefix == GlobalConstants.STRING_WARNING && Properties.Settings.Default.logging_level < 2 ||
               prefix == GlobalConstants.STRING_INFO && Properties.Settings.Default.logging_level < 3 ||
               prefix == GlobalConstants.STRING_COMMAND && Properties.Settings.Default.logging_level < 4 ||
               prefix == GlobalConstants.STRING_STATS && Properties.Settings.Default.logging_level < 5 ||
               prefix == GlobalConstants.STRING_WEBPAGE && Properties.Settings.Default.logging_level < 6 ||
               prefix == GlobalConstants.STRING_DEBUG && Properties.Settings.Default.logging_level < 99 ||
               prefix == GlobalConstants.STRING_SUPER_DEBUG && Properties.Settings.Default.logging_level < 100 ||
               Properties.Settings.Default.logging_level > 100)
            {
                // Don't log anything
                return;
            }

            
            // Update the log file name
            string logfile = LogManager.GetLogFileFullName();
            if (GlobalVariables.LogFileName != logfile)
            {
                GlobalVariables.LogFileName = logfile;
                // Time to do some pruning!
                LogManager.PruneLogDirectory();
            }

            // Try to write to it
            try
            {
                StreamWriter sw = System.IO.File.AppendText(GlobalVariables.LogFileName);
                sw.WriteLine(DateTime.Now.ToString() + GlobalConstants.LOG_SEPARATOR_STRING + prefix + GlobalConstants.LOG_SEPARATOR_STRING + msg);
                sw.Flush();
                sw.Close();
                sw.Dispose();
                sw = null;
            }
            catch (Exception ex)
            {
                // We must exit with a dialog and, if possible write to the event log
                try
                {
                    EventLog.WriteEntry(Assembly.GetExecutingAssembly().GetName().Name, GlobalConstants.STRING_ERROR + GlobalConstants.LOG_SEPARATOR_STRING + GlobalConstants.ERROR_LOG_FILE, EventLogEntryType.Error);
                }
                catch
                {
                    // If we can't write to the event log, at least we tried!
                }
                GlobalMethods.Disaster(ex, GlobalConstants.ERROR_LOG_FILE);
            }
        }

        public static void LogException(Exception ex)
        {
            string msg = "";
            msg = "Exception Type: " + Environment.NewLine + ex.GetType().FullName + Environment.NewLine + Environment.NewLine +
                  "Exception Message: " + Environment.NewLine + ex.Message + Environment.NewLine + Environment.NewLine +
                  "Stack Trace: " + Environment.NewLine + ex.StackTrace + Environment.NewLine + Environment.NewLine;

            // Try to write to it
            try
            {
                StreamWriter sw = System.IO.File.AppendText(LogManager.GetExceptionLogFileFullName());
                sw.WriteLine(DateTime.Now.ToString() + Environment.NewLine + msg);
                sw.Flush();
                sw.Close();
                sw.Dispose();
                sw = null;
            }
            catch
            {
            }
        }
        #endregion

        #region Properties
        public static bool PruningEnabled
        {
            get
            {
                return _pruningenabled;
            }
            set
            {
                _pruningenabled = value;
            }
        }
        #endregion
    }
}
