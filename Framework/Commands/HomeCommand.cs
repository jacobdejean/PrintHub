using System;
using System.Collections.Generic;

namespace PrintHub.Framework.Commands
{
    class HomeCommand : Command
    {
        public HomeCommand()
        {
            ParameterTokens = new string[] { };
            Operation = (manager, parameters) => 
            {
                string[,] operations = new string[,] 
                    { 
                    { "help", "display", "test" }, 
                    { "search", "file", "printer" },
                    { "quit", "b", "b" }
                    };
                int selectedX = 0, selectedY = 0;
                bool commandSelected = false;

                while(!commandSelected)
                {
                    Console.Clear();
                    WhiteBack();
                    Console.WriteLine("    [Home] Choose an operation below    ");
                    BlackBack();
                    
                    for(int x = 0; x < operations.GetLength(0); x++)
                    {
                        for(int y = 0; y < operations.GetLength(1); y++)
                        {
                            if(selectedX == x && selectedY == y)
                                WhiteBack();
                            else
                                BlackBack();

                            Console.Write("  {0,-10}  ", operations[x, y]);
                        }
                        Console.Write("\n");
                    }

                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.Write("Or type command: ");
                    BlackBack();

                    ConsoleKey key = Console.ReadKey().Key;
                    
                    switch(key)
                    {
                        case ConsoleKey.UpArrow:
                            selectedX -= selectedX > 0 ? 1 : 0;
                            break;
                        case ConsoleKey.DownArrow:
                            selectedX += selectedX < 3 ? 1 : 0;
                            break;
                        case ConsoleKey.LeftArrow:
                            selectedY -= selectedY > 0 ? 1 : 0;
                            break;
                        case ConsoleKey.RightArrow:
                            selectedY += selectedY < 3 ? 1 : 0;
                            break;
                        case ConsoleKey.Enter:
                        {
                            Command cmd = CommandDeclarations.Commands[operations[selectedX, selectedY]];
                            List<string> p = new List<string>();
                            Console.Clear();
                            if(cmd.ParameterTokens.Length > 0)
                            {
                                WhiteBack();
                                Console.WriteLine("Parameters: ");
                                BlackBack();

                                Console.WriteLine(string.Join(' ', cmd.ParameterTokens));
                                Console.Write("Enter parameters: ");

                                string[] input = Console.ReadLine().Split(' ');
                                Console.ResetColor();

                                p = new List<string>(input);
                            }
                            commandSelected = true;

                            
                            Console.Clear();
                            

                            BlackBack();

                            CommandDeclarations.Commands[operations[selectedX, selectedY]].Operation(manager, p);
                            
                            WhiteBack();
                            Console.WriteLine("[Esc] to return operation selection");
                            BlackBack();
                            if(Console.ReadKey().Key == ConsoleKey.Escape)
                            {
                                ;
                            }

                            break;
                        }
                        default:
                            {
                                break;
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