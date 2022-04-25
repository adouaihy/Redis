using System;
using System.Collections.Generic;

namespace Business
{
    public static class Manager
    {
        static Dictionary<string, object> data = new Dictionary<string, object>();

        public static void Add(string key, object value)
        {
            data.Add(key, value);
        }

        public static object Get(string key)
        {
            if (data.TryGetValue(key, out object value))
                return value;

            return $"{key} is not available";
        }
    }
}