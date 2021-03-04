using Docker.DotNet;
using System.IO;
using System.Threading.Tasks;
using Xunit;
using Microsoft.Extensions.Configuration;

namespace IntegrationTests.Dockable
{
    public class SqlServerDockerCollectionFixture : IAsyncLifetime
    {
        private readonly IDockerClient _dockerClient = DockerClientBuilder.Build();

        public const string DATABASE_NAME_PLACEHOLDER = "ApplicationTestDatabase";

        private readonly SqlServerDockerSettings _settings;
        private readonly TestDockerRegistries _dockerRegistries;

        public SqlServerDockerCollectionFixture()
        {
            _dockerRegistries = new TestDockerRegistries();

            var configuration = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json")
               .Build();

            _settings = configuration.GetSection("SqlServerDockerSettings").Get<SqlServerDockerSettings>()
                ?? SqlServerDockerSettings.Default;

            _dockerRegistries.RegisterSqlServer2019(_dockerClient, _settings);
        }

        public string GetSqlConnectionString()
        {
            return $"Data Source=localhost,{ _settings.DatabasePort};" +
                $"Initial Catalog={DATABASE_NAME_PLACEHOLDER};" +
                "Integrated Security=False;" +
                "User ID=SA;" +
                $"Password={_settings.SAPassword}";
        }

        public async Task InitializeAsync()
        {
            await _dockerRegistries.RunAsync();
        }

        public Task DisposeAsync()
        {
            return _dockerRegistries.CleanAsync();
        }
    }
}
