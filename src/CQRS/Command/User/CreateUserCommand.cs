using CQRS.Dto.Out.User;
using Domain.Entity;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace CQRS.Command.User
{
    public class CreateUserCommand : IRequest<CreateUserOutDto>
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
