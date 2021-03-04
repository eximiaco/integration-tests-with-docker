using CustomerAPI.Infrastructure;
using IntegrationTests.Dockable;
using System.Threading.Tasks;
using Xunit;
using Xunit.Extensions.Ordering;

namespace IntegrationTests
{
    /// <summary>
    /// Classe base para testes de Integração.
    /// O IAssemblyFixture determina que o escopo da classe de teste será executado uma vez por projeto de teste. 
    /// Ou seja, InitializeAsync e DisposeAsync esta a nível de Suite de testes.
    ///     -> Assembly Scope <AssemblyFixture>.
    ///         -> Class Scope <ClassFixture>.
    ///             -> Test Scope <Constructor> and <Dispose>.
    /// </summary>
    public abstract class SqlServerIntegrationTestBase : IAsyncLifetime, IAssemblyFixture<SqlServerDockerCollectionFixture>
    {
        protected readonly SqlServerDockerCollectionFixture _fixture;
        protected EntityFrameworkContextTestHelper _testHelper;

        protected SqlServerIntegrationTestBase(SqlServerDockerCollectionFixture fixture)
        {
            _fixture = fixture;
        }

        public virtual async Task InitializeAsync()
        {
            var sqlConnectionString = _fixture.GetSqlConnectionString();
            _testHelper = new EntityFrameworkContextTestHelper(sqlConnectionString);
            await _testHelper.InitializeDatabase();
        }

        public virtual async Task DisposeAsync()
        {
            await _testHelper.CleanupTestsAndDropDatabaseAsync();
        }

        protected CustomerContext GetContext() => _testHelper.CreateContext();
    }
}
