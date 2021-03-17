using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Rootstock.Processors
{
    internal class InstallerProcessor
    {
        private readonly ILogger<InstallerProcessor> _logger;
        private readonly IServiceCollection _serviceCollection;
        private readonly IConfiguration _configuration;

        private InstallerProcessor(ILoggerFactory loggerFactory, IServiceCollection serviceCollection,
            IConfiguration configuration)
        {
            _logger = loggerFactory.CreateLogger<InstallerProcessor>();
            _serviceCollection = serviceCollection;
            _configuration = configuration;
        }

        public static InstallerProcessor Build(ILoggerFactory loggerFactory, IServiceCollection services,
            IConfiguration configuration)
            => new InstallerProcessor(loggerFactory, services, configuration);

        public void Process(IList<RootstockDiscoveryResult> discoveredInstallers)
        {
            var orderedInstallers = discoveredInstallers
                .OrderBy(installer => installer.LocalPriority.Priority)
                .Select(installer => installer)
                .ToList();

            foreach (var installer in orderedInstallers)
            {
                _logger.LogDebug("Installing {InstallerName}", installer.Instance.GetType().Name);

                switch (installer.Instance)
                {
                    case IRootstockServicesInstaller servicesInstaller:
                        servicesInstaller.Install(_serviceCollection);
                        continue;
                    case IRootstockConfigurationInstaller configurationInstaller:
                        configurationInstaller.Install(_configuration);
                        continue;
                    case IRootstockInstaller fullInstaller:
                        fullInstaller.Install(_serviceCollection, _configuration);
                        continue;
                }
            }
        }
    }
}