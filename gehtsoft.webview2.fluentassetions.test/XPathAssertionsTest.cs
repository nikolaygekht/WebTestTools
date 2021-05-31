using System;
using FluentAssertions;
using Gehtsoft.Webview2.Uitest;
using Gehtsoft.Webview2.FluentAssertions;
using Moq;
using Xunit;
using Xunit.Sdk;

namespace Gehtsoft.Webview2.FluentAssertions.Test
{
    public class XPathAssertionsTest
    {
        [Theory]
        [InlineData("")]
        [InlineData("a")]
        [InlineData(null)]
        public void BeAsString_Ok(string v)
        {
            var xpath = new Mock<IXPath>();
            xpath.Setup(e => e.AsString).Returns(v);

            ((Action)(() => (xpath.Object).Should().Be(v))).Should().NotThrow();
        }

        [Theory]
        [InlineData("", "a")]
        [InlineData("a", "")]
        [InlineData(null, "")]
        [InlineData(null, "a")]
        [InlineData("a", null)]
        public void BeAsString_Fail(string v1, string v2)
        {
            var xpath = new Mock<IXPath>();
            xpath.Setup(e => e.AsString).Returns(v1);

            ((Action)(() => (xpath.Object).Should().Be(v2))).Should().Throw<XunitException>();
        }

        [Theory]
        [InlineData("", "a")]
        [InlineData("a", "")]
        [InlineData(null, "")]
        [InlineData(null, "a")]
        [InlineData("a", null)]
        public void NotBeAsString_Ok(string v1, string v2)
        {
            var xpath = new Mock<IXPath>();
            xpath.Setup(e => e.AsString).Returns(v1);

            ((Action)(() => (xpath.Object).Should().NotBe(v2))).Should().NotThrow();
        }

        [Theory]
        [InlineData("")]
        [InlineData("a")]
        [InlineData(null)]
        public void NotBeAsString_Fail(string v)
        {
            var xpath = new Mock<IXPath>();
            xpath.Setup(e => e.AsString).Returns(v);

            ((Action)(() => (xpath.Object).Should().NotBe(v))).Should().Throw<XunitException>();
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void BeAsBoolean_Ok(bool v)
        {
            var xpath = new Mock<IXPath>();
            xpath.Setup(e => e.AsBoolean).Returns(v);

            ((Action)(() => (xpath.Object).Should().Be(v))).Should().NotThrow();
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void BeAsBoolean_Fail(bool v)
        {
            var xpath = new Mock<IXPath>();
            xpath.Setup(e => e.AsBoolean).Returns(v);

            ((Action)(() => (xpath.Object).Should().Be(!v))).Should().Throw<XunitException>();
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void NotBeAsBoolean_Ok(bool v)
        {
            var xpath = new Mock<IXPath>();
            xpath.Setup(e => e.AsBoolean).Returns(v);

            ((Action)(() => (xpath.Object).Should().NotBe(!v))).Should().NotThrow();
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(1.2345)]
        [InlineData(-5)]
        public void BeAsNumberDouble_OK(double v)
        {
            var xpath = new Mock<IXPath>();
            xpath.Setup(e => e.AsNumber).Returns(v);

            ((Action)(() => (xpath.Object).Should().Be(v))).Should().NotThrow();
        }

        [Theory]
        [InlineData(0, 1)]
        [InlineData(1, 1.00001)]
        [InlineData(1.2345, 1.23449)]
        [InlineData(-5, 5)]
        public void BeAsNumberDouble_Fail(double v, double v1)
        {
            var xpath = new Mock<IXPath>();
            xpath.Setup(e => e.AsNumber).Returns(v);

            ((Action)(() => (xpath.Object).Should().Be(v1))).Should().Throw<XunitException>();
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(1.2345)]
        [InlineData(-5)]
        public void NotBeAsNumberDouble_Fail(double v)
        {
            var xpath = new Mock<IXPath>();
            xpath.Setup(e => e.AsNumber).Returns(v);

            ((Action)(() => (xpath.Object).Should().NotBe(v))).Should().Throw<XunitException>();
        }

        [Theory]
        [InlineData(0, 1)]
        [InlineData(1, 1.00001)]
        [InlineData(1.2345, 1.23449)]
        [InlineData(-5, 5)]
        public void NotBeAsNumberDouble_Ok(double v, double v1)
        {
            var xpath = new Mock<IXPath>();
            xpath.Setup(e => e.AsNumber).Returns(v);

            ((Action)(() => (xpath.Object).Should().NotBe(v1))).Should().NotThrow();
        }

        [Theory]
        [InlineData(0, 0, 1e-5)]
        [InlineData(1, 1.000001, 1e-5)]
        [InlineData(1, 0.999999, 1e-5)]
        [InlineData(-1, -1.000001, 1e-5)]
        [InlineData(-1, -0.999999, 1e-5)]
        public void BeApproximately_OK(double v, double v1, double delta)
        {
            var xpath = new Mock<IXPath>();
            xpath.Setup(e => e.AsNumber).Returns(v);

            ((Action)(() => (xpath.Object).Should().BeApproximately(v1, delta))).Should().NotThrow();
        }

        [Theory]
        [InlineData(0, 0.1, 1e-5)]
        [InlineData(1, 1.001, 1e-5)]
        [InlineData(1, 0.999, 1e-5)]
        [InlineData(-1, -1.001, 1e-5)]
        [InlineData(-1, -0.999, 1e-5)]
        public void BeApproximately_Fail(double v, double v1, double delta)
        {
            var xpath = new Mock<IXPath>();
            xpath.Setup(e => e.AsNumber).Returns(v);

            ((Action)(() => (xpath.Object).Should().BeApproximately(v1, delta))).Should().Throw<XunitException>();
        }

        [Theory]
        [InlineData(0, 0.1, 1e-5)]
        [InlineData(1, 1.001, 1e-5)]
        [InlineData(1, 0.999, 1e-5)]
        [InlineData(-1, -1.001, 1e-5)]
        [InlineData(-1, -0.999, 1e-5)]
        public void NotBeApproximately_Ok(double v, double v1, double delta)
        {
            var xpath = new Mock<IXPath>();
            xpath.Setup(e => e.AsNumber).Returns(v);

            ((Action)(() => (xpath.Object).Should().NotBeApproximately(v1, delta))).Should().NotThrow();
        }

        [Theory]
        [InlineData(0, 0, 1e-5)]
        [InlineData(1, 1.000001, 1e-5)]
        [InlineData(1, 0.999999, 1e-5)]
        [InlineData(-1, -1.000001, 1e-5)]
        [InlineData(-1, -0.999999, 1e-5)]
        public void NotBeApproximately_Fail(double v, double v1, double delta)
        {
            var xpath = new Mock<IXPath>();
            xpath.Setup(e => e.AsNumber).Returns(v);

            ((Action)(() => (xpath.Object).Should().NotBeApproximately(v1, delta))).Should().Throw<XunitException>();
        }

        [Theory]
        [InlineData(1, 0)]
        [InlineData(-1, -2)]
        public void BeGreater_OK(double v, double v1)
        {
            var xpath = new Mock<IXPath>();
            xpath.Setup(e => e.AsNumber).Returns(v);

            ((Action)(() => (xpath.Object).Should().BeGreaterThan(v1))).Should().NotThrow();
        }

        [Theory]
        [InlineData(1, 0)]
        [InlineData(1, 1)]
        [InlineData(-1, -2)]
        public void BeGreaterOrEqual_OK(double v, double v1)
        {
            var xpath = new Mock<IXPath>();
            xpath.Setup(e => e.AsNumber).Returns(v);

            ((Action)(() => (xpath.Object).Should().BeGreaterThanOrEquals(v1))).Should().NotThrow();
        }

        [Theory]
        [InlineData(0, 1)]
        [InlineData(-2, -1)]
        public void BeLess_OK(double v, double v1)
        {
            var xpath = new Mock<IXPath>();
            xpath.Setup(e => e.AsNumber).Returns(v);

            ((Action)(() => (xpath.Object).Should().BeLessThan(v1))).Should().NotThrow();
        }

        [Theory]
        [InlineData(0, 1)]
        [InlineData(0, 0)]
        [InlineData(-2, -1)]
        [InlineData(-2, -2)]
        public void BeLessOrEqual_OK(double v, double v1)
        {
            var xpath = new Mock<IXPath>();
            xpath.Setup(e => e.AsNumber).Returns(v);

            ((Action)(() => (xpath.Object).Should().BeLessThanOrEquals(v1))).Should().NotThrow();
        }

        [Theory]
        [InlineData(0, 1)]
        public void BeGreater_Fail(double v, double v1)
        {
            var xpath = new Mock<IXPath>();
            xpath.Setup(e => e.AsNumber).Returns(v);

            ((Action)(() => (xpath.Object).Should().BeGreaterThan(v1))).Should().Throw<XunitException>();
        }

        [Theory]
        [InlineData(0, 1)]
        public void BeGreaterOrEqual_Fail(double v, double v1)
        {
            var xpath = new Mock<IXPath>();
            xpath.Setup(e => e.AsNumber).Returns(v);

            ((Action)(() => (xpath.Object).Should().BeGreaterThanOrEquals(v1))).Should().Throw<XunitException>();
        }

        [Theory]
        [InlineData(1, 0)]
        public void BeLess_Fail(double v, double v1)
        {
            var xpath = new Mock<IXPath>();
            xpath.Setup(e => e.AsNumber).Returns(v);

            ((Action)(() => (xpath.Object).Should().BeLessThan(v1))).Should().Throw<XunitException>();
        }

        [Theory]
        [InlineData(1, 0)]
        public void BeLessOrEqual_Fail(double v, double v1)
        {
            var xpath = new Mock<IXPath>();
            xpath.Setup(e => e.AsNumber).Returns(v);

            ((Action)(() => (xpath.Object).Should().BeLessThanOrEquals(v1))).Should().Throw<XunitException>();
        }

        [Fact]
        public void ReturnElement_Ok()
        {
            var element = new Mock<IElement>();
            element.Setup(e => e.Exists).Returns(true);

            var xpath = new Mock<IXPath>();
            xpath.Setup(x => x.AsElement).Returns(element.Object);

            ((Action)(() => (xpath.Object).Should().ReturnElement())).Should().NotThrow();
        }

        [Fact]
        public void ReturnElement1_Fail()
        {
            var element = new Mock<IElement>();
            element.Setup(e => e.Exists).Returns(false);

            var xpath = new Mock<IXPath>();
            xpath.Setup(x => x.AsElement).Returns(element.Object);

            ((Action)(() => (xpath.Object).Should().ReturnElement())).Should().Throw<XunitException>();
        }

        [Fact]
        public void ReturnElement2_Fail()
        {
            var xpath = new Mock<IXPath>();
            xpath.Setup(x => x.AsElement).Returns((IElement)null);

            ((Action)(() => (xpath.Object).Should().ReturnElement())).Should().Throw<XunitException>();
        }

        [Fact]
        public void NotReturnElement1_Ok()
        {
            var element = new Mock<IElement>();
            element.Setup(e => e.Exists).Returns(false);

            var xpath = new Mock<IXPath>();
            xpath.Setup(x => x.AsElement).Returns(element.Object);

            ((Action)(() => (xpath.Object).Should().NotReturnElement())).Should().NotThrow();
        }

        [Fact]
        public void NotReturnElement2_Ok()
        {
            var xpath = new Mock<IXPath>();
            xpath.Setup(x => x.AsElement).Returns((IElement)null);

            ((Action)(() => (xpath.Object).Should().NotReturnElement())).Should().NotThrow();
        }

        [Fact]
        public void NotReturnElement1_Fail()
        {
            var element = new Mock<IElement>();
            element.Setup(e => e.Exists).Returns(true);

            var xpath = new Mock<IXPath>();
            xpath.Setup(x => x.AsElement).Returns(element.Object);

            ((Action)(() => (xpath.Object).Should().NotReturnElement())).Should().Throw<XunitException>();
        }

        [Fact]
        public void Match_Ok()
        {
            var xpath = new Mock<IXPath>();
            ((Action)(() => (xpath.Object).Should().Match(_ => true))).Should().NotThrow();
        }

        [Fact]
        public void Match_Fail()
        {
            var xpath = new Mock<IXPath>();
            ((Action)(() => (xpath.Object).Should().Match(_ => false))).Should().Throw<XunitException>();
        }

        [Fact]
        public void NotMatch_Ok()
        {
            var xpath = new Mock<IXPath>();
            ((Action)(() => (xpath.Object).Should().NotMatch(_ => false))).Should().NotThrow();
        }

        [Fact]
        public void NotMatch_Fail()
        {
            var xpath = new Mock<IXPath>();
            ((Action)(() => (xpath.Object).Should().NotMatch(_ => true))).Should().Throw<XunitException>();
        }
    }
}
