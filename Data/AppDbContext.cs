// Here I am building the bridge between C# and DB.
// I need to call and set my tables in DB


using EcommerceAPI.Model;
using Microsoft.EntityFrameworkCore;

// Define the folder I am in right now
namespace EcommerceAPI.Data
{
    // Create a class that inherits from DbContext
    // Set tables

    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options): base(options){}

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }



        // Config relationships and contraints
        protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                base.OnModelCreating(modelBuilder);

                // One user with one cart
                modelBuilder.Entity<User>()
                .HasOne(u => u.Cart).WithOne(c => c.User).HasForeignKey<Cart>(c => c.UserID).OnDelete(DeleteBehavior.Cascade);
            

                // One cart with many items
                modelBuilder.Entity<Cart>()
                .HasMany(c => c.CartItems).WithOne(ci => ci.Cart).HasForeignKey(ci => ci.CartID).OnDelete(DeleteBehavior.Cascade);
                
                // Many items with one product
                modelBuilder.Entity<CartItem>()
                .HasOne(ci => ci.Product).WithMany().HasForeignKey(ci => ci.ProductID).OnDelete(DeleteBehavior.Restrict);

                // remove duplicates
                modelBuilder.Entity<CartItem>()
                // Get cartid and proudctid together and no duplicates for those two columns
                .HasIndex(ci => new { ci.CartID, ci.ProductID }).IsUnique();
            
                modelBuilder.Entity<Order>()
                .HasOne(o => o.User).WithMany(u => u.Orders).HasForeignKey(o => o.UserID).OnDelete(DeleteBehavior.Cascade);
 


            }
        
    }

    
}