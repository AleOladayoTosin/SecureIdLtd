using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SecureId.Ecommerce.ShoppingCart.Application.DTOs;
using SecureId.Ecommerce.ShoppingCart.Domain;
using SecureId.Ecommerce.ShoppingCart.Persistence;

namespace SecureId.Ecommerce.ShoppingCart.API.IntegrationEvents
{
    public class GetCartEventHandler
    {
        public class Query : IRequest<ResponseMessage> { public string userId { get; set; } }

        public class Handler : IRequestHandler<Query, ResponseMessage>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;
            public Handler(DataContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }
            public async Task<ResponseMessage> Handle(Query request, CancellationToken cancellationToken)
            {
                Cart cart = new()
                {
                    CartHeader = await _context.CartHeaders
                    .FirstOrDefaultAsync(u => u.UserId == request.userId)
                }; 

                if (cart == null) return new ResponseMessage { Message = "Not Found", Status = false };

                cart.CartDetails = _context.CartDetails
                .Where(c => c.CartHeaderId == cart.CartHeader.CartHeaderId)
                .Include(c => c.Product);

                return new ResponseMessage { Data = _mapper.Map<CartDto>(cart), Status = true };
            }
        }
    }
}
