using CQRS.Command.User;
using CQRS.Dto.In.User;
using CQRS.Dto.Out.UserDto;
using CQRS.Queries;
using Domain.Entity;
using MediatR;
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
        private IMediator _mediator;
        private IUserQueries _userQueries;

        public UserApiController(SignInManager<ApplicationUser> signInManager,
            ILogger<UserApiController> logger,
            UserManager<ApplicationUser> userManager,
            IAccountService accountService,
            IMediator mediator,
            IUserQueries userQueries)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _accountService = accountService;
            _mediator = mediator;
            _userQueries = userQueries;
        }

        [HttpGet]
        [Route("get-user-infor-by-id")]
        public async Task<ResponseEntity<GetUserInforOutDto>> GetUserInforById(string id)
        {
            var user = await _userQueries.GetUserInforById(id);
            return new ResponseEntity<GetUserInforOutDto>
            {
                Data = user,
                Message = "",
                Status = StatusCodes.Status200OK,
                ErrorCode = ResponseResult.SUCCESS,
            };
        }

        [HttpGet]
        [Route("get-user-infor-by-email")]
        public async Task<ResponseEntity<GetUserInforOutDto>> GetUserInforByEmail(string email)
        {
            var user = await _userQueries.GetUserInforByEmail(email);
            return new ResponseEntity<GetUserInforOutDto>
            {
                Data = user,
                Message = "",
                Status = StatusCodes.Status200OK,
                ErrorCode = ResponseResult.SUCCESS,
            };
        }

        [HttpPost]
        [Route("create-new-user")]
        public async Task<ResponseEntity<CreateUserOutDto>> CreateUser(CreateUserCommand userDto)
        {
            var user = await _mediator.Send(userDto);
            if (user != null)
            {
                return new ResponseEntity<CreateUserOutDto>
                {
                    Data = user,
                    Message = "",
                    Status = StatusCodes.Status200OK,
                    ErrorCode = ResponseResult.SUCCESS,
                };
            }
            return new ResponseEntity<CreateUserOutDto>
            {
                Data = null,
                Message = "Something wrong",
                Status = StatusCodes.Status400BadRequest,
                ErrorCode = ResponseResult.ERR_CREATE_USER_FAILED,
            };
        }

        [HttpGet]
        [Route("users")]
        public async Task<ResponseEntity<PagingModel<GetUserInforOutDto>>> GetUsers(string searchString, int pageSize = 10, int pageIndex = 1)
        {
            var users = await _userQueries.GetUsers(new GetUsersInDto
            {
                SearchString = searchString,
                PageSize = pageSize,
                PageIndex = pageIndex
            });

            var response = new ResponseEntity<PagingModel<GetUserInforOutDto>>
            {
                Message = "",
                Status = StatusCodes.Status200OK,
                ErrorCode = ResponseResult.SUCCESS,
                Data = new PagingModel<GetUserInforOutDto>
                {
                    Data = users.Users,
                    PageIndex = pageIndex,
                    PageSize = pageSize,
                    TotalRecords = users.Total
                }
            };
            return response;
        }
    }
}
