using Gehtsoft.Webview2.Uitest;

namespace Gehtsoft.Webview2.FluentAssertions
{
    public static class XPathExtension
    {
        public static XPathAssertions Should(this IXPath xpath) => new XPathAssertions(xpath);
    }
}
