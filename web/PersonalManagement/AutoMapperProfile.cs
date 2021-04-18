using AutoMapper;
using Domain.Entity;
using Microsoft.AspNetCore.Identity;
using PersonalManagement.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalManagement
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            #region Post
            CreateMap<Post, PostDto>()
                .ForMember(x => x.Tags, o => o.MapFrom(x => x.PostTags.Select(x => x.TagId).ToList()))
                .ForMember(x => x.TagNames, o => o.MapFrom(x => x.PostTags.Select(x => x.Tag.Name).ToList()))
                .ForMember(x => x.CreatedOn, o => o.MapFrom(x => x.CreatedOn.ToString("hh:mm:ss dd/MM/yyyy")))
                .ForMember(x => x.Author, o => o.MapFrom(x => x.Author.UserName))
                .ForMember(x => x.AuthorId, o => o.MapFrom(x => x.Author.Id));
            CreateMap<PostDto, Post>()
                .ForMember(x => x.PostTags, o => o.Ignore())
                .ForMember(x => x.CreatedOn, o => o.Ignore())
                //.ForMember(x => x.Author, o => o.MapFrom(x => userManager.FindByIdAsync(x.AuthorId).Result))
                ;

            CreateMap<Post, Post_PostDto>()
                .ForMember(x => x.Tags, o => o.MapFrom(x => x.PostTags.Select(x => x.TagId)));
            CreateMap<Post_PostDto, Post>()
                .ForMember(x => x.PostTags, o => o.Ignore());
            #endregion
        }
    }
}
