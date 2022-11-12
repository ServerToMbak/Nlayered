using Application.Features.Models.Dtos;
using Application.Features.Models.Models;
using AutoMapper;
using Core.Persistence.Paging;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Models.Profiles
{
    public class MappingProfiles:Profile
    {
        public MappingProfiles()
        {
            CreateMap<Model, ModelListDto>().ForMember(j=>j.BrandName,opt=>opt.MapFrom(c=>c.brand.Name)).ReverseMap();
            CreateMap<IPaginate<Model>, ModelListModel>().ReverseMap();
            CreateMap<Model, ModelListDto>().ReverseMap();
        }
    }
}
