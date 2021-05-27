using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalManagement.Models.Response
{
    public class ResponseEntity<T> where T : class
    {
        public string Message { get; set; }
        public string Status { get; set; }
        public T Data { get; set; }
    }
}
