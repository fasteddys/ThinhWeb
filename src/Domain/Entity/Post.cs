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
        public virtual IList<PostTag> PostTags { get; set; } = new List<PostTag>();
        public virtual IList<Blog_BlogCategory> Categories { get; set; } = new List<Blog_BlogCategory>();
        public virtual BlogSerie? Serie { get; set; }
        //public IList<Post> SubPosts { get; set; } = new List<Post>();
    }
}
