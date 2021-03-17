using System;
using System.Diagnostics.CodeAnalysis;
using Rootstock.Settings;

namespace Rootstock
{
    internal class RootstockGroup : IRootstockGroup, IEquatable<RootstockGroup>
    {
        public RootstockGroup(IRootstockGroup? other)
        {
            Name = other?.Name ?? DefaultGroupSettings.Name;
            Priority = other?.Priority ?? DefaultGroupSettings.Priority;
        }

        [DisallowNull] public string Name { get; }

        public int Priority { get; }

        public bool Equals(RootstockGroup? other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Name == other.Name && Priority == other.Priority;
        }

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((RootstockGroup) obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name, Priority);
        }
    }
}