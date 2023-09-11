using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SecureId.Ecommerce.ShoppingCart.Application.DTOs;
using SecureId.Ecommerce.ShoppingCart.Application.Interfaces;
using SecureId.Ecommerce.ShoppingCart.Domain;
using SecureId.Ecommerce.ShoppingCart.Persistence;
using SecureId.Ecommerce.ShoppingCart.Persistence.Migrations;

namespace SecureId.Ecommerce.ShoppingCart.API.IntegrationEvent
{
    public class CheckoutEventHandler
    {
        public class Command : IRequest<ResponseMessage>
        {
            public CheckoutHeaderDto checkoutHeader { get; set; }
            public string accessToken { get; set; }
        }
        public class Handler : IRequestHandler<Command, ResponseMessage>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;
            private readonly IProductService _productService;
            public Handler(DataContext context, IMapper mapper, IProductService productServic)
            {
                _context = context;
                _mapper = mapper;
                _productService = productServic;
            }

            public async Task<ResponseMessage> Handle(Command request, CancellationToken cancellationToken)
            {
                Cart cart = new()
                {
                    CartHeader = await _context.CartHeaders
                .FirstOrDefaultAsync(u => u.UserId == request.checkoutHeader.UserId)
                };
                cart.CartDetails = _context.CartDetails
                    .Where(c => c.CartHeaderId == cart.CartHeader.CartHeaderId)
                    .Include(c => c.Product);

                if (!string.IsNullOrEmpty(request.checkoutHeader.CouponCode))
                {
                    var coupon = await _productService.GetCoupon(request.checkoutHeader.CouponCode, request.accessToken);
                    if (coupon.Status)
                    {
                        var couponDetail = JsonConvert.SerializeObject(coupon.Data);
                        var _couponDetail = JsonConvert.DeserializeObject<CouponDto>(couponDetail);
                        if (request.checkoutHeader.DiscountTotal != _couponDetail.DiscountAmount)
                            return new ResponseMessage { Status = false, Message = "Coupon Price has changed, please confirm" };
                    }
                }

                OrderHeader orderHeader = new()
                {
                    UserId = request.checkoutHeader.UserId,
                    FirstName = request.checkoutHeader.FirstName,
                    LastName = request.checkoutHeader.LastName,
                    orderDetails = new List<OrderDetails>(),
                    CardNumber = request.checkoutHeader.CardNumber,
                    CouponCode = request.checkoutHeader.CouponCode,
                    CVV = request.checkoutHeader.CVV,
                    DiscountTotal = request.checkoutHeader.DiscountTotal,
                    Email = request.checkoutHeader.Email,
                    ExpiryMonthYear = request.checkoutHeader.ExpiryMonthYear,
                    DateCreated = DateTime.Now,
                    OrderTotal = request.checkoutHeader.OrderTotal,
                    Phone = request.checkoutHeader.Phone,
                    PaymentStatus = "PENDING",
                    PickUpDateTime = request.checkoutHeader.PickUpDateTime,
                };
                foreach (var item in request.checkoutHeader.cartDetails)
                {
                    OrderDetails orderDetails = new()
                    {
                        ProductId = item.ProductId,
                        Count = item.Count,
                    };
                    orderHeader.CartTotalItem += item.Count;
                    orderHeader.orderDetails.Add(orderDetails);
                }
                _context.orderHeaders.Add(orderHeader);
                await _context.SaveChangesAsync();

                return new ResponseMessage { Status = true, Message = "Checkout Completed" };
            }
        }
    }
    
}
