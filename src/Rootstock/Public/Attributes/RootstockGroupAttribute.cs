using System;
using System.Diagnostics.CodeAnalysis;
using Rootstock.Settings;

namespace Rootstock.Attributes
{
    public class RootstockGroupAttribute : Attribute, IRootstockGroup
    {
        [DisallowNull] public string Name { get;  set; } =  DefaultGroupSettings.Name;

        public int Priority { get; set; } = DefaultGroupSettings.Priority;
    }
}