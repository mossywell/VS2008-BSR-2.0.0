using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.IO;
using Microsoft.Win32;
using System.Management;
using System.Net;
using System.Text.RegularExpressions;
using System.Diagnostics;

using Mossywell.WebUtils2;
using Mossywell.NetUtils1;

namespace Mossywell.BSR
{
    public partial class NotifyIconForm  : Form
    {
        #region Class Fields
        private NotifyIconData _notifyicondata;
        private Stopwatch _rebootstopwatch;
        private FormAbout _formabout;
        private FormUsernamePassword _formusernamepassword;
        private FormVersion _formversion;
        private FormLogReader _formlogreader;
        private List<Icon> _icons;

        private WebPageGetter _wpg;
        private RouterCommander _rc;
        private RouterStatsGetter _rsg;
        private VersionGetter _vg;
        #endregion

        #region Constructor
        public NotifyIconForm()
        {
            InitializeComponent();
        }
        #endregion

        #region Utilities
        private void LoadStartLoggingAndRouterIP()
        {
            // Log it and then allow pruning straight away after
            GlobalMethods.EnsureDirectory(new DirectoryInfo(LogManager.GetLogFileDirectory()));
            LogManager.Log(GlobalConstants.STRING_INFO, GlobalConstants.ASSEMBLY_TITLE + " version " + Assembly.GetExecutingAssembly().GetName().Version.ToString() + " started.");
            GlobalVariables.RouterIp = GetDefaultGateway();
            LogSettings();
            LogManager.PruningEnabled = true;
            LogManager.PruneLogDirectory();
        }

        private void LogSettings()
        {   
            // this is the first method to access the Properties.Settings. There is
            // an event SettingsLoaded which will run immediately after the settings are read and
            // before the next line is run. The SettingsLoaded validates the settings and writes
            // the validated values to the log file.
            LogManager.Log(GlobalConstants.STRING_INFO, "Exe Directory: " + GlobalMethods.GetAssemblyDirectory());
            LogManager.Log(GlobalConstants.STRING_INFO, "URL Interval: " + Properties.Settings.Default.url_check_interval.ToString());
            LogManager.Log(GlobalConstants.STRING_INFO, "URL Interval When Rebooting: " + Properties.Settings.Default.url_check_interval_when_rebooting.ToString()); 
            LogManager.Log(GlobalConstants.STRING_INFO, "URL Timeout: " + Properties.Settings.Default.url_check_timeout.ToString());
            LogManager.Log(GlobalConstants.STRING_INFO, "Router Stats Interval: " + Properties.Settings.Default.router_stats_interval.ToString());
            LogManager.Log(GlobalConstants.STRING_INFO, "Router Command Timeout: " + Properties.Settings.Default.router_command_timeout.ToString());
            LogManager.Log(GlobalConstants.STRING_INFO, "Maximum log file count: " + Properties.Settings.Default.max_logfile_count.ToString());
            LogManager.Log(GlobalConstants.STRING_INFO, "Logging level: " + Properties.Settings.Default.logging_level.ToString());
            LogManager.Log(GlobalConstants.STRING_INFO, "Router IP: " + GlobalVariables.RouterIp);
            LogManager.Log(GlobalConstants.STRING_INFO, "Startup delay: " + Properties.Settings.Default.startup_delay.ToString());
            LogManager.Log(GlobalConstants.STRING_INFO, "Low SNR reboot threshold: " + Properties.Settings.Default.snr_reboot_threshold.ToString());
            LogManager.Log(GlobalConstants.STRING_INFO, "Automatic version checking: " + Properties.Settings.Default.auto_version_check.ToString());
            LogManager.Log(GlobalConstants.STRING_INFO, "URL check disabled: " + Properties.Settings.Default.url_check_disabled.ToString());
        }

        private void LoadEvents()
        {
            SystemEvents.PowerModeChanged += new PowerModeChangedEventHandler(SystemEvents_PowerModeChanged);
            _wpg.Response += new HttpWebResponseEventHandler(_wpg_Response);
            _rsg.Response += new EventHandler(_rsg_Response);
            _vg.Response += new EventHandler(_vg_Response);
            _rc.RouterCommanderConnectivityChange += new EventHandler(_rc_ConnectivityChange);
        }

        private void LoadSettings()
        {
            _wpg = new WebPageGetter(this);
            _rc = new RouterCommander();
            _rsg = new RouterStatsGetter(this, _rc);
            _vg = new VersionGetter(this);
            _notifyicondata = new NotifyIconData(Properties.Settings.Default.urls.Count);
            _rebootstopwatch = new Stopwatch();
            _formabout = null;
            _formusernamepassword = null;
            _formversion = null;
            _formlogreader = null;
        }

        private void LoadIcons()
        {
            _icons = new List<Icon>();
            _icons.Add(new Icon(Assembly.GetExecutingAssembly().GetManifestResourceStream("Mossywell.BSR.Icons.router-green.ico"))); // 0
            _icons.Add(new Icon(Assembly.GetExecutingAssembly().GetManifestResourceStream("Mossywell.BSR.Icons.router-amber.ico"))); // 1
            _icons.Add(new Icon(Assembly.GetExecutingAssembly().GetManifestResourceStream("Mossywell.BSR.Icons.router-red.ico"))); // 2
            _icons.Add(new Icon(Assembly.GetExecutingAssembly().GetManifestResourceStream("Mossywell.BSR.Icons.router-green-norouter.ico"))); // 3
            _icons.Add(new Icon(Assembly.GetExecutingAssembly().GetManifestResourceStream("Mossywell.BSR.Icons.router-amber-norouter.ico"))); // 4
            _icons.Add(new Icon(Assembly.GetExecutingAssembly().GetManifestResourceStream("Mossywell.BSR.Icons.router-red-norouter.ico"))); // 5
            _icons.Add(new Icon(Assembly.GetExecutingAssembly().GetManifestResourceStream("Mossywell.BSR.Icons.router-green-mm.ico"))); // 6
            _icons.Add(new Icon(Assembly.GetExecutingAssembly().GetManifestResourceStream("Mossywell.BSR.Icons.router-amber-mm.ico"))); // 7
            _icons.Add(new Icon(Assembly.GetExecutingAssembly().GetManifestResourceStream("Mossywell.BSR.Icons.router-red-mm.ico"))); // 8
            _icons.Add(new Icon(Assembly.GetExecutingAssembly().GetManifestResourceStream("Mossywell.BSR.Icons.router-green-norouter-mm.ico"))); // 9
            _icons.Add(new Icon(Assembly.GetExecutingAssembly().GetManifestResourceStream("Mossywell.BSR.Icons.router-amber-norouter-mm.ico"))); // 10
            _icons.Add(new Icon(Assembly.GetExecutingAssembly().GetManifestResourceStream("Mossywell.BSR.Icons.router-red-norouter-mm.ico"))); // 11
            _icons.Add(new Icon(Assembly.GetExecutingAssembly().GetManifestResourceStream("Mossywell.BSR.Icons.router-grey.ico"))); // 12
            _icons.Add(new Icon(Assembly.GetExecutingAssembly().GetManifestResourceStream("Mossywell.BSR.Icons.router-grey-mm.ico"))); // 13
        }

        private void LoadTimers()
        {
            // Set the intervals
            timerStateCheck.Interval = GlobalConstants.TIMER_STATE_CHECK_INTERVAL;
            timerDnsResolverCacheFlush.Interval = GlobalConstants.TIMER_DNS_RESOLVER_CACHE_FLUSH_INTERVAL;
            timerRebootStateCheckDelay.Interval = Properties.Settings.Default.url_check_interval + Properties.Settings.Default.url_check_timeout + GlobalConstants.TIMER_DELAY_FUDGE_FACTOR;
            timerCustomCommandStateCheckDelay.Interval = Properties.Settings.Default.url_check_interval + Properties.Settings.Default.url_check_timeout + GlobalConstants.TIMER_DELAY_FUDGE_FACTOR;

            // Do we need to pause?
            if (Properties.Settings.Default.startup_delay > 0)
            {
                // Behave as if the icon had been double clicked
                timerStartupDelay.Interval = Properties.Settings.Default.startup_delay; 
                DoPauseOrRestartMe(RunningState.Paused);
                timerStartupDelay.Start(); // When this ticks, it double clicks the icon, which in
                                           // turn starts the timers just like the else clause below
                                           // but it updates the icon, flips AppState and logs stuff.
            }
            else
            {
                // No delay specified, so we can start the timers as normal
                DoTimerStartsAndTicks();
            }
        }

        private void DoRebootRouter(RebootReason reason)
        {          
            // We need to close any outstanding responses as we'll be
            // rebooting the router, so we know they'll fail anyway.
            _wpg.Stop(true, true);

            // Now play the boing (only if the reboot was automatic)
            if (reason == RebootReason.AutomaticLowSNR || reason == RebootReason.AutomaticNoConnectivity)
            {
                PlayRebootWav();
            }

            // Start the stopwatch so that we know the time between the command being issued
            // and the reboot success. We read the value on success and compare it with the 
            // uptime to see if the reboot was actually a success.
            _rebootstopwatch.Reset();
            _rebootstopwatch.Start();
            timerDnsResolverCacheFlush.Start();

            // Change the state flag and start the timer
            LogManager.Log(GlobalConstants.STRING_DEBUG, "DoRebootRouter(): States.RebootState = RebootState.RebootRequestedPreDelay");
            States.RebootState = RebootState.RebootRequestedPreDelay;
            timerRebootStateCheckDelay.Start();
            
            // Now add the reboot to the queue
            _rc.AddCommand(this, GlobalConstants.ROUTER_COMMAND_REBOOT, null);

            // Change the webstats timer to the reboot interval but don't do an immediate
            // run as it could finish before the reboot has had a chance to dequeue.
            _wpg.TimerInterval = Properties.Settings.Default.url_check_interval_when_rebooting;
            _wpg.Start(false); // Even if it's disabled

            // Log the attempt
            if (reason == RebootReason.AutomaticNoConnectivity)
            {
                LogManager.Log(GlobalConstants.STRING_COMMAND, GlobalConstants.NOTICE_REBOOT_AUTO);
            }
            else if (reason == RebootReason.AutomaticLowSNR)
            {
                LogManager.Log(GlobalConstants.STRING_COMMAND, GlobalConstants.NOTICE_REBOOT_AUTO_SNR);
            }
            else
            {
                LogManager.Log(GlobalConstants.STRING_COMMAND, GlobalConstants.NOTICE_REBOOT_MAN);
            }
        }

        private void DoUpdateNotifyIcon()
        {
            // Are we paused?
            if (States.RunningState == RunningState.Paused)
            {
                notifyIcon.Text = DateTime.Now.ToLongTimeString() + Environment.NewLine + GlobalConstants.STRING_PAUSED;
                if (States.MaintenanceMode == MaintenanceMode.Off)
                {
                    notifyIcon.Icon = _icons[12]; // grey
                }
                else
                {
                    notifyIcon.Icon = _icons[13];
                }
            }
            else
            {
                LogManager.Log(GlobalConstants.STRING_DEBUG, "DoUpdateNotifyIcon(): _wpg.ResponsesState = " + _wpg.ResponsesState.ToString());
                
                notifyIcon.Text = _notifyicondata.GetCompleteTooltipText;

                // Now the icon. Note that this way has an interesting effect because when the form is first loaded,
                // it is red, then very quickly it goes amber and then green. (Assuming all are OK.) Nice!

                if (!_wpg.Enabled || _wpg.ResponsesState == WebPagesGetterResponsesState.AllOK)
                {
                    if (_rc.RouterAcceptingCommands)
                    {
                        if (States.MaintenanceMode == MaintenanceMode.Off)
                        {
                            notifyIcon.Icon = _icons[0]; // green
                        }
                        else
                        {
                            notifyIcon.Icon = _icons[6]; // green |
                        }
                    }
                    else
                    {
                        if (States.MaintenanceMode == MaintenanceMode.Off)
                        {
                            notifyIcon.Icon = _icons[3]; // green -
                        }
                        else
                        {
                            notifyIcon.Icon = _icons[9]; // green +
                        }
                    }
                }
                else if (_wpg.ResponsesState == WebPagesGetterResponsesState.NoneOKNoneStarting ||
                    _wpg.ResponsesState == WebPagesGetterResponsesState.NoneOKSomeStarting)
                {
                    if (_rc.RouterAcceptingCommands)
                    {
                        if (States.MaintenanceMode == MaintenanceMode.Off)
                        {
                            notifyIcon.Icon = _icons[2]; // red
                        }
                        else
                        {
                            notifyIcon.Icon = _icons[8]; // red |
                        }
                    }
                    else
                    {
                        if (States.MaintenanceMode == MaintenanceMode.Off)
                        {
                            notifyIcon.Icon = _icons[5]; // red -
                        }
                        else
                        {
                            notifyIcon.Icon = _icons[11]; // red +
                        }
                    }
                }
                else
                {
                    if (_rc.RouterAcceptingCommands)
                    {
                        if (States.MaintenanceMode == MaintenanceMode.Off)
                        {
                            notifyIcon.Icon = _icons[1]; // amber
                        }
                        else
                        {
                            notifyIcon.Icon = _icons[7]; // amber |
                        }
                    }
                    else
                    {
                        if (States.MaintenanceMode == MaintenanceMode.Off)
                        {
                            notifyIcon.Icon = _icons[4]; // amber -
                        }
                        else
                        {
                            notifyIcon.Icon = _icons[10]; // amber +
                        }
                    }
                }
            }
        }

        private string GetDefaultGateway()
        {
            // Local variables
            string gateway = String.Empty;

            // Get the config file initially and check for IP address format
            gateway = Properties.Settings.Default.router_ip_override;
            try
            {
                IPAddress.Parse(gateway);
            }
            catch
            {
                // Gateway in config file is invalid format, so blank the string
                gateway = String.Empty;
            }
            // If the string isn't empty, it's a valid IP, so exit
            if (gateway != String.Empty)
            {
                return gateway;
            }

            // Next use the netutils1 dll and get an array of non-ppp default routes with the lowest metric
            UInt32 minmetric = UInt32.MaxValue;
            List<IPAddress> gateways = new List<IPAddress>();
            IPForwardAndIFTable ipfift = RoutingAndInterfaceTable.IPForwardAndIFTable; // Shortening for convenience

            for (int row = 0; row < ipfift.NumEntries; row++)
            {
                if (ipfift.Table[row].ForwardDest.ToString() == "0.0.0.0")
                {
                    // Ensure that's it's not a PPP connection
                    if (ipfift.Table[row].Type != IFType.IF_TYPE_PPP)
                    {
                        // It's a default route that's not a PPP, but does it have the lowest metric?
                        if (ipfift.Table[row].ForwardMetric1 < minmetric)
                        {
                            minmetric = ipfift.Table[row].ForwardMetric1;
                            gateways.Clear();
                            gateways.Add(ipfift.Table[row].ForwardNextHop);
                        }
                        else if (ipfift.Table[row].ForwardMetric1 == minmetric)
                        {
                            gateways.Add(ipfift.Table[row].ForwardNextHop);
                        }
                    }
                }
            }

            // OK, what did we get?
            if (gateways.Count == 0)
            {
                LogManager.Log(GlobalConstants.STRING_WARNING, GlobalConstants.WARNING_GATEWAY_1);
                return GlobalConstants.DEFAULT_GATEWAY;
            }
            else if (gateways.Count == 1)
            {
                return gateways[0].ToString();
            }
            else
            {
                LogManager.Log(GlobalConstants.STRING_WARNING, GlobalConstants.WARNING_MULTIPLE_GATEWAYS_1);
                foreach (IPAddress addr in gateways)
                {
                    LogManager.Log(GlobalConstants.STRING_WARNING, "Gateway: " + addr.ToString());
                }
                LogManager.Log(GlobalConstants.STRING_WARNING, GlobalConstants.WARNING_MULTIPLE_GATEWAYS_2);
                return gateways[0].ToString();
            }
        }

        private int DoRunCustomCommands()
        {
            int customcommandcount = 0;
            foreach (string s in Properties.Settings.Default.custom_commands)
            {
                if (s.Trim() != String.Empty)
                {
                    customcommandcount++;
                }
            }

            LogManager.Log(GlobalConstants.STRING_COMMAND, GlobalConstants.NOTICE_CUSTOM_QUEUE);
           
            if (customcommandcount > 0)
            {
                // Change the state to pre-delay and start the timer to
                // change the state
                LogManager.Log(GlobalConstants.STRING_DEBUG, "DoRunCustomCommands(): States.RebootState = RebootState.CustomCommandsRequestedPreDelay");
                States.RebootState = RebootState.CustomCommandsRequestedPreDelay;
                timerCustomCommandStateCheckDelay.Start();

                // Run the commands
                foreach (string s in Properties.Settings.Default.custom_commands)
                {
                    if (s.Trim() != String.Empty)
                    {
                        _rc.AddCommand(this, GlobalConstants.ROUTER_COMMAND_PREFIX + s, null);
                        LogManager.Log(GlobalConstants.STRING_COMMAND, GlobalConstants.NOTICE_CUSTOM_MAN + s);
                    }
                }
                LogManager.Log(GlobalConstants.STRING_INFO, GlobalConstants.NOTICE_CUSTOM_AUTO);
            }
            else
            {
                LogManager.Log(GlobalConstants.STRING_INFO, GlobalConstants.NOTICE_CUSTOM_AUTO_NONE);
            }

            return customcommandcount;
        }

        private void DoResetFlagsAndLogCompletion(int commandsrun)
        {
            // Stop the stopwatches and timers
            _rebootstopwatch.Stop();
            timerDnsResolverCacheFlush.Stop();

            // Flush the cache one last time (for Vista)
            LogManager.Log(GlobalConstants.STRING_DEBUG, "DoResetFlagsAndLogCompletion(): " + GlobalConstants.NOTICE_DNS_CACHE);
            Mossywell.NetUtils1.DnsApi.DnsFlushResolverCache();

            // The customcommandonly flag tells us if we're here from a custom command only
            // command, or a post-reboot custom command. If the former, we don't want to
            // be logging the fact that we're rebooted successfully!
            LogManager.Log(GlobalConstants.STRING_DEBUG, "DoResetFlagsAndLogCompletion(): States.RebootState = RebootState.Normal");
            States.RebootState = RebootState.Normal;
            if (commandsrun == 0)
            {
                LogManager.Log(GlobalConstants.STRING_INFO, GlobalConstants.NOTICE_CUSTOM_OVER_NONE_TO_RUN);
            }
            else
            {
                LogManager.Log(GlobalConstants.STRING_INFO, GlobalConstants.NOTICE_CUSTOM_OVER);
            }

            // Reset the timer interval back to normal. No need to do an immediate
            // run as we've just done one.
            _wpg.TimerInterval = Properties.Settings.Default.url_check_interval;
            _wpg.Stop(true, false);
            if (!Properties.Settings.Default.url_check_disabled)
            {
                _wpg.Start(false);
            }

            // Get the uptime - used for logging purposes only
            if (States.CustomCommandReason == CustomCommandReason.AutomaticAfterReboot)
            {
                // Work out if the reboot worked
                LogManager.Log(GlobalConstants.STRING_DEBUG, "DoResetFlagsAndLogCompletion(): _rsg.UptimeInMilliseconds = " + _rsg.UptimeInMilliseconds.ToString());
                LogManager.Log(GlobalConstants.STRING_DEBUG, "DoResetFlagsAndLogCompletion(): _rebootstopwatch.ElapsedMilliseconds = " + _rebootstopwatch.ElapsedMilliseconds.ToString());
                if (_rsg.UptimeInMilliseconds == 0d || (_rsg.UptimeInMilliseconds > _rebootstopwatch.ElapsedMilliseconds))
                {
                    LogManager.Log(GlobalConstants.STRING_INFO, GlobalConstants.NOTICE_REBOOT_OVER_FAILED);
                }
                else
                {
                    LogManager.Log(GlobalConstants.STRING_INFO, GlobalConstants.NOTICE_REBOOT_OVER_SUCCESS);
                }
            }
        }

        private void DoPauseOrRestartMe(RunningState newrunningstate)
        {
            if (newrunningstate == RunningState.Running)
            {
                // Stop the startup delay timer just in case it's running
                timerStartupDelay.Stop();

                // Visual stuff first
                LogManager.Log(GlobalConstants.STRING_DEBUG, "notifyIcon_DoubleClick(): States.RunningState = RunningState.Running");
                States.RunningState = RunningState.Running;
                DoUpdateMenuOptions();
                DoUpdateNotifyIcon();

                // Now the timers (including an immediate run of the Ticks)
                DoTimerStartsAndTicks();

                // Finally log it
                LogManager.Log(GlobalConstants.STRING_INFO, GlobalConstants.ASSEMBLY_TITLE + " re-started.");
            }
            else
            {
                // Stop the normal timers first
                _wpg.Stop(true, true);
                _rsg.Stop(true);
                _vg.Stop(true);

                // Close everything that launches a thread
                _rc.Close();

                // Stop the state checker but leave the maintenance mode flag as it is so that DoTimerStartsAndTicks()
                // can put it back to how it was before the app was paused. The icon uses the maintenance mode flag
                // so it won't show the fact that the state checker has been stopped (unless it was already stopped).
                timerStateCheck.Enabled = false;

                // Visual stuff second
                LogManager.Log(GlobalConstants.STRING_DEBUG, "notifyIcon_DoubleClick(): States.RunningState = RunningState.Paused");
                States.RunningState = RunningState.Paused;
                DoUpdateMenuOptions();
                DoUpdateNotifyIcon();

                // Finally log it
                LogManager.Log(GlobalConstants.STRING_INFO, GlobalConstants.ASSEMBLY_TITLE + " paused.");
            }
        }

        private void DoUpdateMenuOptions()
        {
            if (States.RunningState == RunningState.Running)
            {
                if (_rc.RouterAcceptingCommands)
                {
                    // Set 1
                    toolStripMenuItemUpdateWebInfo.Enabled = true;
                    toolStripMenuItemUpdateStatsNow.Enabled = true;
                    toolStripMenuItemRunCustomCommandsNow.Enabled = true;
                    toolStripMenuItemRebootRouter.Enabled = true;

                    // Set 2
                    toolStripMenuItemDisplayCurrentStats.Enabled = true;
                    toolStripMenuItemDisplayDetailedStats.Enabled = true;
                    toolStripMenuItemDisplayUsernamePassword.Enabled = true;
                    toolStripMenuItemStartUtelnetd.Enabled = true;
                    toolStripMenuItemKillUtelnetd.Enabled = true;
                }
                else
                {
                    // Set 1
                    toolStripMenuItemUpdateWebInfo.Enabled = false;
                    toolStripMenuItemUpdateStatsNow.Enabled = false;
                    toolStripMenuItemRunCustomCommandsNow.Enabled = false;
                    toolStripMenuItemRebootRouter.Enabled = false;

                    // Set 2
                    toolStripMenuItemDisplayCurrentStats.Enabled = false;
                    toolStripMenuItemDisplayDetailedStats.Enabled = false;
                    toolStripMenuItemDisplayUsernamePassword.Enabled = false;
                    toolStripMenuItemStartUtelnetd.Enabled = false;
                    toolStripMenuItemKillUtelnetd.Enabled = false;
                }
            }
            else
            {
                if (_rc.RouterAcceptingCommands)
                {
                    // Set 1
                    toolStripMenuItemUpdateWebInfo.Enabled = false;
                    toolStripMenuItemUpdateStatsNow.Enabled = false;
                    toolStripMenuItemRunCustomCommandsNow.Enabled = false;
                    toolStripMenuItemRebootRouter.Enabled = false;

                    // Set 2
                    toolStripMenuItemDisplayCurrentStats.Enabled = true;
                    toolStripMenuItemDisplayDetailedStats.Enabled = true;
                    toolStripMenuItemDisplayUsernamePassword.Enabled = true;
                    toolStripMenuItemStartUtelnetd.Enabled = true;
                    toolStripMenuItemKillUtelnetd.Enabled = true;
                }
                else
                {
                    // Set 1
                    toolStripMenuItemUpdateWebInfo.Enabled = false;
                    toolStripMenuItemUpdateStatsNow.Enabled = false;
                    toolStripMenuItemRunCustomCommandsNow.Enabled = false;
                    toolStripMenuItemRebootRouter.Enabled = false;

                    // Set 2
                    toolStripMenuItemDisplayCurrentStats.Enabled = false;
                    toolStripMenuItemDisplayDetailedStats.Enabled = false;
                    toolStripMenuItemDisplayUsernamePassword.Enabled = false;
                    toolStripMenuItemStartUtelnetd.Enabled = false;
                    toolStripMenuItemKillUtelnetd.Enabled = false;
                }
            }
        }

        private void PlayRebootWav()
        {
            try
            {
                System.Media.SoundPlayer player = new System.Media.SoundPlayer(GlobalMethods.GetAssemblyDirectory() + @"\" + GlobalConstants.REBOOT_SOUND);
                player.Play();
                player.Dispose();
            }
            catch
            {
            }
        }

        private void DoTimerStartsAndTicks()
        {
            // Now start the timers and do the ticks
            if (!Properties.Settings.Default.url_check_disabled)
            {
                _wpg.Start(true);
            }
            _rsg.Start(true);
            if (Properties.Settings.Default.auto_version_check)
            {
                _vg.Start(true);
            }
            if (States.MaintenanceMode == MaintenanceMode.Off)
            {
                timerStateCheck.Start();
                timerStateCheck_Tick(null, null);
            }

            // Update the icon
            DoUpdateNotifyIcon();
        }

        private void DoStateCheck()
        {
            // Do nothing at all if the router isn't contactable or admin is already online
            if (!_rc.RouterAcceptingCommands)
            {
                return;
            }

            // Also do nothing if we're in one of the two PreDelay states
            switch (States.RebootState)
            {
                case RebootState.Normal:
                    if (_wpg.ResponsesState == WebPagesGetterResponsesState.NoneOKNoneStarting)
                    {
                        this.DoRebootRouter(RebootReason.AutomaticNoConnectivity);
                    }
                    else if (_rsg.SNRIsUnderTheThreshold)
                    {
                        this.DoRebootRouter(RebootReason.AutomaticLowSNR);
                    }
                    break;

                case RebootState.RebootRequestedPreDelay:
                    break;

                case RebootState.RebootRequestedPostDelay:
                    if (_wpg.ResponsesState == WebPagesGetterResponsesState.AllOK || _wpg.ResponsesState == WebPagesGetterResponsesState.SomeOK)
                    {
                        // Just reconnected after a reboot so do the custom commands
                        // RebootState is changed within DoRunCustomCommands
                        LogManager.Log(GlobalConstants.STRING_DEBUG, "DoStateCheck(): _wpg.ResponsesState = " + _wpg.ResponsesState.ToString());
                        LogManager.Log(GlobalConstants.STRING_DEBUG, "DoStateCheck(): States.CustomCommandReason = CustomCommandReason.AutomaticAfterReboot");
                        States.CustomCommandReason = CustomCommandReason.AutomaticAfterReboot;
                        if (this.DoRunCustomCommands() == 0)
                        {
                            // Log the completion
                            // RebootState is changed within DoResetFlagsAndLogCompletion
                            this.DoResetFlagsAndLogCompletion(0);
                        }
                    }
                    break;

                case RebootState.CustomCommandsRequestedPreDelay:
                    break;

                case RebootState.CustomCommandsRequestedPostDelay:
                    if (_wpg.ResponsesState == WebPagesGetterResponsesState.AllOK || _wpg.ResponsesState == WebPagesGetterResponsesState.SomeOK)
                    {
                        // Log the completion
                        // RebootState is changed within DoResetFlagsAndLogCompletion
                        this.DoResetFlagsAndLogCompletion(1);
                    }
                    break;
            }
        }
        #endregion

        #region Callbacks
        private void RouterCommandDisplayCurrentStatsCallback()
        {
            // Called after selecting the Display Current Stats option
            // Get the response
            if (_rc.Response.Length > 0)
            {
                File.WriteAllText(LogManager.GetLogFileDirectory() + @"\" + GlobalConstants.STATS_PAGE, _rc.Response);
                System.Diagnostics.Process.Start(LogManager.GetLogFileDirectory() + @"\" + GlobalConstants.STATS_PAGE);
            }
        }

        private void RouterCommandDisplayDetailedStatsCallback1()
        {
            _rc.AddCommand(this, GlobalConstants.ROUTER_COMMAND_NETGEAR_CFG, new RouterCommanderCallback(RouterCommandDisplayDetailedStatsCallback2));
        }

        private void RouterCommandDisplayDetailedStatsCallback2()
        {
            // Get the response
            if (_rc.Response.Length > 0)
            {
                File.WriteAllText(LogManager.GetLogFileDirectory() + @"\" + GlobalConstants.DETAILED_STATS_PAGE, _rc.Response.Replace("\n", Environment.NewLine));
                System.Diagnostics.Process.Start(LogManager.GetLogFileDirectory() + @"\" + GlobalConstants.DETAILED_STATS_PAGE);
            }
        }

        private void RouterCommandDisplayUsernamePasswordCallback1()
        {
            _rc.AddCommand(this, GlobalConstants.ROUTER_COMMAND_NETGEAR_CFG, new RouterCommanderCallback(RouterCommandDisplayUsernamePasswordCallback2));
        }

        private void RouterCommandDisplayUsernamePasswordCallback2()
        {
            if (_formusernamepassword == null || _formusernamepassword.IsDisposed)
            {
                _formusernamepassword = new FormUsernamePassword();
                _formusernamepassword.textBoxUsername.Text = _rc.ParseUsername();
                _formusernamepassword.textBoxPassword.Text = _rc.ParsePassword();
                _formusernamepassword.Show();
            }
            else
            {
                _formusernamepassword.textBoxUsername.Text = _rc.ParseUsername();
                _formusernamepassword.textBoxPassword.Text = _rc.ParsePassword();
                _formusernamepassword.Activate();
                _formusernamepassword.WindowState = FormWindowState.Normal;
            }
        }
        #endregion

        #region Events
        private void SystemEvents_PowerModeChanged(object sender, PowerModeChangedEventArgs e)
        {
            // Log all power changes
            LogManager.Log(GlobalConstants.STRING_DEBUG, "SystemEvents_PowerModeChanged(): PowerModes = " + e.Mode.ToString());

            // If we suspend, the pause. If we resume, pause again (to be safe)
            // but set the restart timer
            if (e.Mode == PowerModes.Suspend)
            {
                DoPauseOrRestartMe(RunningState.Paused);
            }
            else if (e.Mode == PowerModes.Resume)
            {
                DoPauseOrRestartMe(RunningState.Paused);
                timerStartupDelay.Interval = GlobalConstants.TIMER_DELAY_AFTER_RESUME;
                timerStartupDelay.Start(); // When this ticks, it double clicks the icon, which in
                                           // turn starts the timers just like the else clause below
                                           // but it updates the icon, flips AppState and logs stuff.
            }
        }

        protected override void OnActivated(System.EventArgs e)
        {
            this.Hide();
        }

        private void NotifyIconForm_Load(object sender, EventArgs e)
        {
            // Set the flag. The LoadLoggingAndRouterIP call writes to the log. This log writing can fail.
            // If it does fail, it issues an application.exit which in turn fires up the form_closing event
            // which sets the AppState.Closing to true! So, by the time we get to all the LoadThings, we could
            // already be closing. This is why we test again for the state of AppState.Closing.
            LogManager.Log(GlobalConstants.STRING_DEBUG, "NotifyIconForm_Load(): States.RunningState = RunningState.Running");
            States.RunningState = RunningState.Running;

            // Load all the logging related stuff
            LoadStartLoggingAndRouterIP();

            if (States.RunningState != RunningState.Closing)
            {
                // Load the various settings
                LoadSettings();
                
                // Load the custom events
                LoadEvents();

                // Load the embedded icons
                LoadIcons();

                // Start the timers and run each thing straight away
                LoadTimers();
            }
        }

        private void NotifyIconForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Set the closing flag. This flag is used in the case of an emergency close. See
            // the LogIt method for more information.
            LogManager.Log(GlobalConstants.STRING_DEBUG, "NotifyIconForm_FormClosing(): States.RunningState = RunningState.Closing");
            States.RunningState = RunningState.Closing;

            // Stop all the timers
            _rebootstopwatch.Stop();
            _wpg.Stop(true, true);
            _rsg.Stop(true);
            _vg.Stop(true);

            // Unhook the events
            _wpg.Response -= new HttpWebResponseEventHandler(_wpg_Response);
            _rsg.Response -= new EventHandler(_rsg_Response);

            // Close everything that launches a thread
            _rc.Close();

            // Log the close
            LogManager.Log(GlobalConstants.STRING_INFO, GlobalConstants.ASSEMBLY_TITLE + " version " + Assembly.GetExecutingAssembly().GetName().Version.ToString() + " closed.");
        }

        private void timerStartupDelay_Tick(object sender, EventArgs e)
        {
            // Stop me and then double click the icon
            timerStartupDelay.Stop();
            DoPauseOrRestartMe(RunningState.Running);
        }

        private void timerStateCheck_Tick(object sender, EventArgs e)
        {
            DoStateCheck();
        }
     
        private void timerCustomCommandStateCheckDelay_Tick(object sender, EventArgs e)
        {
            // Stop the timer and change the state
            timerCustomCommandStateCheckDelay.Stop();
            LogManager.Log(GlobalConstants.STRING_DEBUG, "timerCustomCommandStateCheckDelay_Tick(): States.RebootState = RebootState.CustomCommandsRequestedPostDelay");
            States.RebootState = RebootState.CustomCommandsRequestedPostDelay;
        }

        private void timerRebootStateCheckDelay_Tick(object sender, EventArgs e)
        {
            // Stop the timer and change the state
            timerRebootStateCheckDelay.Stop();
            LogManager.Log(GlobalConstants.STRING_DEBUG, "timerRebootStateCheckDelay_Tick(): States.RebootState = RebootState.RebootRequestedPostDelay");
            States.RebootState = RebootState.RebootRequestedPostDelay;
        }

        private void timerDnsResolverCacheFlush_Tick(object sender, EventArgs e)
        {
            LogManager.Log(GlobalConstants.STRING_DEBUG, "timerDnsResolverCacheFlush_Tick(): " + GlobalConstants.NOTICE_DNS_CACHE);
            Mossywell.NetUtils1.DnsApi.DnsFlushResolverCache();
        }

        private void toolStripMenuItemClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void toolStripMenuItemAbout_Click(object sender, EventArgs e)
        {
            if (_formabout == null || _formabout.IsDisposed)
            {
                _formabout = new FormAbout();
                _formabout.Show();
            }
            else
            {
                _formabout.Activate();
                _formabout.WindowState = FormWindowState.Normal;
            }
        }

        private void toolStripMenuItemDisplayLog_Click(object sender, EventArgs e)
        {
            if (_formlogreader == null || _formlogreader.IsDisposed)
            {
                _formlogreader = new FormLogReader(GlobalVariables.LogFileName);
                _formlogreader.Show();
            }
            else
            {
                _formlogreader.Activate();
                _formlogreader.WindowState = FormWindowState.Normal;
            }
        }

        private void toolStripMenuItemRebootRouter_Click(object sender, EventArgs e)
        {
            DialogResult result;

            if (States.MaintenanceMode == MaintenanceMode.Off)
            {
                // Let's make sure
                result = MessageBox.Show(GlobalConstants.QUESTION_REBOOT, GlobalConstants.ASSEMBLY_TITLE, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            }
            else
            {
                // Let's make sure especially as we're in maintenance mode
                result = MessageBox.Show(GlobalConstants.QUESTION_REBOOT_MAINTENANCE_IS_ON, GlobalConstants.ASSEMBLY_TITLE, MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            }

            if (result == DialogResult.Yes)
            {
                DoRebootRouter(RebootReason.Manual);
            }
        }
        
        private void toolStripMenuItemRunCustomCommandsNow_Click(object sender, EventArgs e)
        {
            DialogResult result;

            if (States.MaintenanceMode == MaintenanceMode.Off)
            {
                // Let's make sure
                result = MessageBox.Show(GlobalConstants.QUESTION_RUN_CUSTOM_COMMANDS, GlobalConstants.ASSEMBLY_TITLE, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            }
            else
            {
                // Let's make sure especially as we're in maintenance mode
                result = MessageBox.Show(GlobalConstants.QUESTION_RUN_CUSTOM_COMMANDS_MAINTENANCE, GlobalConstants.ASSEMBLY_TITLE, MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            }

            if (result == DialogResult.Yes)
            {
                LogManager.Log(GlobalConstants.STRING_DEBUG, "toolStripMenuItemRunCustomCommandsNow_Click(): States.CustomCommandReason = CustomCommandReason.Manual");
                States.CustomCommandReason = CustomCommandReason.Manual;
                if (DoRunCustomCommands() == 0)
                {
                    LogManager.Log(GlobalConstants.STRING_INFO, GlobalConstants.NOTICE_CUSTOM_OVER_NONE_TO_RUN);
                }
            }
        }

        private void toolStripMenuItemStartUtelnetd_Click(object sender, EventArgs e)
        {
            _rc.AddCommand(this, GlobalConstants.ROUTER_COMMAND_START_UTELNETD, null);
            
            // Log the attempt
            LogManager.Log(GlobalConstants.STRING_COMMAND, GlobalConstants.NOTICE_TELNET_START);
        }

        private void toolStripMenuItemKillUtelnetd_Click(object sender, EventArgs e)
        {
            _rc.AddCommand(this, GlobalConstants.ROUTER_COMMAND_KILL_UTELNETD, null);

            // Log the attempt
            LogManager.Log(GlobalConstants.STRING_COMMAND, GlobalConstants.NOTICE_TELNET_STOP);
        }

        private void toolStripMenuItemDisplayCurrentStats_Click(object sender, EventArgs e)
        {
            _rc.AddCommand(this, GlobalConstants.ROUTER_COMMAND_GET_OR_DISPLAY_CURRENT_STATS, new RouterCommanderCallback(RouterCommandDisplayCurrentStatsCallback));
        }

        private void toolStripMenuItemUpdateStatsNow_Click(object sender, EventArgs e)
        {
            // Simply do what the timer does!
            _rsg.TimerTick();
            // timerStats_Tick(null, null);
        }

        private void toolStripMenuItemUpdateWebInfo_Click(object sender, EventArgs e)
        {
            // Simply do what the timer does!
            _wpg.TimerTick();
        }

        private void toolStripMenuItemViewEditConfigFile_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("notepad.exe", GlobalMethods.GetAssemblyDirectory() + @"\" + System.Reflection.Assembly.GetExecutingAssembly().GetName().Name + ".exe.config");
        }

        private void toolStripMenuItemDisplayUsernamePassword_Click(object sender, EventArgs e)
        {
            _rc.AddCommand(this, GlobalConstants.ROUTER_COMMAND_USERNAME_PASSWORD, new RouterCommanderCallback(RouterCommandDisplayUsernamePasswordCallback1));
        }

        private void toolStripMenuItemHelp_Click(object sender, EventArgs e)
        {
            Help.ShowHelp(this, GlobalMethods.GetAssemblyDirectory() + @"\" + GlobalConstants.HELP_FILE);
        }

        private void toolStripMenuItemDisplayDetailedStats_Click(object sender, EventArgs e)
        {
            _rc.AddCommand(this, GlobalConstants.ROUTER_COMMAND_DISPLAY_DETAILED_STATS, new RouterCommanderCallback(RouterCommandDisplayDetailedStatsCallback1));
        }

        private void notifyIcon_DoubleClick(object sender, EventArgs e)
        {
            if (States.RunningState == RunningState.Paused)
            {
                DoPauseOrRestartMe(RunningState.Running);
            }
            else
            {
                DoPauseOrRestartMe(RunningState.Paused);
            }
        }

        private void toolStripMenuItemPause_Click(object sender, EventArgs e)
        {
            // Just do what the double click event does!
            DoPauseOrRestartMe(RunningState.Paused);
        }

        private void toolStripMenuItemMaintenanceMode_Click(object sender, EventArgs e)
        {
            // Check that the router state is normal. If it isn't ask if we want to
            // put it in maintenance mode as doing so during a reboot cycle can confuse things

            DialogResult result = DialogResult.Yes; // Default

            if(States.RebootState != RebootState.Normal && States.MaintenanceMode == MaintenanceMode.Off)
            {
                result = MessageBox.Show(GlobalConstants.QUESTION_MAINTENANCE_IN_REBOOT, GlobalConstants.ASSEMBLY_TITLE, MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            }

            if(result == DialogResult.Yes)
            {
                if (States.MaintenanceMode == MaintenanceMode.On)
                {
                    States.MaintenanceMode = MaintenanceMode.Off;
                    this.timerStateCheck.Enabled = true;
                    LogManager.Log(GlobalConstants.STRING_INFO, GlobalConstants.NOTICE_MAINTENANCE_MODE_OFF);
                }
                else
                {
                    States.MaintenanceMode = MaintenanceMode.On;
                    this.timerStateCheck.Enabled = false;
                    LogManager.Log(GlobalConstants.STRING_INFO, GlobalConstants.NOTICE_MAINTENANCE_MODE_ON);
                }
            }

            // Update the icon
            DoUpdateNotifyIcon();
        }

        private void toolStripMenuItemReadme_Click(object sender, EventArgs e)
        {
            string str = GlobalMethods.GetAssemblyDirectory() + @"\README.TXT";
            try
            {
                System.Diagnostics.Process.Start(str);
            }
            catch
            {
                LogManager.Log(GlobalConstants.STRING_ERROR, GlobalConstants.ERROR_README + str);
            }
        }

        private void _wpg_Response(object sender, HttpWebResponseEventArgs e)
        {
            // Debugging
            LogManager.Log(GlobalConstants.STRING_SUPER_DEBUG, "_wpg_Response(): Response = " + _wpg.Requests[e.RequestID].Response.ResponseString);

            // Transfer the values to the _notifyicondata class
            _notifyicondata.AbbreviatedStatusArray[e.RequestID] = _wpg.Requests[e.RequestID].Response.AbbreviatedStatus;
            _notifyicondata.ElapsedMillisecondsArray[e.RequestID] = _wpg.Requests[e.RequestID].Response.ElapsedMilliseconds;

            // Update the icon
            DoUpdateNotifyIcon();

            // Log the values
            LogManager.Log(GlobalConstants.STRING_WEBPAGE, _wpg.Requests[e.RequestID].Url + GlobalConstants.SEPARATOR_STRING_STATS + _wpg.Requests[e.RequestID].Response.FullStatus + GlobalConstants.SEPARATOR_STRING_STATS + GlobalConstants.SEPARATOR_STRING_STATS +  _wpg.Requests[e.RequestID].Response.ElapsedMilliseconds.ToString() + "ms");
        }

        private void _rsg_Response(object sender, EventArgs e)
        {
            // Debugging
            LogManager.Log(GlobalConstants.STRING_SUPER_DEBUG, "_rsg_Response(): Response = " + _rc.Response);

            // Transfer the values to the _notifyicondata class
            _notifyicondata.Uptime = _rsg.Uptime;
            _notifyicondata.Speed = _rsg.Speed;
            _notifyicondata.SNR = _rsg.SNR;

            // Update the icon and menus
            DoUpdateMenuOptions();
            DoUpdateNotifyIcon();

            // Log the values
            LogManager.Log(GlobalConstants.STRING_STATS,
                _notifyicondata.Uptime + GlobalConstants.LOG_SEPARATOR_STATS_STRING +
                _notifyicondata.Speed + GlobalConstants.STRING_KBPS + GlobalConstants.LOG_SEPARATOR_STATS_STRING +
                _notifyicondata.SNR + GlobalConstants.STRING_DB + GlobalConstants.LOG_SEPARATOR_STATS_STRING +
                _rsg.RxBytes  + GlobalConstants.STRING_BYTES + GlobalConstants.LOG_SEPARATOR_STATS_STRING +
                _rsg.TxBytes + GlobalConstants.STRING_BYTES);
        }

        private void _vg_Response(object sender, EventArgs e)
        {
            // Debugging
            LogManager.Log(GlobalConstants.STRING_SUPER_DEBUG, "_vg_Response(): Response = " + _rc.Response);

            LogManager.Log(GlobalConstants.STRING_DEBUG, "_vg_Response(): ThisVersion = " + _vg.ThisVersion);
            LogManager.Log(GlobalConstants.STRING_DEBUG, "_vg_Response(): LatestVersion = " + _vg.LatestVersion);
            LogManager.Log(GlobalConstants.STRING_DEBUG, "_vg_Response(): NewUrl = " + _vg.NewUrl);
            LogManager.Log(GlobalConstants.STRING_DEBUG, "_vg_Response(): CompareToResult = " + _vg.CompareToResult.ToString());

            if (_vg.CompareToResult < 0)
            {
                if (_formversion == null || _formversion.IsDisposed)
                {
                    _formversion = new FormVersion(_vg.ThisVersion, _vg.LatestVersion, _vg.NewUrl);
                    _formversion.Show();
                }
                else
                {
                    _formversion.Activate();
                    _formversion.WindowState = FormWindowState.Normal;
                }
            }
        }

        private void _rc_ConnectivityChange(object sender, EventArgs e)
        {

            LogManager.Log(GlobalConstants.STRING_SUPER_DEBUG, "_rc_ConnectivityChange(): Response = " + _rc.Response); 
            
            string msg = String.Empty;
            
            switch (_rc.Connectivity)
            {
                case RouterCommanderConnectivity.OK:
                    msg = GlobalConstants.ROUTER_COMMS_NORMAL;
                    break;
                case RouterCommanderConnectivity.AnotherAdminIsOnline:
                    msg = GlobalConstants.ROUTER_COMMS_ADMIN_ONLINE;
                    break;
                case RouterCommanderConnectivity.ErrorCommunicatingWithRouter:
                    msg = GlobalConstants.ROUTER_COMMS_ERROR;
                    break;
                case RouterCommanderConnectivity.ErrorCreatingCommand:
                    msg = GlobalConstants.ROUTER_COMMS_ERROR_CREATING_COMMAND;
                    break;
            }

            if (_rc.RouterAcceptingCommands)
            {
                msg += " " + GlobalConstants.ROUTER_COMMS_AR_ENABLED;
                LogManager.Log(GlobalConstants.STRING_INFO, msg);
            }
            else
            {
                msg += " " + GlobalConstants.ROUTER_COMMS_AR_DISABLED;
                LogManager.Log(GlobalConstants.STRING_WARNING, msg);
            }
        }
        #endregion
    }
}