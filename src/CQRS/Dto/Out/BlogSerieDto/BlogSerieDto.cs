using Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace CQRS.Dto.Out.BlogSerieDto
{
    public class BlogSerieDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public eStatus Status { get; set; }
    }
}
