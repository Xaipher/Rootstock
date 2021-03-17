using System.Collections.Generic;
using System.Reflection;

namespace Rootstock.Core.Extensions
{
    public
        static class AssemblyNameCollectionExtensions
    {
        public static AssemblyName[] RemoveMicrosoftAssemblyNames(this AssemblyName[] self)
        {
            var nonMicrosoftAssemblies = new List<AssemblyName>();

            foreach (var assemblyName in self)
            {
                if (assemblyName.IsMicrosoftAssembly())
                {
                    continue;
                }
                
                nonMicrosoftAssemblies.Add(assemblyName);
            }

            return nonMicrosoftAssemblies.ToArray();
        }
        
        public static AssemblyName[] RemoveRootstockAssemblyNames(this AssemblyName[] self)
        {
            var nonRootstockAssemblies = new List<AssemblyName>();

            foreach (var assemblyName in self)
            {
                if (assemblyName.IsRootstockAssembly())
                {
                    continue;
                }
                
                nonRootstockAssemblies.Add(assemblyName);
            }

            return nonRootstockAssemblies.ToArray();
        }
    }
}