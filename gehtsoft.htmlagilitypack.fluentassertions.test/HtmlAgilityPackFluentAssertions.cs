using System;
using FluentAssertions;
using Xunit;
using Xunit.Sdk;

namespace Gehtsoft.HtmlAgilityPack.FluentAssertions.Test
{
    public class HtmlAgilityPackFluentAssertions : IClassFixture<TestPage>
    {
        private readonly TestPage TestPage;

        public HtmlAgilityPackFluentAssertions(TestPage testPage)
        {
            TestPage = testPage;
        }

        [Fact]
        public void PageLoads()
        {
            TestPage.Content.Should().NotBeNullOrEmpty();
            TestPage.Content.Should().StartWith("<!DOCTYPE html>");
            TestPage.Document.Should().NotBeNull();
        }

        [Fact]
        public void SelectById()
        {
            TestPage.Document.SelectById("text")
                .Should()
                .Exist()
                .And.Node.Should()
                .BeElement("input");
        }

        [Fact]
        public void SelectById_Fail()
        {
            TestPage.Document.SelectById("Text")
                .Should()
                .NotExist();
        }

        [Fact]
        public void SelectByName1()
        {
            TestPage.Document.SelectByName("Text")
                .Should()
                .Exist()
                .And.HaveCount(1)
                .And.Node.Should()
                    .BeElement("input");
        }

        [Fact]
        public void SelectByName2()
        {
            TestPage.Document.SelectByName("input", "Text")
                .Should()
                .Exist()
                .And.Node.Should()
                    .BeElement("input");
        }

        [Fact]
        public void SelectByClass()
        {
            TestPage.Document.SelectByClass("form-control")
                .Should()
                .Exist()
                .And.HaveCount(7);
        }

        [Fact]
        public void SelectByXPath()
        {
            TestPage.Document.Select("/html/body/noscript/p")
                .Should()
                .Exist()
                .And.HaveCount(1)
                .And.Node.InnerText.Should().StartWith("JavaScript must be enabled");
        }

        [Fact]
        public void HaveAttribute1_Ok()
        {
            TestPage.Document.SelectById("text")
                .Should().Exist()
                .And.Node.Should().HaveAttribute("autofocus");
        }

        [Fact]
        public void HaveAttribute1_Fail1()
        {
            ((Action)(() =>
            TestPage.Document.SelectById("text")
                .Should().Exist()
                .And.Node.Should().HaveAttribute("autoficus"))).Should().Throw<XunitException>();
        }

        [Fact]
        public void HaveAttribute1_Fail2()
        {
            ((Action)(() =>
            TestPage.Document.SelectById("tixt")
                .Should().Exist()
                .And.Node.Should().HaveAttribute("autoficus"))).Should().Throw<XunitException>();
        }

        [Fact]
        public void HaveAttribute2_Ok()
        {
            TestPage.Document.SelectById("text")
                .Should().Exist()
                .And.Node.Should().HaveAttribute("name", "Text");
        }

        [Fact]
        public void HaveAttribute2_Fail1()
        {
            ((Action)(() =>
            TestPage.Document.SelectById("text")
                .Should().Exist()
                .And.Node.Should().HaveAttribute("name", "Tixt"))).Should().Throw<XunitException>();
        }

        [Fact]
        public void HaveValue1_Ok()
        {
            TestPage.Document.SelectById("text")
                .Should().Exist()
                .And.Node.Should().HaveValue(null);
        }

        [Fact]
        public void HaveValue1_Ok1()
        {
            TestPage.Document.SelectById("text")
                .Should().Exist()
                .And.Node.Should().HaveValue("");
        }

        [Fact]
        public void HaveValue1_Fail()
        {
            ((Action)(() =>
           TestPage.Document.SelectById("text")
               .Should().Exist()
               .And.Node.Should().HaveValue("1"))).Should().Throw<XunitException>();
        }

        [Fact]
        public void HaveValue2_Ok()
        {
            TestPage.Document.SelectById("Allwords")
                .Should().Exist()
                .And.Node.Should().HaveValue("true");
        }

        [Fact]
        public void HaveValue2_Fail()
        {
            ((Action)(() =>
           TestPage.Document.SelectById("Allwords")
               .Should().Exist()
               .And.Node.Should().HaveValue(null))).Should().Throw<XunitException>();
        }

        [Fact]
        public void HaveValue2_Fail1()
        {
            ((Action)(() =>
           TestPage.Document.SelectById("Allwords")
               .Should().Exist()
               .And.Node.Should().HaveValue(""))).Should().Throw<XunitException>();
        }


        [Fact]
        public void Match_Ok()
        {
            TestPage.Document.DocumentNode.Should().Match(_ => true);
        }

        [Fact]
        public void NotMatch_Ok()
        {
            TestPage.Document.DocumentNode.Should().NotMatch(_ => false);
        }

        [Fact]
        public void Match_Fail()
        {
            ((Action)(() =>
            TestPage.Document.DocumentNode.Should().Match(_ => false))).Should().Throw<XunitException>();
        }

        [Fact]
        public void NotMatch_Fail()
        {
            ((Action)(() =>
            TestPage.Document.DocumentNode.Should().NotMatch(_ => true))).Should().Throw<XunitException>();
        }

        [Fact]
        public void ContainText_Ok()
        {
            TestPage.Document.Select("/html/body/noscript").Node.Should().ContainText("Java");
        }

        [Fact]
        public void ContainText_Fail()
        {
            ((Action)(() =>
            TestPage.Document.Select("/html/body/noscript").Node.Should().ContainText("Jaba")))
            .Should().Throw<XunitException>();
        }

        [Fact]
        public void ContainHtml_Ok()
        {
            TestPage.Document.Select("/html/body/noscript").Node.Should().ContainHtml("<p>JavaScript");
        }

        [Fact]
        public void ContainHtml_Fail()
        {
            ((Action)(() =>
            TestPage.Document.Select("/html/body/noscript").Node.Should().ContainHtml("<b>JavaScript")))
            .Should().Throw<XunitException>();
        }

        [Fact]
        public void ContainNodeMatching_Ok()
        {
            TestPage.Document.SelectByClass("k-checkbox").Should()
                .Exist()
                .And.ContainNodeMatching(e => e.Attributes["name"] != null && e.Attributes["name"].Value == "InAuthors");
        }

        [Fact]
        public void ContainAllNodesMatching_Ok()
        {
            TestPage.Document.SelectByClass("k-checkbox").Should()
                .Exist()
                .And.ContainAllNodesMatching(e => e.Attributes["value"] != null && e.Attributes["value"].Value == "true");
        }

        [Fact]
        public void ContainNoNodesMatching_Ok()
        {
            TestPage.Document.SelectByClass("k-checkbox").Should()
                .Exist()
                .And.ContainNoNodesMatching(e => e.Attributes["value"] != null && e.Attributes["value"].Value == "false");
        }
    }
}



