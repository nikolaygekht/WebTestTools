using System;
using System.Linq.Expressions;
using System.Security.Policy;
using FluentAssertions;
using FluentAssertions.Execution;
using FluentAssertions.Primitives;
using HtmlAgilityPack;

namespace Gehtsoft.HtmlAgilityPack.FluentAssertions
{
    /// <summary>
    /// Assertions for XPath object
    /// </summary>
    public class XPathAssertions : ReferenceTypeAssertions<XPath, XPathAssertions>
    {
        /// <summary>
        /// The link to the first node in the collection returned by the XPath
        /// </summary>
        public HtmlNode Node => Subject.Count > 0 ? Subject[0] : null;

        internal XPathAssertions(XPath subject) : base(subject)
        {
        }

        /// <summary>
        /// Subject identifier.
        /// </summary>
        protected override string Identifier => "xpath";

        /// <summary>
        /// Asserts that the XPath returns at least one element.
        /// </summary>
        /// <param name="because"></param>
        /// <param name="becauseParameters"></param>
        /// <returns></returns>
        public AndConstraint<XPathAssertions> Exist(string because = null, params object[] becauseParameters)
        {
            Execute.Assertion
                .BecauseOf(because, becauseParameters)
                .Given(() => Subject)
                .ForCondition(element => element.Count > 0)
                .FailWith("Expected {context:xpath} exists but it does not");
            return new AndConstraint<XPathAssertions>(this);
        }

        /// <summary>
        /// Asserts that the XPath does not return any elements.
        /// </summary>
        /// <param name="because"></param>
        /// <param name="becauseParameters"></param>
        /// <returns></returns>
        public AndConstraint<XPathAssertions> NotExist(string because = null, params object[] becauseParameters)
        {
            Execute.Assertion
                .BecauseOf(because, becauseParameters)
                .Given(() => Subject)
                .ForCondition(element => element.Count < 1)
                .FailWith("Expected {context:xpath} does not exist but it does");
            return new AndConstraint<XPathAssertions>(this);
        }

        /// <summary>
        /// Asserts that XPath returns the specified number of the elements.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="because"></param>
        /// <param name="becauseParameters"></param>
        /// <returns></returns>
        public AndConstraint<XPathAssertions> HaveCount(int value, string because = null, params object[] becauseParameters)
        {
            Execute.Assertion
                .BecauseOf(because, becauseParameters)
                .Given(() => Subject)
                .ForCondition(element => element.Count == value)
                .FailWith("Expected {context:xpath} exists but it does not");
            return new AndConstraint<XPathAssertions>(this);
        }

        /// <summary>
        /// Asserts that the XPath results contains a node that matches the predicate specified.
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="because"></param>
        /// <param name="becauseParameters"></param>
        /// <returns></returns>
        public AndConstraint<XPathAssertions> ContainNodeMatching(Expression<Func<HtmlNode, bool>> predicate, string because = null, params object[] becauseParameters)
        {
            Execute.Assertion
                .BecauseOf(because, becauseParameters)
                .Given(() => Subject)
                .ForCondition(element =>
                {
                    var f = predicate.Compile();
                    for (int i = 0; i < element.Count; i++)
                        if (f(element[i]))
                            return true;
                    return false;
                })
                .FailWith("Expected {context:xpath} to contain a node matching {0} but it does not", predicate);
            return new AndConstraint<XPathAssertions>(this);
        }

        /// <summary>
        /// Asserts that all nodes returned by XPath matches the predicate.
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="because"></param>
        /// <param name="becauseParameters"></param>
        /// <returns></returns>
        public AndConstraint<XPathAssertions> ContainAllNodesMatching(Expression<Func<HtmlNode, bool>> predicate, string because = null, params object[] becauseParameters)
        {
            Execute.Assertion
                .BecauseOf(because, becauseParameters)
                .Given(() => Subject)
                .ForCondition(element =>
                {
                    var f = predicate.Compile();
                    for (int i = 0; i < element.Count; i++)
                        if (!f(element[i]))
                            return false;
                    return true;
                })
                .FailWith("Expected {context:xpath} to contain all nodes matching {0} but it does not", predicate);
            return new AndConstraint<XPathAssertions>(this);
        }

        /// <summary>
        /// Asserts that all no nodes returned by XPath matches the predicate.
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="because"></param>
        /// <param name="becauseParameters"></param>
        /// <returns></returns>
        public AndConstraint<XPathAssertions> ContainNoNodesMatching(Expression<Func<HtmlNode, bool>> predicate, string because = null, params object[] becauseParameters)
        {
            Execute.Assertion
                .BecauseOf(because, becauseParameters)
                .Given(() => Subject)
                .ForCondition(element =>
                {
                    var f = predicate.Compile();
                    for (int i = 0; i < element.Count; i++)
                        if (f(element[i]))
                            return false;
                    return true;
                })
                .FailWith("Expected {context:xpath} to contain no nodes matching {0} but it does", predicate);
            return new AndConstraint<XPathAssertions>(this);
        }
    }
}
