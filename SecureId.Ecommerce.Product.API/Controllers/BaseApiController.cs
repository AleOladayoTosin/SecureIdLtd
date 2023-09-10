using MediatR;
using Microsoft.AspNetCore.Mvc;
using SecureId.Ecommerce.Product.Application.DTOs;

namespace SecureId.Ecommerce.Product.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BaseApiController : ControllerBase
    {
        private IMediator _mediator;
        protected IMediator Mediator => _mediator ??=
        HttpContext.RequestServices.GetService<IMediator>();

        protected IActionResult HandleResult(ResponseMessage result)
        {
            if (result.Status == true)
                return Ok(result);

            return BadRequest(result);
        }
    }
}