using CustomerAPI.Customers;
using Microsoft.EntityFrameworkCore;

namespace CustomerAPI.Infrastructure
{
    public class CustomerContext : DbContext
    {
        public CustomerContext()
        {

        }

        public CustomerContext(DbContextOptions options) : base(options)
        {
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlite("Data Source=database.db");
        //}

        public DbSet<Customer> Customers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CustomerEntityMap());
        }
    }
}
