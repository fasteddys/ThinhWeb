using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Common
{
    public class BaseEntity : IBaseEntity
    {
        public string Id { get; set; }
        public DateTime CreatedOn { get; } = DateTime.Now;
        public DateTime ModifiedOn { get; } = DateTime.Now;
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
    }
}
