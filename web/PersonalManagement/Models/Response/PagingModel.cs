using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalManagement.Models.Response
{
    public class PagingModel<T> where T:class
    {
        public List<T> Data { get; set; }
        public long TotalRecords { get; set; }
        public int PageSize { get; set; }
        public int PageIndex { get; set; }
    }
}
