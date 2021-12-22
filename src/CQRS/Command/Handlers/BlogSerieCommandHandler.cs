using AutoMapper;
using CQRS.Command.BlogSerieCommands;
using CQRS.Dto.Out.BlogSerieDto;
using Domain.Entity;
using Infrastructure.Data;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CQRS.Command.Handlers
{
    public class BlogSerieCommandHandler : IRequestHandler<CreateBlogSerieCommand, BlogSerieDto>
    {
        private ApplicationDbContext _dbContext;
        private IMapper _mapper;
        private ILogger<BlogSerieCommandHandler> _logger;

        public BlogSerieCommandHandler(ApplicationDbContext dbContext, IMapper mapper, ILogger<BlogSerieCommandHandler> logger)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<BlogSerieDto> Handle(CreateBlogSerieCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var blogSerie = new BlogSerie
                {
                    Name = request.Name,
                    Description = request.Description,
                    Status = request.Status,
                    Posts = _dbContext.Posts.Where(x => request.Posts.Contains(x.Id)).ToList()
                };
                _dbContext.BlogSerie.Add(blogSerie);
                await _dbContext.SaveChangesAsync();

                var blogSerieDto = _mapper.Map<BlogSerieDto>(blogSerie);
                return blogSerieDto;
            }
            catch (Exception e)
            {
                _logger.LogError(typeof(CreateBlogSerieCommand).Name, e);
            }
            return null;
        }
    }
}
