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
    public class Test
    {
        [Fact]
        public void MinimumTest()
        {
            using var f = new WebBrowserDriver();
            f.Start();
            f.Navigate("https://www.google.com");

            f.SetValue(Element.ByName("q"), "gehtsoft");
            f.Click(Element.ByName("btnK"));

            f.WaitFor(d => d.Location().StartsWith("https://www.google.com/search"), 1);

            f.EvaluateXPathBool("count(/html/body//cite[text()='https://gehtsoftusa.com']) > 0").Should().BeTrue();
        }


        [Fact]
        public void CurrentTest()
        {
            using var f = new WebBrowserDriver();
            f.Start();
            f.HasCore.Should().BeTrue();
            f.Show(true);
            f.Navigate("https://www.google.com");

            f.SetValue(Element.ByName("q"), "gehtsoft");
            f.ValueOf<string>(Element.ByName("q"), "value").Should().Be("gehtsoft");

            f.EvaluateXPathString("/html/body//input[@name='btnK']/@name").Should().Be("btnK");
            f.EvaluateXPathNumber("count(/html/body//input[@name='btnK'])").Should().BeGreaterThan(0);
            f.EvaluateXPathBool("count(/html/body//input[@name='btnK']) > 0").Should().BeTrue();

            f.Exists(Element.ByXPath("/html/body//input[@name='btnK']")).Should().BeTrue();
            f.Exists(Element.ByXPath("/html/body//input[@name='q']")).Should().BeTrue();
            f.Exists(Element.ByXPath("/html/body//input[@name='k']")).Should().BeFalse();
            f.Click(Element.ByName("btnK"));

            f.WaitFor(d => d.Location().StartsWith("https://www.google.com/search"), 1);
            
            var s = f.Body();
            s.Should().Contain("https://gehtsoftusa.com/");

            f.EvaluateXPathNumber("count(/html/body//cite[text()='https://gehtsoftusa.com'])").Should().BeGreaterThan(0);
            f.EvaluateXPathBool("count(/html/body//cite[text()='https://gehtsoftusa.com']) > 0").Should().BeTrue();
        }
    }
}
