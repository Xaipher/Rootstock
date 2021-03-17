using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;
using Rootstock.Core;

namespace Rootstock.Mvc
{
    public static class MvcBuilderExtensions
    {
        
        public static IMvcBuilder AddRootstockApplicationParts(this IMvcBuilder self, Action<RootstockOptions> options)
        {
            var localRootstockOptions = RootstockOptions.DefaultOptions;
            options(localRootstockOptions);

            InternalAddRootstockApplicationParts(self, localRootstockOptions);
            
            return self;
        }
        
        public static IMvcBuilder AddRootstockApplicationParts(this IMvcBuilder self)
        {
            InternalAddRootstockApplicationParts(self, RootstockOptions.DefaultOptions);
            
            return self;
        }

        private static void InternalAddRootstockApplicationParts(IMvcBuilder mvcBuilder, RootstockOptions options)
        {
            AppDomainEagerLoader.LoadReferencedAssemblies();

            if (options.IncludeUnreferencedAssemblies)
            {
                AppDomainEagerLoader.LoadUnreferencedAssemblies(options);
            }
            
            var assemblyProvider = new AssemblyProvider(options);
            var discoveryService = new RootstockMvcApplicationPartDiscoveryService(assemblyProvider);

            var applicationPartAssemblies = discoveryService.Find();

            foreach (var applicationPartAssembly in applicationPartAssemblies)
            {
                mvcBuilder.AddApplicationPart(applicationPartAssembly);
            }
        }
        
    }
}