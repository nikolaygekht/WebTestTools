using System;
using System.Linq.Expressions;
using System.Security.Policy;
using FluentAssertions;
using FluentAssertions.Execution;
using FluentAssertions.Primitives;
using HtmlAgilityPack;

namespace Gehtsoft.HtmlAgilityPack.FluentAssertions
{
    public class XPathAssertions : ReferenceTypeAssertions<XPath, XPathAssertions>
    {
        public HtmlNode Node => Subject.Count > 0 ? Subject[0] : null;

        public XPathAssertions(XPath subject) : base(subject)
        {
        }

        protected override string Identifier => "xpath";

        public AndConstraint<XPathAssertions> Exist(string because = null, params object[] becauseParameters)
        {
            Execute.Assertion
                .BecauseOf(because, becauseParameters)
                .Given(() => Subject)
                .ForCondition(element => element.Count > 0)
                .FailWith("Expected {context:xpath} exists but it does not");
            return new AndConstraint<XPathAssertions>(this);
        }

        public AndConstraint<XPathAssertions> NotExist(string because = null, params object[] becauseParameters)
        {
            Execute.Assertion
                .BecauseOf(because, becauseParameters)
                .Given(() => Subject)
                .ForCondition(element => element.Count < 1)
                .FailWith("Expected {context:xpath} does not exist but it does");
            return new AndConstraint<XPathAssertions>(this);
        }

        public AndConstraint<XPathAssertions> HaveCount(int value, string because = null, params object[] becauseParameters)
        {
            Execute.Assertion
                .BecauseOf(because, becauseParameters)
                .Given(() => Subject)
                .ForCondition(element => element.Count == value)
                .FailWith("Expected {context:xpath} exists but it does not");
            return new AndConstraint<XPathAssertions>(this);
        }

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
