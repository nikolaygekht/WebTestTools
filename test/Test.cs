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
using System.IO;

namespace webviewtest
{
    public sealed class WebDriverFixture : IDisposable
    {
        public WebBrowserDriver Driver { get; }

        public WebDriverFixture() : this("Cache")
        {
        }

        private WebDriverFixture(string cache)
        {
            Driver = new WebBrowserDriver();
            Driver.CacheFolder = Path.Combine(new FileInfo(this.GetType().Assembly.Location).DirectoryName, cache);
            Driver.EnsureCacheFolder(true);
            Driver.Start();
            Thread.Sleep(10);
        }

        public static WebDriverFixture Create(string cache) => new(cache);

        public void Dispose()
        {
            Driver.Dispose();
        }
    }

    [CollectionDefinition(nameof(WebDriverFixture), DisableParallelization = true)]
    public class WebDriverFixtureCollection : ICollectionFixture<WebDriverFixture> { }

    [Collection(nameof(WebDriverFixture))]
    public class Test
    {
        private readonly WebDriverFixture mDriver;

        public Test(WebDriverFixture fixture)
        {
            mDriver = fixture;
        }

        [Theory]
        [InlineData("gehtsoft", "https://gehtsoftusa.com")]
        [InlineData("microsoft", "https://www.microsoft.com")]
        public void TestWithScript(string keyword, string url)
        {
            var f = mDriver.Driver;
            f.Navigate("https://www.google.com");
            f.Reload();
            f.WaitFor(d => d.DocumentState == "complete", 10);

            f.ByName["q"].Value = keyword;
            f.ByName["btnK"].Click();
            f.WaitFor(d => d.Location.StartsWith("https://www.google.com/search"), 15);
            f.WaitFor(d => d.DocumentState == "complete", 10);

            f.XPath("count(/html/body//cite[text()='"+ url + "']) > 0").Should().BeTrue();
        }

        [Fact]
        public void CurrentTest()
        {
            var f = mDriver.Driver;

            f.Navigate("https://www.google.com");
            f.Reload();

            f.ByName["q"]
                .Should().Exist()
                .And.BeHtmlTag("input");

            f.ByName["q"].Value = "gehtsoft";

            f.ByName["q"].Should().HaveValue("gehtsoft")
                    .And.HaveProperty("value", "gehtsoft")
                    .And.HaveAttribute("maxlength", "2048");

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

            f.WaitFor(d => d.Location.StartsWith("https://www.google.com/search"), 5);

            f.ByXPath["/html/body"].Exists.Should().BeTrue();

            var s = f.ByXPath["/html/body"].OuterHTML;
            s.Should().Contain("https://gehtsoftusa.com/");

            f.XPath("count(/html/body//cite[text()='https://gehtsoftusa.com'])").AsNumber.Should().BeGreaterThan(0);
            f.XPath("count(/html/body//cite[text()='https://gehtsoftusa.com']) > 0").AsBoolean.Should().BeTrue();
        }
    }
}
