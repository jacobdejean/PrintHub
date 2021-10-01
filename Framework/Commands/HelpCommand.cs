using System;
using System.Collections.Generic;
using PrintHub.Framework.Commands.GUI;

namespace PrintHub.Framework.Commands
{
    class HelpCommand : Command
    {
        public HelpCommand()
        {
            Description = "View usage information.";
            ParameterTokens = new string[] { };
            Operation = (manager, parameters) =>
            {
                bool finished = false;
                while (!finished)
                {
                    string[,,] commands = new string[CommandDeclarations.Commands.Keys.Count, 1, 2];

                    int i = 0;
                    foreach (KeyValuePair<string, Command> kvp in CommandDeclarations.Commands)
                    {
                        commands[i, 0, 0] = kvp.Key;
                        commands[i, 0, 1] = kvp.Value.Description;
                        i++;
                    }

                    Page help = new Page(PageLayout.List, "  {0}: {1}", commands);

                    help.SetHeader("Help", "View usage infomation");

                    help.SetFooter("to return to previous", "to view more info");

                    Coordinate selection = help.Execute();

                    if (selection.X == -1)
                    {
                        finished = true;
                        break;
                    }

                    string cmd = commands[selection.X, 0, 0];

                    string[,,] mparams = new string[CommandDeclarations.Commands[cmd].ParameterTokens.Length, 1, 2];

                    for (int mi = 0; mi < mparams.GetLength(0); mi++)
                    {
                        mparams[mi, 0, 0] = CommandDeclarations.Commands[cmd].ParameterTokens[mi];
                        mparams[mi, 0, 1] = CommandDeclarations.Commands[cmd].ParameterDescriptions[mi];
                    }

                    Page more = new Page(PageLayout.List, "  {0}: {1}", mparams);

                    more.SetHeader(cmd, "View usage infomation");

                    more.SetFooter("to return to previous", "-");

                    Coordinate mselection = more.Execute();
                }
            };
        }
    }
}