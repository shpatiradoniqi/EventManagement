﻿using AutoMapper;
using EventManagement.Server.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventManagement.Server.Application.Comments
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Comment, CommentDto>()
                    .ForMember(d => d.Username, o => o.MapFrom(s => s.Author.UserName))
                    .ForMember(d => d.DisplayName, o => o.MapFrom(s => s.Author.DisplayName))
                    .ForMember(d => d.Image, o => o.MapFrom(s => s.Author.Photos.FirstOrDefault(x => x.IsMain).Url));
        }
    }
}
