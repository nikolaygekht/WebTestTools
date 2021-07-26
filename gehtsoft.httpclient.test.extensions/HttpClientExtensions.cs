using System;
using System.Collections;
using System.Collections.Generic;
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
        public static Task<HttpResponseMessage> SendAsync(this HttpClient client, HttpMethod method, string url, CookieContainer cookies = null, HttpContent content = null)
        {
            var request = new HttpRequestMessage(method, url);
            if (cookies != null)
                request.AddCookies(cookies);
            if (content != null)
                request.Content = content;
            return client.SendAsync(request);
        }

        /// <summary>
        /// Post form content
        /// </summary>
        /// <param name="client"></param>
        /// <param name="url"></param>
        /// <param name="formContent"></param>
        /// <returns></returns>
        public static Task<HttpResponseMessage> PostFormAsync(this HttpClient client, string url, params string[] formContent) => client.PostAsync(url, HttpClientHelper.CreateContent(formContent));

        /// <summary>
        /// Post form content with file uploads
        /// </summary>
        /// <param name="client"></param>
        /// <param name="url"></param>
        /// <param name="uploadParameterName"></param>
        /// <param name="files"></param>
        /// <param name="formContent"></param>
        /// <returns></returns>
        public static Task<HttpResponseMessage> PostFormAsync(this HttpClient client, string url, string uploadParameterName, IEnumerable<HttpUploadFile> files, params string[] formContent) => client.PostAsync(url, HttpClientHelper.CreateContent(uploadParameterName, files, formContent));

        /// <summary>
        /// Adds cookies to the request
        /// </summary>
        /// <param name="message"></param>
        /// <param name="cookieContainer"></param>
        public static void AddCookies(this HttpRequestMessage message, CookieContainer cookieContainer)
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
            => client.SendAsync(HttpMethod.Post, url, cookieContainer, HttpClientHelper.CreateContent(formContent));

        /// <summary>
        /// Post form content with file uploads
        /// </summary>
        /// <param name="client"></param>
        /// <param name="url"></param>
        /// <param name="cookieContainer"></param>
        /// <param name="uploadParameterName"></param>
        /// <param name="files"></param>
        /// <param name="formContent"></param>
        /// <returns></returns>
        public static Task<HttpResponseMessage> PostFormAsync(this HttpClient client, string url, CookieContainer cookieContainer, string uploadParameterName, IEnumerable<HttpUploadFile> files, params string[] formContent) 
            => client.SendAsync(HttpMethod.Post, url, cookieContainer, HttpClientHelper.CreateContent(uploadParameterName, files, formContent));


        /// <summary>
        /// Gets with cookies
        /// </summary>
        /// <param name="client"></param>
        /// <param name="url"></param>
        /// <param name="cookieContainer"></param>
        /// <returns></returns>
        public static Task<HttpResponseMessage> GetAsync(this HttpClient client, string url, CookieContainer cookieContainer)
            => client.SendAsync(HttpMethod.Get, url, cookieContainer);

        /// <summary>
        /// Deletes with cookies
        /// </summary>
        /// <param name="client"></param>
        /// <param name="url"></param>
        /// <param name="cookieContainer"></param>
        /// <returns></returns>
        public static Task<HttpResponseMessage> DeleteAsync(this HttpClient client, string url, CookieContainer cookieContainer)
            => client.SendAsync(HttpMethod.Delete, url, cookieContainer);

        /// <summary>
        /// Puts with cookies
        /// </summary>
        /// <param name="client"></param>
        /// <param name="url"></param>
        /// <param name="cookieContainer"></param>
        /// <returns></returns>
        public static Task<HttpResponseMessage> PutAsync(this HttpClient client, string url, CookieContainer cookieContainer)
            => client.SendAsync(HttpMethod.Put, url, cookieContainer);
    }
}
