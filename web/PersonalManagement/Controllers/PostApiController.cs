using Domain.Entity;
using Infrastructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PersonalManagement.DTO;
using PersonalManagement.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostApiController : ControllerBase
    {
        private IAccountService _accountService;
        private ApplicationDbContext _dbContext;

        public PostApiController(IAccountService accountService, ApplicationDbContext dbContext)
        {
            _accountService = accountService;
            _dbContext = dbContext;
        }

        [HttpPost]
        [Route("create-new-tag")]
        public async Task<string> CreateNewTag(Tag_PostDto tagDto)
        {
            var tag = new Tag { Name = tagDto.Name, CreatedBy = _accountService.CurrentUserId };
            var result = _dbContext.Tags.Add(tag);
            await _dbContext.SaveChangesAsync();
            return JsonConvert.SerializeObject(tag);
        }

        
    }
}
