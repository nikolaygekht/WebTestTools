using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Gehtsoft.Webview2.Uitest;
using Newtonsoft.Json;
using Xunit;

namespace webviewtest
{
    public class Test
    {
        [Fact]
        public void MinimumTest()
        {
            using var f = new WebBrowserDriver();
            f.Start();
            f.Navigate("https://www.google.com");

            f.ByName["q"].Value = "gehtsoft";
            f.ByName["btnK"].Click();

            f.WaitFor(d => d.Location.StartsWith("https://www.google.com/search"), 1);

            f.XPath("count(/html/body//cite[text()='https://gehtsoftusa.com']) > 0").AsBoolean.Should().BeTrue();
        }

        [Fact]
        public void CurrentTest()
        {
            using var f = new WebBrowserDriver();
            f.Start();
            f.HasCore.Should().BeTrue();
            f.Show(true);
            f.Navigate("https://www.google.com");

            f.ByName["q"].Value = "gehtsoft";
            f.ByName["q"].Value.Should().Be("gehtsoft");
            f.ByName["q"].GetProperty<string>("value").Should().Be("gehtsoft");

            f.XPath("/html/body//input[@name='btnK']/@name").AsString.Should().Be("btnK");
            f.XPath("count(/html/body//input[@name='btnK'])").AsNumber.Should().BeGreaterThan(0);
            f.XPath("count(/html/body//input[@name='btnK']) > 0").AsBoolean.Should().BeTrue();

            f.ByXPath["/html/body//input[@name='btnK']"].Exists.Should().BeTrue();

            f.ByXPath["/html/body//input[@name='q']"].Exists.Should().BeTrue();
            f.XPath("/html/body//input[@name='q']").AsElement.Exists.Should().BeTrue();

            f.ByXPath["/html/body//input[@name='k']"].Exists.Should().BeFalse();
            f.XPath("/html/body//input[@name='k']").AsElement.Exists.Should().BeFalse();

            f.ByName["btnK"].Click();

            f.WaitFor(d => d.Location.StartsWith("https://www.google.com/search"), 1);

            f.ByXPath["/html/body"].Exists.Should().BeTrue();

            var s = f.ByXPath["/html/body"].OuterHTML;
            s.Should().Contain("https://gehtsoftusa.com/");

            f.XPath("count(/html/body//cite[text()='https://gehtsoftusa.com'])").AsNumber.Should().BeGreaterThan(0);
            f.XPath("count(/html/body//cite[text()='https://gehtsoftusa.com']) > 0").AsBoolean.Should().BeTrue();
        }
    }
}
