using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;
using Xunit.Sdk;

namespace Gehtsoft.HtmlAgilityPack.FluentAssertions.Test
{
    public class JsonFluentAssertions
    {
        [Theory]
        [InlineData("null")]
        [InlineData("true")]
        [InlineData("123")]
        [InlineData("1.23")]
        [InlineData("[1, 2, 3]")]
        [InlineData("{ \"a\" : 1 }")]
        public void IsJson_Ok(string json)
        {
            ((Action)(() => json.Should().BeJson())).Should().NotThrow();
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("1a")]
        [InlineData("{ \"a\" : 1 ")]
        [InlineData("{ a : b }")]
        public void IsJson_Failed(string json)
        {
            ((Action)(() => json.Should().BeJson())).Should().Throw<XunitException>();
        }

        [Theory]
        [InlineData("null", JsonValueKind.Null)]
        [InlineData("1", JsonValueKind.Number)]
        [InlineData("\"1\"", JsonValueKind.String)]
        [InlineData("true", JsonValueKind.True)]
        [InlineData("false", JsonValueKind.False)]
        [InlineData("[1, 2, 3]", JsonValueKind.Array)]
        [InlineData("{ \"a\" : 1 }", JsonValueKind.Object)]
        public void AsJson(string json, JsonValueKind expectedType)
        {
            json.AsJson().Should().BeOfType<JsonElement>()
                .And.Match(e => e.ValueKind == expectedType)
                .And.BeKindOf(expectedType);
        }

        [Theory]
        [InlineData("null", null)]
        [InlineData("1", 1)]
        [InlineData("1", 1.0)]
        [InlineData("1.2345", 1.2345)]
        [InlineData("\"aaa\"", "aaa")]
        [InlineData("true", true)]
        [InlineData("false", false)]
        public void Be_Ok(string json, object value)
        {
            ((Action)(() => json.AsJson().Should().Be(value))).Should().NotThrow();
        }

        [Theory]
        [InlineData("null", 1)]
        [InlineData("1", null)]
        [InlineData("1", 1.1)]
        [InlineData("\"aaa\"", "aab")]
        [InlineData("\"1\"", 1)]
        [InlineData("true", false)]
        [InlineData("false", true)]
        [InlineData("false", 1)]
        [InlineData("[]", 1)]
        [InlineData("{}", 1)]
        public void Be_Fail(string json, object value)
        {
            ((Action)(() => json.AsJson().Should().Be(value))).Should().Throw<XunitException>();
        }

        [Theory]
        [InlineData("null", 1)]
        [InlineData("1", null)]
        [InlineData("1", 1.1)]
        [InlineData("\"aaa\"", "aab")]
        [InlineData("\"1\"", 1)]
        [InlineData("true", false)]
        [InlineData("false", true)]
        [InlineData("false", 1)]
        [InlineData("[]", 1)]
        [InlineData("{}", 1)]
        public void NotBe_Ok(string json, object value)
        {
            ((Action)(() => json.AsJson().Should().NotBe(value))).Should().NotThrow();
        }

        [Theory]
        [InlineData("null", null)]
        [InlineData("1", 1)]
        [InlineData("1", 1.0)]
        [InlineData("1.2345", 1.2345)]
        [InlineData("\"aaa\"", "aaa")]
        [InlineData("true", true)]
        [InlineData("false", false)]
        public void NotBe_Fail(string json, object value)
        {
            ((Action)(() => json.AsJson().Should().NotBe(value))).Should().Throw<XunitException>();
        }

        [Fact]
        public void Match_Ok()
        {
            ((Action)(() => "null".AsJson().Should().Match(_ => true))).Should().NotThrow();
        }

        [Fact]
        public void Match_Fail()
        {
            ((Action)(() => "null".AsJson().Should().Match(_ => false))).Should().Throw<XunitException>();
        }

        [Fact]
        public void NotMatch_Ok()
        {
            ((Action)(() => "null".AsJson().Should().NotMatch(_ => false))).Should().NotThrow();
        }

        [Fact]
        public void NotMatch_Fail()
        {
            ((Action)(() => "null".AsJson().Should().NotMatch(_ => true))).Should().Throw<XunitException>();
        }

        [Theory]
        [InlineData("[]", 0)]
        [InlineData("[1]", 1)]
        [InlineData("[1, 2, 3, [4, 5]]", 4)]
        public void HaveCount_OK(string json, int count)
        {
            ((Action)(() => json.AsJson().Should().HaveCount(count))).Should().NotThrow();
        }

        [Theory]
        [InlineData("null", 0)]
        [InlineData("123", 0)]
        [InlineData("123", 1)]
        [InlineData("{}", 0)]
        [InlineData("[]", 1)]
        [InlineData("[1]", 0)]
        [InlineData("[1, 2, 3, [4, 5]]", 5)]
        public void HaveCount_Fail(string json, int count)
        {
            ((Action)(() => json.AsJson().Should().HaveCount(count))).Should().Throw<XunitException>();
        }

        [Theory]
        [InlineData("[]")]
        public void IsEmpty_Ok(string json)
        {
            ((Action)(() => json.AsJson().Should().BeEmptyArray())).Should().NotThrow();
        }

        [Theory]
        [InlineData("null")]
        [InlineData("1")]
        [InlineData("{}")]
        [InlineData("[1]")]
        public void IsEmpty_Fail(string json)
        {
            ((Action)(() => json.AsJson().Should().BeEmptyArray())).Should().Throw<XunitException>();
        }

        [Theory]
        [InlineData("[]")]
        [InlineData("null")]
        public void IsEmptyOrNull_Ok(string json)
        {
            ((Action)(() => json.AsJson().Should().BeEmptyArrayOrNull())).Should().NotThrow();
        }

        [Theory]
        [InlineData("1")]
        [InlineData("{}")]
        [InlineData("[1]")]
        public void IsEmptyOrNull_Fail(string json)
        {
            ((Action)(() => json.AsJson().Should().BeEmptyArrayOrNull())).Should().Throw<XunitException>();
        }

        [Theory]
        [InlineData("[1, 2]", 1)]
        [InlineData("[1, 2]", 2)]
        public void HaveElement_Ok(string json, object element)
        {
            ((Action)(() => json.AsJson().Should().HaveElement(element))).Should().NotThrow();
        }

        [Theory]
        [InlineData("3", 3)]
        [InlineData("[]", 3)]
        [InlineData("[1, 2]", 3)]
        public void HaveElement_Fail(string json, object element)
        {
            ((Action)(() => json.AsJson().Should().HaveElement(element))).Should().Throw<XunitException>();
        }

        [Theory]
        [InlineData("[]", 3)]
        [InlineData("[1, 2]", 3)]
        public void HaveNoElement_Ok(string json, object element)
        {
            ((Action)(() => json.AsJson().Should().HaveNoElement(element))).Should().NotThrow();
        }

        [Theory]
        [InlineData("1", 1)]
        [InlineData("[1, 2]", 1)]
        [InlineData("[1, 2]", 2)]
        public void HaveNoElement_Fail(string json, object element)
        {
            ((Action)(() => json.AsJson().Should().HaveNoElement(element))).Should().Throw<XunitException>();
        }

        [Fact]
        public void HaveElement_Matching_Ok()
        {
            ((Action)(() => "[1, 2, 3]".AsJson().Should().HaveElementMatching(e => e.GetInt32() == 2))).Should().NotThrow();
        }

        [Fact]
        public void HaveElement_Matching_Fail()
        {
            ((Action)(() => "[1, 2, 3]".AsJson().Should().HaveElementMatching(e => e.GetInt32() == 4))).Should().Throw<XunitException>();
        }

        [Fact]
        public void HaveNoElement_Matching_Ok()
        {
            ((Action)(() => "[1, 2, 3]".AsJson().Should().HaveNoElementsMatching(e => e.GetInt32() == 4))).Should().NotThrow();
        }

        [Fact]
        public void HaveNoElement_Matching_Fail()
        {
            ((Action)(() => "[1, 2, 3]".AsJson().Should().HaveNoElementsMatching(e => e.GetInt32() == 3))).Should().Throw<XunitException>();
        }

        [Fact]
        public void HaveAllElements_Matching_Ok()
        {
            ((Action)(() => "[1, 2, 3]".AsJson().Should().HaveAllElementsMatching(e => e.GetInt32() > 0))).Should().NotThrow();
        }

        [Fact]
        public void HaveAllElements_Matching_Fail()
        {
            ((Action)(() => "[1, 2, 3]".AsJson().Should().HaveAllElementsMatching(e => e.GetInt32() > 1))).Should().Throw<XunitException>();
        }

        [Theory]
        [InlineData("{ \"a\" : 1}", "a")]
        public void HaveProperty1_Ok(string json, string propertyName)
        {
            ((Action)(() => json.AsJson().Should().HaveProperty(propertyName))).Should().NotThrow();
        }

        [Theory]
        [InlineData("\"a\"", "b")]
        [InlineData("{}", "b")]
        [InlineData("{ \"a\" : 1}", "b")]
        public void HaveProperty1_Fail(string json, string propertyName)
        {
            ((Action)(() => json.AsJson().Should().HaveProperty(propertyName))).Should().Throw<XunitException>();
        }

        [Theory]
        [InlineData("{ \"a\" : 1 }", "a", 1)]
        public void HaveProperty2_Ok(string json, string propertyName, int value)
        {
            ((Action)(() => json.AsJson().Should().HaveProperty(propertyName, value))).Should().NotThrow();
        }

        [Theory]
        [InlineData("null", "b", 1)]
        [InlineData("[]", "b", 1)]
        [InlineData("{}", "b", 1)]
        [InlineData("{ \"a\" : 1}", "b", 1)]
        [InlineData("{ \"a\" : 1}", "a", 2)]
        public void HaveProperty2_Fail(string json, string propertyName, int value)
        {
            ((Action)(() => json.AsJson().Should().HaveProperty(propertyName, value))).Should().Throw<XunitException>();
        }

        [Theory]
        [InlineData("{ \"a\" : 1}", "a", 1)]
        [InlineData("{ \"a\" : \"1\"}", "a", "1")]
        public void HavePropertyMatching_Ok(string json, string propertyName, object value)
        {
            ((Action)(() => json.AsJson().Should().HavePropertyMatching(propertyName, e => e.EqualsTo(value)))).Should().NotThrow();
        }

        [Theory]
        [InlineData("null", "a", 1)]
        [InlineData("[]", "a", 1)]
        [InlineData("{}", "a", 1)]
        [InlineData("{ \"b\" : 1}", "a", 1)]
        [InlineData("{ \"a\" : 1}", "a", 2)]
        [InlineData("{ \"a\" : \"1\"}", "a", 1)]
        public void HavePropertyMatching_Failed(string json, string propertyName, object value)
        {
            ((Action)(() => json.AsJson().Should().HavePropertyMatching(propertyName, e => e.EqualsTo(value)))).Should().Throw<XunitException>();
        }
    }
}



