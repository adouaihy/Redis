using Entities;

namespace Business.Commands
{
    public class LPopCommandExecution : PopCommandExecution
    {
        public override object ExecuteCommand(string command)
        {
            return ExecuteCommand_Internal(command, false);
        }
    }
}