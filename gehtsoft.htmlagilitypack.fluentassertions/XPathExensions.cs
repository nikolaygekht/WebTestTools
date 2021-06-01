namespace Gehtsoft.HtmlAgilityPack.FluentAssertions
{
    public static class XPathExensions
    {
        public static XPathAssertions Should(this XPath xpath) => new XPathAssertions(xpath);
    }
}
