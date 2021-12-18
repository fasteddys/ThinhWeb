using CQRS.Dto.Out.PostDto;
using Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace CQRS.Command.PostCommands
{
    public class EditPostCommand : IRequest<PostDto>
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public eStatus Status { get; set; }
        public string UserId { get; set; }
        public List<string> Tags { get; set; }
        public List<int> Categories { get; set; }
    }
}
