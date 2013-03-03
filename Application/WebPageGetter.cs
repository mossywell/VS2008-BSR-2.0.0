using System;
using System.Windows.Forms;
using System.Collections.Generic;
using Mossywell.WebUtils2;

namespace Mossywell.BSR
{
    #region Delegates
    public delegate void HttpWebResponseEventHandler(object sender, HttpWebResponseEventArgs e);    
    #endregion

    #region Enums
    public enum WebPagesGetterResponsesState
    {
        AllOK,
        SomeOK,
        NoneOKNoneStarting,
        NoneOKSomeStarting,
    }
    #endregion

    class HttpWebResponseEventArgs : EventArgs
    {
        public int RequestID;
    }

    class WebPageGetter
    {
        #region Class Fields
        private Control _caller;
        private List<HttpWebRequestAsyncWrapper> _requests;
        private System.Windows.Forms.Timer _timer = new Timer();
        private bool _enabled;
        #endregion

        #region Public Events
        public event HttpWebResponseEventHandler Response;
        #endregion

        #region Constructor
        public WebPageGetter(Control caller)
        {
            // Save the caller (used to marshal responses back to UI thread)
            _caller = caller;

            // Initialise the timer
            _timer.Interval = Properties.Settings.Default.url_check_interval;
            _timer.Tick += new EventHandler(_timer_Tick);

            // Create the web requests to callback to this class
            HttpWebRequestAsyncWrapperCallback callback = new HttpWebRequestAsyncWrapperCallback(MyCallback);

            // Load the requests
            _requests = new List<HttpWebRequestAsyncWrapper>();

            int requestid = 0;
            foreach (string s in Properties.Settings.Default.urls)
            {
                _requests.Add(new HttpWebRequestAsyncWrapper(requestid, _caller, s, Properties.Settings.Default.url_check_timeout, callback));
                requestid++;
            }

            _enabled = false;
        }
        #endregion

        #region Public Utilities
        public void Start(bool immediatetick)
        {
            _enabled = true;
            _timer.Start();
            if (immediatetick)
            {
                this._timer_Tick(null, null);
            }
        }

        public void Stop(bool forceclose, bool resetflags)
        {
            _enabled = false;
            _timer.Stop();

            if (forceclose)
            {
                if (_requests != null)
                {
                    foreach (HttpWebRequestAsyncWrapper request in _requests)
                    {
                        request.Close(resetflags);
                    }
                }
            }
        }

        public void TimerTick()
        {
            _timer_Tick(null, null);
        }
        #endregion

        #region Private Utilities
        private void MyCallback(int requestid)
        {
            // Call all subscribers to the event, passing the requestid
            HttpWebResponseEventArgs e = new HttpWebResponseEventArgs();
            e.RequestID = requestid;
            if (Response != null)
            {
                this.Response(this, e);
            }
        }
        #endregion

        #region Events
        private void _timer_Tick(object sender, EventArgs e)
        {
            foreach (HttpWebRequestAsyncWrapper request in _requests)
            {
                request.GetResponse();
            }
        }
        #endregion

        #region Properties
        public List<HttpWebRequestAsyncWrapper> Requests
        {
            get
            {
                return _requests;
            }
        }

        public int TimerInterval
        {
            set
            {
                _timer.Interval = value;
            }
        }

        public WebPagesGetterResponsesState ResponsesState
        {
            get
            {
                int startingcount = 0;
                int okcount = 0;
                foreach (HttpWebRequestAsyncWrapper request in _requests)
                {
                    if (request.Response.AbbreviatedStatus == AbbreviatedStatus.GETTING)
                    {
                        startingcount++;
                    }
                    else if (request.Response.AbbreviatedStatus == AbbreviatedStatus.OK)
                    {
                        okcount++;
                    }
                }

                if (okcount == _requests.Count)
                {
                    return WebPagesGetterResponsesState.AllOK;
                }
                else if (okcount == 0)
                {
                    return (startingcount == 0 ? WebPagesGetterResponsesState.NoneOKNoneStarting : WebPagesGetterResponsesState.NoneOKSomeStarting);
                }
                else
                {
                    return WebPagesGetterResponsesState.SomeOK;
                }
            }
        }

        public bool Enabled
        {
            get
            {
                return _enabled;
            }
        }
        #endregion

    }
}
