using System;
using System.Collections.Generic;
using PrintHub.Framework.Commands.GUI;

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
                    string[,,] commands = new string[CommandDeclarations.Commands.Keys.Count, 1, 2];

                    int i = 0; 
                    foreach(KeyValuePair<string, Command> kvp in CommandDeclarations.Commands)
                    {
                        commands[i, 0, 0] = kvp.Key;
                        commands[i, 0, 1] = kvp.Value.Description;
                        i++;
                    }

                    Page help = new Page(PageLayout.List, " {0} ", ) 
                    Console.WriteLine("    {0,-30}    ","[Help] View usage information below");
                    
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