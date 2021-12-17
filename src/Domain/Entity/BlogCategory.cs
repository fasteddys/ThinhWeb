using Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entity
{
    public class BlogCategory : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public eStatus Status { get; set; } = eStatus.ENABLE;
        public virtual IList<Blog_BlogCategory> Posts { get; set; } = new List<Blog_BlogCategory>();
    }
}
