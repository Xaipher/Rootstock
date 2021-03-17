using System.Reflection;

namespace Rootstock.Core.Extensions
{
    public static class AssemblyNameExtensions
    {
        public static bool IsMicrosoftAssembly(this AssemblyName self)
        {
            var fullNameLower = self.FullName.ToLowerInvariant();
            return fullNameLower.StartsWith("microsoft") ||
                   fullNameLower.StartsWith("system") ||
                   fullNameLower.StartsWith("netstandard");
        }
        
        public static bool IsRootstockAssembly(this AssemblyName self)
        {
            var fullNameLower = self.FullName.ToLowerInvariant();
            return fullNameLower.StartsWith("rootstock");
        }
        
    }
}