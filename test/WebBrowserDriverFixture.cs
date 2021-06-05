using System;
using System.IO;

namespace webviewtest
{
    public sealed class WebBrowserDriverFixture : IDisposable
    {
        public WebBrowserDriver Driver { get; }

        public WebBrowserDriverFixture()
        {
            string cacheFolder = Path.Combine((new FileInfo(typeof(Test).Assembly.Location)).DirectoryName, "Cache");
            if (Directory.Exists(cacheFolder))
                Directory.Delete(cacheFolder, true);
            Directory.CreateDirectory(cacheFolder);

            Driver = new WebBrowserDriver()
            {
                CacheFolder = cacheFolder
            };
            Driver.Start();
        }

        public void Dispose() => Driver.Dispose();
    }
}
