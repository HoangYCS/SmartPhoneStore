using Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Data.ModelDbContext
{
    public class MyDBContext(DbContextOptions options) : IdentityDbContext<User, IdentityRole<int>, int>(options)
    {
        public DbSet<Battery> Batteries { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<CategoryProduct> CategoryProducts { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<ChargingPort> ChargingPorts { get; set; }
        public DbSet<Chip> Chips { get; set; }
        public DbSet<ColorMS> Colors { get; set; }
        public DbSet<MemoryStick> MemorySticks { get; set; }
        public DbSet<OperatingSystemProduct> OperatingSystemProducts { get; set; }
        public DbSet<PhoneSIMType> PhoneSIMTypes { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<RAMMemory> RAMMemories { get; set; }
        public DbSet<ROMMemory> ROMMemories { get; set; }
        public DbSet<Screen> Screens { get; set; }
        public DbSet<SIMType> SIMTypes { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartDetail> CartDetails { get; set; }
        public DbSet<Bill> Bills { get; set; }
        public DbSet<BillDetail> BillDetails { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<PaymentDetail> PaymentDetails { get; set; }
    }
}

