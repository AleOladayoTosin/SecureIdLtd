using AutoMapper;
using MediatR;
using SecureId.Ecommerce.Product.Application.DTOs;
using SecureId.Ecommerce.Product.Persistence;

namespace SecureId.Ecommerce.Product.API.IntegrationEvent
{
    public class CreateProductEventHandler
    {
        public class Command : IRequest<ResponseMessage>
        {
            public ProductDto productRequest { get; set; }
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
                var activity = _mapper.Map<SecureId.Ecommerce.Product.Domain.Product>(request.productRequest);

                _context.Products.Add(activity);

                var result = await _context.SaveChangesAsync() > 0;

                if (!result) return new ResponseMessage { Message = "Failed to create product", Status = false };

                return new ResponseMessage { Message = "Successfully created", Status = true };
            }
        }
    }
    
}
