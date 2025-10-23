using Microsoft.EntityFrameworkCore;
using Apteka_razor.Data.Models;

namespace Apteka_razor.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Drug> Drugs { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<SaleDetail> SaleDetails { get; set; }
        public DbSet<Pharmacy> Pharmacies { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>()
                .HasOne(e => e.Pharmacy)
                .WithMany(p => p.Employees)
                .HasForeignKey(e => e.PharmacyId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
