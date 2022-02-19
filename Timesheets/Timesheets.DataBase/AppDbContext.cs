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
    }
}
