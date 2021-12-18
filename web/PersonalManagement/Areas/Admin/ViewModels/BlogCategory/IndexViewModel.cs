using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalManagement.Areas.Admin.ViewModels.BlogCategory
{
    public class IndexViewModel
    {
        public SearchViewModel SearchInfor { get; set; }
        public CreateViewModel CreateBlogCategoryInfor { get; set; }
        public IList<Domain.Entity.BlogCategory> BlogCategories { get; set; }
    }
}
