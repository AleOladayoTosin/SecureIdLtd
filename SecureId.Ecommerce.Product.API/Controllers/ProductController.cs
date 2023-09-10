using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SecureId.Ecommerce.Product.API.IntegrationEvent;
using SecureId.Ecommerce.Product.Application.DTOs;

namespace SecureId.Ecommerce.Product.API.Controllers
{
    public class ProductController : BaseApiController
    {
        [Authorize(Roles = "SuperAdmin")]
        [HttpGet]
        public async Task<IActionResult> GetProducts() => 
            HandleResult(await Mediator.Send(new GetProductsEventHandler.Query()));
        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct(Guid id) => 
            HandleResult(await Mediator.Send(new GetSingleProductEventHandler.Query { Id = id }));

        [Authorize(Roles = "SuperAdmin")]
        [HttpPost]
        public async Task<IActionResult> CreateProduct(ProductDto product) => 
            HandleResult(await Mediator.Send(new CreateProductEventHandler.Command { productRequest = product }));

        [Authorize(Roles = "SuperAdmin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> EditProduct(Guid id, ProductDto product) => 
            HandleResult(await Mediator.Send(new EditProductEventHandler.Command { productRequest = product, Id = id }));

        [Authorize(Roles = "SuperAdmin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(Guid id) =>
            HandleResult(await Mediator.Send(new RemoveProductEventHandler.Command { Id = id }));

        [Authorize]
        [HttpGet("{code}")]
        public async Task<IActionResult> GetProduGetDiscountForCodect(string code) =>
            HandleResult(await Mediator.Send(new GetDiscountForCodeEventHandler.Query { Code = code }));

    }
}
