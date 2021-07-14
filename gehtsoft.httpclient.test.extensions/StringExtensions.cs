using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Encodings.Web;

namespace Gehtsoft.Httpclient.Test.Extensions
{
    public static class StringExtensions
    {
        /// <summary>
        /// Add URL parameters to URL string
        /// </summary>
        /// <param name="url"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static string AddUrlParams(this string url, params string[] parameters)
        {
            if (parameters == null || parameters.Length == 0)
                return url;

            if (parameters.Length % 2 != 0)
                throw new ArgumentException("The parameters must be passed as pairs of string. First is the name, and the second is the value", nameof(parameters));

            StringBuilder sb = new StringBuilder();
            sb.Append(url)
                .Append('?');

            for (int i = 0; i < parameters.Length; i += 2)
            {
                if (i > 0)
                    sb.Append('&');

                sb.Append(UrlEncoder.Default.Encode(parameters[i]))
                    .Append('=')
                    .Append(UrlEncoder.Default.Encode(parameters[i + 1]));
            }
            return sb.ToString();
        }
    }
}
