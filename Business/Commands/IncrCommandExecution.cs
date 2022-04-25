﻿using Entities;

namespace Business.Commands
{
    public class IncrCommandExecution : IncrDecrCommandExecution
    {
        public override object ExecuteCommand(string command)
        {
            return ExecuteCommand_Internal(command, true);
        }
    }
}