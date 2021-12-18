using AutoMapper;
using CQRS.Dto.In.User;
using CQRS.Dto.Out.MenuDto;
using CQRS.Dto.Out.UserDto;
using CQRS.Queries.UserQueries;
using Domain.Entity;
using Infrastructure.Data;
using Infrastructure.Helpers;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CQRS.Queries.Handlers
{
    public class UserQueriesHandler : IUserQueries,
        IRequestHandler<GetMenusByUserId, IList<MenuDto>>
    {
        private IMapper _mapper;
        private ILogger<UserQueriesHandler> _logger;
        private UserManager<ApplicationUser> _userManager;
        private ApplicationDbContext _dbContext;

        public UserQueriesHandler(ILogger<UserQueriesHandler> logger,
            UserManager<ApplicationUser> userManager,
            ApplicationDbContext dbContext,
            IMapper mapper)
        {
            _mapper = mapper;
            _logger = logger;
            _userManager = userManager;
            _dbContext = dbContext;
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

        public async Task<IList<MenuDto>> Handle(GetMenusByUserId request, CancellationToken cancellationToken)
        {
            return _mapper.Map<List<MenuDto>>(_dbContext.Menu.ToList());
            //throw new NotImplementedException();
        }
    }
}
