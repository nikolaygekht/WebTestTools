using System.Linq;
using System.Text.RegularExpressions;
using FluentAssertions;
using FluentAssertions.Execution;
using FluentAssertions.Primitives;
using Gehtsoft.Webview2.Uitest;

namespace Gehtsoft.Webview2.FluentAssertions
{
    public class WebBrowserDriverAssertions : ReferenceTypeAssertions<WebBrowserDriver, WebBrowserDriverAssertions>
    {
        protected override string Identifier => "driver";

        public WebBrowserDriverAssertions(WebBrowserDriver subject) : base(subject)
        {
        }

        public AndConstraint<WebBrowserDriverAssertions> BeInitialized(string because = null, params object[] becauseParameters)
        {
            Execute.Assertion
               .BecauseOf(because, becauseParameters)
               .Given(() => Subject)
               .ForCondition(driver => driver.HasCore)
               .FailWith("Expected {context:driver} to be initialized but it isn't");
            return new AndConstraint<WebBrowserDriverAssertions>(this);
        }

        public AndConstraint<WebBrowserDriverAssertions> HaveCookie(string cookieName, string cookieUri, string because = null, params object[] becauseParameters)
        {
            Execute.Assertion
               .BecauseOf(because, becauseParameters)
               .Given(() => Subject)
               .ForCondition(driver =>
               {
                   var cookies = driver.GetCookies(cookieUri);
                   return cookies.Any(cookie => cookie.Name == cookieName);
               })
               .FailWith("Expected {context:driver} to be have cookie {0}:{1} but it does not", cookieUri, cookieName);
            return new AndConstraint<WebBrowserDriverAssertions>(this);
        }

        public AndConstraint<WebBrowserDriverAssertions> HaveCookie(string cookieName, string value, string cookieUri, string because = null, params object[] becauseParameters)
        {
            Execute.Assertion
               .BecauseOf(because, becauseParameters)
               .Given(() => Subject)
               .ForCondition(driver =>
               {
                   var cookies = driver.GetCookies(cookieUri);
                   return cookies.Any(cookie => cookie.Name == cookieName && cookie.Value == value);
               })
               .FailWith("Expected {context:driver} to be have no cookie {0}:{1}={2} but it has", cookieUri, cookieName, value);
            return new AndConstraint<WebBrowserDriverAssertions>(this);
        }

        public AndConstraint<WebBrowserDriverAssertions> HaveNoCookie(string cookieName, string cookieUri, string because = null, params object[] becauseParameters)
        {
            Execute.Assertion
               .BecauseOf(because, becauseParameters)
               .Given(() => Subject)
               .ForCondition(driver =>
               {
                   var cookies = driver.GetCookies(cookieUri);
                   return !cookies.Any(cookie => cookie.Name == cookieName);
               })
               .FailWith("Expected {context:driver} to be have cookie {0}:{1} but it does not", cookieUri, cookieName);
            return new AndConstraint<WebBrowserDriverAssertions>(this);
        }

        public AndConstraint<WebBrowserDriverAssertions> HaveNoCookie(string cookieName, string value, string cookieUri, string because = null, params object[] becauseParameters)
        {
            Execute.Assertion
               .BecauseOf(because, becauseParameters)
               .Given(() => Subject)
               .ForCondition(driver =>
               {
                   var cookies = driver.GetCookies(cookieUri);
                   return !cookies.Any(cookie => cookie.Name == cookieName && cookie.Value == value);
               })
               .FailWith("Expected {context:driver} to be have no cookie {0}:{1}={2} but it has", cookieUri, cookieName, value);
            return new AndConstraint<WebBrowserDriverAssertions>(this);
        }
    }
}

