using Entities;

namespace Business
{
    public class CommandEvaluator
    {
        public object EvaluateCommand(string command)
        {
            if (string.IsNullOrEmpty(command))
                return "Empty Command received";

            string commandToExecute = command.Split(" ")[0];
            var attribute = CommandActionAttribute.GetCommandActionAttribute(commandToExecute);
            if (attribute == null)
                return $"Invalid Command {commandToExecute}";

            var commandExecution = attribute.GetCommandExecution();
            return commandExecution.ExecuteCommand(command);
        }
    }
}