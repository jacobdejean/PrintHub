using System;
using System.Collections.Generic;

namespace PrintHub.Framework.Commands
{
    class HelpCommand : Command
    {
        public HelpCommand()
        {
            ParameterTokens = new string[] { };
            Operation = (manager, parameters) => 
            {
                Console.WriteLine("Usage Information: ");
                foreach(KeyValuePair<string, Command> kvp in CommandDeclarations.Commands)
                {
                    Console.WriteLine(kvp.Key + ": " + kvp.Value.Description);
                    for(int i = 0; i < kvp.Value.ParameterTokens.Length; i++)
                    {
                        Console.WriteLine("    {0}: {1}", kvp.Value.ParameterTokens[i], kvp.Value.ParameterDescriptions[i]);
                    }
                }
            };
        }
    }
}