using System;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace Gehtsoft.Httpclient.Test.Extensions
{
    /// <summary>
    /// Extensions of HttpClient classes
    /// </summary>
    public static class HttpClientExtensions
    {
        /// <summary>
        /// Post form content
        /// </summary>
        /// <param name="client"></param>
        /// <param name="url"></param>
        /// <param name="formContent"></param>
        /// <returns></returns>
        public static Task<HttpResponseMessage> PostFormAsync(this HttpClient client, string url, params string[] formContent) => client.PostAsync(url, HttpClientHelper.CreateContent(formContent));

        private static void AddCookies(HttpRequestMessage message, CookieContainer cookieContainer)
        {
            var uri = message.RequestUri;
            if (!uri.IsAbsoluteUri)
                uri = new Uri("https://localhost");

            var cookies = cookieContainer.GetCookies(uri);
            for (int i = 0; i < cookies.Count; i++)
                message.Headers.Add("Cookie", $"{cookies[i].Name}={cookies[i].Value}");
        }

        /// <summary>
        /// Post form content
        /// </summary>
        /// <param name="client"></param>
        /// <param name="url"></param>
        /// <param name="formContent"></param>
        /// <returns></returns>
        public static Task<HttpResponseMessage> PostFormAsync(this HttpClient client, string url, CookieContainer cookieContainer, params string[] formContent)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, url);
            AddCookies(request, cookieContainer);
            request.Content = HttpClientHelper.CreateContent(formContent);
            return client.SendAsync(request);
        }

        public static Task<HttpResponseMessage> GetAsync(this HttpClient client, string url, CookieContainer cookieContainer)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, url);
            AddCookies(request, cookieContainer);
            return client.SendAsync(request);
        }

        public static Task<HttpResponseMessage> DeleteAsync(this HttpClient client, string url, CookieContainer cookieContainer)
        {
            var request = new HttpRequestMessage(HttpMethod.Delete, url);
            AddCookies(request, cookieContainer);
            return client.SendAsync(request);
        }

        public static Task<HttpResponseMessage> PutAsync(this HttpClient client, string url, CookieContainer cookieContainer)
        {
            var request = new HttpRequestMessage(HttpMethod.Put, url);
            AddCookies(request, cookieContainer);
            return client.SendAsync(request);
        }
    }
}
