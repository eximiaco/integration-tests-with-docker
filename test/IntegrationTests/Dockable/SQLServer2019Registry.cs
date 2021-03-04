using Docker.DotNet.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace IntegrationTests.Dockable
{
    public class SQLServer2019Registry : DockerRegistry
    {
        private readonly SqlServerDockerSettings _settings;
        private readonly DockerImageInfo _dockerImageInfo;

        public SQLServer2019Registry(DockerEngine dockerEngine, SqlServerDockerSettings settings) : base(dockerEngine)
        {
            _settings = settings;
            _dockerImageInfo = DockerImageInfo.New(_settings);
        }

        public override async Task HealthCheckContainerAsync()
        {
            await WaitUntilDatabaseAvailableAsync(
                _settings.TotalTimeToWaitUntilDatabaseStartedInSeconds,
                _settings.GetConnectionString()
            );
        }

        public override async Task DownloadImageAsync()
        {
            await _dockerEngine.DownloadImageAsync(_dockerImageInfo);
        }

        public override async Task InstallContainerAsync()
        {
            await _dockerEngine.RemoveContainersByPrefixAsync(_settings.DockerContainerPrefix);
            await _dockerEngine.CreateImageAsync(_dockerImageInfo);
            var containerId = await _dockerEngine.EnsureCreateAndStartContainer(SqlParameters(_dockerImageInfo));
            StoreContainerId(containerId);
        }

        private CreateContainerParameters SqlParameters(DockerImageInfo dockerImageInfo)
        {
            return new CreateContainerParameters
            {
                Name = _settings.DockerContainerPrefix + Guid.NewGuid(),
                Image = dockerImageInfo.Image,
                Env = new List<string>
                {
                    "ACCEPT_EULA=Y",
                    $"SA_PASSWORD={_settings.SAPassword}"
                },
                HostConfig = new HostConfig
                {
                    PortBindings = new Dictionary<string, IList<PortBinding>>
                    {
                        {
                            "1433/tcp",
                            new PortBinding[]
                            {
                                new PortBinding
                                {
                                    HostPort = _settings.DatabasePort
                                }
                            }
                        }
                    }
                }
            };
        }

        private async Task WaitUntilDatabaseAvailableAsync(int maxWaitTimeInSeconds, string connectionString)
        {
            var start = DateTime.UtcNow;
            var connectionEstablised = false;

            while (!connectionEstablised && start.AddSeconds(maxWaitTimeInSeconds) > DateTime.UtcNow)
            {
                try
                {
                    using var sqlConnection = new SqlConnection(connectionString);
                    await sqlConnection.OpenAsync();
                    connectionEstablised = true;
                }
                catch
                {
                    // If opening the SQL connection fails, SQL Server is not ready yet
                    await Task.Delay(500);
                }
            }

            if (!connectionEstablised)
            {
                throw new Exception("Connection to the SQL docker database could not be established within 60 seconds.");
            }

            return;
        }
    }
}
