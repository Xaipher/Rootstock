using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Rootstock.Core.Extensions
{
    public static class AssemblyCollectionExtensions
    {
        public static Assembly[] RemoveMicrosoftAssemblies(this Assembly[] self)
        {
            var nonMicrosoftAssemblies = new List<Assembly>();

            foreach (var assembly in self)
            {
                var assemblyName = assembly.GetName();

                if (assemblyName.IsMicrosoftAssembly())
                {
                    continue;
                }
                
                nonMicrosoftAssemblies.Add(assembly);
            }

            return nonMicrosoftAssemblies.ToArray();
        }
        
        public static Assembly[] RemoveRootstockAssemblies(this Assembly[] self)
        {
            var nonMicrosoftAssemblies = new List<Assembly>();

            foreach (var assembly in self)
            {
                var assemblyName = assembly.GetName();

                if (assemblyName.IsRootstockAssembly())
                {
                    continue;
                }
                
                nonMicrosoftAssemblies.Add(assembly);
            }

            return nonMicrosoftAssemblies.ToArray();
        }

        public static Assembly[] Exclude(this Assembly[] self, IEnumerable<string> assemblyNamesToExclude)
        {
            var localAssemblyNamesToExclude = assemblyNamesToExclude.ToList();
            
            if (!localAssemblyNamesToExclude.Any())
            {
                return self;
            }

            var assembliesToInclude = new List<Assembly>();

            foreach (var nameToExclude in localAssemblyNamesToExclude)
            {
                if (string.IsNullOrWhiteSpace(nameToExclude))
                {
                    continue;
                }
                
                foreach (var assembly in self)
                {
                    if (string.IsNullOrWhiteSpace(assembly.FullName))
                    {
                        continue;
                    }

                    if (assembly.FullName.ToLowerInvariant().Contains(nameToExclude))
                    {
                        continue;
                    }
                    
                    assembliesToInclude.Add(assembly);
                }
                
            }

            return assembliesToInclude.ToArray();
        }

        public static Assembly[] FilterTo(this Assembly[] self, IEnumerable<string> assembliesToInclude)
        {
            var localAssembliesToInclude = assembliesToInclude.ToList();

            var filteredAssemblies = new HashSet<Assembly>();

            foreach (var assemblyName in localAssembliesToInclude)
            {
                foreach (var assembly in self)
                {
                    if (string.IsNullOrWhiteSpace(assembly.FullName))
                    {
                        continue;
                    }

                    if (!assembly.FullName.Contains(assemblyName))
                    {
                        continue;
                    }
                    
                    if (!filteredAssemblies.Contains(assembly))
                    {
                        filteredAssemblies.Add(assembly);
                    }
                }
            }

            return filteredAssemblies.ToArray();
        }
    }
}