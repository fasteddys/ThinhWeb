using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalManagement.Models.Home
{
    public class Rate
    {
        public String Match { get; set; }
        public String Site { get; set; }
        public List<double> Total { get; set; } = new List<double>();
        public List<double> Over { get; set; } = new List<double>();
        public List<double> Fee { get; set; } = new List<double>();
        public List<double> Under { get; set; } = new List<double>();
        public List<double> OverReal { get; set; } = new List<double>();
        public List<double> UnderReal { get; set; } = new List<double>();
        public double TotalAverage { get; set; }
        public double OverAverage { get; set; }
        public double UnderAverage { get; set; }
        public double OverRealAverage { get; set; }
        public double UnderRealAverage { get; set; }
        public List<double> Spread { get; set; } = new List<double>();
        public List<double> Team1 { get; set; } = new List<double>();
        public List<double> Team2 { get; set; } = new List<double>();
    }
    public class ChartViewModel
    {
        public List<List<Rate>> Rates { get; set; } = new List<List<Rate>>();
    }
}
