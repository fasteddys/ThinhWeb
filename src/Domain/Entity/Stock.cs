using Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Entity
{
    public class Stock : BaseEntity
    {
        public string MaCP { get; set; }
        public string TenCongTy { get; set; }
        public string UrlChiTiet { get; set; }
        public string SanNiemYet { get; set; }
        public long SoLuongLuuHanh { get; set; }
        public string NhomNganhId { get; set; }
        [ForeignKey("NhomNganhId")]
        public virtual Stock_NhomNganh NhomNganh { get; set; }
    }
}
