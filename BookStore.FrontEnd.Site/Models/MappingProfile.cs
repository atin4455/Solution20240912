﻿using AutoMapper;
using BookStore.FrontEnd.Site.Models.Dtos;
using BookStore.FrontEnd.Site.Models.EFModels;
using BookStore.FrontEnd.Site.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookStore.FrontEnd.Site.Models
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //單向映射
            CreateMap<LoginVM, LoginDto>();

            CreateMap<Member, MemberDto>();

            CreateMap<MemberDto, EditProfileVm>();

        }
    }
}