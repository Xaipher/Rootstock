using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Rootstock.Core;
using Rootstock.Core.Extensions;
using Rootstock.Mvc.Attributes;

namespace Rootstock.Mvc
{
    internal class RootstockMvcApplicationPartDiscoveryService
    {
        private readonly AssemblyProvider _assemblyProvider;

        public RootstockMvcApplicationPartDiscoveryService(AssemblyProvider assemblyProvider)
        {
            _assemblyProvider = assemblyProvider;
        }

        public IList<Assembly> Find()
        {
            var assemblies = _assemblyProvider.GetCurrentAssembliesFromAppDomain();

            var applicationPartAssemblies = assemblies
                .Where(assembly => assembly.HasAttribute<RootstockMvcApplicationPartAttribute>())
                .Select(assembly => assembly)
                .ToList();

            return applicationPartAssemblies;
        }
    }
}