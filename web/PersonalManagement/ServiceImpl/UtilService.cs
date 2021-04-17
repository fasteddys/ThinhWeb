using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using PersonalManagement.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalManagement.ServiceImpl
{
    public class UtilService : IUtilService
    {
        private ApplicationDbContext _dbContext;

        public UtilService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<SelectListItem> GetListTags()
        {
            return _dbContext.Tags
                        .Select(x => new SelectListItem { 
                            Text = x.Name,
                            Value = x.Id
                        })
                        .ToList();
        }
    }
}
