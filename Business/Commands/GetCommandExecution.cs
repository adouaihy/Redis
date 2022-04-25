using Entities;
using System;

namespace Business.Commands
{
    public class GetCommandExecution : CommandExecution
    {
        public override object ExecuteCommand(string command)
        {
            string[] parameters = command.Split(" ");
            if (parameters.Length != 2)
                return $"Invalid Command {command}";

            var value = CommandExecutionManager.CurrentInstance.Get(parameters[1], out string errorMessage);
            if (value == null)
                return errorMessage;

            return value;
        }
    }
}