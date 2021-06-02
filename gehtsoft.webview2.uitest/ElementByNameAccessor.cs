namespace Gehtsoft.Webview2.Uitest
{
    /// <summary>
    /// Controller to access element by its name
    /// </summary>
    public class ElementByNameAccessor
    {
        private readonly WebBrowserDriver mDriver;

        internal ElementByNameAccessor(WebBrowserDriver driver)
        {
            mDriver = driver;
        }

        /// <summary>
        /// Gets the specified element by its name and index among the elements with the same name
        /// </summary>
        /// <param name="name"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public IElement this[string name, int index = 0] => new Element(mDriver, Element.LocatorTypes.Name, name, index);
    }
}
