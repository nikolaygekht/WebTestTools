using HtmlAgilityPack;

namespace Gehtsoft.HtmlAgilityPack.FluentAssertions
{
    /// <summary>
    /// The extension methods that simplifies HtmlAgigilyPack operations
    /// </summary>
    public static class HtmlAgigilyPackExtensions
    {
        /// <summary>
        /// Select children node(s) using XPath.
        /// </summary>
        /// <param name="document"></param>
        /// <param name="xpath"></param>
        /// <returns></returns>
        public static XPath Select(this HtmlNode document, string xpath) => new XPath(document, xpath);

        /// <summary>
        /// Select children node(s) by the value of the name attribute and the element tag
        /// </summary>
        /// <param name="document"></param>
        /// <param name="tag"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static XPath SelectByName(this HtmlNode document, string tag, string name) => new XPath(document, $"//{tag}[@name='{name}']");

        /// <summary>
        /// Select node(s) from the document by the value of the name attribute
        /// </summary>
        /// <param name="document"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static XPath SelectByName(this HtmlNode document, string name) => new XPath(document, $"//*[@name='{name}']");

        /// <summary>
        /// Select node(s) from the document by the value of the id attribute
        /// </summary>
        /// <param name="document"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static XPath SelectById(this HtmlNode document, string id) => new XPath(document, $"//*[@id='{id}']");

        /// <summary>
        /// Select node(s) from the document by the value of the class attribute
        /// </summary>
        /// <param name="document"></param>
        /// <param name="classname"></param>
        /// <returns></returns>
        public static XPath SelectByClass(this HtmlNode document, string classname) => new XPath(document, $"//*[contains(concat(' ', @class, ' '), ' {classname} ')]");

        /// <summary>
        /// Select node(s) from the document using XPath.
        /// </summary>
        /// <param name="document"></param>
        /// <param name="xpath"></param>
        /// <returns></returns>
        public static XPath Select(this HtmlDocument document, string xpath) => Select(document.DocumentNode, xpath);

        /// <summary>
        /// Select node(s) from the document by the value of the name attribute and the element tag
        /// </summary>
        /// <param name="document"></param>
        /// <param name="tag"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static XPath SelectByName(this HtmlDocument document, string tag, string name) => SelectByName(document.DocumentNode, tag, name);

        /// <summary>
        /// Select node(s) from the document by the value of the name attribute
        /// </summary>
        /// <param name="document"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static XPath SelectByName(this HtmlDocument document, string name) => SelectByName(document.DocumentNode, name);

        /// <summary>
        /// Select node(s) from the document by the value of the id attribute
        /// </summary>
        /// <param name="document"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static XPath SelectById(this HtmlDocument document, string id) => SelectById(document.DocumentNode, id);

        /// <summary>
        /// Select node(s) from the document by the value of the class attribute
        /// </summary>
        /// <param name="document"></param>
        /// <param name="classname"></param>
        /// <returns></returns>
        public static XPath SelectByClass(this HtmlDocument document, string classname) => SelectByClass(document.DocumentNode, classname);
    }
}
