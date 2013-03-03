using System;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace Mossywell.BSR
{
    class RouterStatsGetter
    {
        #region Class Fields
        private bool _stopping;
        private bool _snrisunderthreshold;
        private Control _caller;
        private RouterCommander _rc;
        private System.Windows.Forms.Timer _timer = new Timer();

        private string _uptime;
        private string _speed;
        private string _snr;
        private string _rx;
        private string _tx;

        private double _uptimeinmilliseconds;
        private double _snrasdouble;
        #endregion

        #region Public Events
        public event EventHandler Response;
        #endregion

        #region Constructor
        public RouterStatsGetter(Control caller, RouterCommander routercommander)
        {
            // Set class fields - THESE ARE THE DEFAULTS
            _uptime = GlobalConstants.STRING_QUESTION_MARK;
            _speed = GlobalConstants.STRING_QUESTION_MARK;
            _snr = GlobalConstants.STRING_QUESTION_MARK;
            _rx = GlobalConstants.STRING_QUESTION_MARK;
            _tx = GlobalConstants.STRING_QUESTION_MARK;

            _stopping = false;
            _uptimeinmilliseconds = 0d;
            _snrasdouble = 0d;
            _snrisunderthreshold = false;

            // Save stuff
            _caller = caller;
            _rc = routercommander;

            // Initialise the timer
            _timer.Interval = Properties.Settings.Default.router_stats_interval;
            _timer.Tick += new EventHandler(_timer_Tick);
        }
        #endregion

        #region Public Methods
        public void Start(bool immediatetick)
        {
            _stopping = false;

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
                _stopping = true;
            }
        }

        public void TimerTick()
        {
            _timer_Tick(null, null);
        }
        #endregion

        #region Private Methods
        private void MyCallback()
        {
            // Check we're not stopping
            if(_stopping) return;

            // Parse out the values
            Match m;
            string resp = _rc.Response;

            // Uptime is a number starting on a line xxxxx.xx - it's the only one in the
            // output so we're quite safe in just asking for only one
            _uptime = Regex.Match(resp, @"^\d+\.?\d*", RegexOptions.Multiline).ToString().Trim();
            if (_uptime != String.Empty)
            {
                _uptimeinmilliseconds = Convert.ToDouble(_uptime) * 1000;
                TimeSpan ts = TimeSpan.FromMilliseconds(_uptimeinmilliseconds);
                _uptime = (ts.Days * 24 + ts.Hours).ToString("00") + ":" + ts.Minutes.ToString("00") + ":" + ts.Seconds.ToString("00");
            }
            else
            {
                _uptimeinmilliseconds = 0d;
                _uptime = GlobalConstants.STRING_QUESTION_MARK;
            }

            // Speed
            m = Regex.Match(resp, @"^Rate.*?(\d+)", RegexOptions.Multiline);
            if (m.Groups.Count == 2)
            {
                _speed = m.Groups[1].Value.Trim();
            }
            else
            {
                _speed = GlobalConstants.STRING_QUESTION_MARK;
            }
            _speed = (_speed == String.Empty ? GlobalConstants.STRING_QUESTION_MARK : _speed);
            
            // SNR
            m = Regex.Match(resp, @"^SNR.*?(\d+\.?\d*)", RegexOptions.Multiline);
            if (m.Groups.Count == 2)
            {
                _snr = m.Groups[1].Value.Trim();
                _snrasdouble = 0d;
                _snrisunderthreshold = false;
                try
                {
                    _snrasdouble = Convert.ToDouble(_snr);
                    if (Properties.Settings.Default.snr_reboot_threshold != 0d && _snrasdouble < Properties.Settings.Default.snr_reboot_threshold)
                    {
                        _snrisunderthreshold = true;
                    }
                }
                catch { }
            }
            else
            {
                _snr = GlobalConstants.STRING_QUESTION_MARK;
            }
            _snr = (_snr == String.Empty ? GlobalConstants.STRING_QUESTION_MARK : _snr);

            // Tx and Rx
            m = Regex.Match(resp, @"ppp0.*?RX bytes:(\d*).*?TX bytes:(\d*)", RegexOptions.Singleline);
            if (m.Groups.Count == 3) // All values found
            {
                _rx = m.Groups[1].Value.Trim();
                _tx = m.Groups[2].Value.Trim();
            }
            else
            {
                _rx = String.Empty;
                _tx = String.Empty;
            }
            _rx = (_rx == String.Empty ? GlobalConstants.STRING_QUESTION_MARK : _rx);
            _tx = (_tx == String.Empty ? GlobalConstants.STRING_QUESTION_MARK : _tx);

            // Call all subscribers to the event
            if (Response != null)
            {
                this.Response(this, new EventArgs());
            }
        } 
        #endregion

        #region Events
        private void _timer_Tick(object sender, EventArgs e)
        {
            _rc.AddCommand(_caller, GlobalConstants.ROUTER_COMMAND_ALL_STATS, new RouterCommanderCallback(MyCallback));
        }
        #endregion

        #region Properties
        public string Uptime
        {
            get
            {
                return _uptime;
            }
        }

        public double UptimeInMilliseconds
        {
            get
            {
                return _uptimeinmilliseconds;
            }
        }

        public string Speed
        {
            get
            {
                return _speed;
            }
        }

        public string SNR
        {
            get
            {
                return _snr;
            }
        }

        public string RxBytes
        {
            get
            {
                return _rx;
            }
        }

        public string TxBytes
        {
            get
            {
                return _tx;
            }
        }
        
        public bool SNRIsUnderTheThreshold
        {
            get
            {
                return _snrisunderthreshold;
            }
        }
        #endregion
    }
}
