using System;
using System.Text;
using HtmlAgilityPack;

namespace Gehtsoft.HtmlAgilityPack.FluentAssertions.Test
{
    public class TestPage
    {
        private readonly Lazy<string> mContent = new Lazy<string>(LoadContent);

        private readonly Lazy<HtmlDocument> mDocument;

        public string Content => mContent.Value;
        public HtmlDocument Document => mDocument.Value;

        public TestPage()
        {
            mDocument = new Lazy<HtmlDocument>(() =>
            {
                var doc = new HtmlDocument();
                doc.LoadHtml(Content);
                return doc;
            });
        }

        private static string LoadContent()
        {
            Type type = typeof(HtmlAgilityPackFluentAssertions);
            var assembly = type.Assembly;

            using var stream = assembly.GetManifestResourceStream($"{type.Namespace}.test.html");
            var content = new byte[stream.Length];
            stream.Read(content);
            return Encoding.UTF8.GetString(content);
        }
    }
}



