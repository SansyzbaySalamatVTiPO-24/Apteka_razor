using Microsoft.EntityFrameworkCore;
using Apteka_razor.Data.Models;
using Apteka_razor.Data;


namespace Apteka_razor.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Pharmacy> Pharmacies { get; set; }
        public DbSet<Drug> Drugs { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<SaleDetail> SaleDetails { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Drug>()
                .Property(d => d.Price)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Sale>()
                .Property(s => s.TotalPrice)
                .HasPrecision(18, 2);
        }

    }

}
