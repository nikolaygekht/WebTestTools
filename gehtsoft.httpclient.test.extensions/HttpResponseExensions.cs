using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace Gehtsoft.Httpclient.Test.Extensions
{
    /// <summary>
    /// HTTP Response extensions
    /// </summary>
    public static class HttpResponseExensions
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
        public static async Task<byte[]> AsBinaryAsync(this HttpResponseMessage response, string expectedContentType = null)
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
        /// Gets cookie by the name
        /// </summary>
        /// <param name="cookieName"></param>
        /// <returns></returns>
        public static CookieContainer GetCookies(this HttpResponseMessage response)
        {
            var cc = new CookieContainer();
            if (response.Headers.TryGetValues("set-cookie", out IEnumerable<string> setcookies))
            {
                foreach (var setcookie in setcookies)
                    cc.SetCookies(response.RequestMessage.RequestUri, setcookie);
            }
            return cc; 
        }

    }
}
