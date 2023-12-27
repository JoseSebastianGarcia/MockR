using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MockR.Dtos
{
    public class PageDto
    {
        public Guid Id { get; set; }
        public string AbsolutePath { set; get; } = null!;
        public string Method { set; get; } = null!;
        public string Body { set; get; } = null!;
        public string AbsolutePathReduced 
        {
            get 
            {
                string result = string.Empty;

                if (AbsolutePath.Length > 17)
                    result = $"{AbsolutePath.Substring(1, 17)}...";
                else
                    result = AbsolutePath;

                return result;
            }
        }
    }
}
