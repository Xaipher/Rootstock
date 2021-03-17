using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace Rootstock.Core.Extensions
{
    public static class AssemblyExtensions
    {
        public static TAttribute GetAttribute<TAttribute>(this Assembly self) where TAttribute : Attribute => (TAttribute)Attribute.GetCustomAttribute(self, typeof(TAttribute));

        public static bool HasAttribute<TAttribute>(this Assembly self) where TAttribute : Attribute => self.GetAttribute<TAttribute>() != null;
    }
}