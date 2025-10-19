using Apteka_razor.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Apteka_razor.Data
{
    public class PharmacyDbContext : DbContext
    {
        public DbSet<Pharmacy> Pharmacy => Set<Pharmacy>();
        public DbSet<Employee> Employees => Set<Employee>();
        public DbSet<Customer> Customer => Set<Customer>();
        public DbSet<Drug> Drugs => Set<Drug>();
        public DbSet<Sale> Sales => Set<Sale>();
        public DbSet<SaleDetail> SaleDetail => Set<SaleDetail>();

        public PharmacyDbContext(DbContextOptions<PharmacyDbContext> options) : base(options) { }
    }
}