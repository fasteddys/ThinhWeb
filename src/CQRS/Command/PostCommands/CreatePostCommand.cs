using CQRS.Dto.Out.PostDto;
using Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace CQRS.Command.PostCommands
{
    public class CreatePostCommand : IRequest<PostDto>
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public eStatus Status { get; set; }
        public string UserId { get; set; }
        public List<string> Tags { get; set; }
        public List<int> Categories { get; set; }
    }
}
