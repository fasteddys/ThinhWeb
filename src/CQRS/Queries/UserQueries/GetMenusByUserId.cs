using CQRS.Dto.Out.MenuDto;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace CQRS.Queries.UserQueries
{
    public class GetMenusByUserId : IRequest<IList<MenuDto>>
    {
        public string UserId { get; set; }
    }
}
