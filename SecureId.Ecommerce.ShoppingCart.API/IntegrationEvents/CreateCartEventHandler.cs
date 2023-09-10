using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SecureId.Ecommerce.ShoppingCart.Application.DTOs;
using SecureId.Ecommerce.ShoppingCart.Application.Interfaces;
using SecureId.Ecommerce.ShoppingCart.Domain;
using SecureId.Ecommerce.ShoppingCart.Persistence;

namespace SecureId.Ecommerce.ShoppingCart.API.IntegrationEvent
{
    public class CreateCartEventHandler
    {
        public class Command : IRequest<ResponseMessage>
        {
            public CartDto cartRequest { get; set; }
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
                Cart cart = _mapper.Map<Cart>(request.cartRequest);
                //check if product exists in database, if not create it!
                foreach (var cartItem in cart.CartDetails)
                {

                    var prodInDb = await _context.Products
                        .FirstOrDefaultAsync(u => u.ProductId == cartItem
                        .ProductId);

                    if (prodInDb == null)
                    {
                        var product = await _productService.GetProduct(cartItem.ProductId, request.accessToken);
                        if (product.Status)
                        {
                            var productDetail = JsonConvert.SerializeObject(product.Data);
                            prodInDb = JsonConvert.DeserializeObject<Product>(productDetail);
                            _context.Products.Add(prodInDb);
                            await _context.SaveChangesAsync();
                        }
                    }

                    var cartHeaderFromDb = await _context.CartHeaders.AsNoTracking()
                        .FirstOrDefaultAsync(u => u.UserId == cart.CartHeader.UserId);
                    if (cartHeaderFromDb == null)
                    {
                        CartHeader cartHeader = new();

                        cartHeader.UserId = cart.CartHeader.UserId;

                        var createHeader = _context.CartHeaders.Add(cartHeader);
                        await _context.SaveChangesAsync();

                        cartItem.CartHeaderId = cartHeader.CartHeaderId;
                        cartItem.ProductId = prodInDb.ProductId;

                        _context.CartDetails.Add(cartItem);
                        await _context.SaveChangesAsync();
                    }
                    else
                    {
                        var cartDetailsFromDb = await _context.CartDetails.AsNoTracking()
                            .FirstOrDefaultAsync(u => u.ProductId == prodInDb.ProductId
                            && u.CartHeaderId == cartHeaderFromDb.CartHeaderId);

                        if (cartDetailsFromDb == null)
                        {
                           cartItem.CartHeaderId = cartHeaderFromDb.CartHeaderId;
                           cartItem.ProductId = prodInDb.ProductId;

                           _context.CartDetails.Add(cartItem);
                            await _context.SaveChangesAsync();
                        }
                        else
                        {
                            cartItem.ProductId = prodInDb.ProductId;
                            cartItem.Count += cartDetailsFromDb.Count;
                            cartItem.CartDetailsId = cartDetailsFromDb.CartDetailsId;
                            cartItem.CartHeaderId = cartDetailsFromDb.CartHeaderId;

                            _context.CartDetails.Update(cartItem);
                            await _context.SaveChangesAsync();
                        }
                    }
                    
                }

                return new ResponseMessage { Data = _mapper.Map<CartDto>(cart), Status = true, Message = "Add Product to cart" };
            }
        }
    }
    
}
