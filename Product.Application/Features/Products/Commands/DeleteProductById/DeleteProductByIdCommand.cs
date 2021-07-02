using MediatR;
using Product.Application.Dtos;
using Product.Application.Exceptions;
using Product.Application.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace Product.Application.Features.Products.Commands.DeleteProductById
{
    public class DeleteProductByIdCommand : IRequest<ResponseDto>
    {
        public int Id { get; set; }
        public class DeleteProductByIdCommandHandler : IRequestHandler<DeleteProductByIdCommand, ResponseDto>
        {
            private readonly IProductRepository _productRepository;
            public DeleteProductByIdCommandHandler(IProductRepository productRepository)
            {
                _productRepository = productRepository;
            }
            public async Task<ResponseDto> Handle(DeleteProductByIdCommand command, CancellationToken cancellationToken)
            {
                var product = await _productRepository.GetProductById(command.Id);
                if (product == null) throw new ApiException($"Product Not Found.");
                await _productRepository.DeleteProduct(product.ProductId);
                return new ResponseDto
                {
                    IsSuccess = true,
                    Result = command.Id
                };
            }
        }
    }
}
