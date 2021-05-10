using AutoMapper;
using HackerNewsModule.Domain;
using HackerNewsModule.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackerNewsModule.MappingProfiles
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<HackerNewsItem, HackerNewsItemDomain>()
                .ForMember(x=>x.Kids, o=> o.MapFrom(s => String.Join(",", s.Kids).ToString()))
                .ForMember(x => x.Parts, o => o.MapFrom(s => String.Join(",", s.Parts).ToString()));

            CreateMap<HackerNewsItem, HackerNewsDemoDTO>()
                .ForMember(x=>x.Kids, o=> o.Ignore())
                .ForMember(x => x.Parts, o => o.Ignore());
        }
    }
}
