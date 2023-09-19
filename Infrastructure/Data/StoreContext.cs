using Microsoft.EntityFrameworkCore;
using Core.Entities;

namespace Infrastructure.Data
{
    public class StoreContext : DbContext
    {
        public StoreContext(DbContextOptions<StoreContext> options) : base(options)
        {
        }

        // DbSet properties for your entities go here
        public DbSet<Product> Products { get; set; }

        // Other configuration and methods specific to your context
    }
}
