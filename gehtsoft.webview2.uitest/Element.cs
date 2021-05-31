using System;
using System.Linq;
using System.Text;

namespace Gehtsoft.Webview2.Uitest
{
    /// <summary>
    /// <para>The element specification</para>
    /// <para>The class is used to refer an element in <see cref="WebBrowserDriverExtensions"/> methods.</para>
    /// <para>You can create an instance of this element using <see cref="WebBrowserDriverExtensions.ElementById(WebBrowserDriver, string)"/>,
    /// <see cref="WebBrowserDriverExtensions.ElementByName(WebBrowserDriver, string, int)"/>, and
    /// <see cref="<see cref="WebBrowserDriverExtensions.ElementByPath(WebBrowserDriver, string)"/></para>
    /// </summary>
    public sealed class Element : IFormattable
    {
        /// <summary>
        /// Locator types
        /// </summary>
        public enum LocatorTypes
        {
            Name,
            Id,
            XPath,
            Class,
        }

        public LocatorTypes LocatorType { get; }

        /// <summary>
        /// The locator
        /// </summary>
        public string Locator { get; }

        /// <summary>
        /// Index if locator should select specific element from the ordered list returned by the locator expression.
        /// </summary>
        public int? Index { get; }

        /// <summary>
        /// The driver associated with the element
        /// </summary>
        public WebBrowserDriver Driver { get; }

        internal Element(WebBrowserDriver driver, LocatorTypes locatorType, string locator, int? index = null)
        {
            Driver = driver;
            LocatorType = locatorType;
            Locator = locator;
            Index = index;
        }

        /// <summary>
        /// Creates a JScript expression to find the element
        /// </summary>
        /// <returns></returns>
        internal string CreateAccessor()
        {
            return LocatorType switch
            {
                LocatorTypes.Id => $"document.getElementById('{Locator}')",
                LocatorTypes.Name => $"document.getElementsByName('{Locator}')[{Index ?? 0}]",
                LocatorTypes.Class => $"document.getElementsByClassName('{Locator}')[{Index ?? 0}]",
                LocatorTypes.XPath => $"((document.evaluate(\"{Locator}\", document)).iterateNext())",
                _ => throw new InvalidOperationException($"Unsupported locator type {LocatorType}")
            };
        }

        /// <summary>
        /// Creates a JScript expression to check whether the element exists
        /// </summary>
        /// <returns></returns>
        internal string CheckIfExists()
        {
            return LocatorType switch
            {
                LocatorTypes.Id => $"document.getElementById('{Locator}') != null",
                LocatorTypes.Name => $"document.getElementsByName('{Locator}').length > {Index ?? 0}",
                LocatorTypes.Class => $"document.getElementByClassName('{Locator}').length > {Index ?? 0}",
                LocatorTypes.XPath => $"document.evaluate(\"count({Locator})>0\", document, null, XPathResult.BOOLEAN_TYPE).booleanValue",
                _ => throw new InvalidOperationException($"Unsupported locator type {LocatorType}")
            };
        }

        /// <summary>
        /// Returns flag indicating whether the element exists
        /// </summary>
        public bool Exists => Driver.ExecuteScript<bool>(CheckIfExists());

        /// <summary>
        /// <para>Returns the outer HTML</para>
        /// <para>Outer HTML is the content the element including its open and closing tags</para>
        /// </summary>
        public string OuterHTML => Driver.ExecuteScript<string>(CreateAccessor() + ".outerHTML");

        /// <summary>
        /// <para>Returns the element tag name</para>
        /// </summary>
        public string TagName => Driver.ExecuteScript<string>(CreateAccessor() + ".tagName");

        /// <summary>
        /// <para>Returns the element class(es)</para>
        /// </summary>
        public string Class => Driver.ExecuteScript<string>(CreateAccessor() + ".getAttribute('class')");

        /// <summary>
        /// <para>Returns the element style(s)</para>
        /// <para>The method returns only styles applied directly on the element, it won't include styles applied by class</para>
        /// </summary>
        public string Style => Driver.ExecuteScript<string>(CreateAccessor() + ".getAttribute('class')");

        /// <summary>
        /// <para>Returns the inner HTML</para>
        /// <para>Outer HTML is the content the element excluding its open and closing tags</para>
        /// </summary>
        public string InnerHTML => Driver.ExecuteScript<string>(CreateAccessor() + ".innerHTML");

        /// <summary>
        /// <para>Returns the inner text</para>
        /// </summary>
        public string InnerText => Driver.ExecuteScript<string>(CreateAccessor() + ".innerText");

        /// <summary>
        /// Gets or sets the value of the element
        /// </summary>
        public string Value
        {
            get => Driver.ExecuteScript<string>(CreateAccessor() + ".value");
            set
            {
                if (value == null)
                    Driver.ExecuteScript<string>($"{CreateAccessor()}.value = null");
                else
                {
                    if (value.Contains('\''))
                        value = value.Replace("\'", "\\\'");
                    Driver.ExecuteScript($"{CreateAccessor()}.value = '{value}'");
                }
            }
        }

        /// <summary>
        /// Gets or sets the checked flag of the element
        /// </summary>
        public bool? Checked
        {
            get => Driver.ExecuteScript<bool?>(CreateAccessor() + ".checked");
            set
            {
                if (value == null)
                    Driver.ExecuteScript<string>($"{CreateAccessor()}.checked = null");
                else
                    Driver.ExecuteScript($"{CreateAccessor()}.checked = {(value.Value ? "true" : "false")}");
            }
        }

        public string GetAttribute(string propertyName) => Driver.ExecuteScript<string>($"{CreateAccessor()}.getAttribute('{propertyName}')");

        public T GetProperty<T>(string propertyName) => Driver.ExecuteScript<T>($"{CreateAccessor()}.{propertyName}");

        public void Click() => Driver.ExecuteScript(CreateAccessor() + ".click()");

        public override string ToString() => ToString(null, null);

        public string ToString(string format, IFormatProvider formatProvider)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("element[")
              .Append(LocatorType)
              .Append('=')
              .Append(Locator);

            if (Index != null)
            {
                sb.Append('[')
                 .Append(Index.Value)
                 .Append(']');
            }

            sb.Append(']');
            return sb.ToString();
        }
    }
}
