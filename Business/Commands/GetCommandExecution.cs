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

            return Manager.Get(parameters[1]);
        }
    }
}