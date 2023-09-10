namespace SecureId.Ecommerce.ShoppingCart.Application.DTOs
{
    public class CartDto
    {
        public CartHeaderDto CartHeader { get; set; }
        public IEnumerable<CartDetailsDto> cartDetails { get; set; }
    }
}
