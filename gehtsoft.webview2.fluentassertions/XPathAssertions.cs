using System;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using FluentAssertions;
using FluentAssertions.Execution;
using FluentAssertions.Primitives;
using Gehtsoft.Webview2.Uitest;

namespace Gehtsoft.Webview2.FluentAssertions
{
    /// <summary>
    /// The assertions for <see cref="IXPath"/> objects.
    /// </summary>
    public class XPathAssertions : ReferenceTypeAssertions<IXPath, XPathAssertions>
    {
        /// <summary>
        /// Identifier.
        /// </summary>
        protected override string Identifier => "xpath";

        internal XPathAssertions(IXPath subject) : base(subject)
        {
        }

        /// <summary>
        /// Asserts that the xpath value is equal to the specified string value.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="because"></param>
        /// <param name="becauseParameters"></param>
        /// <returns></returns>
        public AndConstraint<XPathAssertions> Be(string value, string because = null, params object[] becauseParameters)
        {
            Execute.Assertion
                .BecauseOf(because, becauseParameters)
                .Given(() => Subject)
                .ForCondition(xpath => xpath?.AsString == value)
                .FailWith("Expected {context:xpath} {0} return {1} but it returns {2}", (object)Subject ?? "null", value, Subject?.AsString);
            return new AndConstraint<XPathAssertions>(this);
        }

        /// <summary>
        /// Asserts that the XPath value is not equal to the specified string value.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="because"></param>
        /// <param name="becauseParameters"></param>
        /// <returns></returns>
        public AndConstraint<XPathAssertions> NotBe(string value, string because = null, params object[] becauseParameters)
        {
            Execute.Assertion
                .BecauseOf(because, becauseParameters)
                .Given(() => Subject)
                .ForCondition(xpath => xpath?.AsString != value)
                .FailWith("Expected {context:xpath} {0} return not {1} but it returns {2}", (object)Subject ?? "null", value, Subject?.AsString);
            return new AndConstraint<XPathAssertions>(this);
        }

        /// <summary>
        /// Asserts that the XPath value is equal to the specified numeric value.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="because"></param>
        /// <param name="becauseParameters"></param>
        /// <returns></returns>
        public AndConstraint<XPathAssertions> Be(double value, string because = null, params object[] becauseParameters)
        {
            Execute.Assertion
                .BecauseOf(because, becauseParameters)
                .Given(() => Subject)
                .ForCondition(xpath => xpath?.AsNumber == value)
                .FailWith("Expected {context:xpath} {0} return {1} but it returns {2}", (object)Subject ?? "null", value, Subject?.AsNumber);
            return new AndConstraint<XPathAssertions>(this);
        }

        /// <summary>
        /// Asserts that the XPath value is not equal to the specified numeric value.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="because"></param>
        /// <param name="becauseParameters"></param>
        /// <returns></returns>
        public AndConstraint<XPathAssertions> NotBe(double value, string because = null, params object[] becauseParameters)
        {
            Execute.Assertion
                .BecauseOf(because, becauseParameters)
                .Given(() => Subject)
                .ForCondition(xpath => xpath?.AsNumber != value)
                .FailWith("Expected {context:xpath} {0} return {1} but it returns {2}", (object)Subject ?? "null", value, Subject?.AsNumber);
            return new AndConstraint<XPathAssertions>(this);
        }

        /// <summary>
        /// Asserts that XPath value is greater than the specified numeric value.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="because"></param>
        /// <param name="becauseParameters"></param>
        /// <returns></returns>
        public AndConstraint<XPathAssertions> BeGreaterThan(double value, string because = null, params object[] becauseParameters)
        {
            Execute.Assertion
                .BecauseOf(because, becauseParameters)
                .Given(() => Subject)
                .ForCondition(xpath => xpath?.AsNumber > value)
                .FailWith("Expected {context:xpath} {0} return value greater than {1} but it returns {2}", (object)Subject ?? "null", value, Subject?.AsNumber);
            return new AndConstraint<XPathAssertions>(this);
        }

        /// <summary>
        /// Asserts that XPath value is less than the specified numeric value.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="because"></param>
        /// <param name="becauseParameters"></param>
        /// <returns></returns>
        public AndConstraint<XPathAssertions> BeLessThan(double value, string because = null, params object[] becauseParameters)
        {
            Execute.Assertion
                .BecauseOf(because, becauseParameters)
                .Given(() => Subject)
                .ForCondition(xpath => xpath?.AsNumber < value)
                .FailWith("Expected {context:xpath} {0} return value less than {1} but it returns {2}", (object)Subject ?? "null", value, Subject?.AsNumber);
            return new AndConstraint<XPathAssertions>(this);
        }

        /// <summary>
        /// Asserts that XPath value is greater than or equals to the specified numeric value.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="because"></param>
        /// <param name="becauseParameters"></param>
        /// <returns></returns>
        public AndConstraint<XPathAssertions> BeGreaterThanOrEquals(double value, string because = null, params object[] becauseParameters)
        {
            Execute.Assertion
                .BecauseOf(because, becauseParameters)
                .Given(() => Subject)
                .ForCondition(xpath => xpath?.AsNumber >= value)
                .FailWith("Expected {context:xpath} {0} return value greater than or equal to {1} but it returns {2}", (object)Subject ?? "null", value, Subject?.AsNumber);
            return new AndConstraint<XPathAssertions>(this);
        }

        /// <summary>
        /// Asserts that XPath value is less than or equals to the specified numeric value.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="because"></param>
        /// <param name="becauseParameters"></param>
        /// <returns></returns>
        public AndConstraint<XPathAssertions> BeLessThanOrEquals(double value, string because = null, params object[] becauseParameters)
        {
            Execute.Assertion
                .BecauseOf(because, becauseParameters)
                .Given(() => Subject)
                .ForCondition(xpath => xpath?.AsNumber <= value)
                .FailWith("Expected {context:xpath} {0} return value less than or equal to {1} but it returns {2}", (object)Subject ?? "null", value, Subject?.AsNumber);
            return new AndConstraint<XPathAssertions>(this);
        }

        /// <summary>
        /// Asserts that XPath value is within the specified range to the specified numeric value.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="delta"></param>
        /// <param name="because"></param>
        /// <param name="becauseParameters"></param>
        /// <returns></returns>
        public AndConstraint<XPathAssertions> BeApproximately(double value, double delta, string because = null, params object[] becauseParameters)
        {
            Execute.Assertion
                .BecauseOf(because, becauseParameters)
                .Given(() => Subject)
                .ForCondition(xpath => Math.Abs((xpath?.AsNumber ?? Double.MaxValue) - value) <= delta)
                .FailWith("Expected {context:xpath} {0} return {1}+/-{3} but it returns {2}", (object)Subject ?? "null", value, Subject?.AsNumber, delta);
            return new AndConstraint<XPathAssertions>(this);
        }

        /// <summary>
        /// Asserts that XPath value is not within the specified range to the specified numeric value.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="delta"></param>
        /// <param name="because"></param>
        /// <param name="becauseParameters"></param>
        /// <returns></returns>
        public AndConstraint<XPathAssertions> NotBeApproximately(double value, double delta, string because = null, params object[] becauseParameters)
        {
            Execute.Assertion
                .BecauseOf(because, becauseParameters)
                .Given(() => Subject)
                .ForCondition(xpath => Math.Abs((xpath?.AsNumber ?? Double.MaxValue) - value) > delta)
                .FailWith("Expected {context:xpath} {0} not return {1}+/-{3} but it returns {2}", (object)Subject ?? "null", value, Subject?.AsNumber, delta);
            return new AndConstraint<XPathAssertions>(this);
        }

        /// <summary>
        /// Asserts that XPath value is within the 1e-3 range to the specified integer value.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="because"></param>
        /// <param name="becauseParameters"></param>
        /// <returns></returns>
        public AndConstraint<XPathAssertions> Be(int value, string because = null, params object[] becauseParameters) => BeApproximately(value, 0.001, because, becauseParameters);

        /// <summary>
        /// Asserts that XPath value is not the 1e-3 range to the specified integer value.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="because"></param>
        /// <param name="becauseParameters"></param>
        /// <returns></returns>
        public AndConstraint<XPathAssertions> NotBe(int value, string because = null, params object[] becauseParameters) => NotBeApproximately(value, 0.001, because, becauseParameters);

        /// <summary>
        /// Asserts that XPath value is the specified boolean value
        /// </summary>
        /// <param name="value"></param>
        /// <param name="because"></param>
        /// <param name="becauseParameters"></param>
        /// <returns></returns>
        public AndConstraint<XPathAssertions> Be(bool value, string because = null, params object[] becauseParameters)
        {
            Execute.Assertion
               .BecauseOf(because, becauseParameters)
               .Given(() => Subject)
               .ForCondition(xpath => xpath?.AsBoolean == value)
               .FailWith("Expected {context:xpath} {0} return {1} but it returns {2}", (object)Subject ?? "null", value, Subject?.AsBoolean);

            return new AndConstraint<XPathAssertions>(this);
        }

        /// <summary>
        /// Asserts that XPath value is not the specified boolean value.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="because"></param>
        /// <param name="becauseParameters"></param>
        /// <returns></returns>
        public AndConstraint<XPathAssertions> NotBe(bool value, string because = null, params object[] becauseParameters)
        {
            Execute.Assertion
               .BecauseOf(because, becauseParameters)
               .Given(() => Subject)
               .ForCondition(xpath => xpath?.AsBoolean != value)
               .FailWith("Expected {context:xpath} {0} return not {1} but it returns {2}", (object)Subject ?? "null", value, Subject?.AsBoolean);

            return new AndConstraint<XPathAssertions>(this);
        }

        /// <summary>
        /// Asserts that the XPAth returns an element or elements collection.
        /// </summary>
        /// <param name="because"></param>
        /// <param name="becauseParameters"></param>
        /// <returns></returns>
        public AndConstraint<XPathAssertions> ReturnElement(string because = null, params object[] becauseParameters)
        {
            Execute.Assertion
               .BecauseOf(because, becauseParameters)
               .Given(() => Subject)
               .ForCondition(xpath => xpath?.AsElement?.Exists == true)
               .FailWith("Expected {context:xpath} {0} returns an xpath but it does not", (object)Subject ?? "null");

            return new AndConstraint<XPathAssertions>(this);
        }

        /// <summary>
        /// Asserts that the XPAth does not return an element or elements collection.
        /// </summary>
        /// <param name="because"></param>
        /// <param name="becauseParameters"></param>
        /// <returns></returns>
        public AndConstraint<XPathAssertions> NotReturnElement(string because = null, params object[] becauseParameters)
        {
            Execute.Assertion
               .BecauseOf(because, becauseParameters)
               .Given(() => Subject)
               .ForCondition(xpath => xpath?.AsElement?.Exists != true)
               .FailWith("Expected {context:xpath} {0} not return an xpath but it does", (object)Subject ?? "null");

            return new AndConstraint<XPathAssertions>(this);
        }

        /// <summary>
        /// Asserts that the XPath return a boolean value equals to `true`
        /// </summary>
        /// <param name="because"></param>
        /// <param name="becauseParameters"></param>
        /// <returns></returns>
        public AndConstraint<XPathAssertions> BeTrue(string because = null, params object[] becauseParameters) => Be(true, because, becauseParameters);

        /// <summary>
        /// Asserts that the XPath return a boolean value equals to `false`
        /// </summary>
        /// <param name="because"></param>
        /// <param name="becauseParameters"></param>
        /// <returns></returns>
        public AndConstraint<XPathAssertions> BeFalse(string because = null, params object[] becauseParameters) => Be(false, because, becauseParameters);

        /// <summary>
        /// Asserts that XPath matches the predicate
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="because"></param>
        /// <param name="becauseParameters"></param>
        /// <returns></returns>
        new public AndConstraint<XPathAssertions> Match(Expression<Func<IXPath, bool>> predicate, string because = null, params object[] becauseParameters)
        {
            Execute.Assertion
                .BecauseOf(because, becauseParameters)
                .Given(() => Subject)
                .ForCondition(xpath => predicate.Compile().Invoke(xpath))
                .FailWith("Expected {context:xpath} {0} matches the predicate {1} but it does not", (object)Subject ?? "null", predicate);
            return new AndConstraint<XPathAssertions>(this);
        }

        /// <summary>
        /// Asserts that XPath does not match the predicate.
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="because"></param>
        /// <param name="becauseParameters"></param>
        /// <returns></returns>
        public AndConstraint<XPathAssertions> NotMatch(Expression<Func<IXPath, bool>> predicate, string because = null, params object[] becauseParameters)
        {
            Execute.Assertion
                .BecauseOf(because, becauseParameters)
                .Given(() => Subject)
                .ForCondition(xpath => !predicate.Compile().Invoke(xpath))
                .FailWith("Expected {context:xpath} {0} not matches the predicate {1} but it does not", (object)Subject ?? "null", predicate);
            return new AndConstraint<XPathAssertions>(this);
        }
    }
}

