using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading;

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

        /// <summary>
        /// Creates a multi-part content for file uploads
        /// </summary>
        /// <param name="fileParameterName"></param>
        /// <param name="files"></param>
        /// <param name="formContent"></param>
        /// <returns></returns>
        public static HttpContent CreateContent(string fileParameterName, IEnumerable<HttpUploadFile> files, params string[] formContent)
        {
            if (string.IsNullOrEmpty(fileParameterName))
                throw new ArgumentNullException(nameof(fileParameterName));

            if (files == null)
                throw new ArgumentNullException(nameof(files));

            if (formContent != null && formContent.Length > 0 && formContent.Length % 2 != 0)
                throw new ArgumentException("The values must be passed as pairs of string. First is the name, and the second is the value", nameof(formContent));

            var multipart = new MultipartFormDataContent();

            foreach (var file in files)
            {
                var c = new StreamContent(new MemoryStream(file.Content));
                if (!string.IsNullOrEmpty(file.ContentType))
                    c.Headers.ContentType.MediaType = file.ContentType;
                multipart.Add(c, fileParameterName, file.Name);
            }

            for (int i = 0; i < (formContent?.Length ?? 0); i += 2)
                multipart.Add(new StringContent(formContent[i + 1]), formContent[i]);

            return multipart;
        }
    }
}
