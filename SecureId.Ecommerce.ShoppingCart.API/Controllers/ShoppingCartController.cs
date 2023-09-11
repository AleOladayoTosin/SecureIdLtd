using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SecureId.AccessControl.API.IntegrationEvent;
using SecureId.Ecommerce.ShoppingCart.API.IntegrationEvent;
using SecureId.Ecommerce.ShoppingCart.API.IntegrationEvents;
using SecureId.Ecommerce.ShoppingCart.Application.DTOs;

namespace SecureId.Ecommerce.ShoppingCart.API.Controllers
{
    public class ShoppingCartController : BaseApiController
    {
        [Authorize]
        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUserCart(string userId) => 
            HandleResult(await Mediator.Send(new GetCartEventHandler.Query { userId = userId }));

        [Authorize(Roles = "SuperAdmin")]
        [HttpPost("add-cart")]
        public async Task<IActionResult> AddCart(CartDto cart) => HandleResult(await Mediator.Send(new CreateCartEventHandler.Command { cartRequest = cart, accessToken = accessToken() }));
           
       
        [Authorize(Roles = "SuperAdmin")]
        [HttpDelete("{cartId}")]
        public async Task<IActionResult> DeleteProduct(Guid cartId) =>
            HandleResult(await Mediator.Send(new RemoveCartEventHandler.Command { cartDetailsId = cartId }));

        [Authorize]
        [HttpPost("checkout")]
        public async Task<IActionResult> Checkout(CheckoutHeaderDto checkout) => HandleResult(await Mediator.Send(new CheckoutEventHandler.Command { checkoutHeader = checkout, accessToken = accessToken() }));
       

    }
}
