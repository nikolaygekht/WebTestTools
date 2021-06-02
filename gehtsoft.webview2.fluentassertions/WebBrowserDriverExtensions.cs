using Gehtsoft.Webview2.Uitest;

namespace Gehtsoft.Webview2.FluentAssertions
{
    /// <summary>
    /// The extension class to create assertions for the web driver.
    /// </summary>
    public static class WebBrowserDriverExtensions
    {
        /// <summary>
        /// Creates assertion for the web driver.
        /// </summary>
        /// <param name="driver"></param>
        /// <returns></returns>
        public static WebBrowserDriverAssertions Should(this WebBrowserDriver driver) => new WebBrowserDriverAssertions(driver);
    }
}

