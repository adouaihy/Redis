using Entities;
using System.Collections.Generic;

namespace Business.Commands
{
    public abstract class PushCommandExecution : CommandExecution
    {
        protected object ExecuteCommand_Internal(string command, bool isRight)
        {
            string[] parameters = command.Split(" ", System.StringSplitOptions.RemoveEmptyEntries);
            if (parameters.Length < 3)
                return $"Invalid Command {command}";

            string variableName = parameters[1];
            List<object> variableValues = new List<object>();

            for (int i = 2; i < parameters.Length; i++)
                variableValues.Add(parameters[i]);

            List<object> data = CommandExecutionManager.CurrentInstance.Push(variableName, variableValues, isRight, out string errorMessage);
            if (data == null)
                return errorMessage;

            return $"{variableName} conatins: {string.Join(" ", data)}";
        }
    }
}