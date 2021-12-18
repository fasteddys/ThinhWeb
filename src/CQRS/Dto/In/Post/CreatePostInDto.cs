using Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace CQRS.Dto.In.Post
{
    public class CreatePostInDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public eStatus Status { get; set; }
    }
}
