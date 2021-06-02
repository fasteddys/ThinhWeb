﻿using AutoMapper;
using CQRS.Dto.Out.User;
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
        }
    }
}