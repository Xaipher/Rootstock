using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Rootstock.Core;

namespace Rootstock
{
    public static class RootstockServiceCollectionExtensions
    {
        [return: NotNull]
        public static IServiceCollection AddRootstock(this IServiceCollection self, [DisallowNull] IConfiguration configuration)
        {
            _ = self ?? throw new ArgumentNullException(nameof(IServiceCollection), new NullServiceCollectionException());
            
            InternalAddRootstock(self, configuration, RootstockOptions.DefaultOptions);

            return self;
        }
        
        [return: NotNull]
        public static IServiceCollection AddRootstock(this IServiceCollection self, [DisallowNull] IConfiguration configuration, [DisallowNull] Action<RootstockOptions> options)
        {
            _ = self ?? throw new ArgumentNullException(nameof(IServiceCollection), new NullServiceCollectionException());

            var localObjectGraphOptions = RootstockOptions.DefaultOptions;
            options(localObjectGraphOptions);

            InternalAddRootstock(self, configuration, localObjectGraphOptions);

            return self;
        }

        private static void InternalAddRootstock([DisallowNull] IServiceCollection services, [DisallowNull] IConfiguration configuration, [DisallowNull] RootstockOptions options)
        {
            var loggerFactory = services.GetLoggerFactory();

            var assemblyProvider = new AssemblyProvider(options);
            var discoveryService = new RootstockInstallerDiscoveryService(loggerFactory, assemblyProvider);
            var rootstockEngine = new RootstockEngine(loggerFactory, services, configuration, discoveryService, options);
            rootstockEngine.Run();
        }
    }
}