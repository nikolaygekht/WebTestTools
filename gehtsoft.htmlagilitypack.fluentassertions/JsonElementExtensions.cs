using System.Text.Json;

namespace Gehtsoft.HtmlAgilityPack.FluentAssertions
{
    public static class JsonElementExtensions
    {
        public static JsonElementAssertions Should(this JsonElement element) => new JsonElementAssertions(element);
    }
}
