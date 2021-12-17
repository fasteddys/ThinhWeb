using Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entity
{
    public class BlogSerie : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public eStatus Status { get; set; } = eStatus.ENABLE;
        public IList<Post> Posts { get; set; } = new List<Post>();
    }
}
