using System;
using Rootstock.Attributes;
using Rootstock.Core.Extensions;

namespace Rootstock.Extensions
{
    internal static class TypeExtensions
    {
        public static IRootstockGroup GetObjectGraphGroup(this Type self) => new RootstockGroup(self.GetAttribute<RootstockGroupAttribute>());
        
        public static IRootstockPriority GetObjectGraphPriority(this Type self) => new RootstockPriority(self.GetAttribute<RootstockPriorityAttribute>());   
    }
}