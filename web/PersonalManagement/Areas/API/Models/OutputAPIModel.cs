using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace PersonalManagement.Areas.API.Models
{
    public class OutputAPIModel<T>
    {
        public HttpStatusCode Status { get; set; } = HttpStatusCode.OK;
        public string Messsage { get; set; }
        public string MessageDetail { get; set; }
        public ErrorCodeEnum ErrorCode { get; set; } = ErrorCodeEnum.OK;
        public T Data { get; set; }
    }

    public class PageModel<T>
    {
        public long PageNo { get; set; }
        public long PageSize { get; set; }
        public long TotalRecords { get; set; }
        public long TotalPages
        {
            get {
                return PageSize == 0 ? 0 : (long)Math.Ceiling(1.0 * TotalRecords / PageSize);
            }
        }

        public List<T> Data { get; set; } = new List<T>();
    }
}
