using HtmlAgilityPack;

namespace Gehtsoft.HtmlAgilityPack.FluentAssertions
{
    public static class HtmlAgigilyPackExtensions
    {
        public static XPath Select(this HtmlNode document, string xpath) => new XPath(document, xpath);
        public static XPath SelectByName(this HtmlNode document, string tag, string name) => new XPath(document, $"//{tag}[@name='{name}']");
        public static XPath SelectByName(this HtmlNode document, string name) => new XPath(document, $"//*[@name='{name}']");
        public static XPath SelectById(this HtmlNode document, string id) => new XPath(document, $"//*[@id='{id}']");
        public static XPath SelectByClass(this HtmlNode document, string classname) => new XPath(document, $"//*[contains(concat(' ', @class, ' '), ' {classname} ')]");

        public static XPath Select(this HtmlDocument document, string xpath) => Select(document.DocumentNode, xpath);
        public static XPath SelectByName(this HtmlDocument document, string tag, string name) => SelectByName(document.DocumentNode, tag, name);
        public static XPath SelectByName(this HtmlDocument document, string name) => SelectByName(document.DocumentNode, name);
        public static XPath SelectById(this HtmlDocument document, string id) => SelectById(document.DocumentNode, id);
        public static XPath SelectByClass(this HtmlDocument document, string classname) => SelectByClass(document.DocumentNode, classname);
    }
}
