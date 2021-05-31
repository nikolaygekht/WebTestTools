using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gehtsoft.Webview2.Uitest
{
    public class XPath : IFormattable
    {
        private readonly WebBrowserDriver mDriver;
        private readonly string mExpression;

        public Element AsElement => new Element(mDriver, Element.LocatorTypes.XPath, mExpression);

        public string AsString => mDriver.ExecuteScript<string>($"document.evaluate(\"{mExpression}\", document, null, XPathResult.STRING_TYPE).stringValue");

        public double AsNumber => mDriver.ExecuteScript<double>($"document.evaluate(\"{mExpression}\", document, null, XPathResult.NUMBER_TYPE).numberValue");

        public bool AsBoolean => mDriver.ExecuteScript<bool>($"document.evaluate(\"{mExpression}\", document, null, XPathResult.BOOLEAN_TYPE).booleanValue");

        internal XPath(WebBrowserDriver driver, string expression)
        {
            mDriver = driver;
            mExpression = expression;
        }

        public override string ToString() => mExpression;

        public string ToString(string format, IFormatProvider formatProvider) => mExpression;
    }
}
