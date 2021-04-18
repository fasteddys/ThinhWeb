using Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entity
{
    public class Post : BaseEntity
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public eStatus Status { get; set; } = eStatus.DISABLE;
        public virtual ApplicationUser Author { get; set; }
        public IList<PostTag> PostTags { get; set; } = new List<PostTag>();
    }
}
