using Business;
using Helper;
using System;

namespace Redis
{
    class Program
    {
        static CommandEvaluator commandEvaluator = new CommandEvaluator();
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Redis Application");
            Receiver.Listen(8000, OnCommandReceived);
        }

        public static object OnCommandReceived(string command)
        {
            object result =  commandEvaluator.EvaluateCommand(command);
            Console.WriteLine($@"Command: {command}");
            Console.WriteLine($@"Result: {result}");
            return result;
        }
    }
}
