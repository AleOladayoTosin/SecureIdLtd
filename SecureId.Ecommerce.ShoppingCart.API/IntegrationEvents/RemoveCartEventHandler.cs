using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SecureId.Ecommerce.ShoppingCart.Application.DTOs;
using SecureId.Ecommerce.ShoppingCart.Domain;
using SecureId.Ecommerce.ShoppingCart.Persistence;

namespace SecureId.AccessControl.API.IntegrationEvent
{
    public class RemoveCartEventHandler
    {
        public class Command : IRequest<ResponseMessage>
        {
            public Guid cartDetailsId { get; set; }
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
                CartDetails cartDetails = await _context.CartDetails
                .FirstOrDefaultAsync(x => x.CartDetailsId == request.cartDetailsId);

                int totalCountOfCartItems = _context.CartDetails
               .Where(u => u.CartHeaderId == cartDetails.CartHeaderId).Count();

                _context.CartDetails.Remove(cartDetails);

                if (totalCountOfCartItems == 1)
                {
                    var cartHeaderToRemove = await _context.CartHeaders
                        .FirstOrDefaultAsync(u => u.CartHeaderId == cartDetails.CartHeaderId);

                    _context.CartHeaders.Remove(cartHeaderToRemove);
                }

                var result = await _context.SaveChangesAsync() > 0;
               
                if (!result) return new ResponseMessage { Message = "Failed to remove product", Status = false };

                return new ResponseMessage { Message = "Successfully Remove", Status = true };
            }
        }
    }
    
}
