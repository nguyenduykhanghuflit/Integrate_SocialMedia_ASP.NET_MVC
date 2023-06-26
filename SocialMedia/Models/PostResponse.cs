using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SocialMedia.Models
{
    public class PostResponse
    {
        public int Id { get; set; }
        public string Notes { get; set; }
        public string Title { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }

    public class PostDto
    {
        public string Notes { get; set; }
        public string Title { get; set; }
    }

    public class PostUpdateDto : PostDetail
    {
        public string Notes { get; set; }
        public string Title { get; set; }
    }
    public class PostDetail { public int Id { get; set; } }
}