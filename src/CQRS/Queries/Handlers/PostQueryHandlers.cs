using AutoMapper;
using CQRS.Dto.Out;
using CQRS.Dto.Out.PostDto;
using CQRS.Queries.PostQueries;
using Infrastructure.Data;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CQRS.Queries.Handlers
{
    public class PostQueryHandlers 
        : IRequestHandler<GetPostByIdQuery, PostDto>,
        IRequestHandler<SearchPostsQuery, IList<PostDto>>
        IRequestHandler<GetPostsQuery, OutputAPIModel<PageModel<PostDto>>>
    {
        private ApplicationDbContext _dbContext;
        private ILogger<PostQueryHandlers> _logger;
        private IMapper _mapper;

        public PostQueryHandlers(ApplicationDbContext dbContext, ILogger<PostQueryHandlers> logger, IMapper mapper)
        {
            _dbContext = dbContext;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<PostDto> Handle(GetPostByIdQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<IList<PostDto>> Handle(SearchPostsQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<OutputAPIModel<PageModel<PostDto>>> Handle(GetPostsQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
