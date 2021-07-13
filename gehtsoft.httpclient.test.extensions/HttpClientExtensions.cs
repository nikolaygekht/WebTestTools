using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace Gehtsoft.Httpclient.Test.Extensions
{
    /// <summary>
    /// Extensions of HttpClient classes
    /// </summary>
    public static class HttpClientExtensions
    {
        /// <summary>
        /// Read response as a string with optional checking the response type.
        /// </summary>
        /// <param name="response"></param>
        /// <param name="expectedContentType"></param>
        /// <returns></returns>
        public static async Task<string> AsStringAsync(this HttpResponseMessage response, string expectedContentType = null)
        {
            if (!response.IsSuccessStatusCode)
                throw new ArgumentException($"Can't get the URL specified, the code is {response.StatusCode}", nameof(response));
            if (expectedContentType != null && response.Content.Headers.ContentType.MediaType != expectedContentType)
                throw new ArgumentException($"The content type {response.Content.Headers.ContentType.MediaType} does not match to the content type expected", nameof(response));
            return await response.Content.ReadAsStringAsync();
        }

        /// <summary>
        /// Await response and read it as a string with optional checking the response type.
        /// </summary>
        /// <param name="response"></param>
        /// <param name="expectedContentType"></param>
        /// <returns></returns>
        public static async Task<string> AsStringAsync(this Task<HttpResponseMessage> response, string expectedContentType = null) => await AsStringAsync(await response, expectedContentType);

        /// <summary>
        /// Read response as a binary with optional checking the response type.
        /// </summary>
        /// <param name="response"></param>
        /// <param name="expectedContentType"></param>
        /// <returns></returns>
        public static async Task<byte[]> AsBinaryAsync(this HttpResponseMessage response, string expectedContentType)
        {
            if (!response.IsSuccessStatusCode)
                throw new ArgumentException($"Can't get the URL specified, the code is {response.StatusCode}", nameof(response));
            if (expectedContentType != null && response.Content.Headers.ContentType.MediaType != expectedContentType)
                throw new ArgumentException($"The content type {response.Content.Headers.ContentType.MediaType} does not match to the content type expected", nameof(response));
            return await response.Content.ReadAsByteArrayAsync();
        }

        /// <summary>
        /// Await response and read it as a binary with optional checking the response type.
        /// </summary>
        /// <param name="response"></param>
        /// <param name="expectedContentType"></param>
        /// <returns></returns>
        public static async Task<byte[]> AsBinaryAsync(this Task<HttpResponseMessage> response, string expectedContentType = null) => await AsBinaryAsync(await response, expectedContentType);

        /// <summary>
        /// Reads response as a json
        /// </summary>
        /// <param name="response"></param>
        /// <param name="contentType"></param>
        /// <returns></returns>
        public static async Task<JsonElement> AsJsonAsync(this HttpResponseMessage response, string contentType = "application/json")
        {
            var s = await response.AsStringAsync(contentType);
            return JsonSerializer.Deserialize<JsonElement>(s);
        }

        /// <summary>
        /// Awaits response and reads it as a json
        /// </summary>
        /// <param name="response"></param>
        /// <param name="contentType"></param>
        /// <returns></returns>
        public static async Task<JsonElement> AsJsonAsync(this Task<HttpResponseMessage> response, string contentType = "application/json")
            => await AsJsonAsync(await response, contentType);

        /// <summary>
        /// Reads response as an HTML document
        /// </summary>
        /// <param name="response"></param>
        /// <param name="contentType"></param>
        /// <returns></returns>
        public static async Task<HtmlDocument> AsHtmlAsync(this HttpResponseMessage response, string contentType = "text/html")
        {
            var s = await response.AsStringAsync(contentType);
            var d = new HtmlDocument();
            d.LoadHtml(s);
            return d;
        }

        /// <summary>
        /// Awaits response and read it as an HTML document
        /// </summary>
        /// <param name="response"></param>
        /// <param name="contentType"></param>
        /// <returns></returns>
        public static async Task<HtmlDocument> AsHtmlAsync(this Task<HttpResponseMessage> response, string contentType = "text/html")
            => await AsHtmlAsync(await response, contentType);

        /// <summary>
        /// Post form content
        /// </summary>
        /// <param name="client"></param>
        /// <param name="url"></param>
        /// <param name="formContent"></param>
        /// <returns></returns>
        public static Task<HttpResponseMessage> PostFormAsync(this HttpClient client, string url, params string[] formContent)
        {
            if (formContent == null || formContent.Length == 0)
                throw new ArgumentNullException(nameof(formContent));

            if (formContent.Length % 2 != 0)
                throw new ArgumentException("The values must be passed as pairs of string. First is the name, and the second is the value", nameof(formContent));

            KeyValuePair<string, string> [] originalContent = new KeyValuePair<string, string>[formContent.Length / 2];
            for (int i = 0; i < originalContent.Length; i++)
                originalContent[i] = new KeyValuePair<string, string>(formContent[i * 2], formContent[i * 2 + 1]);

            var content = new FormUrlEncodedContent(originalContent);

            return client.PostAsync(url, content);
        }

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
