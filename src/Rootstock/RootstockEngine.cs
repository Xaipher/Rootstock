using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Rootstock.Core;
using Rootstock.Processors;

namespace Rootstock
{
    internal class RootstockEngine
    {
        private readonly ILogger<RootstockEngine> _logger;

        private readonly ILoggerFactory _loggerFactory;
        private readonly IServiceCollection _serviceCollection;
        private readonly IConfiguration _configuration;
        private readonly RootstockOptions _options;

        private readonly RootstockInstallerDiscoveryService _discoveryService;

        public RootstockEngine(ILoggerFactory loggerFactory, IServiceCollection services, IConfiguration configuration, RootstockInstallerDiscoveryService discoveryService, RootstockOptions options)
        {
            _loggerFactory = loggerFactory;
            _logger = loggerFactory.CreateLogger<RootstockEngine>();
            _discoveryService = discoveryService;
            _options = options;

            _serviceCollection = services;
            _configuration = configuration;
        }

        public void Run()
        {
            var discoveredInstallers = InitializeAppDomainAndFindInstallers(_loggerFactory);
            ExecuteInstallers(_loggerFactory, _serviceCollection, _configuration, discoveredInstallers);

            _logger.LogInformation("Successfully executed {InstallerCount} installers", discoveredInstallers.Count);
        }

        private IList<RootstockDiscoveryResult> InitializeAppDomainAndFindInstallers(ILoggerFactory loggerFactory)
        {
            var appDomainProcessor = AppDomainProcessor.Build(loggerFactory, _options);
            appDomainProcessor.InitializeAppDomain();

            var discoveredInstallers = _discoveryService.Find();
            return discoveredInstallers;
        }

        /// <summary>
        /// Searches the AppDomain for installers
        /// Should be executed after InitializeAppDomain()
        /// </summary>
        /// <returns></returns>
        public IList<RootstockDiscoveryResult> FindInstallers()
        {
            var discoveredInstallers = _discoveryService.Find();
            return discoveredInstallers;
        }

        private void ExecuteInstallers(ILoggerFactory loggerFactory, IServiceCollection services,
            IConfiguration configuration,
            IList<RootstockDiscoveryResult> discoveredInstallers)
        {
            var discoveryResultProcessor = DiscoveryResultProcessor.Build(loggerFactory, services, configuration);
            discoveryResultProcessor.Process(discoveredInstallers);
        }
    }
}