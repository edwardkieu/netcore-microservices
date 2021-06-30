using AutoMapper;
using Product.API.Dtos;
using Product.API.Models;

namespace Product.API.AutoMapper
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {            
            CreateMap<BookDto, Book>().ReverseMap();
        }
    }
}
