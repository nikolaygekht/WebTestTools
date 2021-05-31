namespace Gehtsoft.Webview2.Uitest
{
    public interface IXPath
    {
        IElement AsElement { get; }
        string AsString { get; }
        double AsNumber { get; }
        bool AsBoolean { get; }
    }
}
