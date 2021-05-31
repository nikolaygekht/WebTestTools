using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Gehtsoft.Webview2.Uitest;
using Gehtsoft.Webview2.FluentAssertions;
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
            f.Should().BeInitialized();
            f.Navigate("https://www.google.com");
            f.ByName["q"].Value = "gehtsoft";
            f.ByName["btnK"].Click();
            f.WaitFor(d => d.Location.StartsWith("https://www.google.com/search"), 1);
            f.XPath("count(/html/body//cite[text()='https://gehtsoftusa.com']) > 0").Should().BeTrue();
        }

        [Fact]
        public void CurrentTest()
        {
            using var f = new WebBrowserDriver();
            f.Start();
            f.HasCore.Should().BeTrue();
            f.Show(true);
            f.Navigate("https://www.google.com");

            f.ByName["q"]
                .Should().Exist()
                .And.BeHtmlTag("input");

            f.ByName["q"].Value = "gehtsoft";

            f.ByName["q"].Should().HaveValue("gehtsoft")
                    .And.HaveProperty("value", "gehtsoft")
                    .And.HaveAttribute("maxlength", "2048")
                    .And.HaveAttribute<int>("maxlength", 2048);

            var cookies = f.GetCookies(null);

            f.ByXPath["/html/head/title"].Should().ContainText("google", StringComparison.OrdinalIgnoreCase);

            f.XPath("/html/body//input[@name='btnK']/@name").Should().Be("btnK");
            f.XPath("count(/html/body//input[@name='btnK'])").Should().BeGreaterThan(1);
            f.XPath("count(/html/body//input[@name='btnK']) > 0").Should().BeTrue();

            f.ByXPath["/html/body//input[@name='btnK']"].Should().Exist();

            f.ByXPath["/html/body//input[@name='q']"].Should().Exist();
            f.XPath("/html/body//input[@name='q']").Should().ReturnElement();

            f.ByXPath["/html/body//input[@name='k']"].Should().NotExist();
            f.XPath("/html/body//input[@name='k']").Should().NotReturnElement();

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
