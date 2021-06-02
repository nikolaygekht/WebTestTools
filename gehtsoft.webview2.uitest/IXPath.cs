namespace Gehtsoft.Webview2.Uitest
{
    /// <summary>
    /// The interface to an xpath expression
    /// </summary>
    public interface IXPath
    {
        /// <summary>
        /// Returns the first item of the selection as an element
        /// </summary>
        IElement AsElement { get; }
        /// <summary>
        /// Returns the expression result as a string
        /// </summary>
        string AsString { get; }
        /// <summary>
        /// Returns the expression result as a number
        /// </summary>
        double AsNumber { get; }
        /// <summary>
        /// Returns the expression result as a boolean value
        /// </summary>
        bool AsBoolean { get; }
    }
}
