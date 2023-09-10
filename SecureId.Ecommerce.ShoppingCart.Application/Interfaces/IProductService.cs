using SecureId.Ecommerce.ShoppingCart.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecureId.Ecommerce.ShoppingCart.Application.Interfaces
{
    public interface IProductService
    {
        Task<ResponseMessage> GetProduct(Guid productId, string accessToken);
        Task<ResponseMessage> GetCoupon(string code, string accessToken);
    }
}
