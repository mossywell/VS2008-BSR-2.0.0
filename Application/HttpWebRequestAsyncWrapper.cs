using System;
using System.Windows.Forms;
using System.Net;
using Mossywell.WebUtils2;
using System.Threading;

namespace Mossywell.BSR
{
    #region Delegates
    delegate void HttpWebRequestAsyncWrapperCallback(int requestid);
    #endregion

    #region Enums
    public enum RequestStatus
    {
        Running,
        Stopped,
    }

    public enum AbbreviatedStatus
    {
        GETTING,
        OK,
        PE,
        NRF,
        CF,
        T,
        SF,
        UE,
    }
    #endregion

    class HttpWebAsyncResponse
    {
        public string ResponseString;
        public Exception Exception;
        public string FullStatus;
        public AbbreviatedStatus AbbreviatedStatus;
        public RequestStatus RequestStatus;
        public long ElapsedMilliseconds;
    }

    class HttpWebRequestAsyncWrapper
    {
        /* This class is a wrapper around the HttpWebRequestAsync class. It is very similar to it.
         * In fact, all it really does is accept all the same arguments as HttpWebRequestAsync, but
         * in addition, it accepts an arbitraty index int, which allows the callback function to
         * identify which particular HttpWebRequestAsync it was that called back. Also, the callback
         * from HttpWebRequestAsync comes back to here and we call back the caller after a bit of
         * number crunching! All the number crunching is is parsing the result and setting a few
         * properties for the caller to query. */

        #region Class Fields
        private HttpWebRequestAsync _request;
        private int _requestid;
        private string _url;
        private HttpWebRequestAsyncWrapperCallback _callback;
        private System.Diagnostics.Stopwatch _sw;
        private HttpWebAsyncResponse _response;
        #endregion

        #region Constructor
        public HttpWebRequestAsyncWrapper(int requestid, Control caller, string url, int timeout, HttpWebRequestAsyncWrapperCallback callback)
        {
            _request = new HttpWebRequestAsync(caller, url, timeout, new HttpWebRequestAsyncCallback(CallbackFromParent));
            _requestid = requestid;
            _url = url;
            _callback = callback;
            _sw = new System.Diagnostics.Stopwatch();
            _request.ThreadAbort += new ThreadAbortEventHandler(_request_ThreadAbort);

            // THESE ARE THE DEFAULT VALUES FOR A WEBRESPONSE
            _response = new HttpWebAsyncResponse();
            _response.AbbreviatedStatus = AbbreviatedStatus.GETTING;
            _response.ElapsedMilliseconds = 0;
            _response.Exception = null;
            _response.FullStatus = String.Empty;
            _response.RequestStatus = RequestStatus.Stopped;
            _response.ResponseString = String.Empty;
        }

        public HttpWebRequestAsyncWrapper(int requestid, Control caller, string url, int timeout, HttpWebRequestAsyncWrapperCallback callback, CredentialCache cache)
        {
            _request = new HttpWebRequestAsync(caller, url, timeout, new HttpWebRequestAsyncCallback(CallbackFromParent), cache);
            _requestid = requestid;
            _url = url;
            _callback = callback;
            _sw = new System.Diagnostics.Stopwatch();
            _response = new HttpWebAsyncResponse();

            _response.AbbreviatedStatus = AbbreviatedStatus.OK;
            _response.ElapsedMilliseconds = 0;
            _response.Exception = null;
            _response.FullStatus = String.Empty;
            _response.RequestStatus = RequestStatus.Stopped;
            _response.ResponseString = String.Empty;
        }
        #endregion

        #region Utilities
        public void GetResponse()
        {
            // No need to reset _request as HttpWebRequestAsync does it for us

            // Set the internal status flag
            _response.RequestStatus = RequestStatus.Running;

            // Start the stopwatch and make the request
            _sw.Reset();
            _sw.Start();
            _request.GetResponse();
        }

        public void Close(bool resetflags)
        {
            if (_request != null)
            {
                _request.Abort();
            }

            // Set the internal status flags
            _response.RequestStatus = RequestStatus.Stopped;
            if (resetflags)
            {
                _response.AbbreviatedStatus = AbbreviatedStatus.GETTING;
            }
        }

        private void CallbackFromParent()
        {
            // Stop the stopwatch!
            _sw.Stop();

            // Save the response, exception
            _response.ElapsedMilliseconds = _request.ElapsedMilliseconds; 
            _response.ResponseString = _request.Response;
            _response.Exception = _request.Exception;

            // Did we get an exception?
            if (_response.Exception == null)
            {
                // Capture when no exception is returned but something went wrong
                // because the elapsed time is still 0
                if (_response.ElapsedMilliseconds <= 0L || _response.ResponseString.Trim() == String.Empty)
                {
                    _response.FullStatus = "Unknown error";
                    _response.AbbreviatedStatus = AbbreviatedStatus.UE;
                }
                else
                {
                    _response.FullStatus = "Success";
                    _response.AbbreviatedStatus = AbbreviatedStatus.OK;
                }
            }
            else
            {
                // Did we get a web exception? If so, save it, otherwise throw
                if (_response.Exception.GetType().FullName == "System.Net.WebException")
                {
                    WebException webexception = (WebException)_response.Exception;
                    _response.FullStatus = webexception.Message;
                    switch (webexception.Status.ToString())
                    {
                        case "ProtocolError":
                            _response.AbbreviatedStatus = AbbreviatedStatus.PE;
                            break;
                        case "NameResolutionFailure": // E.g. non existent web site
                            _response.AbbreviatedStatus = AbbreviatedStatus.NRF;
                            break;
                        case "ConnectFailure":
                            _response.AbbreviatedStatus = AbbreviatedStatus.CF;
                            break;
                        case "Timeout": // E.g. site is in dns but is down
                            _response.AbbreviatedStatus = AbbreviatedStatus.T;
                            break;
                        case "SendFailure": // E.g. using https instead of http
                            _response.AbbreviatedStatus = AbbreviatedStatus.SF;
                            break;
                        default:
                            _response.AbbreviatedStatus = AbbreviatedStatus.UE;
                            break;
                    }
                }
                else
                {
                    throw _response.Exception;
                }
            }

            // Set the internal status flag BERFORE doing the callback
            _response.RequestStatus = RequestStatus.Stopped;

            // Finally, call the caller back
            _callback(_requestid);
        }
        #endregion

        #region Events
        private void _request_ThreadAbort(object sender, ThreadAbortEventArgs e)
        {
            // Just log it
            if (e.ThreadStateSituation == ThreadStateSituation.BeforeAbort)
            {
                LogManager.Log(GlobalConstants.STRING_DEBUG, GlobalConstants.NOTICE_THREAD_KILLED_BEFORE + _url + GlobalConstants.NOTICE_THREAD_STATE + e.ThreadState.ToString());
            }
            else
            {
                LogManager.Log(GlobalConstants.STRING_DEBUG, GlobalConstants.NOTICE_THREAD_KILLED_AFTER + _url + GlobalConstants.NOTICE_THREAD_STATE + e.ThreadState.ToString());
            }
        }
        #endregion

        #region Properties
        public HttpWebAsyncResponse Response
        {
            get
            {
                return _response;
            }
        }

        public string Url
        {
            get
            {
                return _url;
            }
        }
        #endregion
    }
}
