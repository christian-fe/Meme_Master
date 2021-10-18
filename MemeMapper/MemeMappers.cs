using AutoMapper;//*
using Meme.Models;//*
using Meme.Models.Dto;//*
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Meme.MemeMapper
{
    public class MemeMappers:Profile
    {
        public MemeMappers()
        {
            CreateMap<Category, CategoryDto>().ReverseMap();
        }
    }
}
