using System;

namespace MockR.Entities
{
    [Serializable]
    public class Page
    {
        public Guid Id { get; set; }
        public string AbsolutePath { set; get; } = null!;
        public string Method { set; get; } = null!;
        public string Body { set; get; } = null!;
    }
}
