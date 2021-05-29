using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace webviewtest
{
    public static class WebBrowserDriverExtensions
    {
        /// <summary>
        /// Evaluates an XPath value that returns a string
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="xpath"></param>
        /// <returns></returns>
        public static string EvaluateXPathString(this WebBrowserDriver driver, string xpath) => driver.ExecuteScript<string>($"document.evaluate(\"{xpath}\", document, null, XPathResult.STRING_TYPE).stringValue");

        /// <summary>
        /// Evaluates an XPath value that returns a number
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="xpath"></param>
        /// <returns></returns>
        public static double EvaluateXPathNumber(this WebBrowserDriver driver, string xpath) => driver.ExecuteScript<double>($"document.evaluate(\"{xpath}\", document, null, XPathResult.NUMBER_TYPE).numberValue");

        /// <summary>
        /// Evaluates an XPath value that returns a boolean value
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="xpath"></param>
        /// <returns></returns>
        public static bool EvaluateXPathBool(this WebBrowserDriver driver, string xpath) => driver.ExecuteScript<bool>($"document.evaluate(\"{xpath}\", document, null, XPathResult.BOOLEAN_TYPE).booleanValue");

        /// <summary>
        /// Returns a body of the document
        /// </summary>
        /// <param name="driver"></param>
        /// <returns></returns>
        public static string Body(this WebBrowserDriver driver) => driver.ExecuteScript<string>("document.body.outerHTML");

        /// <summary>
        /// Return the current location (URL)
        /// </summary>
        /// <param name="driver"></param>
        /// <returns></returns>
        public static string Location(this WebBrowserDriver driver) => driver.ExecuteScript<string>("document.location.href");

        /// <summary>
        /// Checks whether the element specified exists.
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="locator"></param>
        /// <returns></returns>
        public static bool Exists(this WebBrowserDriver driver, Element locator) => driver.ExecuteScript<bool>(locator.CheckIfExists());

        /// <summary>
        /// Returns the value of the element as a string
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="locator"></param>
        /// <returns></returns>
        public static string ValueOf(this WebBrowserDriver driver, Element locator) => driver.ExecuteScript<string>(locator.CreateAccessor() + ".value");

        /// <summary>
        /// Returns the value of the element's property as a type specified
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="driver"></param>
        /// <param name="locator"></param>
        /// <param name="property"></param>
        /// <returns></returns>
        public static T ValueOf<T>(this WebBrowserDriver driver, Element locator, string property) => driver.ExecuteScript<T>(locator.CreateAccessor() + "." + property);

        /// <summary>
        /// Returns the element in HTML form
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="locator"></param>
        /// <returns></returns>
        public static string Body(this WebBrowserDriver driver, Element locator) => driver.ExecuteScript<string>(locator.CreateAccessor() + ".outerHTML");

        /// <summary>
        /// Returns the element "text" attribute
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="locator"></param>
        /// <returns></returns>
        public static string Text(this WebBrowserDriver driver, Element locator) => driver.ExecuteScript<string>(locator.CreateAccessor() + ".text");

        /// <summary>
        /// Sets the element value
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="locator"></param>
        /// <param name="value"></param>
        public static void SetValue(this WebBrowserDriver driver, Element locator, string value) => driver.ExecuteScriptRaw($"{locator.CreateAccessor()}.value = '{value}'");

        /// <summary>
        /// Clicks on the element
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="locator"></param>
        public static void Click(this WebBrowserDriver driver, Element locator) => driver.ExecuteScriptRaw($"{locator.CreateAccessor()}.click()");

        /// <summary>
        /// Waits until the predicate returns true
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="predicate"></param>
        /// <param name="timeoutInSeconds"></param>
        public static void WaitFor(this WebBrowserDriver driver, Func<WebBrowserDriver, bool> predicate, double? timeoutInSeconds = null)
        {
            DateTime start = DateTime.Now;
            TimeSpan? waitTime = null;
            if (timeoutInSeconds != null)
                waitTime = TimeSpan.FromSeconds((double)timeoutInSeconds);

            while (!predicate(driver))
            {
                Thread.Sleep(1);
                if (waitTime != null && (DateTime.Now - start) > waitTime.Value)
                    throw new TimeoutException();
            }
        }

        /// <summary>
        /// Wait while the predicate returns true.
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="predicate"></param>
        /// <param name="timeoutInSeconds"></param>
        public static void WaitUntil(this WebBrowserDriver driver, Func<WebBrowserDriver, bool> predicate, double? timeoutInSeconds = null)
            => WaitFor(driver, (d) => !predicate(d), timeoutInSeconds);
    }
}
