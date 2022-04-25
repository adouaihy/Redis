using Entities;
using System.Collections.Generic;

namespace Business.Commands
{
    public abstract class PopCommandExecution : CommandExecution
    {
        protected object ExecuteCommand_Internal(string command, bool isRight)
        {
            string[] parameters = command.Split(" ", System.StringSplitOptions.RemoveEmptyEntries);
            if (parameters.Length != 2 && parameters.Length != 3)
                return $"Invalid Command {command}";

            string variableName = parameters[1];

            int countToPop = 1;
            if (parameters.Length == 3)
            {
                if (!int.TryParse(parameters[2], out int countToPopAsInt))
                {
                    return $"Invalid Command {command}";
                }
                countToPop = countToPopAsInt;
            }

            List<object> data = CommandExecutionManager.CurrentInstance.Pop(variableName, countToPop, isRight, out string errorMessage);
            if (data == null)
                return errorMessage;

            return $"{string.Join(" ", data)}";
        }
    }
}