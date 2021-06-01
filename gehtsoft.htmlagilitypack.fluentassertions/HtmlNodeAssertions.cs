using System;
using System.Linq.Expressions;
using System.Reflection;
using FluentAssertions;
using FluentAssertions.Execution;
using FluentAssertions.Primitives;
using HtmlAgilityPack;

namespace Gehtsoft.HtmlAgilityPack.FluentAssertions
{
    public class HtmlNodeAssertions : ReferenceTypeAssertions<HtmlNode, HtmlNodeAssertions>
    {
        public HtmlNode Node => Subject;

        public HtmlNodeAssertions(HtmlNode subject) : base(subject)
        {
        }

        protected override string Identifier => "node";

        public AndConstraint<HtmlNodeAssertions> Exist(string because = null, params object[] becauseParameters)
        {
            Execute.Assertion
                .BecauseOf(because, becauseParameters)
                .Given(() => Subject)
                .ForCondition(element => element != null)
                .FailWith("Expected {context:node} to exist but it doesn't");
            return new AndConstraint<HtmlNodeAssertions>(this);
        }
        public AndConstraint<HtmlNodeAssertions> NotExist(string because = null, params object[] becauseParameters)
        {
            Execute.Assertion
                .BecauseOf(because, becauseParameters)
                .Given(() => Subject)
                .ForCondition(element => element == null)
                .FailWith("Expected {context:node} to exist but it doesn't");
            return new AndConstraint<HtmlNodeAssertions>(this);
        }

        public AndConstraint<HtmlNodeAssertions> BeElement(string because = null, params object[] becauseParameters)
        {
            Execute.Assertion
                .BecauseOf(because, becauseParameters)
                .Given(() => Subject)
                .ForCondition(element => element != null)
                .FailWith("Expected {context:node} to exists but it does not")
                .Then
                .ForCondition(element => element?.NodeType == HtmlNodeType.Element)
                .FailWith("Expected {context:node} to be an element but it is {0}", Subject.NodeType);
            return new AndConstraint<HtmlNodeAssertions>(this);
        }

        public AndConstraint<HtmlNodeAssertions> IsElement(string tag, string because = null, params object[] becauseParameters)
        {
            Execute.Assertion
                .BecauseOf(because, becauseParameters)
                .Given(() => Subject)
                .ForCondition(element => element != null)
                .FailWith("Expected {context:node} to exists but it does not")
                .Then
                .ForCondition(element => element.NodeType == HtmlNodeType.Element)
                .FailWith("Expected {context:node} to be an element but it is {0}", Subject.NodeType)
                .Then
                .ForCondition(element => element.Name?.Equals(tag) ?? false)
                .FailWith("Expected {context:node} to be an element name is {0} but it is {1}", Subject.Name);
            return new AndConstraint<HtmlNodeAssertions>(this);
        }

        public AndConstraint<HtmlNodeAssertions> IsText(string because = null, params object[] becauseParameters)
        {
            Execute.Assertion
                .BecauseOf(because, becauseParameters)
                .Given(() => Subject)
                .ForCondition(element => element?.NodeType == HtmlNodeType.Text)
                .FailWith("Expected {context:node} to be an element but it is {0}", Subject?.NodeType);
            return new AndConstraint<HtmlNodeAssertions>(this);
        }

        public AndConstraint<HtmlNodeAssertions> ContainsText(string text, StringComparison comparison = StringComparison.Ordinal, string because = null, params object[] becauseParameters)
        {
            Execute.Assertion
                .BecauseOf(because, becauseParameters)
                .Given(() => Subject)
                .ForCondition(element => element != null)
                .FailWith("Expected {context:node} to exists but it does not")
                .Then
                .ForCondition(element => element.NodeType == HtmlNodeType.Element)
                .FailWith("Expected {context:node} to be an element but it is {0}", Subject.NodeType)
                .Then
                .ForCondition(element => element.InnerText?.Contains(text, comparison) ?? false)
                .FailWith("Expected {context:node} to contain the text {0} but it does not", text);

            return new AndConstraint<HtmlNodeAssertions>(this);
        }

        public AndConstraint<HtmlNodeAssertions> ContainsHtml(string text, StringComparison comparison = StringComparison.Ordinal, string because = null, params object[] becauseParameters)
        {
            Execute.Assertion
                .BecauseOf(because, becauseParameters)
                .Given(() => Subject)
                .ForCondition(element => element != null)
                .FailWith("Expected {context:node} to exists but it does not")
                .Then
                .ForCondition(element => element.NodeType == HtmlNodeType.Element)
                .FailWith("Expected {context:node} to be an element but it is {0}", Subject.NodeType)
                .Then
                .ForCondition(element => element.InnerHtml?.Contains(text, comparison) ?? false)
                .FailWith("Expected {context:node} to contain the html {0} but it does not", text);

            return new AndConstraint<HtmlNodeAssertions>(this);
        }

        public AndConstraint<HtmlNodeAssertions> HaveAttribute(string name, string value = null, StringComparison comparison = StringComparison.Ordinal, string because = null, params object[] becauseParameters)
        {
            Execute.Assertion
                .BecauseOf(because, becauseParameters)
                .Given(() => Subject)
                .ForCondition(element => element != null)
                .FailWith("Expected {context:node} to exists but it does not")
                .Then
                .ForCondition(element => element.NodeType == HtmlNodeType.Element)
                .FailWith("Expected {context:node} to be an element but it is {0}", Subject.NodeType)
                .Then
                .ForCondition(element => element.Attributes != null && element.Attributes[name] != null)
                .FailWith("Expected {context:node} to have the attribute {0} but it does not", name)
                .Then
                .ForCondition(element => value == null || (element.Attributes[name]?.Value?.Equals(value, comparison) ?? false))
                .FailWith("Expected {context:node} to have the attribute {0} have value {1} but it has value {2}", name, value, Subject.Attributes[name]?.Value);

            return new AndConstraint<HtmlNodeAssertions>(this);
        }

        public AndConstraint<HtmlNodeAssertions> HaveNoAttribute(string name, string value = null, StringComparison comparison = StringComparison.Ordinal, string because = null, params object[] becauseParameters)
        {
            Execute.Assertion
                .BecauseOf(because, becauseParameters)
                .Given(() => Subject)
                .ForCondition(element => element != null)
                .FailWith("Expected {context:node} to exists but it does not")
                .Then
                .ForCondition(element => element.NodeType == HtmlNodeType.Element)
                .FailWith("Expected {context:node} to be an element but it is {0}", Subject.NodeType)
                .Then
                .ForCondition(element => value == null || element.Attributes[name] == null || !element.Attributes[name].Value.Equals(value, comparison))
                .FailWith("Expected {context:node} to have no attribute {0} with value {1} but it has", name, value);
            return new AndConstraint<HtmlNodeAssertions>(this);
        }

        public AndConstraint<HtmlNodeAssertions> HaveAttributeMatching(string name, Expression<Func<HtmlAttribute, bool>> predicate, string because = null, params object[] becauseParameters)
        {
            Execute.Assertion
                .BecauseOf(because, becauseParameters)
                .Given(() => Subject)
                .ForCondition(element => element != null)
                .FailWith("Expected {context:node} to exists but it does not")
                .Then
                .ForCondition(element => element.NodeType == HtmlNodeType.Element)
                .FailWith("Expected {context:node} to be an element but it is {0}", Subject.NodeType)
                .Then
                .ForCondition(element => element.Attributes != null && element.Attributes[name] != null)
                .FailWith("Expected {context:node} to have the attribute {0} but it does not", name)
                .Then
                .ForCondition(element => predicate.Compile()(element.Attributes[name]))
                .FailWith("Expected {context:node} to have the attribute {0} match predicate {1} but it does not", name, predicate);

            return new AndConstraint<HtmlNodeAssertions>(this);
        }

        new public AndConstraint<HtmlNodeAssertions> Match(Expression<Func<HtmlNode, bool>> predicate, string because = null, params object[] becauseParameters)
        {
            Execute.Assertion
                  .BecauseOf(because, becauseParameters)
                  .Given(() => Subject)
                  .ForCondition(element => predicate.Compile()(element))
                  .FailWith("Expected {context:node} to match predicate {0} but it does not", predicate);
            return new AndConstraint<HtmlNodeAssertions>(this);
        }

        public AndConstraint<HtmlNodeAssertions> NotMatch(Expression<Func<HtmlNode, bool>> predicate, string because = null, params object[] becauseParameters)
        {
            Execute.Assertion
                  .BecauseOf(because, becauseParameters)
                  .Given(() => Subject)
                  .ForCondition(element => !predicate.Compile()(element))
                  .FailWith("Expected {context:node} to not match predicate {0} but it does", predicate);
            return new AndConstraint<HtmlNodeAssertions>(this);
        }
    }
}
