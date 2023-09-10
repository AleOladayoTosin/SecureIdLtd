using MediatR;
using Microsoft.EntityFrameworkCore;
using SecureId.Ecommerce.Product.Application.DTOs;
using SecureId.Ecommerce.Product.Persistence;

namespace SecureId.Ecommerce.Product.API.IntegrationEvent
{
    public class GetProductsEventHandler
    {
        public class Query : IRequest<ResponseMessage> { }

        public class Handler : IRequestHandler<Query, ResponseMessage>
        {
            private readonly DataContext _context;
            public Handler(DataContext context)
            {
                _context = context;
            }
            public async Task<ResponseMessage> Handle(Query request, CancellationToken cancellationToken)
            {
                var products =  await _context.Products.ToListAsync();
                if(products.Any())
                    return new ResponseMessage { Data = products, Status = true };

                return new ResponseMessage { Message = "Not Found", Status = false };

            }
        }
    }
}
