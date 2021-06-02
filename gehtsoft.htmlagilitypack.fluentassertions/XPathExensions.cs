namespace Gehtsoft.HtmlAgilityPack.FluentAssertions
{
    /// <summary>
    /// The extension class to create XPath assertions.
    /// </summary>
    public static class XPathExensions
    {
        /// <summary>
        /// Creates XPath assertions
        /// </summary>
        /// <param name="xpath"></param>
        /// <returns></returns>
        public static XPathAssertions Should(this XPath xpath) => new XPathAssertions(xpath);
    }
}
