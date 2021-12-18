using AutoMapper;
using CQRS.Dto.In.Post;
using CQRS.Dto.Out.BlogCategory;
using CQRS.Dto.Out.MenuDto;
using CQRS.Dto.Out.PostDto;
using CQRS.Dto.Out.UserDto;
using Domain.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace CQRS
{
    public class CQRSMappingProfile : Profile
    {
        public CQRSMappingProfile()
        {
            CreateMap<ApplicationUser, GetUserInforOutDto>().ReverseMap();
            CreateMap<Post, CreatePostInDto>();
            CreateMap<Post, PostDto>();

            CreateMap<BlogCategory, BlogCategoryDto>();
            CreateMap<Menu, MenuDto>();
        }
    }
}
