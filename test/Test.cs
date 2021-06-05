using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Newtonsoft.Json;
using Xunit;

namespace webviewtest
{
    [Collection(nameof(WebBrowserDriverFixture))]
    public class Test
    {
        private readonly WebBrowserDriver mDriver;

        public Test(WebBrowserDriverFixture driver)
        {
            mDriver = driver.Driver;
        }

        [Fact]
        public void MinimumTest()
        {
            mDriver.Navigate("https://www.google.com");
            mDriver.WaitFor(d => d.DocumentState == "complete", 5);

            mDriver.SetValue(Element.ByName("q"), "gehtsoft");
            mDriver.Click(Element.ByName("btnK"));

            mDriver.WaitFor(d => d.Location().StartsWith("https://www.google.com/search") && d.DocumentState == "complete", 5);

            mDriver.EvaluateXPathBool("count(/html/body//cite[text()='https://gehtsoftusa.com']) > 0").Should().BeTrue();
        }

        [Fact]
        public void CurrentTest()
        {
            mDriver.Navigate("https://www.google.com");
            mDriver.WaitFor(d => d.DocumentState == "complete", 5);

            mDriver.SetValue(Element.ByName("q"), "gehtsoft");
            mDriver.ValueOf<string>(Element.ByName("q"), "value").Should().Be("gehtsoft");

            mDriver.EvaluateXPathString("/html/body//input[@name='btnK']/@name").Should().Be("btnK");
            mDriver.EvaluateXPathNumber("count(/html/body//input[@name='btnK'])").Should().BeGreaterThan(0);
            mDriver.EvaluateXPathBool("count(/html/body//input[@name='btnK']) > 0").Should().BeTrue();

            mDriver.Exists(Element.ByXPath("/html/body//input[@name='btnK']")).Should().BeTrue();
            mDriver.Exists(Element.ByXPath("/html/body//input[@name='q']")).Should().BeTrue();
            mDriver.Exists(Element.ByXPath("/html/body//input[@name='k']")).Should().BeFalse();
            mDriver.Click(Element.ByName("btnK"));

            mDriver.WaitFor(d => d.Location().StartsWith("https://www.google.com/search"), 5);
            mDriver.WaitFor(d => d.DocumentState == "complete", 5);

            var s = mDriver.Content;
            s.Should().Contain("https://gehtsoftusa.com/");

            mDriver.EvaluateXPathNumber("count(/html/body//cite[text()='https://gehtsoftusa.com'])").Should().BeGreaterThan(0);
            mDriver.EvaluateXPathBool("count(/html/body//cite[text()='https://gehtsoftusa.com']) > 0").Should().BeTrue();
        }
    }
}
