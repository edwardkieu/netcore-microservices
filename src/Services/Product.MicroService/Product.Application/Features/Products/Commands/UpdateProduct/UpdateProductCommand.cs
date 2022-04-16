using System;
using System.Collections.Generic;
using System.Text;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Product.Application.Dtos;
using Product.Application.Interfaces;
using Product.Application.Exceptions;

namespace Product.Application.Features.Products.Commands.UpdateProduct
{
    public class UpdateProductCommand : IRequest<ResponseDto>
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public string CategoryName { get; set; }
        public string ImageUrl { get; set; }

        public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, ResponseDto>
        {
            private readonly IProductRepository _productRepository;
            public UpdateProductCommandHandler(IProductRepository productRepository)
            {
                _productRepository = productRepository;
            }
            public async Task<ResponseDto> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
            {
                var product = await _productRepository.GetProductById(command.ProductId);

                if (product == null) throw new ApiException($"Product Not Found.");
                {

                }
                product.Name = command.Name;
                product.Description = command.Description;
                await _productRepository.CreateUpdateProduct(product);
                return new ResponseDto { IsSuccess = true, Result = product };
            }
        }
    }
}
