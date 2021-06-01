using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace Gehtsoft.HtmlAgilityPack.FluentAssertions
{
    public class XPath : IFormattable
    {
        private readonly HtmlNode mRoot;
        private readonly string mXPath;
        private HtmlNode[] Nodes { get; }

        public HtmlNode this[int index] => Nodes == null || index < 0 || index >= Nodes.Length ? null : Nodes[index];

        public int Count => Nodes?.Length ?? 0;

        public HtmlNode Node => Count < 1 ? null : this[0];

        internal XPath(HtmlNode document, string path)
        {
            mRoot = document;
            mXPath = path;
            var nodes = mRoot.SelectNodes(path);
            if (nodes == null)
                Nodes = null;
            else
                Nodes = nodes.ToArray();
        }

        public override string ToString() => mXPath;

        public string ToString(string format, IFormatProvider formatProvider) => ToString();
    }
}
