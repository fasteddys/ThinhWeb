using Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entity
{
    public class Tag : BaseEntity
    {
        public string Name { get; set; }
        public IList<PostTag> PostTags { get; set; }
    }
}
