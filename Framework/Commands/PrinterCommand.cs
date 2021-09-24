using System;

namespace PrintHub.Framework.Commands
{
    class PrinterCommand : Command
    {
        public PrinterCommand()
        {
            Description = "Facilitates operations related to the printers.";
            ParameterTokens = new string[] { "-add", "-remove", "-validate" };
            ParameterDescriptions = new string[]
            {
                "Adds printer to manager. Specify model name with no spaces.",
                "Removes printer from manager. Specify restriction ID.",
                "Validates state of printers."
            };
            Operation = (manager, parameters) =>
            {
                if (parameters.Contains("-add"))
                {
                    manager.AddPrinter(parameters[parameters.IndexOf("-add") + 1]);

                    Console.WriteLine("Added Printer.");
                }

                if (parameters.Contains("-remove"))
                {
                    manager.RemovePrinter(int.Parse(parameters[parameters.IndexOf("-remove") + 1]));

                    Console.WriteLine("Removed Printer.");
                }
            };
        }
    }
}