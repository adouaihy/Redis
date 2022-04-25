using System;
using System.Collections.Generic;
using System.Reflection;

namespace Data
{
    public static class DataFactory
    {
        static Dictionary<Type, Type> s_explicitImplementedTypes;
        static List<Assembly> _assemblies;

        static DataFactory()
        {
            s_explicitImplementedTypes = new Dictionary<Type, Type>();
            _assemblies = new List<Assembly>() { Assembly.Load("Data.Memory") };
        }

        public static T GetDataManager<T>() where T : class, ICommandExecutionDataManager
        {
            Type implementationType = GetImplementationType(typeof(T));
            return Activator.CreateInstance(implementationType) as T;
        }

        private static Type GetImplementationType(Type baseType)
        {
            if (s_explicitImplementedTypes.TryGetValue(baseType, out Type implementationType))
                return implementationType;

            foreach (var a in _assemblies)
            {
                foreach (Type t in a.GetTypes())
                {
                    if (baseType.IsAssignableFrom(t) && t.IsClass && !t.IsAbstract)
                    {
                        return t;
                    }
                }
            }
            throw new ArgumentException($"Could not find implementation of the base type '{baseType}'");
        }
    }
}