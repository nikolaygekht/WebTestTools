using System;
using System.Linq;
using Esprima.Ast;
using HtmlAgilityPack;
using Xunit;
using Xunit.Sdk;
using FluentAssertions;
using System.Linq.Expressions;

namespace Gehtsoft.HtmlAgilityPack.FluentAssertions.Test
{
    public class JSFluentAssertions : IClassFixture<TestPageJS>
    {
        private readonly TestPageJS mPage;

        public JSFluentAssertions(TestPageJS page)
        {
            mPage = page;
        }

        private HtmlNode mScriptNode;

        private HtmlNode ScriptNode
        {
            get => mScriptNode ??= mPage.Document.Select("//script").Nodes.FirstOrDefault(n => n.InnerText.Contains("var grid;"));
        }

        private Script mScript;

        private Script Script
        {
            get => mScript ??= ScriptNode.InnerText.AsScript();
        }

        [Theory]
        [InlineData("function x() { }")]
        [InlineData("a = b + c")]
        public void HaveChildren_Success(string script)
        {
            ((Action)(() => script.AsScript().Should().HaveChildren())).Should().NotThrow<XunitException>();
        }

        [Theory]
        [InlineData("")]
        [InlineData("//comment")]
        public void HaveChildren_Fail(string script)
        {
            ((Action)(() => script.AsScript().Should().HaveChildren())).Should().Throw<XunitException>();
        }

        [Theory]
        [InlineData("function x() { }")]
        [InlineData("a = b + c")]
        public void HaveNoChildren_Fail(string script)
        {
            ((Action)(() => script.AsScript().Should().HaveNoChildren())).Should().Throw<XunitException>();
        }

        [Theory]
        [InlineData("")]
        [InlineData("//comment")]
        public void HaveNoChildren_Success(string script)
        {
            ((Action)(() => script.AsScript().Should().HaveNoChildren())).Should().NotThrow<XunitException>();
        }

        [Fact]
        public void Contain_Success()
        {
            var script = "$('#a').hide();";
            ((Action)(() => script.AsScript()
                            .Should()
                            .Contain<CallExpression>(
                                    e => e.Callee.Is<StaticMemberExpression>(
                                        e => e.Property.Is<Identifier>(
                                            i => i.Name == "hide"))))).Should().NotThrow<XunitException>();
        }

        [Fact]
        public void Contain_Failed()
        {
            var script = "$('#a').hide();";
            ((Action)(() => script.AsScript()
                            .Should()
                            .Contain<CallExpression>(
                                    e => e.Callee.Is<StaticMemberExpression>(
                                        e => e.Property.Is<Identifier>(
                                            i => i.Name == "show"))))).Should().Throw<XunitException>();
        }

        [Fact]
        public void Match_Success()
        {
            var script = "$('#a').hide();";
            var expr = script.AsScript().Find<CallExpression>(e => e.Callee.Is<Identifier>());

            ((Action)(() => expr.Should()
                    .Match<CallExpression>(e => e.Callee.As<Identifier>().Name == "$"))).Should().NotThrow<XunitException>();
        }


        [Fact]
        public void Match_Failed1()
        {
            var script = "$('#a').hide();";
            var expr = script.AsScript().Find<CallExpression>(e => e.Callee.Is<Identifier>());

            ((Action)(() => expr.Should()
                    .Match<CallExpression>(e => e.Callee.As<Identifier>().Name == "!"))).Should().Throw<XunitException>();
        }

        [Fact]
        public void Match_Failed2()
        {
            var script = "$('#a').hide();";
            var expr = script.AsScript().Find<CallExpression>(e => e.Callee.Is<Identifier>());

            ((Action)(() => expr.Should()
                    .Match<StaticMemberExpression>(_ => true))).Should().Throw<XunitException>();
        }

        [Fact]
        public void NodeAsScript()
        {
            ScriptNode.AsScript()
                .Should()
                .NotBeNull()
                .And.HaveChildren();
        }

        [Fact]
        public void AttributeAsScript()
        {
            mPage.Document
                .SelectById("clicktest").Node
                .Should().NotBeNull()
                .And.Subject
                .GetAttributeValue("onclick", null)
                .Should().NotBeNull()
                .And.Subject.AsScript()
                .Should().HaveChildren();
        }

        [Fact]
        public void JQueryExpression()
        {
            var script = mPage.Document
                .SelectById("clicktest").Node
                .GetAttributeValue("onclick", null)
                .AsScript();


            var method = script.Find<CallExpression>(e => e.Callee.Is<StaticMemberExpression>(e => e.Property.As<Identifier>()?.Name == "hide"));

            var jqueryObject = method.Callee.As<StaticMemberExpression>().Object.As<CallExpression>();

            jqueryObject.Callee.Should()
                .BeOfType<Identifier>()
                .And.Subject.As<Identifier>().Name
                .Should().Be("$");

            jqueryObject.Arguments.Should()
                .HaveCount(1)
                .And.Subject.First()
                .Should()
                .Match<Literal>(l => l.StringValue == "#a");
        }

        [Fact]
        public void RealWorldScenario_Success()
        {
            var script = Script;
            var allFiles = script.Find<FunctionDeclaration>(f => f.Id.Is<Identifier>(i => i.Name == "allfiles"));
            allFiles.Should().NotBeNull();

            var @return = allFiles.Find<ReturnStatement>(_ => true);

            @return.Should()
                .NotBeNull()
                .And.Subject.As<ReturnStatement>().Argument
                    .Should().BeOfType<ArrayExpression>();

            foreach (var returnObject in @return.Argument.FindAll<ObjectExpression>())
            {
                returnObject.Should()
                    .Contain<Property>(p => p.Key.Is("FileName"));
            }
        }
    }
}



