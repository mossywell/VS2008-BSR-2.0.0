using System;
using System.Windows.Forms;
using System.Collections.Generic;
using Mossywell.WebUtils2;
using System.Net;
using System.Text.RegularExpressions;

namespace Mossywell.BSR
{
    #region Delegates
    internal delegate void RouterCommanderCallback();
    #endregion

    internal enum RouterCommanderState
    {
        Idle,         // Not doing anything
        Busy,         // In here and busy
    }

    public enum RouterCommanderConnectivity
    {
        Start,
        OK,
        AnotherAdminIsOnline,
        ErrorCreatingCommand,
        ErrorCommunicatingWithRouter,
    }

    internal struct RouterCommanderQueueItem
    {
        internal Control control;
        internal string command;
        internal RouterCommanderCallback callback;
    }

    class RouterCommander
    {
        #region Public Events
        public event EventHandler RouterCommanderConnectivityChange;
        #endregion

        #region Class Fields
        private Mossywell.WebUtils2.HttpWebRequestAsync _request;
        private Control _caller;
        private RouterCommanderCallback _callback;
        private RouterCommanderState _routercommanderstate;
        private string _LogFileName;
        private List<RouterCommanderQueueItem> _queue;
        private System.Windows.Forms.Timer _timer;
        private RouterCommanderConnectivity _rcconnectivity;
        private bool _routeracceptingcommands; // A quick way of saying RouterCommanderConnectivity.OK and ErrorCreatingCommand. It
                                               // is faster than calculating the property each time it is got. All we have to remember
                                               // is to recalculate _routeracceptingcommands when _rcconnectivity changes.
        #endregion

        #region Constructor
        public RouterCommander()
        {
            _routercommanderstate = RouterCommanderState.Idle;
            _LogFileName = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name + ".log";
            _queue = new List<RouterCommanderQueueItem>();
            _timer = new Timer();
            _timer.Interval = 100;
            _timer.Tick += new EventHandler(_timer_Tick);
            _timer.Enabled = true;
            _rcconnectivity = RouterCommanderConnectivity.Start;
            _routeracceptingcommands = false;
        }
        #endregion

        #region Utilities
        private void SendCommandToRouter(Control caller, string command, RouterCommanderCallback callback)
        {
            // If we are busy, then just bail out
            if (_routercommanderstate == RouterCommanderState.Busy)
            {
                return;
            }

            // Note that we are busy
            _routercommanderstate = RouterCommanderState.Busy;

            // Save the caller and callback
            _caller = caller;
            _callback = callback;

            // Run the command
            Uri uri = new Uri("http://" + GlobalVariables.RouterIp);
    
            CredentialCache cache = new CredentialCache();
            cache.Add(uri, "Basic", new NetworkCredential(Properties.Settings.Default.router_username, Properties.Settings.Default.router_password));
            _request = new HttpWebRequestAsync(caller, uri.OriginalString + command, Properties.Settings.Default.router_command_timeout, new HttpWebRequestAsyncCallback(MyCallback), cache);
            _request.GetResponse();
        }

        public void AddCommand(Control caller, string command, RouterCommanderCallback callback)
        {
            RouterCommanderQueueItem item = new RouterCommanderQueueItem();
            item.callback = callback;
            item.command = command;
            item.control = caller;
            _queue.Add(item);
        }

        private void MyCallback()
        {
            RouterCommanderConnectivity rcconnectivitybackup = _rcconnectivity;
            _rcconnectivity = RouterCommanderConnectivity.OK;
            _routeracceptingcommands = true;

            if (_request.Exception != null)
            {
                // Check the cause of the exception and ignore it if it's a Stream timeout
                // (which happens if a kill and start utelnetd are done in quick sucession for some reason)
                // and also if the router state is already rebooting (no point in saying that we can't
                // contact the router if the router's just been rebooted!)
                if (States.RebootState == RebootState.Normal)
                {
                    if (_request.ExceptionCause == HttpWebRequestAsyncExceptionCause.HttpWebResponse)
                    {
                        // Was it a 401 with the string "Another Administrator online." in it?
                        // If so, there's another instance of BSR running or aomeone is logged in as
                        // an administrator on another machine!
                        if (Regex.IsMatch(_request.Response, GlobalConstants.STRING_IS_ADMIN_ONLINE))
                        {
                            _rcconnectivity = RouterCommanderConnectivity.AnotherAdminIsOnline;
                            _routeracceptingcommands = false;
                        }
                        else
                        {
                            _rcconnectivity = RouterCommanderConnectivity.ErrorCommunicatingWithRouter;
                            _routeracceptingcommands = false;
                        }
                    }
                    else if(_request.ExceptionCause == HttpWebRequestAsyncExceptionCause.Create)
                    {
                        _rcconnectivity = RouterCommanderConnectivity.ErrorCreatingCommand;
                        _routeracceptingcommands = true;
                    }
                }
            }

            // NOTE: The change event to notify subscribers of a change in the comnectivity
            // occurs before the callback.

            // Has the connectivity changed?
            if (_rcconnectivity != rcconnectivitybackup)
            {
                rcconnectivitybackup = _rcconnectivity;

                // Call the subscribers to let them know that the connectivity has changed;
                if (RouterCommanderConnectivityChange != null)
                {
                    this.RouterCommanderConnectivityChange(this, new EventArgs());
                }
            }

            // Call the caller
            if (_callback != null)
            {
                _callback();
            }

            // We are no longer busy
            _routercommanderstate = RouterCommanderState.Idle;
        }

        public void Close()
        {
            if (_request != null)
            {
                _request.Abort();
            }
            _request = null;
        }

        public string ParseUsername()
        {
            return Regex.Match(_request.Response, "pppoa_username=(.*)$", RegexOptions.Multiline).Groups[1].ToString();
        }

        public string ParsePassword()
        {
            return Regex.Match(_request.Response, "pppoa_password=(.*)$", RegexOptions.Multiline).Groups[1].ToString();
        }
        #endregion

        #region Events
        private void _timer_Tick(object sender, EventArgs e)
        {
            // Is there anything in the queue?
            if (_queue.Count > 0)
            {
                // Are we able to process it?
                if (_routercommanderstate == RouterCommanderState.Idle)
                {
                    // Send the command
                    RouterCommanderQueueItem item = new RouterCommanderQueueItem();
                    item.callback = _queue[0].callback;
                    item.command = _queue[0].command;
                    item.control = _queue[0].control;
                    _queue.RemoveAt(0);
                    SendCommandToRouter(item.control, item.command, item.callback);
                }
            }
        }
        #endregion

        #region Properties
        public string Response
        {
            get
            {
                if (_request != null)
                {
                    return _request.Response;
                }
                else
                {
                    return String.Empty;
                }
            }
        }

        public RouterCommanderConnectivity Connectivity
        {
            get
            {
                return _rcconnectivity;
            }
        }

        public bool RouterAcceptingCommands
        {
            get
            {
                return _routeracceptingcommands;
            }
        }
        #endregion
    }
}
