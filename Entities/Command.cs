using System;
using System.Collections.Generic;
using System.Linq;

namespace Entities
{
    public enum Command
    {
        [CommandAction("Business.Commands.SetCommandExecution, Business")]
        SET = 0,
        [CommandAction("Business.Commands.GetCommandExecution, Business")]
        GET = 1,
        [CommandAction("Business.Commands.IncrCommandExecution, Business")]
        INCR = 2,
    }

    public class CommandActionAttribute : Attribute
    {
        string FQTN;
        static Dictionary<string, CommandActionAttribute> commandActionAttributeByAction = new Dictionary<string, CommandActionAttribute>();

        static CommandActionAttribute()
        {
            commandActionAttributeByAction = new Dictionary<string, CommandActionAttribute>();
            foreach (var member in typeof(Command).GetFields())
            {
                CommandActionAttribute mbrAttribute = member.GetCustomAttributes(typeof(CommandActionAttribute), true).FirstOrDefault() as CommandActionAttribute;
                if (mbrAttribute != null)
                    commandActionAttributeByAction.Add(member.Name, mbrAttribute);
            }
        }

        public CommandActionAttribute(string fqtn)
        {
            FQTN = fqtn;
        }

        public CommandExecution GetCommandExecution()
        {
            return Activator.CreateInstance(Type.GetType(FQTN)) as CommandExecution;
        }

        public static CommandActionAttribute GetCommandActionAttribute(string action)
        {
            if (commandActionAttributeByAction.TryGetValue(action.ToUpper(), out CommandActionAttribute attribute))
                return attribute;
            return null;
        }
    }
}