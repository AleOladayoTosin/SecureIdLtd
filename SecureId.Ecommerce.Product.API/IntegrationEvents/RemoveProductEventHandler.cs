using AutoMapper;
using MediatR;
using SecureId.Ecommerce.Product.Application.DTOs;
using SecureId.Ecommerce.Product.Persistence;

namespace SecureId.Ecommerce.Product.API.IntegrationEvent
{
    public class RemoveProductEventHandler
    {
        public class Command : IRequest<ResponseMessage>
        {
            public Guid Id { get; set; }
        }
        public class Handler : IRequestHandler<Command, ResponseMessage>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;
            public Handler(DataContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<ResponseMessage> Handle(Command request, CancellationToken cancellationToken)
            {
                var product = await _context.Products.FindAsync(request.Id);

                if (product == null) return new ResponseMessage { Message = "Not Found", Status = false };

                _context.Remove(product);

                var result = await _context.SaveChangesAsync() > 0;
               
                if (!result) return new ResponseMessage { Message = "Failed to delete product", Status = false };

                return new ResponseMessage { Message = "Successfully Deleted", Status = true };
            }
        }
    }
    
}
