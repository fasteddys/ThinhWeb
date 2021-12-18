using System;
using System.Collections.Generic;
using System.Text;

namespace CQRS.Dto.Out.Post
{
    public class PostDto
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
    }
}
