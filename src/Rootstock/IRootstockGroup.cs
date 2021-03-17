using System.Diagnostics.CodeAnalysis;

namespace Rootstock
{
    internal interface IRootstockGroup
    {
        [DisallowNull] public string Name { get; }
        public int Priority { get; }
    }
}