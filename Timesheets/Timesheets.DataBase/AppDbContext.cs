using Microsoft.EntityFrameworkCore;
using Timesheets.Entities;

namespace Timesheets.DataBase
{
    public sealed class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Contract> Contracts { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Sheet> Sheets { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Invoice>()
            .Property(e => e.Sum)
            .HasConversion(
                v => v.Amount,
                v => new Money(v));
        }
    }
}
