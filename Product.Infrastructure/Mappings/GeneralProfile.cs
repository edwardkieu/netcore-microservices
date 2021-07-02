using AutoMapper;
using Product.Application.Dtos;
using Product.Domain.Entities;

namespace Product.Infrastructure.Mappings
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {
            CreateMap<Book, BookDto>().ReverseMap();
        }
    }
}
