using System;

namespace Gehtsoft.Webview2.Uitest
{
    /// <summary>
    /// Cookie
    /// </summary>
    public class Cookie
    {
        /// <summary>
        /// The cookie path
        /// </summary>
        public string Path { get; init; }
        /// <summary>
        /// The cookie name
        /// </summary>
        public string Name { get; init; }
        /// <summary>
        /// The cookie value
        /// </summary>
        public string Value { get; init; }
        /// <summary>
        /// The flag indicating whether the cookie is secure
        /// </summary>
        public bool IsSecure { get; init; }
        /// <summary>
        /// The flag indicating whether the cookie is in-session cookie
        /// </summary>
        public bool IsSession { get; init; }
        /// <summary>
        /// The cookie expiration date/time
        /// </summary>
        public DateTime Expires { get; init; }
    }
}
