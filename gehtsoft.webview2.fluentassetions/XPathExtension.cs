using Gehtsoft.Webview2.Uitest;

namespace Gehtsoft.Webview2.FluentAssertions
{
    public static class XPathExtension
    {
        public static XPathAssertions Should(this XPath xpath) => new XPathAssertions(xpath);
    }
}
