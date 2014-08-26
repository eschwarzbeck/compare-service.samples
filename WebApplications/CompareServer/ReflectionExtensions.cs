using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace CompareServer
{
    public static class ReflectionExtensions
    {
        public static T GetCustomAttribute<T>(this Assembly assembly) where T : Attribute
        {
            return GetCustomAttributes<T>(assembly).FirstOrDefault();
        }

        public static IEnumerable<T> GetCustomAttributes<T>(this Assembly assembly) where T : Attribute
        {
            return Attribute.GetCustomAttributes(assembly, typeof(T), false).Cast<T>();
        }

        public static T GetCustomAttribute<T>(this Type assembly) where T : Attribute
        {
            return GetCustomAttributes<T>(assembly).FirstOrDefault();
        }

        public static IEnumerable<T> GetCustomAttributes<T>(this Type assembly) where T : Attribute
        {
            return Attribute.GetCustomAttributes(assembly, typeof(T), false).Cast<T>();
        }
    }
}
