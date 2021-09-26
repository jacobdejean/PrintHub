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
            PrintHubManager manager = new PrintHubManager();

            CommandDeclarations.InitializeCommands();
            while(!manager.QuitFlag)
            {
                CommandDeclarations.Commands["home"].Operation(manager, new List<string>());
                Console.Clear();
            }
        }
    }
}
