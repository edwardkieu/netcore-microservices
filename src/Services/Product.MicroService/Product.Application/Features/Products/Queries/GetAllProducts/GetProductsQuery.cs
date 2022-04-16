using AutoMapper;
using MediatR;
using Product.Application.Dtos;
using Product.Application.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace Product.Application.Features.Products.Queries.GetAllProducts
{
    public class GetProductsQuery : IRequest<ResponseDto>
    {
        
    }
    public class GetAllProductsQueryHandler : IRequestHandler<GetProductsQuery, ResponseDto>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        public GetAllProductsQueryHandler(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<ResponseDto> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {           
            var products = await _productRepository.GetProducts();
            var response = new ResponseDto
            {
                IsSuccess = true,
                Result = products
            };
            return response;
        }
    }
}
