using Entities;

namespace Data
{
    public interface ICommandExecutionDataManager
    {
        void Set(string key, RedisData data);
        RedisData Get(string key);
        void Remove(string key);
    }
}