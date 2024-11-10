using Microsoft.EntityFrameworkCore;
using PaypalDemo.Models;

namespace PaypalDemo
{
    public class TransactionDbContext : DbContext
    {
        public TransactionDbContext()
        {
        }

        public TransactionDbContext(DbContextOptions<TransactionDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TransactionPayPal>()
                .HasKey(t => t.PaymentId);
        }

        public DbSet<TransactionPayPal> TransactionPayPals { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //LAPTOP-5CRSBD7F\\CMSSQLSERVER;Database=PayPalDemo;User Id=as;Password=123456;TrustServerCertificate=True
            optionsBuilder.UseSqlServer("Server=.;Database=PayPalDemo;User Id=sa;Password=1234567890;TrustServerCertificate=True");
        }
    }
}
