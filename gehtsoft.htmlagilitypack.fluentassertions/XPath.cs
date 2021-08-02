using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace Gehtsoft.HtmlAgilityPack.FluentAssertions
{
    /// <summary>
    /// The XPath result
    /// </summary>
    public class XPath : IFormattable
    {
        private readonly HtmlNode mRoot;
        private readonly string mXPath;
        private readonly HtmlNode[] mNodes;


        /// <summary>
        /// <para>Returns the node from the node collection by its index.</para>
        /// <para>If index is out of range or the XPath does not return node collection, the property will return `null`</para>
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public HtmlNode this[int index] => mNodes == null || index < 0 || index >= mNodes.Length ? null : mNodes[index];

        /// <summary>
        /// Returns the number of the nodes in the collection.
        /// </summary>
        public int Count => mNodes?.Length ?? 0;

        /// <summary>
        /// <para>Returns the first node in the result.</para>
        /// <para>If XPath does not return node collection or returns an empty node collection, the method returns `null`.</para>
        /// </summary>
        public HtmlNode Node => Count < 1 ? null : this[0];

        /// <summary>
        /// Enumeration of all nodes
        /// </summary>
        public IEnumerable<HtmlNode> Nodes
        {
            get
            {
                for (int i = 0; i < Count; i++)
                    yield return this[i];
            }
        }

        internal XPath(HtmlNode document, string path)
        {
            mRoot = document;
            mXPath = path;
            var nodes = mRoot.SelectNodes(path);
            if (nodes == null)
                mNodes = null;
            else
                mNodes = nodes.ToArray();
        }

        /// <summary>Returns a string that represents the current object.</summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString() => mXPath;

        /// <summary>Formats the value of the current instance using the specified format.</summary>
        /// <param name="format">The format to use.
        ///  -or-
        ///  A null reference (<see langword="Nothing" /> in Visual Basic) to use the default format defined for the type of the <see cref="IFormattable" /> implementation.</param>
        /// <param name="formatProvider">The provider to use to format the value.
        ///  -or-
        ///  A null reference (<see langword="Nothing" /> in Visual Basic) to obtain the numeric format information from the current locale setting of the operating system.</param>
        /// <returns>The value of the current instance in the specified format.</returns>
        public string ToString(string format, IFormatProvider formatProvider) => ToString();
    }
}
