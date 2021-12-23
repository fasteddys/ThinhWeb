using CQRS.Dto.Out;
using CQRS.Dto.Out.PostDto;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace CQRS.Queries.PostQueries
{
    public class GetPostsQuery : IRequest<OutputAPIModel<PageModel<PostDto>>>
    {
        public String Title { get; set; }
        public String Tag { get; set; }
        public String Serie { get; set; }
        public String Category { get; set; }
    }
}
