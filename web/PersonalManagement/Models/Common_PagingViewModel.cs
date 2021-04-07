using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalManagement.Models
{
    public class Common_PagingViewModel<T> where T : class
    {
        public List<T> DataSource { get; set; }
        public int TotalRecords { get; set; }
        public int RecordPerPage { get; set; } = 10;
        public int PageIndex { get; set; } = 1;
        public int TotalPages => TotalRecords / RecordPerPage + 1;
    }
}
