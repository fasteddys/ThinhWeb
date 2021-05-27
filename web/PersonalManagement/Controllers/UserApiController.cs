using Domain.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using PersonalManagement.DTO;
using PersonalManagement.Models.Response;
using PersonalManagement.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserApiController : ControllerBase
    {
        private UserManager<ApplicationUser> _userManager;
        private SignInManager<ApplicationUser> _signInManager;
        private ILogger<UserApiController> _logger;
        private IAccountService _accountService;

        public UserApiController(SignInManager<ApplicationUser> signInManager,
            ILogger<UserApiController> logger,
            UserManager<ApplicationUser> userManager,
            IAccountService accountService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _accountService = accountService;
        }

        [HttpGet]
        [Route("get-user-infor-by-id")]
        public async Task<string> GetUserInforById(string Id)
        {
            var user = await _userManager.FindByIdAsync(Id);
            return JsonConvert.SerializeObject(user, Formatting.Indented);
        }

        [HttpGet]
        [Route("get-user-infor-by-email")]
        public async Task<string> GetUserInforByEmail(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            return JsonConvert.SerializeObject(user, Formatting.Indented);
        }

        [HttpPost]
        [Route("create-new-user")]
        public async Task<string> CreateUser(User_PushDto userDto)
        {
            var user = new ApplicationUser { UserName = userDto.UserName, Email = userDto.Email };
            var result = await _userManager.CreateAsync(user, userDto.Password);
            if (result.Succeeded)
                return JsonConvert.SerializeObject(user);

            return "Error";
        }

        [HttpGet]
        [Route("users")]
        public async Task<ResponseEntity<PagingModel<ApplicationUser>>> GetUsers(int pageSize = 10, int pageIndex = 1) {
            var data = _userManager.Users.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            var response = new ResponseEntity<PagingModel<ApplicationUser>>
            {
                Message = "",
                Status = "OK",
                Data = new PagingModel<ApplicationUser>
                {
                    Data = data,
                    PageIndex = pageIndex,
                    PageSize = pageSize,
                    TotalRecords = _userManager.Users.Count()
                }
            };
            return response;
        }
    }
}
