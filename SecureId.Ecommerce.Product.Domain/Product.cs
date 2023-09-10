using System.ComponentModel.DataAnnotations;

namespace SecureId.Ecommerce.Product.Domain
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        [Range(0, 1000)]
        public double Price { get; set; }
        public string Description { get; set; }
        public string CategoryName { get; set; }
        public string ImageUrl { get; set; }
    }
}
