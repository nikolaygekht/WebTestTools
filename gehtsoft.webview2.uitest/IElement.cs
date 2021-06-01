namespace Gehtsoft.Webview2.Uitest
{
    public interface IElement
    {
        /// <summary>
        /// Gets or sets element value
        /// </summary>
        string Value { get; set;  }

        /// <summary>
        /// Gets or sets element checked property
        /// </summary>
        bool? Checked { get; set; }

        /// <summary>
        /// Returns flag indicating whether the element exists
        /// </summary>
        bool Exists { get; }

        /// <summary>
        /// <para>Returns the outer HTML</para>
        /// <para>Outer HTML is the content the element including its open and closing tags</para>
        /// </summary>
        string OuterHTML { get; }

        /// <summary>
        /// <para>Returns the element tag name</para>
        /// </summary>
        string TagName { get; }

        /// <summary>
        /// <para>Returns the element class(es)</para>
        /// </summary>
        string Class { get; }

        /// <summary>
        /// <para>Returns the inner HTML</para>
        /// <para>Outer HTML is the content the element excluding its open and closing tags</para>
        /// </summary>
        string InnerHTML { get; }

        /// <summary>
        /// <para>Returns the inner text</para>
        /// </summary>
        string InnerText { get; }

        /// <summary>
        /// Gets attribute value as a string
        /// </summary>
        /// <param name="attributeName"></param>
        /// <returns></returns>
        string GetAttribute(string attributeName);

        /// <summary>
        /// Gets JavaScript property of the specified type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        T GetProperty<T>(string propertyName);

        /// <summary>
        /// Clicks the element
        /// </summary>
        void Click();
    }
}
