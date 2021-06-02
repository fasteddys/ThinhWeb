using System;
using System.Collections.Generic;
using System.Text;

namespace CQRS.Dto.In.User
{
    public class GetUsersInDto
    {
        public string SearchString { get; set; }
        public int PageIndex { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
