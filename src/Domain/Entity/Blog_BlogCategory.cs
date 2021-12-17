using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entity
{
    public class Blog_BlogCategory
    {
        public string PostId { get; set; }
        public virtual Post Post { get; set; }

        public string CategoryId { get; set; }
        public virtual BlogCategory Category { get; set; }
    }
}
