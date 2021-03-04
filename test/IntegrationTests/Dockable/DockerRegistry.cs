using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IntegrationTests.Dockable
{
    public abstract class DockerRegistry
    {
        protected readonly List<string> _containersRegistred = new List<string>();
        protected readonly DockerEngine _dockerEngine;

        public DockerRegistry(DockerEngine dockerEngine)
        {
            _dockerEngine = dockerEngine;
        }

        public abstract Task DownloadImageAsync();
        public abstract Task InstallContainerAsync();
        public abstract Task HealthCheckContainerAsync();

        public async Task CleanAsync()
        {
            foreach (var containerId in _containersRegistred)
            {
                await _dockerEngine.RemoveContainerAsync(containerId);
            }
        }

        protected void StoreContainerId(string containerId)
        {
            if (string.IsNullOrEmpty(containerId))
                throw new ArgumentNullException(nameof(containerId));

            _containersRegistred.Add(containerId);
        }
    }
}
