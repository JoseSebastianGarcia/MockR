using Microsoft.AspNetCore.Http;

namespace MockR.Request
{
    public class UpdateRequest
    {
        public int Id { get; set; }
        public PathString AbsolutePath { get; set; }
        public string Method { set; get; } = string.Empty;
        public string Body { set; get; } = string.Empty;
    }
}
