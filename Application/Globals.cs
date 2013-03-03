using System;
using System.Reflection;
using System.Diagnostics;
using System.Windows.Forms;
using System.Management;
using System.Runtime.InteropServices;
using System.IO;
using System.Net.NetworkInformation;
using System.Collections;
using System.Net;

namespace Mossywell.BSR
{
    public static class GlobalConstants
    {
        // Class fields
        public static readonly string ASSEMBLY_TITLE; // e.g. "BSR"
        public static readonly string ASSEMBLY_COMPANY; // e.g. "Mossywell"

        #region Static Constructor
        static GlobalConstants()
        {
            object[] attrs;
            attrs = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
            ASSEMBLY_TITLE = ((AssemblyTitleAttribute)attrs[0]).Title;
            attrs = null;
            attrs = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCompanyAttribute), false);
            ASSEMBLY_COMPANY = ((AssemblyCompanyAttribute)attrs[0]).Company;
            attrs = null;
        }
        #endregion

        // General router constants
        public static readonly string DEFAULT_GATEWAY = "192.168.0.1";
        public static readonly int TIMER_DELAY_AFTER_RESUME = 60000; // ms - must be > TIMER_WEBPAGE_INTERVAL
        public static readonly int TIMER_STATE_CHECK_INTERVAL = 500; // ms
        public static readonly int TIMER_DNS_RESOLVER_CACHE_FLUSH_INTERVAL = 30000; // ms
        public static readonly int TIMER_VERSION_INTERVAL = 21600000; // ms (6 hours)
        public static readonly int TIMER_DELAY_FUDGE_FACTOR = 5000; // ms (allow enough time for the thread to abort)
        public static readonly long ALLOWED_UPTIME_DIFFERENCE = 5000; // ms (See RouterCommandCalculateSuccessCallback)
        public static readonly string VERSION_URL = @"http://www.mossywell.com/downloads/BSRInfo.txt";
        public static readonly string STATS_PAGE = @"stattbl.htm";
        public static readonly string DETAILED_STATS_PAGE = @"detailedstats.txt";
        public static readonly string REBOOT_SOUND = @"Bounce.wav";
        public static readonly string HELP_FILE = @"BSR.chm";
        public static readonly string ROUTER_COMMAND_PREFIX = @"/setup.cgi?todo=ping_test&next_file=start.htm&c4_IPAddr=127.0.0.1;";
        public static readonly string ROUTER_COMMAND_PREFIX_NEW = @"/setup.cgi?PATH=/bin:/sbin:/usr/bin:/usr/sbin;";
        public static readonly string ROUTER_COMMAND_SUFFIX_NEW = @";rm+$0&todo=ping_test&next_file=diagping.htm&c4_IPAddr=127.0.0.1%3E/dev/null;(IFS=%2b;/bin/echo%3E/tmp/mel+${QUERY_STRING%25%25%26to*};/bin/sh+/tmp/mel)+%3E%261+2%3E%261;";
        public static readonly string ROUTER_COMMAND_GET_OR_DISPLAY_CURRENT_STATS = @"/" + STATS_PAGE;
        public static readonly string ROUTER_COMMAND_DISPLAY_DETAILED_STATS = ROUTER_COMMAND_PREFIX + @"/usr/sbin/adslctl+info+--stats+%3E+/www/netgear.cfg";
        public static readonly string ROUTER_COMMAND_REBOOT = ROUTER_COMMAND_PREFIX + @"/bin/sh+-c+'/sbin/reboot'";
        public static readonly string ROUTER_COMMAND_START_UTELNETD = ROUTER_COMMAND_PREFIX + @"/bin/sh+-c+'PATH=/bin:/sbin:/usr/bin:/usr/sbin;/usr/sbin/utelnetd+-l+/bin/sh'";
        public static readonly string ROUTER_COMMAND_KILL_UTELNETD = ROUTER_COMMAND_PREFIX + @"/usr/bin/killall+utelnetd";
        public static readonly string ROUTER_COMMAND_NETGEAR_CFG = @"/netgear.cfg"; 
        public static readonly string ROUTER_COMMAND_USERNAME_PASSWORD = ROUTER_COMMAND_PREFIX + @"/bin/grep+ppoa_+/tmp/nvram+%3E+/www/netgear.cfg";
        public static readonly string ROUTER_COMMAND_PPP_STATS = ROUTER_COMMAND_PREFIX + @"/sbin/ifconfig+%3E+/www/netgear.cfg";
        public static readonly string ROUTER_COMMAND_ALL_STATS = ROUTER_COMMAND_PREFIX_NEW + @"ifconfig+ppp0;cat+/proc/uptime;adslctl+info+--stats" + ROUTER_COMMAND_SUFFIX_NEW;
        // public static readonly string ROUTER_COMMAND_ALL_STATS = ROUTER_COMMAND_PREFIX + @"/bin/cat+/proc/uptime+%3E+/tmp/netgear.cfg;/usr/sbin/adslctl+info+--stats+|+/bin/grep+Rate+%3E%3E+/tmp/netgear.cfg;/usr/sbin/adslctl+info+--stats+|+/bin/grep+SNR+%3E%3E+/tmp/netgear.cfg;/sbin/ifconfig+%3E%3E/tmp/netgear.cfg";
        // Logging constants
        public static readonly string LOG_FILE_NAME_RE = @"^.*\\" + Assembly.GetExecutingAssembly().GetName().Name + @"\.(\d{8})\.log$"; // Regular expression to find log files
        public static readonly string STRING_GETTING = "Getting.."; // Leave with only 2 dots
        public static readonly string STRING_DISABLED = "Disabled";
        public static readonly string STRING_PAUSED = "Paused";
        public static readonly string LOG_SEPARATOR_STRING = ", ";
        public static readonly string LOG_SEPARATOR_STATS_STRING = "; ";
        public static readonly string SEPARATOR_STRING_STATS = " ";
        public static readonly string STRING_HTTP = "http://";
        public static readonly string STRING_STATS = "STATS";
        public static readonly string STRING_WEBPAGE = "WEBPAGE";
        public static readonly string STRING_COMMAND = "COMMAND";
        public static readonly string STRING_DEBUG = "DEBUG";
        public static readonly string STRING_SUPER_DEBUG = "SUPER_DEBUG";
        public static readonly string STRING_INFO = "INFO";
        public static readonly string STRING_WARNING = "WARNING";
        public static readonly string STRING_ERROR = "ERROR";
        public static readonly string STRING_KBPS = " kbps";
        public static readonly string STRING_DB = " db";
        public static readonly string STRING_BYTES = " bytes";
        public static readonly string STRING_QUESTION_MARK = "?";
        public static readonly string STRING_IS_ADMIN_ONLINE = @"Another Administrator online\.";
        public static readonly string QUESTION_REBOOT = "Are you sure that you want to reboot the router?";
        public static readonly string QUESTION_REBOOT_MAINTENANCE_IS_ON = "Maintenance Mode is on. Are you really sure that you want to reboot the router?";
        public static readonly string QUESTION_RUN_CUSTOM_COMMANDS = "Are you sure that you want to run the custom commands?";
        public static readonly string QUESTION_RUN_CUSTOM_COMMANDS_MAINTENANCE = "Maintenance Mode is on. Are you sure that you want to run the custom commands?";
        public static readonly string QUESTION_MAINTENANCE_IN_REBOOT = "The router appears to be in a reboot cycle. Are you sure you want to go into maintenance mode?";
        public static readonly string WARNING_GATEWAY_1 = "Unable to get the default gateway from the routing table. Using default value instead.";
        public static readonly string WARNING_MULTIPLE_GATEWAYS_1 = "There is more than one default gateway with the lowest metric:";
        public static readonly string WARNING_MULTIPLE_GATEWAYS_2 = "The first gateway will be used. You may need to override this in the config file.";
        public static readonly string ERROR_LOG_FILE = "Unable to write to the log file. The application will have to close down immediately. The error returned was: ";
        public static readonly string ERROR_LOG_DIR = "Unable to create to the log file directory. The application will have to close down immediately. The error returned was: ";
        public static readonly string ROUTER_COMMS_ADMIN_ONLINE = "Another Administrator is connected to the router.";
        public static readonly string ROUTER_COMMS_AR_DISABLED = "Auto-reboot functionality is now disabled.";
        public static readonly string ROUTER_COMMS_ERROR_CREATING_COMMAND = "There was an error creating the router command.";
        public static readonly string ROUTER_COMMS_ERROR = "Problem communicating with router.";
        public static readonly string ROUTER_COMMS_NORMAL = "Communication with the router has been established.";
        public static readonly string ROUTER_COMMS_AR_ENABLED = "Auto-reboot functionality is now enabled."; 
        public static readonly string ERROR_README = "Unable to load the README.TXT file from: ";
        public static readonly string NOTICE_REBOOT_AUTO = "An attempt to reboot the router automatically was queued because Internet connectivity seems to have been lost.";
        public static readonly string NOTICE_REBOOT_AUTO_SNR = "An attempt to reboot the router automatically was queued because the SNR has dropped below the threshold.";
        public static readonly string NOTICE_REBOOT_MAN = "An attempt to reboot the router manually was queued.";
        public static readonly string NOTICE_REBOOT_OVER_SUCCESS = "The reboot cycle is complete and the uptime indicates that the reboot attempt was a success.";
        public static readonly string NOTICE_REBOOT_OVER_FAILED = "The reboot cycle is complete but the uptime indicates that the reboot attempt failed.";        
        public static readonly string NOTICE_REBOOT_OVER = "The reboot cycle is complete.";
        public static readonly string NOTICE_CUSTOM_OVER = "The custom command cycle is complete.";
        public static readonly string NOTICE_CUSTOM_OVER_NONE_TO_RUN = "The custom command cycle is complete as there were no custom commands to run.";
        public static readonly string NOTICE_TELNET_START = "An attempt to start the telnet daemon was queued.";
        public static readonly string NOTICE_TELNET_STOP = "An attempt to kill the telnet daemon was queued.";
        public static readonly string NOTICE_CUSTOM_AUTO = "The custom command(s) were queued.";
        public static readonly string NOTICE_CUSTOM_AUTO_NONE = "No custom commands are configured.";
        public static readonly string NOTICE_CUSTOM_QUEUE = "Queueing the custom command(s).";
        public static readonly string NOTICE_CUSTOM_MAN = "Queueing the custom command: ";
        public static readonly string NOTICE_THREAD_KILLED_BEFORE = "Killing thread for URL: ";
        public static readonly string NOTICE_THREAD_KILLED_AFTER = "Thread killed for URL: ";
        public static readonly string NOTICE_THREAD_STATE = " ThreadState = ";
        public static readonly string NOTICE_LOG_FILE_PRUNED = "Log file pruned: ";
        public static readonly string NOTICE_LOG_FILE_PRUNED_ERROR = "Error deleting log file: ";
        public static readonly string NOTICE_MAINTENANCE_MODE_ON = "BSR is now in maintenance mode.";
        public static readonly string NOTICE_MAINTENANCE_MODE_OFF = "BSR is no longer in maintenance mode.";
        public static readonly string NOTICE_DNS_CACHE = "Flushing the DNS resolver cache...";

        // Form text
        public static readonly string FORMVERSION_TEXTBOX_QUESTION_1 = "There is a new version of ";
        public static readonly string FORMVERSION_TEXTBOX_QUESTION_2 = " available for download. Do you wish to download it now?";
        public static readonly string FORMVERSION_TITLE_TEXT = " Update";

        // Maximum and Minimum constants (see Settings.cs)
        public static readonly int STARTUP_DELAY_MAX = 90000; // ms (90 secs)
        public static readonly int STARTUP_DELAY_MIN = 0; // ms
        public static readonly int URL_CHECK_TIMEOUT_MAX = 60000; // ms (60 secs)
        public static readonly int URL_CHECK_TIMEOUT_MIN = 2000; // ms (2 secs)
        public static readonly int URL_CHECK_TIMEOUT_WHEN_REBOOTING_MAX = 60000; // ms (60 secs)
        public static readonly int URL_CHECK_TIMEOUT_WHEN_REBOOTING_MIN = 2000; // ms (2 secs)
        public static readonly int URL_CHECK_INTERVAL_MAX = 300000; // ms (5 mins)
        public static readonly int URL_CHECK_INTERVAL_MIN = 2000; // ms (2 secs)
        public static readonly int ROUTER_COMMAND_TIMEOUT_MAX = 30000; // ms (30 secs)
        public static readonly int ROUTER_COMMAND_TIMEOUT_MIN = 2000; // ms (2 secs)
        public static readonly int ROUTER_STATS_INTERVAL_MAX = 120000; // ms (2 mins)
        public static readonly int ROUTER_STATS_INTERVAL_MIN = 2000; // ms (2 secs)

    }

    public static class GlobalVariables
    {
        public static string RouterIp = String.Empty;
        public static string LogFileName = String.Empty;
    }

    public static class GlobalMethods
    {
        #region PInvoke - Checked for 64-bit Safety
        [DllImport("user32.dll")]
        static extern Int32 FlashWindowEx(ref FLASHWINFO pwfi);

        [StructLayout(LayoutKind.Sequential)]
        private struct FLASHWINFO
        {
            public UInt32 cbSize;
            public IntPtr hwnd;
            public UInt32 dwFlags;
            public UInt32 uCount;
            public UInt32 dwTimeout;
        }
        #endregion

        public static void FlashWindowEx(Form form)
        {
            FLASHWINFO finfo = new FLASHWINFO();
            finfo.cbSize = Convert.ToUInt32(Marshal.SizeOf(finfo));
            finfo.hwnd = form.Handle;
            finfo.dwFlags = 14; // Flash tray until the window gets the focus
            finfo.uCount = UInt32.MaxValue;
            finfo.dwTimeout = 0;

            FlashWindowEx(ref finfo);
        }

        public static void ValidateSettings()
        {
            // Validation. It's a two phase thing:
            // 1. Get each value within it's allowed range.
            // 2. Ensure that the interval is never less than the timout, and if it is, make the interval bigger.
            //    This has the potential to lift the interval beyond its upper range if the max timout is greater
            //    than the max interval. However, it's our job to set things up so that this is never the case!
            Properties.Settings.Default.url_check_interval = Properties.Settings.Default.url_check_interval > GlobalConstants.URL_CHECK_INTERVAL_MAX ? GlobalConstants.URL_CHECK_INTERVAL_MAX : Properties.Settings.Default.url_check_interval;
            Properties.Settings.Default.url_check_interval = Properties.Settings.Default.url_check_interval < GlobalConstants.URL_CHECK_INTERVAL_MIN ? GlobalConstants.URL_CHECK_INTERVAL_MIN : Properties.Settings.Default.url_check_interval;
            Properties.Settings.Default.url_check_interval_when_rebooting = Properties.Settings.Default.url_check_interval_when_rebooting > GlobalConstants.URL_CHECK_TIMEOUT_WHEN_REBOOTING_MAX ? GlobalConstants.URL_CHECK_TIMEOUT_WHEN_REBOOTING_MAX : Properties.Settings.Default.url_check_interval_when_rebooting;
            Properties.Settings.Default.url_check_interval_when_rebooting = Properties.Settings.Default.url_check_interval_when_rebooting < GlobalConstants.URL_CHECK_TIMEOUT_WHEN_REBOOTING_MIN ? GlobalConstants.URL_CHECK_TIMEOUT_WHEN_REBOOTING_MIN : Properties.Settings.Default.url_check_interval_when_rebooting;

            Properties.Settings.Default.url_check_timeout = Properties.Settings.Default.url_check_timeout > GlobalConstants.URL_CHECK_TIMEOUT_MAX ? GlobalConstants.URL_CHECK_TIMEOUT_MAX : Properties.Settings.Default.url_check_timeout;
            Properties.Settings.Default.url_check_timeout = Properties.Settings.Default.url_check_timeout < GlobalConstants.URL_CHECK_TIMEOUT_MIN ? GlobalConstants.URL_CHECK_TIMEOUT_MIN : Properties.Settings.Default.url_check_timeout;
            Properties.Settings.Default.url_check_interval_when_rebooting = Properties.Settings.Default.url_check_interval_when_rebooting > Properties.Settings.Default.url_check_interval ? Properties.Settings.Default.url_check_interval : Properties.Settings.Default.url_check_interval_when_rebooting;
            
            Properties.Settings.Default.url_check_interval = Properties.Settings.Default.url_check_interval < Properties.Settings.Default.url_check_timeout ? Properties.Settings.Default.url_check_timeout : Properties.Settings.Default.url_check_interval;
            Properties.Settings.Default.url_check_interval_when_rebooting = Properties.Settings.Default.url_check_interval_when_rebooting < Properties.Settings.Default.url_check_timeout ? Properties.Settings.Default.url_check_timeout : Properties.Settings.Default.url_check_interval_when_rebooting;

            Properties.Settings.Default.router_stats_interval = Properties.Settings.Default.router_stats_interval > GlobalConstants.ROUTER_STATS_INTERVAL_MAX ? GlobalConstants.ROUTER_STATS_INTERVAL_MAX : Properties.Settings.Default.router_stats_interval;
            Properties.Settings.Default.router_stats_interval = Properties.Settings.Default.router_stats_interval < GlobalConstants.ROUTER_STATS_INTERVAL_MIN ? GlobalConstants.ROUTER_STATS_INTERVAL_MIN : Properties.Settings.Default.router_stats_interval;
            Properties.Settings.Default.router_command_timeout = Properties.Settings.Default.router_command_timeout > GlobalConstants.ROUTER_COMMAND_TIMEOUT_MAX ? GlobalConstants.ROUTER_COMMAND_TIMEOUT_MAX : Properties.Settings.Default.router_command_timeout;
            Properties.Settings.Default.router_command_timeout = Properties.Settings.Default.router_command_timeout < GlobalConstants.ROUTER_COMMAND_TIMEOUT_MIN ? GlobalConstants.ROUTER_COMMAND_TIMEOUT_MIN : Properties.Settings.Default.router_command_timeout;
            Properties.Settings.Default.router_stats_interval = Properties.Settings.Default.router_stats_interval < Properties.Settings.Default.router_command_timeout ? Properties.Settings.Default.router_command_timeout : Properties.Settings.Default.router_stats_interval;

            Properties.Settings.Default.max_logfile_count = Properties.Settings.Default.max_logfile_count < 1 ? int.MaxValue : Properties.Settings.Default.max_logfile_count;
            Properties.Settings.Default.logging_level = Properties.Settings.Default.logging_level < 0 ? 0 : Properties.Settings.Default.logging_level;
            Properties.Settings.Default.startup_delay = Properties.Settings.Default.startup_delay > GlobalConstants.STARTUP_DELAY_MAX ? GlobalConstants.STARTUP_DELAY_MAX : Properties.Settings.Default.startup_delay;
            Properties.Settings.Default.startup_delay = Properties.Settings.Default.startup_delay < GlobalConstants.STARTUP_DELAY_MIN ? GlobalConstants.STARTUP_DELAY_MIN : Properties.Settings.Default.startup_delay;
        }

        public static void Disaster(Exception ex, string msg)
        {
            // We only want to put something on the screen once...
            if (!States.LogDisasterAlreadyDisplayed)
            {
                // We only display this dialog once
                States.LogDisasterAlreadyDisplayed = true;

                // Show the dialog - the formload event in the form itself makes it flash
                FormError frm = new FormError(ex, msg);
                frm.Activate();
                frm.WindowState = FormWindowState.Normal;
                frm.ShowDialog();

                // Set the closing flag and then exit
                States.RunningState = RunningState.Closing;
                Application.Exit();
            }
        }

        public static string GetAssemblyDirectory()
        {
            return Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetModules()[0].FullyQualifiedName);
        }

        public static void EnsureDirectory(System.IO.DirectoryInfo oDirInfo)   
        {
            // Recursive function that checks that the full path exists and creates it if not
            if (oDirInfo.Parent != null)
            {
                EnsureDirectory(oDirInfo.Parent);
            }
            if (!oDirInfo.Exists)
            {
                try
                {
                    oDirInfo.Create();
                }
                catch (Exception ex)
                {
                    // We must exit with a dialog and, if possible write to the event log
                    try
                    {
                        EventLog.WriteEntry(Assembly.GetExecutingAssembly().GetName().Name, GlobalConstants.STRING_ERROR + GlobalConstants.LOG_SEPARATOR_STRING + GlobalConstants.ERROR_LOG_DIR, EventLogEntryType.Error);
                    }
                    catch
                    {
                        // If we can't write to the event log, at least we tried!
                    }
                    GlobalMethods.Disaster(ex, GlobalConstants.ERROR_LOG_DIR);
                }
            } 
        }   
    }
}