using HtmlAgilityPack;

namespace Gehtsoft.HtmlAgilityPack.FluentAssertions
{
    public static class HtmlNodeExtensions
    {
        public static HtmlNodeAssertions Should(this HtmlNode node) => new HtmlNodeAssertions(node);
    }
}
