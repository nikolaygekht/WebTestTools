namespace Gehtsoft.Httpclient.Test.Extensions
{
    public class HttpUploadFile
    {
        public string Name { get; private set; }
        public string ContentType { get; private set; }
        public byte[] Content { get; private set; }

        public HttpUploadFile(string name, byte[] content) : this(name, null, content)
        {

        }
        public HttpUploadFile(string name, string contentType, byte[] content) 
        {
            Name = name;
            ContentType = contentType;
            Content = content;
        }
    }
}
