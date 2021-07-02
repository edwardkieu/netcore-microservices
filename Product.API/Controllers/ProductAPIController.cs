using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Product.Application.Features.Products.Commands.CreateProduct;
using Product.Application.Features.Products.Commands.DeleteProductById;
using Product.Application.Features.Products.Commands.UpdateProduct;
using Product.Application.Features.Products.Queries.GetAllProducts;
using Product.Application.Features.Products.Queries.GetProductById;
using System.Threading.Tasks;

namespace Product.API.Controllers
{
    [Authorize]
    [Route("api/products")]
    public class ProductAPIController : BaseApiController
    {
        public ProductAPIController() :base()
        {
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await Mediator.Send(new GetProductsQuery()));
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await Mediator.Send(new GetProductByIdQuery { Id = id }));
        }


        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateProductCommand command)
        {
            return Ok(await Mediator.Send(command));
        }


        [HttpPut("{ProductId}")]
        public async Task<IActionResult> Put(int ProductId, UpdateProductCommand command)
        {
            if (ProductId != command.ProductId)
            {
                return BadRequest();
            }
            return Ok(await Mediator.Send(command));
        }

        [HttpDelete]
        [Route("{ProductId}")]
        public async Task<IActionResult> Delete(int ProductId)
        {
            return Ok(await Mediator.Send(new DeleteProductByIdCommand { Id = ProductId }));
        }
    }
}
