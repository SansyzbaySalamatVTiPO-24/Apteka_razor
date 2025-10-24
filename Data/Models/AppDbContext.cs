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
        public DbSet<Sale> Sales { get; set; } = null!;
        public DbSet<SaleDetail> SaleDetails { get; set; } // EF автоматически множественное


        public DbSet<Pharmacy> Pharmacies { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Указываем точное имя таблицы
            modelBuilder.Entity<SaleDetail>().ToTable("SaleDetail"); // указываем точное имя

            // Если хочешь, можно уточнить типы decimal
            modelBuilder.Entity<SaleDetail>()
                .Property(sd => sd.Price)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Drug>()
                .Property(d => d.Price)
                .HasColumnType("decimal(18,2)");

            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Employee>()
                .HasOne(e => e.Pharmacy)
                .WithMany(p => p.Employees)
                .HasForeignKey(e => e.PharmacyId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Drug>()
                .HasOne(d => d.Pharmacy)
                .WithMany(p => p.Drugs)
                .HasForeignKey(d => d.PharmacyId)
                .OnDelete(DeleteBehavior.NoAction);
        }


    }
}
