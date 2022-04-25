using Entities;

namespace Business.Commands
{
    public abstract class IncrDecrCommandExecution : CommandExecution
    {
        protected object ExecuteCommand_Internal(string command, bool isIncrement)
        {
            string[] parameters = command.Split(" ", System.StringSplitOptions.RemoveEmptyEntries);
            if (parameters.Length != 2 && parameters.Length != 3)
                return $"Invalid Command {command}";

            string variableName = parameters[1];

            int increment = 1;
            if (parameters.Length == 3)
            {
                if (!int.TryParse(parameters[2], out int incrementAsInt))
                {
                    return $"Invalid Command {command}";
                }
                increment = incrementAsInt;
            }

            if (!isIncrement)
                increment *= -1;

            var newValue = CommandExecutionManager.CurrentInstance.Incr(variableName, increment, out string errorMessage);
            if (newValue == null)
                return errorMessage;

            return $"{variableName} is set to {newValue}";
        }
    }
}