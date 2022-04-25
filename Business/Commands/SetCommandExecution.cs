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

            Manager.Add(parameters[1], parameters[2]);
            return $"{variableName} is set to {variableValue}";
        }
    }
}