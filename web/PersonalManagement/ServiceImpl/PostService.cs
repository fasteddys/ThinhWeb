using AutoMapper;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using PersonalManagement.DTO;
using PersonalManagement.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalManagement.ServiceImpl
{
    public class PostService : IPostService
    {
        private ApplicationDbContext _dbContext;
        private IMapper _mapper;

        public PostService(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public List<PostDto> GetListOfPosts(string searchStr, string tag, out int totalRecords, DateTime? CreatedAt = null, 
            int pageIndex = 1, int pageSize = 10)
        {
            pageIndex = pageIndex > 0 ? pageIndex : 1;
            pageSize = pageSize > 0 ? pageSize : 10;
            var query = _dbContext.Posts.AsQueryable().AsNoTracking();
            if (!string.IsNullOrEmpty(searchStr))
            {
                query = query.Where(x =>  x.Title.ToLower().Contains(searchStr.Trim().ToLower()));
            }
            if (CreatedAt.HasValue)
                query = query.Where(x => x.CreatedOn.Date == CreatedAt.Value.Date);

            totalRecords = query.Count();
            var posts = query.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            return posts
                .Select(x => _mapper.Map<PostDto>(x))
                .ToList();
        }
    }
}
