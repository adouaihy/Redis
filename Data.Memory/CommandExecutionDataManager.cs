using Entities;
using System;
using System.Collections.Generic;

namespace Data.Memory
{
    public class CommandExecutionDataManager : ICommandExecutionDataManager
    {
        static Dictionary<string, RedisData> memoryStorage = new Dictionary<string, RedisData>();

        public void Flush()
        {
            memoryStorage.Clear();
        }

        public void Set(string key, RedisData data)
        {
            if (!memoryStorage.ContainsKey(key))
                memoryStorage.Add(key, data);
            else
                memoryStorage[key] = data;
        }

        public RedisData Get(string key)
        {
            if (memoryStorage.TryGetValue(key, out RedisData data))
                return data;

            return null;
        }

        public void Remove(string key)
        {
            memoryStorage.Remove(key);
        }
    }
}
