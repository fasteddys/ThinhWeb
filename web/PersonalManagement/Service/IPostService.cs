using Domain.Entity;
using PersonalManagement.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalManagement.Service
{
    public interface IPostService
    {
        List<PostDto> GetListOfPosts(string searchStr, string tag, out int totalRecords, DateTime? CreatedAt = null, int pageIndex = 1, int pageSize = 10);
    }
}
