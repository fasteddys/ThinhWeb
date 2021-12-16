using Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entity
{
    public class Stock_NhomNganh : BaseEntity
    {
        public string Ten { get; set; }
        public string UrlChiTiet { get; set; }
        public int Level { get; set; }
        public string NhomNganhChaId { get; set; }
        public virtual IList<Stock_NhomNganh> NhomNganhCon { get; set; }
        public virtual IList<Stock> Stocks { get; set; }
    }
}
