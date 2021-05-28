using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Web.WebView2.Core;
using Microsoft.Web.WebView2.WinForms;

namespace webviewtest
{
    /// <summary>
    /// A form with web browser. 
    /// 
    /// Don't create the form directly, use <see cref="WebBrowserDriver"/> class instead.
    /// </summary>
    public partial class WebBrowserForm : Form
    {
        /// <summary>
        /// A web view object
        /// </summary>
        public WebView2 WebView => webViewControl;

        /// <summary>
        /// The flag indicating that the form initialization is completed.
        /// </summary>
        public bool LoadingCompled { get; private set; } = false;

        /// <summary>
        /// The flag indicating that the last initiated navigation operation is completed.
        /// </summary>
        public bool NavigationCompleted { get; private set; } = false;

        /// <summary>
        /// Constructor
        /// </summary>
        public WebBrowserForm()
        {
            InitializeComponent();
            webViewControl.EnsureCoreWebView2Async();
        }

        /// <summary>
        /// Starts navigation to the URL specified
        /// </summary>
        /// <param name="url"></param>
        public void NavigateTo(string url)
        {
            NavigationCompleted = false;
            webViewControl.CoreWebView2.Navigate(url);

        }

        /// <summary>
        /// Sets the browser content to HTML specified
        /// </summary>
        /// <param name="html"></param>
        public void SetContent(string html)
        {
            NavigationCompleted = false;
            webViewControl.CoreWebView2.NavigateToString(html);
        }

        private void webViewControl_CoreWebView2InitializationCompleted(object sender, CoreWebView2InitializationCompletedEventArgs e)
        {
            LoadingCompled = true;
        }

        private void webViewControl_NavigationCompleted(object sender, CoreWebView2NavigationCompletedEventArgs e)
        {
            NavigationCompleted = true;
        }

        private void webViewControl_NavigationStarting(object sender, CoreWebView2NavigationStartingEventArgs e)
        {
            NavigationCompleted = false;
        }
    }
}
