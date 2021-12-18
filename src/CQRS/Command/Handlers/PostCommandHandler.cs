using AutoMapper;
using CQRS.Command.Post;
using CQRS.Dto.Out.Post;
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
    public class PostCommandHandler : 
        IRequestHandler<CreatePostCommand, PostDto>,
        IRequestHandler<EditPostCommand, PostDto>,
        IRequestHandler<DeletePostCommand, bool>
    {
        private ApplicationDbContext _dbContext;
        private ILogger<PostCommandHandler> _logger;
        private IMapper _mapper;

        public PostCommandHandler(ApplicationDbContext dbContext, ILogger<PostCommandHandler> logger, IMapper mapper)
        {
            _dbContext = dbContext;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<PostDto> Handle(CreatePostCommand request, CancellationToken cancellationToken)
        {
            var post = new Domain.Entity.Post();
            post.Title = request.Title;
            post.Content = request.Content;
            post.Status = request.Status;
            post.Id = post.Id ?? Guid.NewGuid().ToString();
            foreach (var tagId in request.Tags)
            {
                var tag = _dbContext.Tags.FirstOrDefault(x => x.Id == tagId);
                if (tag == null)
                {
                    tag = new Tag { Name = tagId };
                    _dbContext.Tags.Add(tag);
                    await _dbContext.SaveChangesAsync();
                }
                post.PostTags.Add(new PostTag { Post = post, Tag = tag });
            }

            _dbContext.Posts.Add(post);
            await _dbContext.SaveChangesAsync();

            var postDto = _mapper.Map<PostDto>(post);
            return postDto;
        }

        public Task<PostDto> Handle(EditPostCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Handle(DeletePostCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
