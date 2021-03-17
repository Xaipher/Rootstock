using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Rootstock.Core.Extensions;

namespace Rootstock.Core
{
    public class AssemblyProvider
    {
        private readonly RootstockOptions _options;

        public AssemblyProvider(RootstockOptions options)
        {
            _options = options;
        }

        public List<Assembly> GetCurrentAssembliesFromAppDomain()
        {
            var assemblies = AppDomain
                .CurrentDomain
                .GetAssemblies()
                .RemoveMicrosoftAssemblies()
                .RemoveRootstockAssemblies();

            switch (_options.AssemblyFilterMode)
            {
                case AssemblyFilterMode.Exclude:
                    assemblies = assemblies.Exclude(_options.AssemblyNames);
                    break;
                case AssemblyFilterMode.Include:
                    assemblies = assemblies.FilterTo(_options.AssemblyNames);
                    break;
            }

            return assemblies.ToList();
        }
    }
}