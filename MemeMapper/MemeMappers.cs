using AutoMapper;//*
using Memes.Models;//*
using Memes.Models.Dto;//*

namespace Memes.MemeMapper
{
    public class MemeMappers:Profile
    {
        public MemeMappers()
        {
            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<Photo, PhotoDto>().ReverseMap();
            CreateMap<Photo, PhotoUpdateDto>().ReverseMap();
            CreateMap<Photo, PhotoCreateDto>().ReverseMap();
        }
    }
}
