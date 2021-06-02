namespace Gehtsoft.Webview2.Uitest
{
    /// <summary>
    /// Controller to access element by its class
    /// </summary>
    public class ElementByClassAccessor
    {
        private readonly WebBrowserDriver mDriver;

        internal ElementByClassAccessor(WebBrowserDriver driver)
        {
            mDriver = driver;
        }

        /// <summary>
        /// Gets the specified element by its class and index among the elements that belong to the same class
        /// </summary>
        /// <param name="name"></param>
        /// <param name="index"></param>
        public IElement this[string name, int index = 0] => new Element(mDriver, Element.LocatorTypes.Class, name, index);
    }
}
