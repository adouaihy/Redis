using Entities;

namespace Business.Commands
{
    public class SetCommandExecution : CommandExecution
    {
        public override object ExecuteCommand(string command)
        {
            string[] parameters = command.Split(" ");
            if (parameters.Length != 3)
                return $"Invalid Command {command}";

            string variableName = parameters[1];
            object variableValue = parameters[2];

            CommandExecutionManager.CurrentInstance.Set(variableName, variableValue);
            return $"{variableName} is set to {variableValue}";
        }
    }
}