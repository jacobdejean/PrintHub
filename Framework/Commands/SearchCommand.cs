using System;
using System.Collections.Generic;

namespace PrintHub.Framework.Commands
{
    class SearchCommand : Command
    {
        public SearchCommand()
        {
            Description = "Searches archived files.";
            ParameterTokens = new string[] { "-name", "-version", "-restriction" };
            ParameterDescriptions = new string[]
            {
                "Specify name of file. Omit to exclude from search.",
                "Specify version of file. Omit to exclude from search.",
                "Specify PrinterRestriction ID. Omit or specify -1 to exclude from search."
            };
            Operation = (manager, parameters) =>
            {
                SearchTerm term = new SearchTerm();

                if(parameters.Contains("-name"))
                {
                    term.Name = parameters[parameters.IndexOf("-name") + 1];
                }
                else
                    term.Name = "";

                if(parameters.Contains("-version"))
                {
                    term.Version = parameters[parameters.IndexOf("-version") + 1];
                }
                else
                    term.Version = "";

                if(parameters.Contains("-restriction"))
                {
                    term.PrinterRestriction = int.Parse(parameters[parameters.IndexOf("-restriction") + 1]);
                }
                else
                    term.PrinterRestriction = -1;

                Console.WriteLine("Results:");
                foreach (PrintFile f in manager.GetArchivedFiles(term))
                {
                    Console.WriteLine("    ~" + f.GetFileName());
                }

                Console.ReadKey();
            };
        }
    }
}