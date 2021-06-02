namespace Gehtsoft.Webview2.Uitest
{
    /// <summary>
    /// Controller to access element by XPath
    /// </summary>
    public class ElementByXPathAccessor
    {
        private readonly WebBrowserDriver mDriver;

        internal ElementByXPathAccessor(WebBrowserDriver driver)
        {
            mDriver = driver;
        }

        /// <summary>
        /// XPath that access to the element
        /// </summary>
        /// <param name="xpath"></param>
        public IElement this[string xpath] => new Element(mDriver, Element.LocatorTypes.XPath, xpath);
    }
}
