using HtmlAgilityPack;

namespace Gehtsoft.HtmlAgilityPack.FluentAssertions
{
    /// <summary>
    /// The extension to HtmlNode class to create the assertions.
    /// </summary>
    public static class HtmlNodeExtensions
    {
        /// <summary>
        /// Create assertions to the node.
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public static HtmlNodeAssertions Should(this HtmlNode node) => new HtmlNodeAssertions(node);
    }
}
