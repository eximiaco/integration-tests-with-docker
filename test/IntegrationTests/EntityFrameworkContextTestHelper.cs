using CustomerAPI.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace IntegrationTests
{
    public class EntityFrameworkContextTestHelper
    {
        private readonly string _databaseConnectionString;

        public EntityFrameworkContextTestHelper(string databaseConnectionString)
        {
            _databaseConnectionString = databaseConnectionString;
        }

        public CustomerContext CreateContext()
        {
            return new CustomerContext(new DbContextOptionsBuilder<CustomerContext>().UseSqlServer(_databaseConnectionString).Options);
        }

        public string GetConnectionString() => _databaseConnectionString;

        public async Task InitializeDatabase()
        {
            await SeedDatabase();
        }

        public async Task SeedDatabase()
        {
            using var context = CreateContext();
            await context.Database.EnsureCreatedAsync();
        }

        public async Task CleanupTestsAndDropDatabaseAsync()
        {
            using var context = CreateContext();
            await context.Database.EnsureDeletedAsync();
        }
    }
}
