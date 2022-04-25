using Entities;
using System.Collections.Generic;

namespace Business.Commands
{
    public class LPushCommandExecution : PushCommandExecution
    {
        public override object ExecuteCommand(string command)
        {
            return ExecuteCommand_Internal(command, false);
        }
    }
}