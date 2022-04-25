using Entities;

namespace Business.Commands
{
    public class DecrCommandExecution : IncrDecrCommandExecution
    {
        public override object ExecuteCommand(string command)
        {
            return ExecuteCommand_Internal(command, false);
        }
    }
}