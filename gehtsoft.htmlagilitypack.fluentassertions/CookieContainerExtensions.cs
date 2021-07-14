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

    public static class CookieContainerExtensions
    {
        public static CookieContainerAssertions Should(this CookieContainer container) => new CookieContainerAssertions(container);
    }
}
