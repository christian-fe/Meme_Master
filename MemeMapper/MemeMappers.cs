using AutoMapper;//*
using Meme.Models;//*
using Meme.Models.Dto;//*

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
