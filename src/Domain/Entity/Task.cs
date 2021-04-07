using Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entity
{
    public class PersonalTask : BaseEntity
    {
        public string Title { get; set; }
        public string Content { get; set; }

        public DateTime? StartedAt { get; set; }
        public DateTime? ExpectedFinishAt { get; set; }
        public DateTime? FinishedAt { get; set; }
        public eTaskStatus Status { get; set; }
        public virtual ApplicationUser UserAssigned { get; set; }
    }
}
