using PersonalManagement.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalManagement.Models
{
    public class Admin_Roles_IndexViewModel
    {
        public Common_PagingViewModel<Admin_RoleIndexDto> Roles { get; set; } = new Common_PagingViewModel<Admin_RoleIndexDto>();
    }
}
