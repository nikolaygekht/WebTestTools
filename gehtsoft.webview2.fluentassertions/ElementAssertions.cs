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
    public class ElementAssertions : ReferenceTypeAssertions<IElement, ElementAssertions>
    {
        protected override string Identifier => "element";

        public ElementAssertions(IElement subject) : base(subject)
        {
        }

        public AndConstraint<ElementAssertions> Exist(string because = null, params object[] becauseParameters)
        {
            Execute.Assertion
                .BecauseOf(because, becauseParameters)
                .Given(() => Subject)
                .ForCondition(element => element.Exists)
                .FailWith("Expected {context:element} {0} exists but it does not", (object)Subject ?? "null");
            return new AndConstraint<ElementAssertions>(this);
        }

        public AndConstraint<ElementAssertions> NotExist(string because = null, params object[] becauseParameters)
        {
            Execute.Assertion
                .BecauseOf(because, becauseParameters)
                .Given(() => Subject)
                .ForCondition(element => !element.Exists)
                .FailWith("Expected {context:element} {0} not exist but it does", (object)Subject ?? "null");
            return new AndConstraint<ElementAssertions>(this);
        }

        public AndConstraint<ElementAssertions> HaveValue(string value, string because = null, params object[] becauseParameters)
        {
            Execute.Assertion
                .BecauseOf(because, becauseParameters)
                .Given(() => Subject)
                .ForCondition(element => element?.Value == value)
                .FailWith("Expected {context:element} {0} have value {1} but it has value {2}", (object)Subject ?? "null", value, Subject.Value);
            return new AndConstraint<ElementAssertions>(this);
        }

        public AndConstraint<ElementAssertions> NotHaveValue(string value, string because = null, params object[] becauseParameters)
        {
            Execute.Assertion
                .BecauseOf(because, becauseParameters)
                .Given(() => Subject)
                .ForCondition(element => element?.Value != value)
                .FailWith("Expected {context:element} {0} have value {1} but it has value {2}", (object)Subject ?? "null", value, Subject.Value);
            return new AndConstraint<ElementAssertions>(this);
        }

        public AndConstraint<ElementAssertions> HaveClass(string value, string because = null, params object[] becauseParameters)
        {
            Execute.Assertion
                .BecauseOf(because, becauseParameters)
                .Given(() => Subject)
                .ForCondition(element => element.Class?.Split(new char[] { ' ' })?.Any(e => e == value) ?? false)
                .FailWith("Expected {context:element} {0} have class {1} but it has classes {2}", (object)Subject ?? "null", value, Subject?.Class);
            return new AndConstraint<ElementAssertions>(this);
        }

        public AndConstraint<ElementAssertions> NotHaveClass(string value, string because = null, params object[] becauseParameters)
        {
            Execute.Assertion
                .BecauseOf(because, becauseParameters)
                .Given(() => Subject)
                .ForCondition(element => !(element.Class?.Split(new char[] { ' ' })?.Any(e => e == value) ?? false))
                .FailWith("Expected {context:element} {0} not have class {1} but it has classes {2}", (object)Subject ?? "null", value, Subject?.Class);
            return new AndConstraint<ElementAssertions>(this);
        }

        public AndConstraint<ElementAssertions> IsChecked(bool? value = true, string because = null, params object[] becauseParameters)
        {
            Execute.Assertion
                .BecauseOf(because, becauseParameters)
                .Given(() => Subject)
                .ForCondition(element => element.Checked == value)
                .FailWith("Expected {context:element} {0} have checked property to be {1} but it has value {2}", (object)Subject ?? "null", value, Subject.Checked);
            return new AndConstraint<ElementAssertions>(this);
        }

        public AndConstraint<ElementAssertions> HaveProperty<T>(string propertyName, T value, string because = null, params object[] becauseParameters)
        {
            Execute.Assertion
                .BecauseOf(because, becauseParameters)
                .Given(() => Subject)
                .ForCondition(element => element?.GetProperty<T>(propertyName).Equals(value) ?? false)
                .FailWith("Expected {context:element} {0} have property {3} value {1} but it has value {2}", (object)Subject ?? "null", value, Subject.GetProperty<T>(propertyName), propertyName);
            return new AndConstraint<ElementAssertions>(this);
        }

        public AndConstraint<ElementAssertions> NotHaveProperty<T>(string propertyName, T value, string because = null, params object[] becauseParameters)
        {
            Execute.Assertion
                .BecauseOf(because, becauseParameters)
                .Given(() => Subject)
                .ForCondition(element => !(element?.GetProperty<T>(propertyName).Equals(value) ?? false))
                .FailWith("Expected {context:element} {0} have property {3} value {1} but it has value {2}", (object)Subject ?? "null", value, Subject.GetProperty<T>(propertyName), propertyName);
            return new AndConstraint<ElementAssertions>(this);
        }

        public AndConstraint<ElementAssertions> HaveAttribute(string attributeName, string value, string because = null, params object[] becauseParameters)
        {
            Execute.Assertion
                .BecauseOf(because, becauseParameters)
                .Given(() => Subject)
                .ForCondition(element => element?.GetAttribute(attributeName).Equals(value) ?? false)
                .FailWith("Expected {context:element} {0} have attribute {3} value {1} but it has value {2}", (object)Subject ?? "null", value, Subject.GetAttribute(attributeName), attributeName);
            return new AndConstraint<ElementAssertions>(this);
        }

        public AndConstraint<ElementAssertions> NotHaveAttribute(string attributeName, string value, string because = null, params object[] becauseParameters)
        {
            Execute.Assertion
                .BecauseOf(because, becauseParameters)
                .Given(() => Subject)
                .ForCondition(element => !(element?.GetAttribute(attributeName).Equals(value) ?? false))
                .FailWith("Expected {context:element} {0} have attribute {3} value {1} but it has value {2}", (object)Subject ?? "null", value, Subject.GetAttribute(attributeName), attributeName);
            return new AndConstraint<ElementAssertions>(this);
        }

        public AndConstraint<ElementAssertions> HaveAttribute<T>(string attributeName, T value, string because = null, params object[] becauseParameters)
        {
            Execute.Assertion
                .BecauseOf(because, becauseParameters)
                .Given(() => Subject)
                .ForCondition(element =>
                {
                    if (element == null)
                        return false;

                    T v = default;

                    try
                    {
                        v = element.GetAttribute<T>(attributeName);
                    }
                    catch (FormatException )
                    {
                        return false;
                    }

                    return v.Equals(value);
                })
                .FailWith("Expected {context:element} {0} have attribute {3} value {1} but it has value {2}", (object)Subject ?? "null", value, Subject.GetAttribute(attributeName), attributeName);
            return new AndConstraint<ElementAssertions>(this);
        }

        public AndConstraint<ElementAssertions> BeHtmlTag(string tag, string because = null, params object[] becauseParameters)
        {
            Execute.Assertion
                .BecauseOf(because, becauseParameters)
                .Given(() => Subject)
                .ForCondition(element => element?.TagName?.Equals(tag, StringComparison.OrdinalIgnoreCase) ?? false)
                .FailWith("Expected {context:element} {0} be HTML tag {1} but it is HTML tag {2}", (object)Subject ?? "null", tag, Subject?.TagName);
            return new AndConstraint<ElementAssertions>(this);
        }

        public AndConstraint<ElementAssertions> ContainText(string text, StringComparison comparisonType = StringComparison.Ordinal, string because = null, params object[] becauseParameters)
        {
            Execute.Assertion
                .BecauseOf(because, becauseParameters)
                .Given(() => Subject)
                .ForCondition(element => element?.InnerText?.Contains(text, comparisonType) ?? false)
                .FailWith("Expected {context:element} {0} contain the text {1} but it does not: ({2})", (object)Subject ?? "null", text, Subject?.InnerText);
            return new AndConstraint<ElementAssertions>(this);
        }

        public AndConstraint<ElementAssertions> ContainHTML(string html, StringComparison comparisonType = StringComparison.Ordinal, string because = null, params object[] becauseParameters)
        {
            Execute.Assertion
                .BecauseOf(because, becauseParameters)
                .Given(() => Subject)
                .ForCondition(element => element?.InnerHTML?.Contains(html, comparisonType) ?? false)
                .FailWith("Expected {context:element} {0} contain the html {1} but it does not: ({2})", (object)Subject ?? "null", html, Subject?.InnerHTML);
            return new AndConstraint<ElementAssertions>(this);
        }

        new public AndConstraint<ElementAssertions> Match(Expression<Func<IElement, bool>> predicate, string because = null, params object[] becauseParameters)
        {
            Execute.Assertion
                .BecauseOf(because, becauseParameters)
                .Given(() => Subject)
                .ForCondition(element => predicate.Compile().Invoke(element))
                .FailWith("Expected {context:element} {0} matches the predicate {1} but it does not", (object)Subject ?? "null", predicate);
            return new AndConstraint<ElementAssertions>(this);
        }

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
