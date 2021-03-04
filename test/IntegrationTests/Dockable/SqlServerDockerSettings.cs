namespace IntegrationTests.Dockable
{
    /// <summary>
    /// Configurações para criar um container de Sql Server no docker.
    /// </summary>
    public class SqlServerDockerSettings
    {
        private const int DefaultTimeWaitUntilDatabaseStartedInSeconds = 60;
        public static SqlServerDockerSettings Default => new SqlServerDockerSettings(
            "Password!@",
            "mcr.microsoft.com/mssql/server",
            "2019-latest",
            "MyProjectIntegrationTestsSql-"
        );

        public SqlServerDockerSettings()
        {
        }

        public SqlServerDockerSettings(string saPassword, string dockerImageName, string dockerImageTag, string dockerContainerPrefix)
            : this(saPassword, dockerImageName, dockerImageTag, dockerContainerPrefix, null, DefaultTimeWaitUntilDatabaseStartedInSeconds)
        {
        }

        public SqlServerDockerSettings(string saPassword, string dockerImageName, string dockerImageTag, string dockerContainerPrefix, int? databasePort, int? waitUntil)
        {
            SAPassword = saPassword;
            DockerImageName = dockerImageName;
            DockerImageTag = dockerImageTag;
            DockerContainerPrefix = dockerContainerPrefix;
            DatabasePort = databasePort.HasValue ? databasePort.Value.ToString() : TcpPortSelector.GetFreePort().ToString();
            TotalTimeToWaitUntilDatabaseStartedInSeconds = waitUntil ?? DefaultTimeWaitUntilDatabaseStartedInSeconds;
        }

        /// <summary>
        /// Senha do usuário SA do banco de dados, utilizado para criar o banco do zero a partir de Code First.
        /// </summary>
        public string SAPassword { get; set; }

        /// <summary>
        /// Endereço para a imagem do docker a ser utilizado.
        /// Por exemplo: mcr.microsoft.com/mssql/server
        /// </summary>
        public string DockerImageName { get; set; }

        /// <summary>
        /// Utilizado para determimnar qual é a Tag da imagem do docker que desejamos baixar.
        /// Por exemplo: 2019-latest
        /// </summary>
        public string DockerImageTag { get; set; }

        /// <summary>
        /// Utilizado para facilitar na limpeza de containers durante a execução dos testes.
        /// Por exemplo: MyProjectIntegrationTestsSql
        /// </summary>
        public string DockerContainerPrefix { get; set; }

        /// <summary>
        /// Porta que o banco deve utilizar no container.
        /// Caso null, será selecionada uma porta aleatória durante a criação do container.
        /// </summary>
        public string DatabasePort { get; set; }

        /// <summary>
        /// Define quantidade de tempo a ser aguardando enquanto o banco de dados inicializa.
        /// </summary>
        public int TotalTimeToWaitUntilDatabaseStartedInSeconds { get; set; }

        public string GetConnectionString()
        {
            return $"Data Source=localhost,{DatabasePort};Integrated Security=False;User ID=SA;Password={SAPassword}";
        }

        public static SqlServerDockerSettings WithRandomPort(string saPassword, string dockerImageName, string dockerImageTag, string dockerContainerPrefix, int? waitUntil)
        {
            return new SqlServerDockerSettings(saPassword, dockerImageName, dockerImageTag, dockerContainerPrefix, null, waitUntil);
        }
    }
}
