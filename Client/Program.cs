using Helper;
using System;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello Client!");
            while (true)
            {
                Console.WriteLine("Please enter new command");
                string command = Console.ReadLine();
                string response = Sender.Send("127.0.0.1", 8000, command);
                Console.WriteLine(response);
            }
        }
    }
}
