using SecureId.Ecommerce.ShoppingCart.Application.DTOs;

namespace SecureId.Ecommerce.ShoppingCart.Application.Interfaces
{
    public interface IBaseService
    {
        Task<T> SendAsync<T>(ApiRequest apiRequest);
    }
}
