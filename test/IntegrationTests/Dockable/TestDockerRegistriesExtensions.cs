using Docker.DotNet;

namespace IntegrationTests.Dockable
{
    public static class TestDockerRegistriesExtensions
    {
        /// <summary>
        /// Adiciona o Registrador para SqlServer 2019
        /// </summary>
        /// <param name="testDockerRegistries"></param>
        /// <param name="dockerEngine"></param>
        /// <param name="settings"></param>
        public static void RegisterSqlServer2019(this TestDockerRegistries testDockerRegistries, DockerEngine dockerEngine, SqlServerDockerSettings settings)
        {
            testDockerRegistries.AddRegistry(new SQLServer2019Registry(dockerEngine, settings));
        }

        /// <summary>
        /// Adiciona o Registrador para SqlServer 2019
        /// </summary>
        /// <param name="testDockerRegistries"></param>
        /// <param name="dockerClient"></param>
        /// <param name="settings"></param>
        public static void RegisterSqlServer2019(this TestDockerRegistries testDockerRegistries, IDockerClient dockerClient, SqlServerDockerSettings settings)
        {
            var dockerEngine = new DockerEngine(dockerClient);
            testDockerRegistries.AddRegistry(new SQLServer2019Registry(dockerEngine, settings));
        }
    }
}
