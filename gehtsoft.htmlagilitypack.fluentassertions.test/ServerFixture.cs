using FluentAssertions;
using Gehtsoft.Test.WebApp;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Text.Json;
using Xunit;

namespace Gehtsoft.HtmlAgilityPack.FluentAssertions.Test
{
    public sealed class ServerFixture : IDisposable
    {
        private TestServer mTestServer = null;
        private HttpClient mClient = null;
        private bool mInit = false;
        private string mServerFolder = null;

        public HttpClient Client
        {
            get
            {
                if (!mInit)
                    Initialize();
                return mClient;
            }
        }

        public void Dispose()
        {
            mClient?.Dispose();
            mTestServer?.Dispose();
        }

        private void Initialize()
        {
            string solutionFolder = GetSolutionFolder();
            solutionFolder.Should().NotBeNull("You should run the test from solution directory structure");

            string serverFolder = Path.Combine(solutionFolder, "Gehtsoft.Test.WebApp");
            Directory.Exists(serverFolder).Should().BeTrue("The solution directory must consists of folder with Gehtsoft.Test.WebApp project");

            mServerFolder = serverFolder;

            string testFolder = Path.Combine(solutionFolder, "gehtsoft.htmlagilitypack.fluentassertions.test");
            Directory.Exists(serverFolder).Should().BeTrue("The solution directory must consists of folder with gehtsoft.htmlagilitypack.fluentassertions.test project");

            var webHostBuilder = new WebHostBuilder()
                .UseContentRoot(mServerFolder)
                .UseEnvironment("Test")
                .ConfigureAppConfiguration((_, builder) =>
                {
                    builder
                        .SetBasePath(mServerFolder)
                        .AddJsonFile("Config/appsettings.json", optional: true, reloadOnChange: true);
                })
                .UseStartup<Startup>();

            mTestServer = new TestServer(webHostBuilder);
            mClient = mTestServer.CreateClient();
            mInit = true;
        }

        private static string GetSolutionFolder()
        {
            string thisLocation = typeof(ServerFixture).Assembly.Location;
            var directory = new DirectoryInfo(thisLocation);
            while (directory.Parent != null)
            {
                directory = directory.Parent;
                var files = directory.GetFiles();
                if (Array.Find(files, fi => fi.Name == "webviewtest.sln") != null)
                    return directory.FullName;
            }
            return null;
        }
    }

    [CollectionDefinition(nameof(ServerFixture), DisableParallelization = true)]
    public class ServerFixtureCollection : ICollectionFixture<ServerFixture>
    {
    }
}