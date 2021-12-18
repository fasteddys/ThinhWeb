using System;
using System.Collections.Generic;
using System.Text;

namespace CQRS.Dto.Out.UserDto
{
    public class GetUserInforOutDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName => $"{FirstName} {LastName}";
        public string Username { get; set; }
        public string Email { get; set; }
    }
}
