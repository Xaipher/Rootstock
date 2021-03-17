using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Microsoft.Extensions.Logging;
using Rootstock.Core;
using Rootstock.Core.Extensions;
using Rootstock.Extensions;

namespace Rootstock
{
    internal class RootstockInstallerDiscoveryService
    {
        private readonly ILogger<RootstockInstallerDiscoveryService> _logger;
        private readonly AssemblyProvider _assemblyProvider;

        public RootstockInstallerDiscoveryService(ILoggerFactory loggerFactory, AssemblyProvider assemblyProvider)
        {
            _logger = loggerFactory.CreateLogger<RootstockInstallerDiscoveryService>();
            _assemblyProvider = assemblyProvider;
        }

        [return: NotNull]
        public IList<RootstockDiscoveryResult> Find()
        {
            var assemblies = _assemblyProvider.GetCurrentAssembliesFromAppDomain();

            _logger.LogDebug("Scanning {AssemblyCount} assemblies for Rootstock Installers", assemblies.Count);

            var discoveredRootstockInstallers = new List<RootstockDiscoveryResult>();

            foreach (var assembly in assemblies)
            {
                var definedObjectInstallerTypes = assembly
                    .GetExportedTypes()
                    .Where(localType => localType.CanBe<IRootstockInstallerMarker>() &&
                                        localType.IsNotInterface() &&
                                        localType.IsNotAbstract())
                    .ToList();
                
                foreach (var objectInstallerType in definedObjectInstallerTypes)
                {
                    var groupData = objectInstallerType.GetObjectGraphGroup();
                    var localPriorityData = objectInstallerType.GetObjectGraphPriority();
                    RootstockDiscoveryResult discoveredInstallerInfo;

                    if (!objectInstallerType.HasParameterlessConstructor())
                    {
                        discoveredInstallerInfo = new RootstockDiscoveryResult(
                            new NullRootstockInstaller(),
                            new RootstockGroup(groupData),
                            new RootstockPriority(localPriorityData),
                            false);
                        discoveredRootstockInstallers.Add(discoveredInstallerInfo);
                        continue;
                    }

                    var instance =  Activator.CreateInstance(objectInstallerType);

                    if (instance is null)
                    {
                        _logger.LogError("Activating installer [{InstallerName}] resulted in a null instance. Installer not added to execution queue", objectInstallerType.FullName);
                        continue;
                    }

                    var rootstockMarkerInstance = (IRootstockInstallerMarker) instance;
                    discoveredInstallerInfo = new RootstockDiscoveryResult(
                        rootstockMarkerInstance,
                        new RootstockGroup(groupData),
                        new RootstockPriority(localPriorityData),
                        true);
                    discoveredRootstockInstallers.Add(discoveredInstallerInfo);
                }
            }

            return discoveredRootstockInstallers;
        }
    }
}