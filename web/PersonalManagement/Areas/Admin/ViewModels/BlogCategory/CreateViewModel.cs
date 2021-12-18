using Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalManagement.Areas.Admin.ViewModels.BlogCategory
{
    public class CreateViewModel
    {
        [Display(Name="Phân loại")]
        [Required(ErrorMessage = "Trường bắt buộc")]
        public string Name { get; set; }

        [Display(Name="Mô tả")]
        public string Description { get; set; }

        [Display(Name="Trạng thái")]
        public eStatus Status { get; set; }
    }
}
