namespace Gehtsoft.Webview2.Uitest
{
    /// <summary>
    /// Controller to access element by its identifier
    /// </summary>
    public class ElementByIdAccessor
    {
        private readonly WebBrowserDriver mDriver;

        internal ElementByIdAccessor(WebBrowserDriver driver)
        {
            mDriver = driver;
        }

        /// <summary>
        /// Gets the specified element by its unique identifier
        /// </summary>
        /// <param name="name"></param>
        public IElement this[string name] => new Element(mDriver, Element.LocatorTypes.Id, name);
    }
}
