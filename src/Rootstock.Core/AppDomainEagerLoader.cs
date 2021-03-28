using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Rootstock.Core.Exceptions;
using Rootstock.Core.Extensions;

namespace Rootstock.Core
{
    public static class AppDomainEagerLoader
    {
        public static void LoadReferencedAssemblies()
        {
            var assembliesToCheck = new Queue<Assembly>();
            var loadedAssemblies = new HashSet<string>();

            var entryAssembly = Assembly.GetEntryAssembly();

            if (entryAssembly is null)
            {
                throw new InvalidOperationException(nameof(AppDomainEagerLoader), new InvalidEntryAssemblyException());
            }
            
            assembliesToCheck.Enqueue(entryAssembly);

            while (assembliesToCheck.Any())
            {
                var currentAssembly = assembliesToCheck.Dequeue();
                var currentReferencedAssemblyNames = currentAssembly
                    .GetReferencedAssemblies()
                    .RemoveRootstockAssemblyNames()
                    .RemoveMicrosoftAssemblyNames();
                
                foreach (var assemblyName in currentReferencedAssemblyNames)
                {
                    var referencedAssemblyName = assemblyName.FullName;
                    if (loadedAssemblies.Contains(referencedAssemblyName))
                    {
                        continue;
                    }

                    var referencedAssembly = Assembly.Load(referencedAssemblyName);
                    assembliesToCheck.Enqueue(referencedAssembly);
                    loadedAssemblies.Add(referencedAssemblyName);
                }
            }
        }

        public static void LoadUnreferencedAssemblies(RootstockOptions options)
        {
            var workingDirectory = AppDomain.CurrentDomain.BaseDirectory;

            var allAssemblyNames = Directory.EnumerateFiles(workingDirectory, "*.dll", SearchOption.AllDirectories)
                .Select(dllName => new AssemblyName(Path.GetFileNameWithoutExtension(dllName)))
                .Where(assemblyName => !assemblyName.IsRootstockAssembly())
                .Where(assemblyName => !assemblyName.IsMicrosoftAssembly())
                .Distinct();

            switch (options.AssemblyFilterMode)
            {
                case AssemblyFilterMode.Exclude:
                    allAssemblyNames = allAssemblyNames
                        .Where(assemblyName => !string.IsNullOrWhiteSpace(assemblyName.Name))
                        .Where(assemblyName => !options.AssemblyNames.Any(assemblyName.Name.StartsWith));
                    break;
                case AssemblyFilterMode.Include:
                    allAssemblyNames = allAssemblyNames
                        .Where(assemblyName => !string.IsNullOrWhiteSpace(assemblyName.Name))
                        .Where(assemblyName => options.AssemblyNames.Any(assemblyName.Name.StartsWith));
                    break;
            }

            var filteredAssemblyNames = allAssemblyNames.ToList();

            var currentAssemblies = AppDomain
                .CurrentDomain
                .GetAssemblies()
                .RemoveRootstockAssemblies()
                .RemoveMicrosoftAssemblies();

            foreach (var assemblyName in filteredAssemblyNames)
            {
                
                if (currentAssemblies.Any(assembly => assembly.GetName().Equals(assemblyName)))
                {
                    continue;
                }

                Assembly.Load(assemblyName);
            }
        }
    }
}