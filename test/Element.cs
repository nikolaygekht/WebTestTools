using System;

namespace webviewtest
{
    /// <summary>
    /// The element specification 
    /// 
    /// The class is used to refer an element in <see cref="WebBrowserDriverExtensions"/> methods.
    /// 
    /// Create an instance of this class using <see cref="ById"/>, <see cref="ByName"/> or <see cref="ByXPath(string)"/> methods. 
    /// </summary>
    public sealed class Element
    {
        /// <summary>
        /// Locator types
        /// </summary>
        public enum LocatorTypes
        {
            Name,
            Id,
            XPath,
        }

        public LocatorTypes LocatorType { get; }

        /// <summary>
        /// The locator 
        /// </summary>
        public string Locator { get; }

        /// <summary>
        /// Index if locator should select specific element from the order list returned by the locator expression.
        /// </summary>
        public int? Index { get; }

        private Element(LocatorTypes locatorType, string locator, int? index = null)
        {
            LocatorType = locatorType;
            Locator = locator;
            Index = index;
        }

        /// <summary>
        /// Find element by its name attribute. 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static Element ByName(string name, int index = 0) => new Element(LocatorTypes.Name, name, index);
        /// <summary>
        /// Find element by its id attribute. 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static Element ById(string id) => new Element(LocatorTypes.Id, id);
        /// <summary>
        /// Find element by xpath specified. 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="index"></param>
        /// <returns></returns>

        public static Element ByXPath(string expression) => new Element(LocatorTypes.XPath, expression);

        /// <summary>
        /// Creates a JScript expression to find the element
        /// </summary>
        /// <returns></returns>
        internal string CreateAccessor()
        {
            return LocatorType switch
            {
                LocatorTypes.Id => $"document.getElementById('{Locator}')",
                LocatorTypes.Name => $"document.getElementsByName('{Locator}')[{Index}]",
                LocatorTypes.XPath => $"document.evaluate(\"{Locator}\", document, null, XPathResult.FIRST_ORDERED_NODE_TYPE)",
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
                LocatorTypes.Name => $"document.getElementsByName('{Locator}').length > 0",
                LocatorTypes.XPath => $"document.evaluate(\"count({Locator})>0\", document, null, XPathResult.BOOLEAN_TYPE).booleanValue",
                _ => throw new InvalidOperationException($"Unsupported locator type {LocatorType}")
            };
        }
    }
}
