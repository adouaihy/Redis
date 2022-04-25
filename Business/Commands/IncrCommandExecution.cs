using Entities;

namespace Business.Commands
{
    public class IncrCommandExecution : CommandExecution
    {
        public override object ExecuteCommand(string command)
        {
            string[] parameters = command.Split(" ");
            if (parameters.Length != 2 && parameters.Length != 3)
                return $"Invalid Command {command}";

            string variableName = parameters[1];

            int? increment = null;
            if (parameters.Length == 3)
            {
                if (!int.TryParse(parameters[2], out int incrementAsInt))
                {
                    return $"Invalid Command {command}";
                }
                increment = incrementAsInt;
            }
                
            var newValue = CommandExecutionManager.CurrentInstance.Incr(variableName, increment, out string errorMessage);
            if (newValue == null)
                return errorMessage;

            return $"{variableName} is set to {newValue}";
        }
    }
}