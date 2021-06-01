using System;

namespace Gehtsoft.Webview2.Uitest
{
    public class Cookie
    {
        public string Path { get; init; }
        public string Name { get; init; }
        public string Value { get; init; }
        public bool IsSecure { get; init; }
        public bool IsSession { get; init; }
        public DateTime Expires { get; init; }
    }
}
