namespace MockR.Entities
{
    public class Page
    {
        public int Id { get; set; }
        public string AbsolutePath { set; get; } = null!;
        public string Method { set; get; } = null!;
        public string Body { set; get; } = null!;
    }
}
