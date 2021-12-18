using CQRS.Dto.Out.BlogCategory;
using Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace CQRS.Command.BlogCategoryCommands
{
    public class CreateBlogCategoryCommand : IRequest<BlogCategoryDto>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public eStatus Status { get; set; } = eStatus.ENABLE;
    }
}
