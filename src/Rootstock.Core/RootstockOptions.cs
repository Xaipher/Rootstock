using System.Collections.Generic;

namespace Rootstock.Core

{
    public class RootstockOptions
    {
        public IEnumerable<string> AssemblyNames { get; set; } = new List<string>();
        public AssemblyFilterMode AssemblyFilterMode { get; set; } = AssemblyFilterMode.Default;

        public bool IncludeUnreferencedAssemblies { get; set; } = false;
        
        public static RootstockOptions DefaultOptions => new RootstockOptions();
    }
}