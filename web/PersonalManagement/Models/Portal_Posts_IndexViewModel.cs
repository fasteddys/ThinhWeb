using PersonalManagement.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalManagement.Models
{
    public class Portal_Posts_IndexViewModel
    {
        public Common_PagingViewModel<PostDto> Posts { get; set; } = new Common_PagingViewModel<PostDto>();
        public string Tag { get; set; }
        public string SearchStr { get; set; }
        public DateTime? CreateAt { get; set; }
    }
}
