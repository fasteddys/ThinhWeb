using CQRS.Dto.Out.Post;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace CQRS.Queries.Post
{
    public class SearchPostsQuery : IRequest<IList<PostDto>>
    {
    }
}
