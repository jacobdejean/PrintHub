using System;
using System.Collections.Generic;

namespace PrintHub.Framework.Commands
{
    class Command
    {
        public string[] ParameterTokens;
        public Action<PrintHubManager, List<string>> Operation;
        public string Description;
        public string[] ParameterDescriptions;
    }
}