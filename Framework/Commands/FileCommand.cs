using System;

namespace PrintHub.Framework.Commands
{
    class FileCommand : Command
    {
        public FileCommand()
        {
            Description = "Facilitates operations related to linking, tracking, and validating files.";
            ParameterTokens = new string[] { "-name", "-restriction", "-track", "-link", "-update", "-newname" };
            ParameterDescriptions = new string[] 
            { 
                "Specifies the target name for file.", 
                "Specifies the printer restriction. Omitting defaults to -1 (no restriction)", 
                "Starts tracking a new file and archives outdated ones.", 
                "Links gcode to a tracked file.",
                "Updates specified restriction and, if -newname is used, renames.",
                "Specifies a new name for file."
            };
            Operation = (manager, parameters) => 
            {
                string name = "";
                int restriction = -1;
                string path = "";
                string newname = "";

                if(parameters.Contains("-name"))
                {
                    name = parameters[parameters.IndexOf("-name") + 1];
                }

                if(parameters.Contains("-restriction"))
                {
                    restriction = int.Parse(parameters[parameters.IndexOf("-restriction") + 1]);
                }

                if(parameters.Contains("-path"))
                {
                    path = parameters[parameters.IndexOf("-path") + 1];
                }

                if(parameters.Contains("-track"))
                {
                    if(!string.IsNullOrEmpty(name))
                    {
                        manager.TrackFile(name, restriction);
                        Console.WriteLine("Tracking file");
                    }
                    else
                        Console.WriteLine("File tracking requires a name to be specified.");
                }

                if(parameters.Contains("-link"))
                {
                    if(!string.IsNullOrEmpty(name))
                    {
                        manager.LinkFile(name, path);
                        Console.WriteLine("Linked file");
                    }
                    else
                        Console.WriteLine("File linking requires a specified file name and path.");
                }

                if(parameters.Contains("-newname"))
                {
                    newname = parameters[parameters.IndexOf("-newname") + 1];
                }

                if(parameters.Contains("-update"))
                {
                    if(restriction > -1)
                    {
                        manager.GetTrackedFile(name).PrinterRestriction = restriction;
                        manager.GetArchivedFiles(new SearchTerm() { Name = name }).ForEach(
                        (p) => 
                        {
                            p.PrinterRestriction = restriction;
                        });
                    }

                    if(!string.IsNullOrEmpty(newname))
                    {
                        if(!manager.GetValidator().ValidateTrack(newname))
                        {
                            manager.GetTrackedFile(name).Name = newname;
                            foreach(PrintFile pf in manager.GetArchivedFiles(new SearchTerm() { Name = name, PrinterRestriction = -1 }))
                            {
                                pf.Name = newname;
                            }
                        }
                        else
                            Console.WriteLine("File already exists with that name.");
                    }

                    Console.WriteLine("Updated file.");
                }
            };
        }
    }
}