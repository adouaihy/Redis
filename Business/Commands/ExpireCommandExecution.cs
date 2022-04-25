using Entities;

namespace Business.Commands
{
    public class ExpireCommandExecution : CommandExecution
    {
        public override object ExecuteCommand(string command)
        {
            string[] parameters = command.Split(" ", System.StringSplitOptions.RemoveEmptyEntries);
            if (parameters.Length != 3)
                return $"Invalid Command {command}";

            string variableName = parameters[1];

            if (!int.TryParse(parameters[2], out int expireAfter))
            {
                return $"Invalid Command {command}";
            }

            CommandExecutionManager.CurrentInstance.Expire(variableName, expireAfter, out string errorMessage);
            if (!string.IsNullOrEmpty(errorMessage))
                return errorMessage;

            return $"{variableName} will expire after {expireAfter} seconds";
        }
    }
}