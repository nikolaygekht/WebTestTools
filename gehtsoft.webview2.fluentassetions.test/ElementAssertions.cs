using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Gehtsoft.Webview2.Uitest;
using Gehtsoft.Webview2.FluentAssertions;
using Moq;
using Xunit;
using Xunit.Sdk;

namespace Gehtsoft.Webview2.FluentAssertions.Test
{
    public class ElementAssertionsTest
    {

        [Fact]
        public void Exist_Ok()
        {
            var element = new Mock<IElement>();
            element.Setup(e => e.Exists).Returns(true);

            ((Action)(() => (element.Object).Should().Exist())).Should().NotThrow();
        }

        [Fact]
        public void Exist_Fail()
        {
            var element = new Mock<IElement>();
            element.Setup(e => e.Exists).Returns(false);

            ((Action)(() => (element.Object).Should().Exist())).Should().Throw<XunitException>();
        }

        [Fact]
        public void NotExist_Ok()
        {
            var element = new Mock<IElement>();
            element.Setup(e => e.Exists).Returns(false);

            ((Action)(() => (element.Object).Should().NotExist())).Should().NotThrow();
        }

        [Fact]
        public void NotExist_Fail()
        {
            var element = new Mock<IElement>();
            element.Setup(e => e.Exists).Returns(true);

            ((Action)(() => (element.Object).Should().NotExist())).Should().Throw<XunitException>();
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("abc")]
        public void HaveValue_Ok(string v)
        {
            var element = new Mock<IElement>();
            element.Setup(e => e.Value).Returns(v);

            ((Action)(() => (element.Object).Should().HaveValue(v))).Should().NotThrow();
        }

        [Theory]
        [InlineData("a", "b")]
        [InlineData("a", "")]
        [InlineData("a", null)]
        [InlineData("", "b")]
        [InlineData(null, "b")]
        public void HaveValue_Fail(string v, string v1)
        {
            var element = new Mock<IElement>();
            element.Setup(e => e.Value).Returns(v);

            ((Action)(() => (element.Object).Should().HaveValue(v1))).Should().Throw<XunitException>();
        }

        [Theory]
        [InlineData("a", "b")]
        [InlineData("a", "")]
        [InlineData("a", null)]
        [InlineData("", "b")]
        [InlineData(null, "b")]
        public void NotHaveValue_Ok(string v, string v1)
        {
            var element = new Mock<IElement>();
            element.Setup(e => e.Value).Returns(v);

            ((Action)(() => (element.Object).Should().NotHaveValue(v1))).Should().NotThrow();
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("abc")]
        public void NotHaveValue_Fail(string v)
        {
            var element = new Mock<IElement>();
            element.Setup(e => e.Value).Returns(v);

            ((Action)(() => (element.Object).Should().NotHaveValue(v))).Should().Throw<XunitException>();
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        [InlineData(null)]
        public void IsChecked_Ok(bool? v)
        {
            var element = new Mock<IElement>();
            element.Setup(e => e.Checked).Returns(v);

            ((Action)(() => (element.Object).Should().IsChecked(v))).Should().NotThrow();
        }

        [Theory]
        [InlineData(true, false)]
        [InlineData(false, true)]
        [InlineData(null, true)]
        [InlineData(null, false)]
        [InlineData(false, null)]
        public void IsChecked_Fail(bool? v1, bool? v2)
        {
            var element = new Mock<IElement>();
            element.Setup(e => e.Checked).Returns(v1);

            ((Action)(() => (element.Object).Should().IsChecked(v2))).Should().Throw<XunitException>();
        }

        [Fact]
        public void Match_Ok()
        {
            var element = new Mock<IElement>();
            ((Action)(() => (element.Object).Should().Match(_ => true))).Should().NotThrow();
        }

        [Fact]
        public void Match_Fail()
        {
            var element = new Mock<IElement>();
            ((Action)(() => (element.Object).Should().Match(_ => false))).Should().Throw<XunitException>();
        }

        [Fact]
        public void NotMatch_Ok()
        {
            var element = new Mock<IElement>();
            ((Action)(() => (element.Object).Should().NotMatch(_ => false))).Should().NotThrow();
        }

        [Fact]
        public void NotMatch_Fail()
        {
            var element = new Mock<IElement>();
            ((Action)(() => (element.Object).Should().NotMatch(_ => true))).Should().Throw<XunitException>();
        }

        [Theory]
        [InlineData("property1", "abcd")]
        [InlineData("property2", 123)]
        [InlineData("property3", true)]
        public void HaveProperty_Ok<T>(string name, T value)
        {
            var element = new Mock<IElement>();
            element.Setup(e => e.GetProperty<T>(It.Is<string>(n => n == name))).Returns(value);
            element.Setup(e => e.GetProperty<T>(It.Is<string>(n => n != name))).Returns(default(T));

            ((Action)(() => (element.Object).Should().HaveProperty<T>(name, value))).Should().NotThrow();
        }

        [Theory]
        [InlineData("property1", "abcd", "abc")]
        [InlineData("property2", 123, 12)]
        [InlineData("property3", true, false)]
        public void HaveProperty_Fail<T>(string name, T value, T value2)
        {
            var element = new Mock<IElement>();
            element.Setup(e => e.GetProperty<T>(It.Is<string>(n => n == name))).Returns(value);
            element.Setup(e => e.GetProperty<T>(It.Is<string>(n => n != name))).Returns(default(T));

            ((Action)(() => (element.Object).Should().HaveProperty<T>(name, value2))).Should().Throw<XunitException>();
        }

        [Theory]
        [InlineData("property1", "abcd", "abc")]
        [InlineData("property2", 123, 12)]
        [InlineData("property3", true, false)]
        public void NotHaveProperty_Ok<T>(string name, T value, T value2)
        {
            var element = new Mock<IElement>();
            element.Setup(e => e.GetProperty<T>(It.Is<string>(n => n == name))).Returns(value);
            element.Setup(e => e.GetProperty<T>(It.Is<string>(n => n != name))).Returns(default(T));

            ((Action)(() => (element.Object).Should().NotHaveProperty<T>(name, value2))).Should().NotThrow();
        }

        [Theory]
        [InlineData("property1", "abcd")]
        [InlineData("property2", 123)]
        [InlineData("property3", true)]
        public void NotHaveProperty_Fail<T>(string name, T value)
        {
            var element = new Mock<IElement>();
            element.Setup(e => e.GetProperty<T>(It.Is<string>(n => n == name))).Returns(value);
            element.Setup(e => e.GetProperty<T>(It.Is<string>(n => n != name))).Returns(default(T));

            ((Action)(() => (element.Object).Should().NotHaveProperty<T>(name, value))).Should().Throw<XunitException>();
        }

        [Theory]
        [InlineData("n1", "value", "value")]
        [InlineData("n1", "123", 123)]
        [InlineData("n1", "123.56", 123.56)]
        [InlineData("n1", "true", true)]
        [InlineData("n1", "false", false)]
        public void Element_GetAttribute_Ok<T>(string name, string value, T expectedValue)
        {
            var element = new Mock<IElement>();
            element.Setup(e => e.GetAttribute(It.Is<string>(n => n == name))).Returns(value);
            element.Setup(e => e.GetAttribute(It.Is<string>(n => n != name))).Returns((string)null);

            var x = (element.Object).GetAttribute<T>(name);
            x.Should().NotBeNull();
            x.Should().BeOfType<T>();
            x.Should().Be(expectedValue);
        }

        [Theory]
        [InlineData("property1", "abcd", "abcd")]
        [InlineData("property2", "123", 123)]
        [InlineData("property3", "true", true)]
        public void HaveAttribute_Ok<T>(string name, string svalue, T value)
        {
            var element = new Mock<IElement>();
            element.Setup(e => e.GetAttribute(It.Is<string>(n => n == name))).Returns(svalue);
            element.Setup(e => e.GetAttribute(It.Is<string>(n => n != name))).Returns((string)null);

            ((Action)(() => (element.Object).Should().HaveAttribute<T>(name, value))).Should().NotThrow();
        }

        [Theory]
        [InlineData("property1", "abcd", "abcd")]
        public void HaveAttribute1_Ok(string name, string svalue, string value)
        {
            var element = new Mock<IElement>();
            element.Setup(e => e.GetAttribute(It.Is<string>(n => n == name))).Returns(svalue);
            element.Setup(e => e.GetAttribute(It.Is<string>(n => n != name))).Returns((string)null);

            ((Action)(() => (element.Object).Should().HaveAttribute(name, value))).Should().NotThrow();
        }

        [Theory]
        [InlineData("property1", "abcd", "abc")]
        public void HaveAttribute1_Fail(string name, string svalue, string value)
        {
            var element = new Mock<IElement>();
            element.Setup(e => e.GetAttribute(It.Is<string>(n => n == name))).Returns(svalue);
            element.Setup(e => e.GetAttribute(It.Is<string>(n => n != name))).Returns((string)null);

            ((Action)(() => (element.Object).Should().HaveAttribute(name, value))).Should().Throw<XunitException>();
        }

        [Theory]
        [InlineData("property1", "abcd", "abc")]
        public void NotHaveAttribute1_Ok(string name, string svalue, string value)
        {
            var element = new Mock<IElement>();
            element.Setup(e => e.GetAttribute(It.Is<string>(n => n == name))).Returns(svalue);
            element.Setup(e => e.GetAttribute(It.Is<string>(n => n != name))).Returns((string)null);

            ((Action)(() => (element.Object).Should().NotHaveAttribute(name, value))).Should().NotThrow();
        }

        [Theory]
        [InlineData("property1", "abcd", "abcd")]
        public void NotHaveAttribute1_Fail(string name, string svalue, string value)
        {
            var element = new Mock<IElement>();
            element.Setup(e => e.GetAttribute(It.Is<string>(n => n == name))).Returns(svalue);
            element.Setup(e => e.GetAttribute(It.Is<string>(n => n != name))).Returns((string)null);

            ((Action)(() => (element.Object).Should().NotHaveAttribute(name, value))).Should().Throw<XunitException>();
        }

        [Theory]
        [InlineData("property1", "abcd", "abc")]
        [InlineData("property2", "123", 124)]
        [InlineData("property2", "true", 124)]
        [InlineData("property3", "true", false)]
        public void HaveAttribute_Fail<T>(string name, string svalue, T value)
        {
            var element = new Mock<IElement>();
            element.Setup(e => e.GetAttribute(It.Is<string>(n => n == name))).Returns(svalue);
            element.Setup(e => e.GetAttribute(It.Is<string>(n => n != name))).Returns((string)null);

            ((Action)(() => (element.Object).Should().HaveAttribute<T>(name, value))).Should().Throw<XunitException>();
        }
    }
}
