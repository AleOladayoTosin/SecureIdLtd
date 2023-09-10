using MediatR;
using Microsoft.EntityFrameworkCore;
using SecureId.Ecommerce.Product.Application.DTOs;
using SecureId.Ecommerce.Product.Persistence;

namespace SecureId.Ecommerce.Product.API.IntegrationEvent
{
    public class GetDiscountForCodeEventHandler
    {
        public class Query : IRequest<ResponseMessage> { public string Code { get; set; } }

        public class Handler : IRequestHandler<Query, ResponseMessage>
        {
            private readonly DataContext _context;
            public Handler(DataContext context)
            {
                _context = context;
            }
            public async Task<ResponseMessage> Handle(Query request, CancellationToken cancellationToken)
            {
                var couponModel = await _context.Coupons
                    .Where(c => c.CouponCode == request.Code).FirstOrDefaultAsync();
                if (couponModel != null)
                    return new ResponseMessage { Data = couponModel, Status = true };

                return new ResponseMessage { Message = "Not Found", Status = false };

            }
        }
    }
}
