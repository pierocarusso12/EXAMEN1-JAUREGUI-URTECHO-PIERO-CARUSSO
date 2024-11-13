using Microsoft.EntityFrameworkCore;
using EXAMEN1_JAUREGUI_URTECHO_PIERO_CARUSSO.Models;

namespace EXAMEN1_JAUREGUI_URTECHO_PIERO_CARUSSO.Data
{
    public class NorthwindContext : DbContext
    {
        public NorthwindContext(DbContextOptions<NorthwindContext> options)
            : base(options)
        {
        }

        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrderDetail>()
                .HasKey(od => new { od.OrderID, od.ProductID });

            modelBuilder.Entity<OrderDetail>()
                .HasOne(od => od.Order)
                .WithMany(o => o.OrderDetails)
                .HasForeignKey(od => od.OrderID);

            modelBuilder.Entity<OrderDetail>()
                .HasOne(od => od.Product)
                .WithMany(p => p.OrderDetails)
                .HasForeignKey(od => od.ProductID);
        }
    }
}