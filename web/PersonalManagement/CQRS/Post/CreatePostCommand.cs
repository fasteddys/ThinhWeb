using MediatR;
using PersonalManagement.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Domain.Entity;
using Infrastructure.Data;
using PersonalManagement.Service;
using AutoMapper;

namespace PersonalManagement.CQRS.Post
{
    public class CreatePostCommand : IRequest<Domain.Entity.Post>
    {
        public Post_PostDto PostDto { get; set; }
    }

    public class CreatePostCommandHandler : IRequestHandler<CreatePostCommand, Domain.Entity.Post>
    {
        private ApplicationDbContext _dbContext;
        private IAccountService _accountService;
        private IMapper _mapper;

        public CreatePostCommandHandler(ApplicationDbContext dbContext, IAccountService accountService, IMapper mapper)
        {
            _dbContext = dbContext;
            _accountService = accountService;
            _mapper = mapper;
        }

        public async Task<Domain.Entity.Post> Handle(CreatePostCommand request, CancellationToken cancellationToken)
        {
            var post = _mapper.Map<Domain.Entity.Post>(request.PostDto);
            post.Id = post.Id ?? Guid.NewGuid().ToString();
            foreach (var tagId in request.PostDto.Tags)
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

            return post;
        }
    }
}
