using System;
using System.Diagnostics.CodeAnalysis;

namespace Rootstock.Core.Extensions
{
    public static class TypeExtensions
    {
        public static bool IsNotInterface(this Type self) => !self.IsInterface;
        
        public static bool IsNotAbstract(this Type self) => !self.IsAbstract;
        
        public static bool CanBe<TAssignableFrom>(this Type self) => typeof(TAssignableFrom).IsAssignableFrom(self);

        public static bool HasParameterlessConstructor(this Type self) => self.GetConstructor(Type.EmptyTypes) != null;
        
        public static TAttribute GetAttribute<TAttribute>(this Type self) where TAttribute : Attribute => (TAttribute)Attribute.GetCustomAttribute(self, typeof(TAttribute));

        public static bool HasAttribute<TAttribute>(this Type self) where TAttribute : Attribute => self.GetAttribute<TAttribute>() != null;
    }
}