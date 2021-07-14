using System;
using System.Collections.Generic;
using System.Net.Http;

namespace Gehtsoft.Httpclient.Test.Extensions
{
    /// <summary>
    /// Helper for form class
    /// </summary>
    public static class HttpClientHelper
    {
        /// <summary>
        /// Creates HTTP form content from the list of pairs string -> value
        /// </summary>
        /// <param name="formContent"></param>
        /// <returns></returns>
        public static HttpContent CreateContent(params string[] formContent)
        {
            if (formContent == null || formContent.Length == 0)
                throw new ArgumentNullException(nameof(formContent));

            if (formContent.Length % 2 != 0)
                throw new ArgumentException("The values must be passed as pairs of string. First is the name, and the second is the value", nameof(formContent));

            KeyValuePair<string, string>[] originalContent = new KeyValuePair<string, string>[formContent.Length / 2];
            for (int i = 0; i < originalContent.Length; i++)
                originalContent[i] = new KeyValuePair<string, string>(formContent[i * 2], formContent[i * 2 + 1]);

            return new FormUrlEncodedContent(originalContent);
        }
    }
}
