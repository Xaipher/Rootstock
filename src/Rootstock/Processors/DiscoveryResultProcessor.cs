using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Rootstock.Processors
{
    internal class DiscoveryResultProcessor
    {
        private readonly ILogger<DiscoveryResultProcessor> _logger;
        private readonly InstallerProcessor _installerProcessor;

        private DiscoveryResultProcessor(ILoggerFactory loggerFactory, InstallerProcessor installerProcessor)
        {
            _logger = loggerFactory.CreateLogger<DiscoveryResultProcessor>();
            _installerProcessor = installerProcessor;
        }

        public static DiscoveryResultProcessor Build(ILoggerFactory loggerFactory, IServiceCollection services, IConfiguration configuration) 
            => new DiscoveryResultProcessor(loggerFactory, InstallerProcessor.Build(loggerFactory, services, configuration));

        public void Process(IList<RootstockDiscoveryResult> discoveredInstallers)
        {
            var orderedInstallerGroups = discoveredInstallers
                .Select(discoveryData => discoveryData.RootstockGroup)
                .Distinct()
                .OrderBy(objectGroup => objectGroup.Priority)
                .ToList();

            foreach (var installerGroup in orderedInstallerGroups)
            {
                _logger.LogDebug("Installing Object Group {InstallerGroupName}", installerGroup.Name);
                
                var installersInGroup = discoveredInstallers
                    .Where(installer => installer.RootstockGroup.Equals(installerGroup))
                    .ToList();
                
                _installerProcessor.Process(installersInGroup);
            }
        }
    }
}