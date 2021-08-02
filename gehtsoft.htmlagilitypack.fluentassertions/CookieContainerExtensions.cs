using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using FluentAssertions.Execution;
using FluentAssertions.Primitives;

namespace Gehtsoft.HtmlAgilityPack.FluentAssertions
{

    /// <summary>
    /// Extension for cookie container
    /// </summary>
    public static class CookieContainerExtensions
    {
        /// <summary>
        /// Creates FluentAssertion for the cookie container
        /// </summary>
        /// <param name="container"></param>
        /// <returns></returns>
        public static CookieContainerAssertions Should(this CookieContainer container) => new CookieContainerAssertions(container);
    }
}
