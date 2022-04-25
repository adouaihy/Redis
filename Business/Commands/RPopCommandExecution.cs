using Entities;

namespace Business.Commands
{
    public class RPopCommandExecution : PopCommandExecution
    {
        public override object ExecuteCommand(string command)
        {
            return ExecuteCommand_Internal(command, true);
        }
    }
}