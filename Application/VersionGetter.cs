using System;
using System.Text.RegularExpressions;
using Mossywell.WebUtils2;
using System.Windows.Forms;

namespace Mossywell.BSR
{
    class VersionGetter
    {
        #region Public Events
        public event EventHandler Response;
        #endregion

        #region Class Fields
        private System.Windows.Forms.Control _caller;
        private HttpWebRequestAsync _request;
        private string _thisversion;
        private string _latestversion;
        private int _comparetoresult;
        private string _newurl;
        private Timer _timer = new Timer();
        #endregion

        #region Constructor
        public VersionGetter(System.Windows.Forms.Control caller)
        {
            // Save the values
            _caller = caller;

            // Initialise fields
            _thisversion = GlobalConstants.STRING_QUESTION_MARK;
            _latestversion = GlobalConstants.STRING_QUESTION_MARK;
            _newurl = GlobalConstants.STRING_QUESTION_MARK; 
            _comparetoresult = 0;

            _request = new HttpWebRequestAsync(caller, GlobalConstants.VERSION_URL, Properties.Settings.Default.url_check_timeout, new HttpWebRequestAsyncCallback(MyCallback));

            // Initialise the timer
            _timer.Interval = GlobalConstants.TIMER_VERSION_INTERVAL;
            _timer.Tick += new EventHandler(_timer_Tick);
        }
        #endregion

        #region Public Utilities
        public void Start(bool immediatetick)
        {
            _timer.Start();
            if (immediatetick)
            {
                this._timer_Tick(null, null);
            }
        }

        public void Stop(bool forceclose)
        {
            _timer.Stop();

            if (forceclose)
            {
                if (_request != null)
                {
                    _request.Abort();
                }
            }
        }

        public void GetResponse()
        {
            _request.Abort(); // Just to be safe
            _request.GetResponse();
        }
        #endregion

        #region Private Utilities
        private void MyCallback()
        {
            _thisversion = GlobalConstants.STRING_QUESTION_MARK;
            _latestversion = GlobalConstants.STRING_QUESTION_MARK;
            _newurl = GlobalConstants.STRING_QUESTION_MARK;
            _comparetoresult = 0;

            if (_request.Exception == null)
            {
                Match m = Regex.Match(_request.Response, @"Version=([^,]+),([^\r\n]+)");
                if (m.Groups.Count == 3)
                {
                    Version thisversion = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
                    _thisversion = thisversion.ToString();
                    _latestversion = m.Groups[1].ToString();
                    Version latestversion = new Version(_latestversion);
                    _newurl = m.Groups[2].ToString();
                    _comparetoresult = thisversion.CompareTo(latestversion);
                }
            }

            if (Response != null)
            {
                this.Response(this, new EventArgs());
            }
        }
        #endregion

        #region Events
        private void _timer_Tick(object sender, EventArgs e)
        {
            this.GetResponse();
        }
        #endregion

        #region Properties
        public string ThisVersion
        {
            get
            {
                return _thisversion;
            }
        }

        public string LatestVersion
        {
            get
            {
                return _latestversion;
            }
        }

        public int CompareToResult
        {
            get
            {
                return _comparetoresult;
            }
        }

        public string NewUrl
        {
            get
            {
                return _newurl;
            }
        }
        #endregion
    }
}
