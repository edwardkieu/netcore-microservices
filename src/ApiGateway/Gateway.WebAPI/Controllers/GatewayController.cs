using Microsoft.AspNetCore.Mvc;

namespace Gateway.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GatewayController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(new
            {
                Gateway = "Ocelot Gateway: https://www.c-sharpcorner.com/article/microservices-with-ocelot-apigateway-in-asp-net-core/",
                Version = "1.x"
            });
        }
    }
}