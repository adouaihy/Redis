using Entities;

namespace Business
{
    public class CommandEvaluator
    {
        static object obj = new object();

        public object EvaluateCommand(string command)
        {
            if (string.IsNullOrEmpty(command))
                return "Empty Command received";

            string commandToExecute = command.Split(" ")[0];
            var attribute = CommandActionAttribute.GetCommandActionAttribute(commandToExecute);
            if (attribute == null)
                return $"Invalid Command {commandToExecute}";

            var commandExecution = attribute.GetCommandExecution();
            object result;
            lock (obj)
            {
                result = commandExecution.ExecuteCommand(command);
            }
            return result;
        }
    }
}