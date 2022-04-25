using Entities;
using System;
using System.Collections.Generic;

namespace Data.Memory
{
    public class CommandExecutionDataManager : ICommandExecutionDataManager
    {
        static Dictionary<string, RedisData> memoryStorage = new Dictionary<string, RedisData>();
        static Object obj = new object();

        public void Set(string key, RedisData data)
        {
            lock (obj)
            {
                if (!memoryStorage.ContainsKey(key))
                    memoryStorage.Add(key, data);
                else
                    memoryStorage[key] = data;
            }
        }

        public RedisData Get(string key)
        {
            if (memoryStorage.TryGetValue(key, out RedisData data))
                return data;

            return null;
        }
    }
}
