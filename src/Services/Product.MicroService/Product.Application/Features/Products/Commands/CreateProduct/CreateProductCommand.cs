using AutoMapper;
using MediatR;
using Product.Application.Dtos;
using Product.Application.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace Product.Application.Features.Products.Commands.CreateProduct
{
    public partial class CreateProductCommand : IRequest<ResponseDto>
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public string CategoryName { get; set; }
        public string ImageUrl { get; set; }
    }
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, ResponseDto>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        public CreateProductCommandHandler(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<ResponseDto> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var product = _mapper.Map<BookDto>(request);
            await _productRepository.CreateUpdateProduct(product);
            return new ResponseDto
            {
                IsSuccess = true,
                Result = product
            };
        }
    }
}
