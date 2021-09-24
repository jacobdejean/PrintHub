using System;
using System.Collections.Generic;

namespace PrintHub.Framework.Commands
{
    class DisplayCommand : Command
    {
        public DisplayCommand()
        {
            Description = "Displays current state of the print manager.";
            ParameterTokens = new string[] { "-printers", "-tracked", "-archived", "-all" };
            ParameterDescriptions = new string[]
            {
                "Displays list of printers.",
                "Displays list of tracked files.",
                "Displays list of archived files.",
                "Displays entire state."
            };
            Operation = (manager, parameters) => 
            {
                Console.WriteLine("Current Manager State:");

                if (parameters.Contains("-printers") || parameters.Contains("-all"))
                {
                    Console.WriteLine("Printers:");
                    foreach (Printer p in manager.Printers)
                        Console.WriteLine("  -{0}~{1}", p.PrinterModel, p.ID);
                }

                if (parameters.Contains("-tracked") || parameters.Contains("-all"))
                {
                    Console.WriteLine("Tracked Files:");
                    foreach (PrintFile t in manager.TrackedFiles)
                        Console.WriteLine("  -Name: {0} Version: {1} Restriction: {2}", t.Name, t.Version, t.PrinterRestriction);
                }

                if (parameters.Contains("-archived") || parameters.Contains("-all"))
                {
                    Console.WriteLine("Archived Files:");
                    foreach (PrintFile a in manager.ArchivedFiles)
                        Console.WriteLine("  -Name: {0} Version: {1} Restriction: {2}", a.Name, a.Version, a.PrinterRestriction);
                }
            };
        }
    }
}