using System;

namespace PrintHub.Framework.Commands
{
    class TestDataCommand : Command
    {
        public TestDataCommand()
        {
            Description = "Fills manager with test data.";
            ParameterTokens = new string[] { };
            ParameterDescriptions = new string[] { };
            Operation = (manager, parameters) => 
            {
                Console.WriteLine("This operation cannot be undone. Continue? (y/n): ");

                if(Console.ReadKey().Key == ConsoleKey.Y)
                {
                    string[] printernames = { "Creality CR-6 SE", "Creality CR-10 Mini", "Creality Ender-3 Pro V2"};
                    for(int i = 0; i < 3; i++)
                    {
                        manager.Printers.Add(new Printer(printernames[i], i));
                    }

                    string[] filenames = {"front_cup", "back_cup", "front_right_rail", "front_left_rail", "back_right_rail", "back_left_rail", "cupholder", "mag_retainer", "stock" };
                    for(int i = 0; i < 9; i++)
                    {
                        string name = filenames[i];
                        int restriction = new Random().Next(3);
                        manager.TrackFile(name, restriction);
                    }

                    for(int i = 0; i < 9; i++)
                    {
                        int increment = new Random().Next(8);
                        for(int j = 0; j < increment; j++)
                        {
                            manager.TrackFile(filenames[i], -1);
                        }
                    }
                }
                    
                Console.WriteLine("\nFilled manager with test data");
            };
        }
    }
}