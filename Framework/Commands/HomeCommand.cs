using System;
using System.Collections.Generic;
using PrintHub.Framework.Commands.GUI;

namespace PrintHub.Framework.Commands
{
    class HomeCommand : Command
    {
        public HomeCommand()
        {
            ParameterTokens = new string[] { };
            Operation = (manager, parameters) =>
            {
                string[,,] operations = new string[,,]
                    {
                     { {"help"}, {"display"}, {"test"}},
                     {{"search"}, {"file"}, {"printer"}},
                     {{"quit"}, {"b"}, {"b"}}
                    };

                bool CommandSelected = false;

                Page home = new Page(PageLayout.Grid, "  {0,-10}  ", operations);

                home.SetHeader("Home", "Choose an operation below");
                home.SetFooter("to exit", "to execute operation");

                home.IncludeBranding = true;

                while (!CommandSelected)
                {
                    Coordinate selection = home.Execute();

                    Command cmd = CommandDeclarations.Commands[operations[selection.X, selection.Y, 0]];

                    List<string> p = new List<string>();

                    if (cmd.ParameterTokens.Length > 0)
                    {
                        Page parameterPage = new Page(PageLayout.Info, "{0}", new string[,,] 
                        { { { string.Join(' ', cmd.ParameterTokens) } }});

                        parameterPage.SetHeader(operations[selection.X, selection.Y, 0], " Enter parameters");

                        parameterPage.Execute();

                        Console.WriteLine();
                        Console.Write("~: ");

                        string[] input = Console.ReadLine().Split(' ');
                        Console.ResetColor();

                        p = new List<string>(input);
                    }
                    commandSelected = true;
                    CommandDeclarations.Commands[operations[selectedX, selectedY]].Operation(manager, p);
                }
                manager.QuitFlag = true;

                /*
                

                */
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