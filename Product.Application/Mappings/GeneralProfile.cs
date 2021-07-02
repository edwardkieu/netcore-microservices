using AutoMapper;
using Product.Application.Dtos;
using Product.Application.Features.Products.Commands.CreateProduct;
using Product.Domain.Entities;

namespace Product.Application.Mappings
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {
            CreateMap<Book, BookDto>().ReverseMap();
            CreateMap<CreateProductCommand, BookDto>();
        }
    }
}
