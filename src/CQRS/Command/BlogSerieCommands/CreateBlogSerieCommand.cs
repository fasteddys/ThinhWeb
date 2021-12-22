using CQRS.Dto.Out.BlogSerieDto;
using Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace CQRS.Command.BlogSerieCommands
{
    public class CreateBlogSerieCommand : IRequest<BlogSerieDto>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public eStatus Status { get; set; } = eStatus.ENABLE;
        public List<string> Posts { get; set; } = new List<string>();
    }
}
