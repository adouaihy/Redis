using Entities;
using System;
using System.Collections.Generic;

namespace Business.Commands
{
    public class GetCommandExecution : CommandExecution
    {
        public override object ExecuteCommand(string command)
        {
            string[] parameters = command.Split(" ", System.StringSplitOptions.RemoveEmptyEntries);
            if (parameters.Length != 2)
                return $"Invalid Command {command}";

            string variableName = parameters[1];

            var value = CommandExecutionManager.CurrentInstance.Get(variableName, out string errorMessage);
            if (value == null)
                return errorMessage;

            List<object> valueAsObjectList = value as List<object>;
            if (valueAsObjectList != null)
                return $"{variableName} is of type list";

            return value;
        }
    }
}