using Data;
using Entities;
using System;
using System.Collections.Generic;

namespace Business
{
    public class CommandExecutionManager
    {
        public static CommandExecutionManager CurrentInstance;

        static CommandExecutionManager()
        {
            CurrentInstance = new CommandExecutionManager();
        }

        public void Flush()
        {
            ICommandExecutionDataManager commandExecutionDataManager = DataFactory.GetDataManager<ICommandExecutionDataManager>();
            commandExecutionDataManager.Flush();
        }

        public void Set(string key, object value, int? expireAfter = null)
        {
            ICommandExecutionDataManager commandExecutionDataManager = DataFactory.GetDataManager<ICommandExecutionDataManager>();
            RedisData data = new RedisData() { Value = value };
            if (expireAfter.HasValue)
                data.SetExpirationDate(expireAfter.Value);

            commandExecutionDataManager.Set(key, data);
        }

        public object GetAtIndex(string key, int index, out string errorMessage)
        {
            object value = Get(key, out errorMessage);
            if (value == null)
                return null;

            List<object> valueAsObjectList = value as List<object>;
            if (valueAsObjectList == null)
            {
                errorMessage = $"{key} is not of type list";
                return null;
            }
            if (index >= valueAsObjectList.Count)
            {
                errorMessage = $"{key} contains only {valueAsObjectList.Count} elements";
                return null;
            }
            return valueAsObjectList[index];
        }

        public List<object> Push(string key, List<object> values, bool isRight, out string errorMessage)
        {
            object value = Get(key, out errorMessage);
            if (value == null)
            {
                Set(key, values);
                return values;
            }

            List<object> valueAsObjectList = value as List<object>;
            if (valueAsObjectList == null)
            {
                errorMessage = $"{key} is not of type list";
                return null;
            }
            if (isRight)
                valueAsObjectList.AddRange(values);
            else
                valueAsObjectList.InsertRange(0, values);

            Set(key, valueAsObjectList);
            return valueAsObjectList;
        }

        internal List<object> Pop(string key, int countToPop, bool isRight, out string errorMessage)
        {
            object value = Get(key, out errorMessage);
            if (value == null)
                return null;

            List<object> valueAsObjectList = value as List<object>;
            if (valueAsObjectList == null)
            {
                errorMessage = $"{key} is not of type list";
                return null;
            }

            if (valueAsObjectList.Count <= countToPop)
            {
                Remove(key);
                return valueAsObjectList;
            }

            List<object> result = new List<object>();
            if (isRight)
            {
                for (int i = countToPop; i >= 1; i--)
                {
                    object item = valueAsObjectList[valueAsObjectList.Count - i];
                    result.Add(item);
                    valueAsObjectList.RemoveAt(valueAsObjectList.Count - i);
                }
            }
            else
            {
                for (int i = 0; i < countToPop; i++)
                {
                    object item = valueAsObjectList[0];
                    result.Add(item);
                    valueAsObjectList.RemoveAt(0);
                }
            }
            Set(key, valueAsObjectList);

            return result;
        }

        public object Incr(string key, int increment, out string errorMessage)
        {
            object oldValue = Get(key, out errorMessage);
            if (oldValue == null)
                return null;

            if (!int.TryParse(oldValue.ToString(), out int oldValueAsInt))
            {
                errorMessage = $"{key} is not of type number";
                return null;
            }

            int newValue = oldValueAsInt + increment;

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
                Remove(key);
                return null;
            }

            return data.Value;
        }

        public void Expire(string key, int expireAfter, out string errorMessage)
        {
            object value = Get(key, out errorMessage);
            if (value == null)
                return;

            Set(key, value, expireAfter);
        }

        public void Remove(string key)
        {
            ICommandExecutionDataManager commandExecutionDataManager = DataFactory.GetDataManager<ICommandExecutionDataManager>();
            commandExecutionDataManager.Remove(key);
        }
    }
}