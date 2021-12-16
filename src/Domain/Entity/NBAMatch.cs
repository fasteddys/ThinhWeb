using Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entity
{
    public class NBAMatch : BaseEntity
    {
        public string Home { get; set; }
        public string Away { get; set; }
        public string Status { get; set; }
        public string HomeScore { get; set; }
        public string AwayScore { get; set; }
        public DateTime? Date { get; set; }
        public string Code { get; set; }
    }
}
