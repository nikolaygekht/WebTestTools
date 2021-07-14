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
    /// Assertions to be applied on HtmlNode class.
    /// </summary>
    public class CookieContainerAssertions : ReferenceTypeAssertions<CookieContainer, CookieContainerAssertions>
    {
        /// <summary>
        /// The reference to the node to which the assertion is applied.
        /// </summary>
        public CookieContainer Node => Subject;

        internal CookieContainerAssertions(CookieContainer subject) : base(subject)
        {
        }

        /// <summary>
        /// The element identifier
        /// </summary>
        protected override string Identifier => "cookies";

        /// <summary>
        /// Asserts that the node exists.
        /// </summary>
        /// <param name="because"></param>
        /// <param name="becauseParameters"></param>
        /// <returns></returns>
        public AndConstraint<CookieContainerAssertions> HaveCookie(string cookie, string url = "https://localhost", string because = null, params object[] becauseParameters)
        {
            Execute.Assertion
                .BecauseOf(because, becauseParameters)
                .Given(() => Subject)
                .ForCondition(container =>
                {
                    var cookies = container.GetCookies(new Uri(url));
                    if (cookies == null || cookies.Count == 0)
                        return false;
                    for (int i = 0; i < cookies.Count; i++)
                        if (cookies[i].Name == cookie)
                            return true;

                    return false;
                })
                .FailWith("Expected {context:node} to exist but it doesn't");
            return new AndConstraint<CookieContainerAssertions>(this);
        }
    }
}
