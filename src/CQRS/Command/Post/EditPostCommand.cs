using CQRS.Dto.Out.Post;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace CQRS.Command.Post
{
    public class EditPostCommand : IRequest<PostDto>
    {
    }
}
