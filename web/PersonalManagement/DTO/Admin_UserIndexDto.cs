using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalManagement.DTO
{
    public class Admin_UserIndexDto
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsConfirmedByEmail { get; set; }
        public bool IsLockOut { get; set; }
    }
}
