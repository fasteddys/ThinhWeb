using AutoMapper;
using CQRS.Command.BlogCategoryCommands;
using CQRS.Dto.Out.BlogCategory;
using Domain.Entity;
using Infrastructure.Data;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CQRS.Command.Handlers
{
    public class BlogCategoryCommandHandler : IRequestHandler<CreateBlogCategoryCommand, BlogCategoryDto>
    {
        private ApplicationDbContext _dbContext;
        private ILogger<BlogCategoryCommandHandler> _logger;
        private IMapper _mapper;

        public BlogCategoryCommandHandler(ApplicationDbContext dbContext, ILogger<BlogCategoryCommandHandler> logger, IMapper mapper)
        {
            _dbContext = dbContext;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<BlogCategoryDto> Handle(CreateBlogCategoryCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var blogCategory = new BlogCategory
                {
                    Name = request.Name,
                    Description = request.Description,
                    Status = request.Status,
                };
                _dbContext.BlogCategory.Add(blogCategory);
                await _dbContext.SaveChangesAsync();

                var blogCategoryDto = _mapper.Map<BlogCategoryDto>(blogCategory);
                return blogCategoryDto;
            }
            catch (Exception e)
            {
                _logger.LogError("");
            }
            return null;
        }
    }
}
