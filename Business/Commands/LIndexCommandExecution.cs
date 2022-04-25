using Entities;

namespace Business.Commands
{
    public class LIndexCommandExecution : PopCommandExecution
    {
        public override object ExecuteCommand(string command)
        {
            string[] parameters = command.Split(" ", System.StringSplitOptions.RemoveEmptyEntries);
            if (parameters.Length != 3)
                return $"Invalid Command {command}";

            string variableName = parameters[1];

            if (!int.TryParse(parameters[2], out int index))
            {
                return $"Invalid Command {command}";
            }

            object data = CommandExecutionManager.CurrentInstance.GetAtIndex(variableName, index, out string errorMessage);
            if (data == null)
                return errorMessage;

            return data;
        }
    }
}