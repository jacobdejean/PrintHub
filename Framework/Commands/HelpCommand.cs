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
                string selectedCommand = "help";
                bool finished = false;
                while(!finished)
                {
                    Console.Clear();
                    Console.WriteLine(@" _                     
                                        /_/ _ . _ _/_ /_/    /_
                                       /   / / / //  / / /_//_/ by Jacob DeJean");

                    WhiteBack();
                    Console.WriteLine("    {0,-30}    ","[Help] View usage information below");
                    BlackBack();
                    
                    foreach(KeyValuePair<string, Command> kvp in CommandDeclarations.Commands)
                    {
                        if(selectedCommand.Equals(kvp.Key))
                            WhiteBack();
                        Console.Write("{0}: ", kvp.Key);
                        for(int i = 0; i < kvp.Value.ParameterTokens.Length; i++)
                        {
                            Console.WriteLine("    {0}: {1}", kvp.Value.ParameterTokens[i], kvp.Value.ParameterDescriptions[i]);
                        }
                    }
                }
            };
        }

        public void WhiteBack()
        {
            Console.ForegroundColor = ConsoleColor.Black;
            Console.BackgroundColor = ConsoleColor.White;
        }

        public void BlackBack()
        {
            Console.ResetColor();   
        }
    }
}