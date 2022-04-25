using System;

namespace Entities
{
    public abstract class CommandExecution
    {
        public abstract object ExecuteCommand(string command);
    }
}
