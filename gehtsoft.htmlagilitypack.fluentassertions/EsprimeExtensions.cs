using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using Esprima;
using Esprima.Ast;
using FluentAssertions.Primitives;
using HtmlAgilityPack;

namespace Gehtsoft.HtmlAgilityPack.FluentAssertions
{
    /// <summary>
    /// Extensions for ESPrime class
    /// </summary>
    public static class EsprimeExtensions
    {
        /// <summary>
        /// Compile specified text to JavaScript AST tree
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static Script AsScript(this string source)
        {
            var parser = new JavaScriptParser(source);
            return parser.ParseScript();
        }

        /// <summary>
        /// Compile node to JavaScript AST tree
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public static Script AsScript(this HtmlNode node) => AsScript(node.InnerText);

        /// <summary>
        /// Compile attribute value to JavaScript AST tree
        /// </summary>
        /// <param name="attribute"></param>
        /// <returns></returns>
        public static Script AsScript(this HtmlAttribute attribute) => AsScript(attribute.Value);

        /// <summary>
        /// Creates fluent assertion for the statement
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public static NodeAssertion Should(this Node node) => new NodeAssertion(node);

        /// <summary>
        /// Check whether the node is a node of the type specified
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="node"></param>
        /// <returns></returns>
        public static bool Is<T>(this Node node)
            where T : Node => node is T;

        /// <summary>
        /// Return the node as a node of the type specified
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="node"></param>
        /// <returns></returns>
        public static T As<T>(this Node node)
            where T : Node => node as T;

        /// <summary>
        /// Find a node in the whole subtree that matches the predicate specified
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public static Node Find(this Node parent, Func<Node, bool> predicate)
        {
            if (predicate(parent))
                return parent;

            foreach (var child in parent.ChildNodes)
            {
                if (child == null)
                    continue;

                var r = child.Find(predicate);
                if (r != null)
                    return r;
            }
            return null;
        }

        /// <summary>
        /// Finds nodes of the particular type that matches the predicate
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="parent"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>

        public static T Find<T>(this Node parent, Func<T, bool> predicate)
            where T : Node
        {
            if (parent is T t && predicate(t))
                return t;

            foreach (var child in parent.ChildNodes)
            {
                if (child == null)
                    continue;
                var r = child.Find<T>(predicate);
                if (r != null)
                    return r;
            }
            return null;
        }

        /// <summary>
        /// Finds nodes of the particular type that matches the predicate
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="parent"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>

        public static IEnumerable<T> FindAll<T>(this Node parent, Func<T, bool> predicate = null)
            where T : Node
        {
            if (predicate == null)
                predicate = _ => true;
            if (parent is T t && predicate(t))
                yield return t;

            foreach (var child in parent.ChildNodes)
            {
                if (child == null)
                    continue;
                foreach (var v in child.FindAll<T>(predicate))
                    yield return v;
            }
        }

        /// <summary>
        /// Checks whether the node contains any nodes that matches the predicate
        /// </summary>
        /// <param name="node"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>

        public static bool Contains(this Node node, Func<Node, bool> predicate) => Find(node, predicate) != null;

        /// <summary>
        /// Checks whether the node contains any nodes that matches the predicate
        /// <param name="node"></param>
        /// <param name="predicate"></param>
        /// </summary>
        public static bool Contains<T>(this Node node, Func<T, bool> predicate)
            where T : Node => Find(node, predicate) != null;

        /// <summary>
        /// Checks whether the node is the specified type and matches the predicate
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="node"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public static bool Is<T>(this Node node, Func<T, bool> predicate)
            where T : Node => node is T t && predicate(t);

        /// <summary>
        /// Checks whether the node is a literal or identifier
        /// </summary>
        /// <param name="node"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        public static bool Is(this Node node, string text)
        {
            if (node is Literal l)
                return l.Raw == text;
            else if (node is Identifier id)
                return id.Name == text;
            return false;
        }
    }
}
