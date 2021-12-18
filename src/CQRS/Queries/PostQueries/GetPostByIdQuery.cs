using CQRS.Dto.Out.PostDto;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace CQRS.Queries.PostQueries
{
    public class GetPostByIdQuery : IRequest<PostDto>
    {
        public int Id { get; set; }
    }
}
