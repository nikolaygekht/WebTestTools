using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Gehtsoft.Httpclient.Test.Extensions;
using FluentAssertions;
using System.Net;

namespace Gehtsoft.HtmlAgilityPack.FluentAssertions.Test
{
    [Collection(nameof(ServerFixture))]
    public class ServerTest
    {
        private readonly ServerFixture mServerFixture;

        public ServerTest(ServerFixture serverFixture)
        {
            mServerFixture = serverFixture;
        }

        [Fact]
        public async Task TestIndex()
        {
            var doc = await mServerFixture.Client.GetAsync("/").AsHtmlAsync();
            doc.Select("//h1").Should().Exist()
                .And.Node.Should().ContainText("Hello!");
        }

        [Fact]
        public async Task GetSave()
        {
            var rq = await mServerFixture.Client.GetAsync("/Home/Save?value=123");
            rq.StatusCode.Should().Be(HttpStatusCode.Redirect);
            rq.Headers.Location.Should().Be("/Home/Load");

            var cookies = rq.GetCookies();
            cookies.Should().HaveCookie("Gehtsoft.Test.WebApp.Session", "https://localhost");

            rq = await mServerFixture.Client.GetAsync("/Home/Load", cookies);
            rq.StatusCode.Should().Be(HttpStatusCode.OK);
            var doc = await rq.AsHtmlAsync();
            doc.SelectById("value").Should().Exist()
                .And.Node.Should().ContainText("123");

        }

        [Fact]
        public async Task PostSave()
        {
            var rq = await mServerFixture.Client.PostFormAsync("/Home/PostSave", "value", "123");
            rq.StatusCode.Should().Be(HttpStatusCode.Redirect);
            rq.Headers.Location.Should().Be("/Home/Load");

            var cookies = rq.GetCookies();

            rq = await mServerFixture.Client.PostFormAsync("/Home/PostAdd", cookies, "value", "456");
            rq.StatusCode.Should().Be(HttpStatusCode.Redirect);
            rq.Headers.Location.Should().Be("/Home/Load");

            rq = await mServerFixture.Client.GetAsync("/Home/Load", cookies);
            
            rq.StatusCode.Should().Be(HttpStatusCode.OK);
            var doc = await rq.AsHtmlAsync();

            doc.SelectById("value").Should().Exist()
                .And.Node.Should().ContainText("123456");

        }

    }
}
