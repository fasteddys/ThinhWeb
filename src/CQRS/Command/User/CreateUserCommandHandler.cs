using AutoMapper;
using CQRS.Dto.Out.UserDto;
using Domain.Entity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CQRS.Command.User
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, CreateUserOutDto>
    {
        private ILogger<CreateUserCommandHandler> _logger;
        private UserManager<ApplicationUser> _userManager;
        private IMapper _mapper;

        public CreateUserCommandHandler(ILogger<CreateUserCommandHandler> logger,
            UserManager<ApplicationUser> userManager,
            IMapper mapper)
        {
            _logger = logger;
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<CreateUserOutDto> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var user = new ApplicationUser { UserName = request.UserName, Email = request.Email };
            var result = await _userManager.CreateAsync(user, request.Password);
            _logger.LogInformation($"Creating user: {JsonConvert.SerializeObject(request, Formatting.Indented)}");

            if (result.Succeeded)
            {
                _logger.LogInformation($"Create user success == Username: {user.UserName} == Email: {user.Email}");
                var userDto = _mapper.Map<CreateUserOutDto>(user);
                return userDto;
            }
            else
            {
                _logger.LogError($"Create user failed == {string.Join(',', result.Errors)}");
                return null;
            }
        }
    }
}
