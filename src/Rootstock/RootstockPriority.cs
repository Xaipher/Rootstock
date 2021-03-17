using System;
using Rootstock.Settings;

namespace Rootstock
{
    internal class RootstockPriority : IRootstockPriority, IEquatable<RootstockPriority>
    {
        public RootstockPriority(IRootstockPriority? other)
        {
            Priority = other?.Priority ?? DefaultPrioritySettings.Priority;
        }
        
        public int Priority { get; }

        public bool Equals(RootstockPriority? other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Priority == other.Priority;
        }

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((RootstockPriority) obj);
        }

        public override int GetHashCode()
        {
            return Priority;
        }
    }
}