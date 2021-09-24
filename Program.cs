using System;
using System.Collections.Generic;
using PrintHub.Framework;
using PrintHub.Framework.Commands;

namespace PrintHub
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to PrintHub! Type 'help' to see how to get started.");
            
            PrintHubManager manager = new PrintHubManager();

            CommandDeclarations.InitializeCommands();

            bool stopped = false;

            while(!stopped)
            {
                string[] input = Console.ReadLine().Split(' ');

                string cmd = input[0];

                List<string> parameters = new List<string>(input);

                parameters.RemoveAt(0);

                if (CommandDeclarations.Commands.ContainsKey(cmd))
                    CommandDeclarations.Commands[cmd].Operation(manager, parameters);
                else
                    Console.WriteLine("Please enter valid command.");

                stopped = manager.QuitFlag;
            }
        }
    }
}
