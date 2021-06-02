using System;
using System.Text.Json;

namespace Gehtsoft.HtmlAgilityPack.FluentAssertions
{
    /// <summary>
    /// The extension class for JsonElement to apply the assertions
    /// </summary>
    public static class JsonElementExtensions
    {
        /// <summary>
        /// Create the assertions for the element.
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public static JsonElementAssertions Should(this JsonElement element) => new JsonElementAssertions(element);

        internal static bool EqualsTo(this JsonElement element, object value)
        {
            if (element.ValueKind == JsonValueKind.Null)
                return value == null;

            if (element.ValueKind == JsonValueKind.True)
                return value is bool b && b;

            if (element.ValueKind == JsonValueKind.False)
                return value is bool b && !b;

            if (element.ValueKind == JsonValueKind.String)
                return value is string s && s == element.GetString();

            if (element.ValueKind == JsonValueKind.Number)
            {
                if (value is Int32 i32)
                    return i32 == element.GetInt32();

                if (value is Int16 i16)
                    return i16 == element.GetInt16();

                if (value is Int64 i64)
                    return i64 == element.GetInt64();

                if (value is Decimal dec)
                    return dec == element.GetDecimal();

                if (value is double dbl)
                    return dbl == element.GetDouble();

                return false;
            }

            return false;
        }
    }
}
