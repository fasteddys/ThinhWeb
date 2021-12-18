using CQRS.Dto.Out.MenuDto;
using Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace CQRS.Command.MenuCommands
{
    public class CreateMenuCommand : IRequest<MenuDto>
    {
        public string TextDisplay { get; set; }
        public string Url { get; set; }
        public eStatus Status { get; set; }
    }
}
