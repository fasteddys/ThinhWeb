using CQRS.Dto.Out.Post;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace CQRS.Queries.Post
{
    public class GetPostByIdQuery : IRequest<PostDto>
    {
        public int Id { get; set; }
    }
}
