using System;
using System.Collections.Generic;
using System.Text;

namespace CQRS.Dto.Out.User
{
    public class GetUsersOutDto
    {
        public List<GetUserInforOutDto> Users { get; set; }
        public long Total { get; set; }
    }
}
