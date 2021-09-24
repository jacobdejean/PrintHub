using System;

namespace PrintHub.Framework.Commands
{
    class QuitCommand : Command
    {
        public QuitCommand()
        {
            Description = "Closes this session.";
            ParameterTokens = new string[] { };
            ParameterDescriptions = new string[] { };
            Operation = (m, parameters) => 
            {
                m.QuitFlag = true;
                Console.WriteLine("Closing...");
            };
        }
    }
}