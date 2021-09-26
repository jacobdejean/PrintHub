using System;
using System.Collections.Generic;

namespace PrintHub.Framework.Commands
{
    class CommandDeclarations
    {
        public static Dictionary<string, Command> Commands;

        public static void InitializeCommands()
        {
            Commands = new Dictionary<string, Command>(
                new KeyValuePair<string, Command>[] 
            {
                new KeyValuePair<string, Command>("help", new HelpCommand()),
                new KeyValuePair<string, Command>("quit", new QuitCommand()),
                new KeyValuePair<string, Command>("test", new TestDataCommand()),
                new KeyValuePair<string, Command>("display", new DisplayCommand()),
                new KeyValuePair<string, Command>("search", new SearchCommand()),
                new KeyValuePair<string, Command>("printer", new PrinterCommand()),
                new KeyValuePair<string, Command>("file", new FileCommand()),
                new KeyValuePair<string, Command>("home", new HomeCommand())
            });
        }
    }
}