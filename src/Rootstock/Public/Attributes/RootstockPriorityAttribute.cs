using System;

namespace Rootstock.Attributes
{
    public class RootstockPriorityAttribute : Attribute, IRootstockPriority
    {
        public int Priority { get; set; }
    }
}