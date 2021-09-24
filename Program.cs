using System;
using System.Collections.Generic;
using Framework;

namespace PrintHub
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to PrintHub! Type -help to see how to get started.");
            Console.WriteLine("Before continuing, use '-unit' to fill with test data");
            PrintHubManager manager = new PrintHubManager();
            if(Console.ReadLine().Equals("-unit"))
            {
                Console.WriteLine("Adding printers");
                string[] printernames = { "Creality CR-6 SE", "Creality CR-10 Mini", "Creality Ender-3 Pro V2"};
                for(int i = 0; i < 3; i++)
                {
                    manager.Printers.Add(new Printer(printernames[i], i));
                    Console.WriteLine("    -Added {0} with ID {1}", printernames[i], i);
                }

                Console.WriteLine("Adding v1 items");
                string[] filenames = {"front_cup", "back_cup", "front_right_rail", "front_left_rail", "back_right_rail", "back_left_rail", "cupholder", "mag_retainer", "stock" };
                for(int i = 0; i < 9; i++)
                {
                    string name = filenames[i];
                    int restriction = new Random().Next(3);
                    manager.TrackFile(name, restriction);
                    Console.WriteLine("    -Added file {0} with restriction {1}", name, restriction);
                }

                Console.WriteLine("Randomizing versions");
                for(int i = 0; i < 9; i++)
                {
                    int increment = new Random().Next(8);
                    for(int j = 0; j < increment; j++)
                    {
                        manager.TrackFile(filenames[i], -1);
                    }
                    
                    Console.WriteLine("    -Upgraded '{0}' by {1} versions", filenames[i], increment);
                }
            }

            bool stopped = false;
            while(!stopped)
            {
                string cmd = Console.ReadLine();

                switch(cmd)
                {
                    case "-help":
                    {
                        Console.WriteLine("Help");
                        break;
                    };
                    case "-display":
                    {
                        Console.WriteLine("Current Manager State:\nPrinters:");
                        foreach(Printer p in manager.Printers)
                            Console.WriteLine("  -{0}~{1}", p.PrinterModel, p.ID);

                        Console.WriteLine("Tracked Files:");
                        foreach(PrintFile t in manager.TrackedFiles)
                            Console.WriteLine("  -Name: {0} Version: {1} Restriction: {2}", t.Name, t.Version, t.PrinterRestriction);

                        Console.WriteLine("Archived Files:");
                        foreach(PrintFile a in manager.ArchivedFiles)
                            Console.WriteLine("  -Name: {0} Version: {1} Restriction: {2}", a.Name, a.Version, a.PrinterRestriction);
                        break;
                    };
                    case "-search":
                    {
                        Console.WriteLine("Enter Name (Leave blank to omit):");
                        string name = Console.ReadLine();

                        Console.WriteLine("Enter Version (Leave blank to omit):");
                        string version = Console.ReadLine();

                        Console.WriteLine("Enter Restriction (-1 to omit):");
                        int restriction = int.Parse(Console.ReadLine());

                        SearchTerm term = new SearchTerm()
                        {
                            Name = name,
                            Version = version,
                            PrinterRestriction = restriction
                        };

                        foreach(PrintFile f in  manager.GetArchivedFiles(term))
                        {
                            Console.WriteLine(f.GetFileName());
                        }

                        break;
                    };
                    case "-addprinter":
                    {
                        Console.WriteLine("Enter full brand and model name:");

                        manager.AddPrinter(Console.ReadLine());

                        Console.WriteLine("Added Printer.");

                        break;
                    };
                    case "-removeprinter":
                    {
                        Console.WriteLine("Enter ID:");

                        manager.AddPrinter(Console.ReadLine());

                        Console.WriteLine("Removed Printer.");

                        break;
                    };
                    case "-trackfile":
                    {
                        Console.WriteLine("Enter part name:");
                        string name = Console.ReadLine();

                        Console.WriteLine("Enter printer restriction ID:");
                        int restriction = int.Parse(Console.ReadLine());

                        manager.TrackFile(name, restriction);

                        Console.WriteLine("Started tracking file {0}. Use -linkfile to link gcode to this tracked file.");
                        
                        break;
                    };
                    case "-linkfile":
                    {
                        Console.WriteLine("Enter part name:");
                        string name = Console.ReadLine();

                        if(manager.ValidateTrack(name))
                        {
                            Console.WriteLine("Enter full path to file:");
                            string path = Console.ReadLine();
                            manager.GetTrackedFile(name).LinkFile(path);
                        }
                        else
                        {
                            Console.WriteLine("Part name not found. Use -display to see a list of all tracked files, or -search to narrow down a specific one.");
                        }
                        break;
                    };
                    case "-quit":
                    {
                        Console.WriteLine("Quit");
                        stopped = true;
                        break;
                    };
                    default:
                        Console.WriteLine("Please enter valid command.");
                        break;
                }
            }
        }
    }
}
