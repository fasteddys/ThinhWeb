using AutoMapper;
using CQRS.Dto.In.User;
using CQRS.Dto.Out.User;
using Domain.Entity;
using Infrastructure.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Queries.Handlers
{
    public class UserQueriesHandler : IUserQueries
    {
        private IMapper _mapper;
        private ILogger<UserQueriesHandler> _logger;
        private UserManager<ApplicationUser> _userManager;

        public UserQueriesHandler(ILogger<UserQueriesHandler> logger,
            UserManager<ApplicationUser> userManager,
            IMapper mapper)
        {
            _mapper = mapper;
            _logger = logger;
            _userManager = userManager;
        }

        public async Task<GetUserInforOutDto> GetUserInforByEmail(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            return _mapper.Map<GetUserInforOutDto>(user);
        }

        public async Task<GetUserInforOutDto> GetUserInforById(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            return _mapper.Map<GetUserInforOutDto>(user);
        }

        public async Task<GetUsersOutDto> GetUsers(GetUsersInDto request)
        {
            _logger.LogInformation($"Search users: {JsonConvert.SerializeObject(request, Formatting.Indented)}");
            var users = _userManager.Users;
            if (!string.IsNullOrEmpty(request.SearchString))
            {
                var searchString = request.SearchString;
                users = users.Where(x => x.Email.IsContainText(searchString, true)
                                        || x.UserName.IsContainText(searchString, true)
                                        || x.FirstName.IsContainText(searchString, true)
                                        || x.LastName.IsContainText(searchString, true));
            }
            var total = users.Count();
            var result = users.Skip((request.PageIndex - 1) * request.PageSize).Take(request.PageSize).ToList();
            return new GetUsersOutDto
            {
                Total = total,
                Users = _mapper.Map<List<GetUserInforOutDto>>(result)
            };
        }
    }
}
