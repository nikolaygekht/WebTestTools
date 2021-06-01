using System;

namespace Gehtsoft.Webview2.Uitest
{
    public static class ElementExtension
    {
        /// <summary>
        /// <para>Gets attribute value as the type specicied.</para>
        /// <para>Only `int`, `double`, `string`, and `bool` types are supproted</para>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="attributeName"></param>
        /// <returns></returns>
        public static T GetAttribute<T>(this IElement element, string attributeName)
        {
            string value = element.GetAttribute(attributeName);
            if (string.IsNullOrEmpty(attributeName))
                return default;

            Type t = typeof(T);
            t = Nullable.GetUnderlyingType(t) ?? t;

            if (t == typeof(string))
                return (T)(object)value;
            else if (t == typeof(int))
                return (T)(object)Int32.Parse(value);
            else if (t == typeof(uint))
                return (T)(object)UInt32.Parse(value);
            else if (t == typeof(bool))
                return (T)(object)(value == "true");
            else if (t == typeof(double))
                return (T)(object)Double.Parse(value);
            else if (t == typeof(DateTime))
                return (T)(object)DateTime.Parse(value);

            throw new InvalidCastException($"Type {typeof(T).FullName} is not supported");
        }
    }
}
