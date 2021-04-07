using Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entity
{
    public class Menu : BaseEntity
    {
        public string TextDisplay { get; set; }
        public string Url { get; set; }
        public eStatus Status { get; set; }
        public virtual Menu ParentMenu {get;set;}
    }
}
