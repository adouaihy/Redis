using Data;
using Entities;
using System;

namespace Business
{
    public class CommandExecutionManager
    {
        public static CommandExecutionManager CurrentInstance;

        static CommandExecutionManager()
        {
            CurrentInstance = new CommandExecutionManager();
        }

        public void Set(string key, object value)
        {
            ICommandExecutionDataManager commandExecutionDataManager = DataFactory.GetDataManager<ICommandExecutionDataManager>();
            RedisData data = new RedisData() { Value = value };
            commandExecutionDataManager.Set(key, data);
        }

        public object Incr(string key, int? increment, out string errorMessage)
        {
            object oldValue = Get(key, out errorMessage);
            if (oldValue == null)
                return null;

            if (!int.TryParse(oldValue.ToString(), out int oldValueAsInt))
            {
                errorMessage = $"{key} is not of type number";
                return null;
            }

            int newIncrement = increment.HasValue ? increment.Value : 1;
            int newValue = oldValueAsInt + newIncrement;

            Set(key, newValue);
            return newValue;
        }

        public object Get(string key, out string errorMessage)
        {
            errorMessage = null;
            ICommandExecutionDataManager commandExecutionDataManager = DataFactory.GetDataManager<ICommandExecutionDataManager>();
            RedisData data = commandExecutionDataManager.Get(key);
            if (data == null)
            {
                errorMessage = $"{key} is not available";
                return null;
            }

            if (data.IsExpired)
            {
                errorMessage = $"{key} is expired";
                return null;
            }

            return data.Value;
        }
    }
}