using Gehtsoft.Webview2.Uitest;

namespace Gehtsoft.Webview2.FluentAssertions
{
    /// <summary>
    /// The extension class to create XPath assertions
    /// </summary>
    public static class XPathExtension
    {
        /// <summary>
        /// Create XPath assertions
        /// </summary>
        /// <param name="xpath"></param>
        /// <returns></returns>
        public static XPathAssertions Should(this IXPath xpath) => new XPathAssertions(xpath);
    }
}
