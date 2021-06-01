using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using FluentAssertions;
using FluentAssertions.Execution;
using FluentAssertions.Primitives;

namespace Gehtsoft.HtmlAgilityPack.FluentAssertions
{
    public class JsonElementAssertions : ReferenceTypeAssertions<JsonElement, JsonElementAssertions>
    {
        internal JsonElementAssertions(JsonElement subject) : base(subject)
        {
        }

        protected override string Identifier => "jsonelement";

        public AndConstraint<JsonElementAssertions> BeKindOf(JsonValueKind valueKind, string because = null, params object[] becauseParameters)
        {
            Execute.Assertion
                .BecauseOf(because, becauseParameters)
                .Given(() => Subject)
                .ForCondition(element => element.ValueKind == valueKind)
                .FailWith("Expected {context:jsonelement} is a {0} but it is {1}", valueKind, Subject.ValueKind);

            return new AndConstraint<JsonElementAssertions>(this);
        }

        new public AndConstraint<JsonElementAssertions> BeNull(string because = null, params object[] becauseParameters) => BeKindOf(JsonValueKind.Null, because, becauseParameters);
        public AndConstraint<JsonElementAssertions> BeNumber(string because = null, params object[] becauseParameters) => BeKindOf(JsonValueKind.Number, because, becauseParameters);
        public AndConstraint<JsonElementAssertions> BeTrue(string because = null, params object[] becauseParameters) => BeKindOf(JsonValueKind.True, because, becauseParameters);
        public AndConstraint<JsonElementAssertions> BeFalse(string because = null, params object[] becauseParameters) => BeKindOf(JsonValueKind.False, because, becauseParameters);

        public AndConstraint<JsonElementAssertions> BeBoolean(string because = null, params object[] becauseParameters)
        {
            Execute.Assertion
                .BecauseOf(because, becauseParameters)
                .Given(() => Subject)
                .ForCondition(element => element.ValueKind == JsonValueKind.True || element.ValueKind == JsonValueKind.False)
                .FailWith("Expected {context:jsonelement} is a Boolean but it is {0}", Subject.ValueKind);

            return new AndConstraint<JsonElementAssertions>(this);
        }

        public AndConstraint<JsonElementAssertions> BeString(string because = null, params object[] becauseParameters) => BeKindOf(JsonValueKind.String, because, becauseParameters);

        public AndConstraint<JsonElementAssertions> Be(object value, string because = null, params object[] becauseParameters)
        {
            Execute.Assertion
                .BecauseOf(because, becauseParameters)
                .Given(() => Subject)
                .ForCondition(element => element.EqualsTo(value))
                .FailWith("Expected {context:jsonelement} is {1} is {0}", Subject.ToString(), value);

            return new AndConstraint<JsonElementAssertions>(this);
        }

        public AndConstraint<JsonElementAssertions> NotBe(object value, string because = null, params object[] becauseParameters)
        {
            Execute.Assertion
                .BecauseOf(because, becauseParameters)
                .Given(() => Subject)
                .ForCondition(element => !element.EqualsTo(value))
                .FailWith("Expected {context:jsonelement} is not {1} it is", Subject.ToString(), value);

            return new AndConstraint<JsonElementAssertions>(this);
        }

        public AndConstraint<JsonElementAssertions> BeObject(string because = null, params object[] becauseParameters) => BeKindOf(JsonValueKind.Object, because, becauseParameters);

        public AndConstraint<JsonElementAssertions> HaveProperty(string propertyName, string because = null, params object[] becauseParameters)
        {
            Execute.Assertion
                .BecauseOf(because, becauseParameters)
                .Given(() => Subject)
                .ForCondition(element => element.ValueKind == JsonValueKind.Object)
                .FailWith("Expected {context:jsonelement} to be an object but it is {0}", Subject.ValueKind)
                .Then
                .ForCondition(element => element.TryGetProperty(propertyName, out _))
                .FailWith("Expected {context:jsonelement} has a property {0} but it does not have", propertyName);

            return new AndConstraint<JsonElementAssertions>(this);
        }

        public AndConstraint<JsonElementAssertions> HaveProperty(string propertyName, object value, string because = null, params object[] becauseParameters)
        {
            Execute.Assertion
               .BecauseOf(because, becauseParameters)
               .Given(() => Subject)
               .ForCondition(element => element.ValueKind == JsonValueKind.Object)
               .FailWith("Expected {context:jsonelement} to be an object but it is {0}", Subject.ValueKind)
               .Then
               .ForCondition(element => element.TryGetProperty(propertyName, out _))
               .FailWith("Expected {context:jsonelement} has a property {0} but it does not have", propertyName)
               .Then
               .ForCondition(element => element.TryGetProperty(propertyName, out var property) && property.EqualsTo(value))
               .FailWith("Expected {context:jsonelement} has a property {0} equal to {1} but it is not equal", propertyName, value);

            return new AndConstraint<JsonElementAssertions>(this);
        }

        public AndConstraint<JsonElementAssertions> HavePropertyMatching(string propertyName, Expression<Func<JsonElement, bool>> predicate, string because = null, params object[] becauseParameters)
        {
            Execute.Assertion
              .BecauseOf(because, becauseParameters)
              .Given(() => Subject)
              .ForCondition(element => element.ValueKind == JsonValueKind.Object)
              .FailWith("Expected {context:jsonelement} to be an object but it is {0}", Subject.ValueKind)
              .Then
              .ForCondition(element => element.TryGetProperty(propertyName, out _))
              .FailWith("Expected {context:jsonelement} has a property {0} but it does not have", propertyName)
              .Then
              .ForCondition(element => element.TryGetProperty(propertyName, out var property) && predicate.Compile()(property))
              .FailWith("Expected {context:jsonelement} has a property {0} matching {1} but it does match", propertyName, predicate);

            return new AndConstraint<JsonElementAssertions>(this);
        }

        public AndConstraint<JsonElementAssertions> HavePropertyNotMatching(string propertyName, Expression<Func<JsonElement, bool>> predicate, string because = null, params object[] becauseParameters)
        {
            Execute.Assertion
              .BecauseOf(because, becauseParameters)
              .Given(() => Subject)
              .ForCondition(element => element.ValueKind == JsonValueKind.Object)
              .FailWith("Expected {context:jsonelement} to be an object but it is {0}", Subject.ValueKind)
              .Then
              .ForCondition(element => !element.TryGetProperty(propertyName, out var property) || !predicate.Compile()(property))
              .FailWith("Expected {context:jsonelement} has no property {0} matching {1} but it does have the property matching", propertyName, predicate);

            return new AndConstraint<JsonElementAssertions>(this);
        }

        public AndConstraint<JsonElementAssertions> BeArray(string because = null, params object[] becauseParameters) => BeKindOf(JsonValueKind.Array, because, becauseParameters);

        public AndConstraint<JsonElementAssertions> HaveCount(int count, string because = null, params object[] becauseParameters)
        {
            Execute.Assertion
                .BecauseOf(because, becauseParameters)
                .Given(() => Subject)
                .ForCondition(element => element.ValueKind == JsonValueKind.Array)
                .FailWith("Expected {context:jsonelement} is an array but it is {0}", Subject.ValueKind)
                .Then
                .ForCondition(element => element.GetArrayLength() == count)
                .FailWith("Expected {context:jsonelement} is an array of {1} elements but it is array of {0} element", Subject.GetArrayLength(), count);

            return new AndConstraint<JsonElementAssertions>(this);
        }

        public AndConstraint<JsonElementAssertions> HaveElement(object value, string because = null, params object[] becauseParameters)
        {
            Execute.Assertion
                .BecauseOf(because, becauseParameters)
                .Given(() => Subject)
                .ForCondition(element => element.ValueKind == JsonValueKind.Array)
                .FailWith("Expected {context:jsonelement} is an array but it is {0}", Subject.ValueKind)
                .Then
                .ForCondition(element => element.EnumerateArray().Any(e => e.EqualsTo(value)))
                .FailWith("Expected {context:jsonelement} is an array that has element {0} but it does not have", value);

            return new AndConstraint<JsonElementAssertions>(this);
        }

        public AndConstraint<JsonElementAssertions> HaveElementMatching(Expression<Func<JsonElement, bool>> predicate, string because = null, params object[] becauseParameters)
        {
            var func = predicate.Compile();

            Execute.Assertion
                .BecauseOf(because, becauseParameters)
                .Given(() => Subject)
                .ForCondition(element => element.ValueKind == JsonValueKind.Array)
                .FailWith("Expected {context:jsonelement} is an array but it is {0}", Subject.ValueKind)
                .Then
                .ForCondition(element => element.EnumerateArray().Any(e => func(e)))
                .FailWith("Expected {context:jsonelement} is an array that has element that matches {0} but it does not have", predicate);

            return new AndConstraint<JsonElementAssertions>(this);
        }

        public AndConstraint<JsonElementAssertions> HaveNoElementsMatching(Expression<Func<JsonElement, bool>> predicate, string because = null, params object[] becauseParameters)
        {
            var func = predicate.Compile();
            Execute.Assertion
                .BecauseOf(because, becauseParameters)
                .Given(() => Subject)
                .ForCondition(element => element.ValueKind == JsonValueKind.Array)
                .FailWith("Expected {context:jsonelement} is an array but it is {0}", Subject.ValueKind)
                .Then
                .ForCondition(element => !element.EnumerateArray().Any(e => func(e)))
                .FailWith("Expected {context:jsonelement} is an array that does not have an element that matches {0} but it has", predicate);
            return new AndConstraint<JsonElementAssertions>(this);
        }

        public AndConstraint<JsonElementAssertions> HaveAllElementsMatching(Expression<Func<JsonElement, bool>> predicate, string because = null, params object[] becauseParameters)
        {
            var func = predicate.Compile();
            Execute.Assertion
                .BecauseOf(because, becauseParameters)
                .Given(() => Subject)
                .ForCondition(element => element.ValueKind == JsonValueKind.Array)
                .FailWith("Expected {context:jsonelement} is an array but it is {0}", Subject.ValueKind)
                .Then
                .ForCondition(element => element.ValueKind == JsonValueKind.Array && element.EnumerateArray().All(e => func(e)))
                .FailWith("Expected {context:jsonelement} is an array that have all elements that matches {0} but it does not have", predicate);

            return new AndConstraint<JsonElementAssertions>(this);
        }

        public AndConstraint<JsonElementAssertions> HaveNoElement(object value, string because = null, params object[] becauseParameters)
        {
            Execute.Assertion
                .BecauseOf(because, becauseParameters)
                .Given(() => Subject)
                .ForCondition(element => element.ValueKind == JsonValueKind.Array)
                .FailWith("Expected {context:jsonelement} is an array but it is {0}", Subject.ValueKind)
                .Then
                .ForCondition(element => !element.EnumerateArray().Any(e => e.EqualsTo(value)))
                .FailWith("Expected {context:jsonelement} is an array that does not ha element {0} but it has such element", value);

            return new AndConstraint<JsonElementAssertions>(this);
        }

        public AndConstraint<JsonElementAssertions> BeEmptyArray(string because = null, params object[] becauseParameters)
        {
            Execute.Assertion
                .BecauseOf(because, becauseParameters)
                .Given(() => Subject)
                .ForCondition(element => element.ValueKind == JsonValueKind.Array)
                .FailWith("Expected {context:jsonelement} is an array but it is {0}", Subject.ValueKind)
                .Then
                .ForCondition(element => element.GetArrayLength() == 0)
                .FailWith("Expected {context:jsonelement} is an array of 0 elements but it is array of {0} elements", Subject.GetArrayLength());

            return new AndConstraint<JsonElementAssertions>(this);
        }

        public AndConstraint<JsonElementAssertions> BeEmptyArrayOrNull(string because = null, params object[] becauseParameters)
        {
            Execute.Assertion
                .BecauseOf(because, becauseParameters)
                .Given(() => Subject)
                .ForCondition(element => (element.ValueKind == JsonValueKind.Array && element.GetArrayLength() == 0) || element.ValueKind == JsonValueKind.Null)
                .FailWith("Expected {context:jsonelement} is an array of 0 elements but it is {0}", Subject.ToString());

            return new AndConstraint<JsonElementAssertions>(this);
        }

        new public AndConstraint<JsonElementAssertions> Match(Expression<Func<JsonElement, bool>> predicate, string because = null, params object[] becauseParameters)
        {
            Execute.Assertion
                .BecauseOf(because, becauseParameters)
                .Given(() => Subject)
                .ForCondition(element => predicate.Compile()(element))
                .FailWith("Expected {context:jsonelement} matches predicate {0} but it does not", predicate);

            return new AndConstraint<JsonElementAssertions>(this);
        }

        public AndConstraint<JsonElementAssertions> NotMatch(Expression<Func<JsonElement, bool>> predicate, string because = null, params object[] becauseParameters)
        {
            Execute.Assertion
                .BecauseOf(because, becauseParameters)
                .Given(() => Subject)
                .ForCondition(element => !predicate.Compile()(element))
                .FailWith("Expected {context:jsonelement} does not match predicate {0} but it does not", predicate);

            return new AndConstraint<JsonElementAssertions>(this);
        }
    }
}
