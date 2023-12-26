using Microsoft.AspNetCore.Http;

namespace MockR.Request
{
    public class CreateRequest
    {
        public PathString AbsolutePath { get; set; }
        public string Method { set; get; } = string.Empty;
        public string Body { set; get; } = string.Empty;
    }
}
