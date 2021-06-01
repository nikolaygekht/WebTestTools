using Gehtsoft.Webview2.Uitest;

namespace Gehtsoft.Webview2.FluentAssertions
{
    public static class WebBrowserDriverExtensions
    {
        public static WebBrowserDriverAssertions Should(this WebBrowserDriver driver) => new WebBrowserDriverAssertions(driver);
    }
}

