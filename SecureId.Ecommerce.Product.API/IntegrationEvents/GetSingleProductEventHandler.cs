using MediatR;
using SecureId.Ecommerce.Product.Application.DTOs;
using SecureId.Ecommerce.Product.Persistence;

namespace SecureId.Ecommerce.Product.API.IntegrationEvent
{
    public class GetSingleProductEventHandler
    {
        public class Query : IRequest<ResponseMessage> { public Guid Id { get; set; } }

        public class Handler : IRequestHandler<Query, ResponseMessage>
        {
            private readonly DataContext _context;
            public Handler(DataContext context)
            {
                _context = context;
            }
            public async Task<ResponseMessage> Handle(Query request, CancellationToken cancellationToken)
            {
                var product = await _context.Products.FindAsync(request.Id);
                if (product != null)
                    return new ResponseMessage { Data = product, Status = true };

                return new ResponseMessage { Message = "Not Found", Status = false };

            }
        }
    }
}
