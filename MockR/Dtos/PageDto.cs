using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MockR.Dtos
{
    public class PageDto
    {
        public int Id { get; set; }
        public string AbsolutePath { set; get; } = null!;
        public string Method { set; get; } = null!;
        public string Body { set; get; } = null!;
    }
}
