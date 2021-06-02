using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using FluentAssertions.Execution;
using FluentAssertions.Primitives;
using Gehtsoft.Webview2.Uitest;

namespace Gehtsoft.Webview2.FluentAssertions
{
    /// <summary>
    /// The fluent assertions support for <see cref="IElement" /> class
    /// </summary>
    public class ElementAssertions : ReferenceTypeAssertions<IElement, ElementAssertions>
    {
        /// <summary>
        /// The subject identifier.
        /// </summary>
        protected override string Identifier => "element";

        internal ElementAssertions(IElement subject) : base(subject)
        {
        }

        /// <summary>
        /// Asserts that at least one of the element with such parameters exist
        /// </summary>
        /// <param name="because"></param>
        /// <param name="becauseParameters"></param>
        /// <returns></returns>
        public AndConstraint<ElementAssertions> Exist(string because = null, params object[] becauseParameters)
        {
            Execute.Assertion
                .BecauseOf(because, becauseParameters)
                .Given(() => Subject)
                .ForCondition(element => element.Exists)
                .FailWith("Expected {context:element} {0} exists but it does not", (object)Subject ?? "null");
            return new AndConstraint<ElementAssertions>(this);
        }

        /// <summary>
        /// Asserts that no elements with such parameters exist
        /// </summary>
        /// <param name="because"></param>
        /// <param name="becauseParameters"></param>
        /// <returns></returns>
        public AndConstraint<ElementAssertions> NotExist(string because = null, params object[] becauseParameters)
        {
            Execute.Assertion
                .BecauseOf(because, becauseParameters)
                .Given(() => Subject)
                .ForCondition(element => !element.Exists)
                .FailWith("Expected {context:element} {0} not exist but it does", (object)Subject ?? "null");
            return new AndConstraint<ElementAssertions>(this);
        }

        /// <summary>
        /// Asserts that the element has the value specified
        /// </summary>
        /// <param name="value"></param>
        /// <param name="because"></param>
        /// <param name="becauseParameters"></param>
        /// <returns></returns>
        public AndConstraint<ElementAssertions> HaveValue(string value, string because = null, params object[] becauseParameters)
        {
            Execute.Assertion
                .BecauseOf(because, becauseParameters)
                .Given(() => Subject)
                .ForCondition(element => element.Exists)
                .FailWith("Expected {context:element} {0} exists but it does not", (object)Subject ?? "null")
                .Then
                .ForCondition(element => element.Value == value)
                .FailWith("Expected {context:element} {0} have value {1} but it has value {2}", (object)Subject ?? "null", value, Subject.Value);
            return new AndConstraint<ElementAssertions>(this);
        }

        /// <summary>
        /// Asserts the element does not have the value specified
        /// </summary>
        /// <param name="value"></param>
        /// <param name="because"></param>
        /// <param name="becauseParameters"></param>
        /// <returns></returns>
        public AndConstraint<ElementAssertions> NotHaveValue(string value, string because = null, params object[] becauseParameters)
        {
            Execute.Assertion
                .BecauseOf(because, becauseParameters)
                .Given(() => Subject)
                .ForCondition(element => element.Exists)
                .FailWith("Expected {context:element} {0} exists but it does not", (object)Subject ?? "null")
                .Then
                .ForCondition(element => element.Value != value)
                .FailWith("Expected {context:element} {0} have value {1} but it has value {2}", (object)Subject ?? "null", value, Subject.Value);
            return new AndConstraint<ElementAssertions>(this);
        }

        /// <summary>
        /// Asserts that the element has the class specified
        /// </summary>
        /// <param name="value"></param>
        /// <param name="because"></param>
        /// <param name="becauseParameters"></param>
        /// <returns></returns>
        public AndConstraint<ElementAssertions> HaveClass(string value, string because = null, params object[] becauseParameters)
        {
            Execute.Assertion
                .BecauseOf(because, becauseParameters)
                .Given(() => Subject)
                .ForCondition(element => element.Exists)
                .FailWith("Expected {context:element} {0} exists but it does not", (object)Subject ?? "null")
                .Then
                .ForCondition(element => element.Class?.Split(new char[] { ' ' })?.Any(e => e == value) ?? false)
                .FailWith("Expected {context:element} {0} have class {1} but it has classes {2}", (object)Subject ?? "null", value, Subject.Class);
            return new AndConstraint<ElementAssertions>(this);
        }

        /// <summary>
        /// Asserts that the element does not have the class specified.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="because"></param>
        /// <param name="becauseParameters"></param>
        /// <returns></returns>
        public AndConstraint<ElementAssertions> NotHaveClass(string value, string because = null, params object[] becauseParameters)
        {
            Execute.Assertion
                .BecauseOf(because, becauseParameters)
                .Given(() => Subject)
                .ForCondition(element => element.Exists)
                .FailWith("Expected {context:element} {0} exists but it does not", (object)Subject ?? "null")
                .Then
                .ForCondition(element => !(element.Class?.Split(new char[] { ' ' })?.Any(e => e == value) ?? false))
                .FailWith("Expected {context:element} {0} not have class {1} but it has classes {2}", (object)Subject ?? "null", value, Subject?.Class);
            return new AndConstraint<ElementAssertions>(this);
        }

        /// <summary>
        /// Asserts that the element Checked state is equal to the value specified.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="because"></param>
        /// <param name="becauseParameters"></param>
        /// <returns></returns>
        public AndConstraint<ElementAssertions> IsChecked(bool? value = true, string because = null, params object[] becauseParameters)
        {
            Execute.Assertion
                .BecauseOf(because, becauseParameters)
                .Given(() => Subject)
                .ForCondition(element => element.Exists)
                .FailWith("Expected {context:element} {0} exists but it does not", (object)Subject ?? "null")
                .Then
                .ForCondition(element => element.Checked == value)
                .FailWith("Expected {context:element} {0} have checked property to be {1} but it has value {2}", (object)Subject ?? "null", value, Subject.Checked);
            return new AndConstraint<ElementAssertions>(this);
        }

        /// <summary>
        /// <para>Asserts that the element has JavaScript property value specified. </para>
        /// <para>Please don't confuse the properties (in JavaScript) and the element attributes (in HTML)</para>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="propertyName"></param>
        /// <param name="value"></param>
        /// <param name="because"></param>
        /// <param name="becauseParameters"></param>
        /// <returns></returns>
        public AndConstraint<ElementAssertions> HaveProperty<T>(string propertyName, T value, string because = null, params object[] becauseParameters)
        {
            Execute.Assertion
                .BecauseOf(because, becauseParameters)
                .Given(() => Subject)
                .ForCondition(element => element.Exists)
                .FailWith("Expected {context:element} {0} exists but it does not", (object)Subject ?? "null")
                .Then
                .ForCondition(element => element.GetProperty<T>(propertyName).Equals(value))
                .FailWith("Expected {context:element} {0} have property {3} value {1} but it has value {2}", (object)Subject ?? "null", value, Subject.GetProperty<T>(propertyName), propertyName);
            return new AndConstraint<ElementAssertions>(this);
        }

        /// <summary>
        /// <para>Asserts that the element does not have JavaScript property value specified. </para>
        /// <para>Please don't confuse the properties (in JavaScript) and the element attributes (in HTML)</para>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="propertyName"></param>
        /// <param name="value"></param>
        /// <param name="because"></param>
        /// <param name="becauseParameters"></param>
        /// <returns></returns>
        public AndConstraint<ElementAssertions> NotHaveProperty<T>(string propertyName, T value, string because = null, params object[] becauseParameters)
        {
            Execute.Assertion
                .BecauseOf(because, becauseParameters)
                .Given(() => Subject)
                .ForCondition(element => element.Exists)
                .FailWith("Expected {context:element} {0} exists but it does not", (object)Subject ?? "null")
                .Then
                .ForCondition(element => !element.GetProperty<T>(propertyName).Equals(value))
                .FailWith("Expected {context:element} {0} have property {3} value {1} but it has value {2}", (object)Subject ?? "null", value, Subject.GetProperty<T>(propertyName), propertyName);
            return new AndConstraint<ElementAssertions>(this);
        }

        /// <summary>
        /// Asserts that the element has the attribute specified.
        /// </summary>
        /// <param name="attributeName"></param>
        /// <param name="value">The value that attribute should have or `null` if only attribute presence should be asserted</param>
        /// <param name="comparison"></param>
        /// <param name="because"></param>
        /// <param name="becauseParameters"></param>
        /// <returns></returns>
        public AndConstraint<ElementAssertions> HaveAttribute(string attributeName, string value = null, StringComparison comparison = StringComparison.Ordinal, string because = null, params object[] becauseParameters)
        {
            Execute.Assertion
                .BecauseOf(because, becauseParameters)
                .Given(() => Subject)
                .ForCondition(element => element.Exists)
                .FailWith("Expected {context:element} {0} exists but it does not", (object)Subject ?? "null")
                .Then
                .ForCondition(element => element.GetAttribute(attributeName) != null)
                .FailWith("Expected {context:element} {0} has the attribute {1}", (object)Subject ?? "null", attributeName)
                .Then
                .ForCondition(element => value == null || value.Equals(element.GetAttribute(attributeName), comparison))
                .FailWith("Expected {context:element} {0} have attribute {3} value {1} but it has value {2}", (object)Subject ?? "null", value, Subject.GetAttribute(attributeName), attributeName);
            return new AndConstraint<ElementAssertions>(this);
        }

        /// <summary>
        /// Checks whether the element does not have the attribute
        /// </summary>
        /// <param name="attributeName"></param>
        /// <param name="value">The value that attribute should not have or `null` if the attribute absence only should be asserted</param>
        /// <param name="comparison"></param>
        /// <param name="because"></param>
        /// <param name="becauseParameters"></param>
        /// <returns></returns>
        public AndConstraint<ElementAssertions> NotHaveAttribute(string attributeName, string value = null, StringComparison comparison = StringComparison.Ordinal, string because = null, params object[] becauseParameters)
        {
            Execute.Assertion
                .BecauseOf(because, becauseParameters)
                .Given(() => Subject)
                .ForCondition(element => element.Exists)
                .FailWith("Expected {context:element} {0} exists but it does not", (object)Subject ?? "null")
                .Then
                .ForCondition(element => (value == null && element.GetAttribute(attributeName) == null) || (!value.Equals(element.GetAttribute(attributeName), comparison)))
                .FailWith("Expected {context:element} {0} have attribute {3} value {1} but it has value {2}", (object)Subject ?? "null", value, Subject.GetAttribute(attributeName), attributeName);
            return new AndConstraint<ElementAssertions>(this);
        }

        /// <summary>
        /// Asserts that the element is the HTML tag specified.
        /// </summary>
        /// <param name="tag"></param>
        /// <param name="because"></param>
        /// <param name="becauseParameters"></param>
        /// <returns></returns>
        public AndConstraint<ElementAssertions> BeHtmlTag(string tag, string because = null, params object[] becauseParameters)
        {
            Execute.Assertion
                .BecauseOf(because, becauseParameters)
                .Given(() => Subject)
                .ForCondition(element => element?.TagName?.Equals(tag, StringComparison.OrdinalIgnoreCase) ?? false)
                .FailWith("Expected {context:element} {0} be HTML tag {1} but it is HTML tag {2}", (object)Subject ?? "null", tag, Subject?.TagName);
            return new AndConstraint<ElementAssertions>(this);
        }

        /// <summary>
        /// Asserts whether the element contains the text specified.
        /// </summary>
        /// <param name="text"></param>
        /// <param name="comparisonType"></param>
        /// <param name="because"></param>
        /// <param name="becauseParameters"></param>
        /// <returns></returns>
        public AndConstraint<ElementAssertions> ContainText(string text, StringComparison comparisonType = StringComparison.Ordinal, string because = null, params object[] becauseParameters)
        {
            Execute.Assertion
                .BecauseOf(because, becauseParameters)
                .Given(() => Subject)
                .ForCondition(element => element?.InnerText?.Contains(text, comparisonType) ?? false)
                .FailWith("Expected {context:element} {0} contain the text {1} but it does not: ({2})", (object)Subject ?? "null", text, Subject?.InnerText);
            return new AndConstraint<ElementAssertions>(this);
        }

        /// <summary>
        /// Asserts that the element contains the specified HTML fragment specified.
        /// </summary>
        /// <param name="html"></param>
        /// <param name="comparisonType"></param>
        /// <param name="because"></param>
        /// <param name="becauseParameters"></param>
        /// <returns></returns>
        public AndConstraint<ElementAssertions> ContainHTML(string html, StringComparison comparisonType = StringComparison.Ordinal, string because = null, params object[] becauseParameters)
        {
            Execute.Assertion
                .BecauseOf(because, becauseParameters)
                .Given(() => Subject)
                .ForCondition(element => element?.InnerHTML?.Contains(html, comparisonType) ?? false)
                .FailWith("Expected {context:element} {0} contain the html {1} but it does not: ({2})", (object)Subject ?? "null", html, Subject?.InnerHTML);
            return new AndConstraint<ElementAssertions>(this);
        }

        /// <summary>
        /// Asserts the element matches the predicate specified.
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="because"></param>
        /// <param name="becauseParameters"></param>
        /// <returns></returns>
        new public AndConstraint<ElementAssertions> Match(Expression<Func<IElement, bool>> predicate, string because = null, params object[] becauseParameters)
        {
            Execute.Assertion
                .BecauseOf(because, becauseParameters)
                .Given(() => Subject)
                .ForCondition(element => predicate.Compile().Invoke(element))
                .FailWith("Expected {context:element} {0} matches the predicate {1} but it does not", (object)Subject ?? "null", predicate);
            return new AndConstraint<ElementAssertions>(this);
        }

        /// <summary>
        /// Asserts the element does not match the element specified.
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="because"></param>
        /// <param name="becauseParameters"></param>
        /// <returns></returns>
        public AndConstraint<ElementAssertions> NotMatch(Expression<Func<IElement, bool>> predicate, string because = null, params object[] becauseParameters)
        {
            Execute.Assertion
                .BecauseOf(because, becauseParameters)
                .Given(() => Subject)
                .ForCondition(element => !predicate.Compile().Invoke(element))
                .FailWith("Expected {context:element} {0} not matches the predicate {1} but it does", (object)Subject ?? "null", predicate);
            return new AndConstraint<ElementAssertions>(this);
        }
    }
}
