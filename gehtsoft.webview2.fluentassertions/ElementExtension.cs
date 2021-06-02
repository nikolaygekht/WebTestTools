using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using FluentAssertions.Execution;
using FluentAssertions.Primitives;
using Gehtsoft.Webview2.Uitest;

namespace Gehtsoft.Webview2.FluentAssertions
{
    /// <summary>
    /// The extension class to create fluent assertions for an element
    /// </summary>
    public static class ElementExtension
    {
        /// <summary>
        /// Create fluent assertions for an element.
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public static ElementAssertions Should(this IElement element) => new ElementAssertions(element);
    }
}
