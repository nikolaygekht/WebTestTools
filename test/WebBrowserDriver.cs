using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Web.WebView2.WinForms;
using Newtonsoft.Json;

namespace webviewtest
{
    /// <summary>
    /// The class to create and use in-process web browser. 
    /// 
    /// See also <see cref="WebBrowserDriverExtensions"/> for extension methods.
    /// </summary>
    public sealed class WebBrowserDriver : IDisposable
    {
        private WebBrowserForm mForm = null;
        private Thread mThread = null;

        /// <summary>
        /// The browser control
        /// </summary>
        public WebView2 WebView => mForm.WebView;

        /// <summary>
        /// The method perform the action specified in the browser's thread.
        /// </summary>
        /// <param name="action"></param>
        private void Perform(Action action)
        {
            if (mForm != null)
            {
                if (mForm.InvokeRequired)
                    mForm.Invoke(action);
                else
                    action();
                return;
            }
            throw new InvalidOperationException("The form is not initialized");
        }

        /// <summary>
        /// The method performs the function specified in the browser's thread and return the value.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="action"></param>
        /// <returns></returns>
        private T Perform<T>(Func<T> action)
        {
            if (mForm != null)
            {
                if (mForm.InvokeRequired)
                    return (T)mForm.Invoke(action);
                else
                    return action();
            }
            throw new InvalidOperationException("The form is not initialized");
        }

        /// <summary>
        /// Closes the form
        /// </summary>
        public void Close() => Perform(() => mForm.Close());

        /// <summary>
        /// Returns the flag indicating whether the WebView2 core is successfully initialized
        /// </summary>
        public bool HasCore
        {
            get => Perform(() => mForm.WebView.CoreWebView2 != null);
        }

        /// <summary>
        /// Returns the flag indicating whether the last initiated navigation operation completed.
        /// </summary>
        public bool NavigationCompleted
        {
            get => Perform(() => mForm.NavigationCompleted);
        }

        /// <summary>
        /// Navigates the browser to the URL specified and waits until navigation finished.
        /// </summary>
        /// <param name="url"></param>
        public void Navigate(string url)
        {
            Perform(() => mForm.NavigateTo(url));
            while (!mForm.NavigationCompleted)
                Thread.Yield();
        }

        /// <summary>
        /// Sets the browser to the specified HTML content and  until initialization is finished.
        /// </summary>
        /// <param name="url"></param>
        public void SetContent(string content)
        {
            Perform(() => mForm.SetContent(content));
            while (!mForm.NavigationCompleted)
                Thread.Yield();
        }

        /// <summary>
        /// Executes the script in the browser and returns a json result as a string
        /// </summary>
        /// <param name="script"></param>
        /// <returns></returns>
        public string ExecuteScriptRaw(string script)
        {
            var task = Perform(() => mForm.WebView.ExecuteScriptAsync(script));
            task.Wait();

            while (!mForm.NavigationCompleted)
                Task.Yield();

            if (task.Exception != null)
                throw task.Exception;

            return task.Result;
        }

        /// <summary>
        /// Executes the script and parses a returned json object into the type specified
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="script"></param>
        /// <returns></returns>
        public T ExecuteScript<T>(string script)
        {
            string s = ExecuteScriptRaw(script);

            if (s == null || s == "null")
                return default(T);

            return JsonConvert.DeserializeObject<T>(s);
        }

        /// <summary>
        /// Shows a window
        /// </summary>
        /// <param name="show"></param>
        public void Show(bool show) => Perform(() => mForm.Visible = show);

        /// <summary>
        /// Starts a driver and wait until the web core is initialized
        /// </summary>
        /// <param name="timeout"></param>
        public void Start(int timeout = 1000)
        {
            if (mThread == null || !mThread.IsAlive)
            {
                mThread = new Thread(Runner);
                mThread.IsBackground = true;
                mThread.SetApartmentState(ApartmentState.STA);
                mThread.Start();
                DateTime end = DateTime.Now.AddMilliseconds(timeout);
                while (mForm == null || !mForm.Visible || !mForm.LoadingCompled)
                {
                    Thread.Yield();
                    if (DateTime.Now >= end)
                        throw new TimeoutException();
                }
            }
        }

        /// <summary>
        /// Disposes the object. 
        /// 
        /// If form is still open, it will be closed. 
        /// </summary>
        public void Dispose()
        {
            if (mThread.IsAlive)
                Close();

            mForm.Dispose();
            mForm = null;
        }

        /// <summary>
        /// Form thread. 
        /// </summary>
        private void Runner()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(mForm = new WebBrowserForm());
        }
    }
}
