using PersonalManagement.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalManagement.Models
{
    public class Admin_Users_IndexViewModel
    {
        public Common_PagingViewModel<Admin_UserIndexDto> Users { get; set; } = new Common_PagingViewModel<Admin_UserIndexDto>();
        public string Email { get; set; }
    }
}
