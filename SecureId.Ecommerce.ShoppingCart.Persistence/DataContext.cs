
using Microsoft.EntityFrameworkCore;
using SecureId.Ecommerce.ShoppingCart.Domain;

namespace SecureId.Ecommerce.ShoppingCart.Persistence
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options) { }
        public DbSet<Product> Products { get; set; }
        public DbSet<CartHeader> CartHeaders { get; set; }
        public DbSet<CartDetails> CartDetails { get; set; }
        public DbSet<OrderHeader> orderHeaders { get; set; }
        public DbSet<OrderDetails> orderDetails { get; set; }
    }
}
