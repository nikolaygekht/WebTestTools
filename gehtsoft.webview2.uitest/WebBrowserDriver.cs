using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Web.WebView2.Core;
using Microsoft.Web.WebView2.WinForms;

namespace Gehtsoft.Webview2.Uitest
{
    /// <summary>
    /// <para>The class to create and use in-process web browser. </para>
    /// <para>See also <see cref="WebBrowserDriverExtensions"/> for extension methods.</para>
    /// </summary>
    public sealed class WebBrowserDriver : IDisposable
    {
        public class ByNameController
        {
            private readonly WebBrowserDriver mDriver;

            internal ByNameController(WebBrowserDriver driver)
            {
                mDriver = driver;
            }

            public Element this[string name, int index = 0] => new Element(mDriver, Element.LocatorTypes.Name, name, index);
        }

        public class ByClassController
        {
            private readonly WebBrowserDriver mDriver;

            internal ByClassController(WebBrowserDriver driver)
            {
                mDriver = driver;
            }

            public Element this[string name, int index = 0] => new Element(mDriver, Element.LocatorTypes.Class, name, index);
        }

        public class ByIdController
        {
            private readonly WebBrowserDriver mDriver;

            internal ByIdController(WebBrowserDriver driver)
            {
                mDriver = driver;
            }

            public Element this[string name] => new Element(mDriver, Element.LocatorTypes.Id, name);
        }
        public class ByXPathController
        {
            private readonly WebBrowserDriver mDriver;

            internal ByXPathController(WebBrowserDriver driver)
            {
                mDriver = driver;
            }

            public Element this[string xpath] => new Element(mDriver, Element.LocatorTypes.XPath, xpath);
        }

        public ByNameController ByName { get; }
        public ByClassController ByClass { get; }
        public ByIdController ById { get; }
        public ByXPathController ByXPath { get; }

        /// <summary>
        /// Return the current location (URL)
        /// </summary>
        /// <param name="driver"></param>
        /// <returns></returns>
        public string Location => ExecuteScript<string>("document.location.href");

        public WebBrowserDriver()
        {
            ByName = new ByNameController(this);
            ByClass = new ByClassController(this);
            ById = new ByIdController(this);
            ByXPath = new ByXPathController(this);
        }

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
        /// Executes script that returns no value
        /// </summary>
        /// <param name="script"></param>
        public void ExecuteScript(string script)
        {
            ExecuteScriptRaw(script);
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
                return default;

            return JsonSerializer.Deserialize<T>(s);
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
            if (mThread?.IsAlive != true)
            {
                mThread = new Thread(Runner)
                {
                    IsBackground = true
                };
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
        /// <para>Disposes the object. </para>
        /// <para>If form is still open, it will be closed. </para>
        /// </summary>
        public void Dispose()
        {
            if (mThread.IsAlive)
                Close();

            mForm.Dispose();
            mForm = null;
        }

        /// <summary>
        /// Gets or sets zoom factor
        /// </summary>
        public double ZoomFactor
        {
            get => Perform<double>(() => WebView.ZoomFactor);
            set => Perform(() => WebView.ZoomFactor = value);
        }

        /// <summary>
        /// Returns the flag indicating that the browser can go back
        /// </summary>
        public bool CanGoBack => Perform<bool>(() => WebView.CanGoBack);

        /// <summary>
        /// Returns the flag indicating that the browser can go forward
        /// </summary>
        public bool CanGoForward => Perform<bool>(() => WebView.CanGoForward);

        /// <summary>
        /// Makes the browser to go to the previous page in the history
        /// </summary>
        public void GoBack() => Perform(() => WebView.GoBack());

        /// <summary>
        /// Makes the browser to go to the next page in the history
        /// </summary>
        public void GoForward() => Perform(() => WebView.GoForward());

        /// <summary>
        /// Gets the list of the cookies
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        public IReadOnlyList<CoreWebView2Cookie> GetCookies(string uri)
        {
            if (WebView?.CoreWebView2 == null)
                throw new InvalidOperationException("The control is not initialized yet");

            var task = WebView.CoreWebView2.CookieManager.GetCookiesAsync(uri);
            task.Wait();
            return task.Result;
        }

        /// <summary>
        /// Deletes the specified cookie
        /// </summary>
        /// <param name="name"></param>
        /// <param name="uri"></param>
        public void DeleteCookie(string name, string uri)
        {
            if (WebView?.CoreWebView2 == null)
                throw new InvalidOperationException("The control is not initialized yet");

            WebView.CoreWebView2.CookieManager.DeleteCookies(name, uri);
        }

        public XPath XPath(string expression) => new XPath(this, expression);

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
