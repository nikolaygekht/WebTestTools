using System;
using System.Linq.Expressions;
using FluentAssertions;
using FluentAssertions.Execution;
using FluentAssertions.Primitives;
using Gehtsoft.Webview2.Uitest;

namespace Gehtsoft.Webview2.FluentAssertions
{
    public class XPathAssertions : ReferenceTypeAssertions<XPath, XPathAssertions>
    {
        protected override string Identifier => "xpath";

        public XPathAssertions(XPath subject) : base(subject)
        {
        }

        public AndConstraint<XPathAssertions> Be(string value, string because = null, params object[] becauseParameters)
        {
            Execute.Assertion
                .BecauseOf(because, becauseParameters)
                .Given(() => Subject)
                .ForCondition(xpath => xpath?.AsString == value)
                .FailWith("Expected {context:xpath} {0} return {1} but it returns {2}", (object)Subject ?? "null", value, Subject?.AsString);
            return new AndConstraint<XPathAssertions>(this);
        }

        public AndConstraint<XPathAssertions> NotBe(string value, string because = null, params object[] becauseParameters)
        {
            Execute.Assertion
                .BecauseOf(because, becauseParameters)
                .Given(() => Subject)
                .ForCondition(xpath => xpath?.AsString != value)
                .FailWith("Expected {context:xpath} {0} return not {1} but it returns {2}", (object)Subject ?? "null", value, Subject?.AsString);
            return new AndConstraint<XPathAssertions>(this);
        }

        public AndConstraint<XPathAssertions> Be(double value, string because = null, params object[] becauseParameters)
        {
            Execute.Assertion
                .BecauseOf(because, becauseParameters)
                .Given(() => Subject)
                .ForCondition(xpath => xpath?.AsNumber == value)
                .FailWith("Expected {context:xpath} {0} return {1} but it returns {2}", (object)Subject ?? "null", value, Subject?.AsNumber);
            return new AndConstraint<XPathAssertions>(this);
        }

        public AndConstraint<XPathAssertions> BeGreaterThan(double value, string because = null, params object[] becauseParameters)
        {
            Execute.Assertion
                .BecauseOf(because, becauseParameters)
                .Given(() => Subject)
                .ForCondition(xpath => xpath?.AsNumber > value)
                .FailWith("Expected {context:xpath} {0} return value greater than {1} but it returns {2}", (object)Subject ?? "null", value, Subject?.AsNumber);
            return new AndConstraint<XPathAssertions>(this);
        }

        public AndConstraint<XPathAssertions> BeLessThan(double value, string because = null, params object[] becauseParameters)
        {
            Execute.Assertion
                .BecauseOf(because, becauseParameters)
                .Given(() => Subject)
                .ForCondition(xpath => xpath?.AsNumber < value)
                .FailWith("Expected {context:xpath} {0} return value less than {1} but it returns {2}", (object)Subject ?? "null", value, Subject?.AsNumber);
            return new AndConstraint<XPathAssertions>(this);
        }

        public AndConstraint<XPathAssertions> BeGreaterThanOrEquals(double value, string because = null, params object[] becauseParameters)
        {
            Execute.Assertion
                .BecauseOf(because, becauseParameters)
                .Given(() => Subject)
                .ForCondition(xpath => xpath?.AsNumber >= value)
                .FailWith("Expected {context:xpath} {0} return value greater than or equal to {1} but it returns {2}", (object)Subject ?? "null", value, Subject?.AsNumber);
            return new AndConstraint<XPathAssertions>(this);
        }

        public AndConstraint<XPathAssertions> BeLessThanOrEquals(double value, string because = null, params object[] becauseParameters)
        {
            Execute.Assertion
                .BecauseOf(because, becauseParameters)
                .Given(() => Subject)
                .ForCondition(xpath => xpath?.AsNumber <= value)
                .FailWith("Expected {context:xpath} {0} return value less than or equal to {1} but it returns {2}", (object)Subject ?? "null", value, Subject?.AsNumber);
            return new AndConstraint<XPathAssertions>(this);
        }

        public AndConstraint<XPathAssertions> BeApproximately(double value, double delta, string because = null, params object[] becauseParameters)
        {
            Execute.Assertion
                .BecauseOf(because, becauseParameters)
                .Given(() => Subject)
                .ForCondition(xpath => Math.Abs(xpath?.AsNumber ?? Double.MaxValue - value) < delta)
                .FailWith("Expected {context:xpath} {0} return {1}+/-{3} but it returns {2}", (object)Subject ?? "null", value, Subject?.AsNumber, delta);
            return new AndConstraint<XPathAssertions>(this);
        }

        public AndConstraint<XPathAssertions> Be(int value, string because = null, params object[] becauseParameters)
        {
            Execute.Assertion
                .BecauseOf(because, becauseParameters)
                .Given(() => Subject)
                .ForCondition(xpath => Math.Abs(xpath?.AsNumber ?? Double.MinValue - value) < 0.1)
                .FailWith("Expected {context:xpath} {0} return {1} but it returns {2}", (object)Subject ?? "null", value, Subject?.AsNumber);

            return new AndConstraint<XPathAssertions>(this);
        }

        public AndConstraint<XPathAssertions> NotBe(int value, string because = null, params object[] becauseParameters)
        {
            Execute.Assertion
                .BecauseOf(because, becauseParameters)
                .Given(() => Subject)
                .ForCondition(xpath => Math.Abs(xpath?.AsNumber ?? Double.MinValue - value) > 0.1)
                .FailWith("Expected {context:xpath} {0} return not {1} but it returns {2}", (object)Subject ?? "null", value, Subject?.AsNumber);

            return new AndConstraint<XPathAssertions>(this);
        }

        public AndConstraint<XPathAssertions> Be(bool value, string because = null, params object[] becauseParameters)
        {
            Execute.Assertion
               .BecauseOf(because, becauseParameters)
               .Given(() => Subject)
               .ForCondition(xpath => xpath?.AsBoolean == value)
               .FailWith("Expected {context:xpath} {0} return {1} but it returns {2}", (object)Subject ?? "null", value, Subject?.AsBoolean);

            return new AndConstraint<XPathAssertions>(this);
        }

        public AndConstraint<XPathAssertions> NotBe(bool value, string because = null, params object[] becauseParameters)
        {
            Execute.Assertion
               .BecauseOf(because, becauseParameters)
               .Given(() => Subject)
               .ForCondition(xpath => xpath?.AsBoolean != value)
               .FailWith("Expected {context:xpath} {0} return not {1} but it returns {2}", (object)Subject ?? "null", value, Subject?.AsBoolean);

            return new AndConstraint<XPathAssertions>(this);
        }

        public AndConstraint<XPathAssertions> ReturnElement(string because = null, params object[] becauseParameters)
        {
            Execute.Assertion
               .BecauseOf(because, becauseParameters)
               .Given(() => Subject)
               .ForCondition(xpath => xpath.AsElement.Exists)
               .FailWith("Expected {context:xpath} {0} returns an element but it does not", (object)Subject ?? "null");

            return new AndConstraint<XPathAssertions>(this);
        }

        public AndConstraint<XPathAssertions> NotReturnElement(string because = null, params object[] becauseParameters)
        {
            Execute.Assertion
               .BecauseOf(because, becauseParameters)
               .Given(() => Subject)
               .ForCondition(xpath => !xpath.AsElement.Exists)
               .FailWith("Expected {context:xpath} {0} not return an element but it does", (object)Subject ?? "null");

            return new AndConstraint<XPathAssertions>(this);
        }

        public AndConstraint<XPathAssertions> BeTrue(string because = null, params object[] becauseParameters) => Be(true, because, becauseParameters);
        public AndConstraint<XPathAssertions> BeFalse(string because = null, params object[] becauseParameters) => Be(false, because, becauseParameters);

        new public AndConstraint<XPathAssertions> Match(Expression<Func<XPath, bool>> predicate, string because = null, params object[] becauseParameters)
        {
            Execute.Assertion
                .BecauseOf(because, becauseParameters)
                .Given(() => Subject)
                .ForCondition(element => predicate.Compile().Invoke(element))
                .FailWith("Expected {context:xpath} {0} matches the predicate {1} but it does not", (object)Subject ?? "null", predicate);
            return new AndConstraint<XPathAssertions>(this);
        }

        public AndConstraint<XPathAssertions> NotMatch(Expression<Func<XPath, bool>> predicate, string because = null, params object[] becauseParameters)
        {
            Execute.Assertion
                .BecauseOf(because, becauseParameters)
                .Given(() => Subject)
                .ForCondition(element => !predicate.Compile().Invoke(element))
                .FailWith("Expected {context:xpath} {0} not matches the predicate {1} but it does not", (object)Subject ?? "null", predicate);
            return new AndConstraint<XPathAssertions>(this);
        }
    }
}
