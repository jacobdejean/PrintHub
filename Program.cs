using System;
using System.Collections.Generic;
using PrintHub.Framework;
using PrintHub.Framework.Commands;
using System.Runtime.InteropServices;

namespace PrintHub
{
    class Program
    {
        [DllImport ("libc")]
        private static extern int system (string exec);

        static void Main(string[] args)
        {
            if(RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                Console.SetWindowSize(50, 15);

            if(RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                system(@"printf '\e[8;15;50t'");
                system(@"printf '\e[?4h'");
                system(@"printf '\e[31m'");
                system(@"printf '\e]50;#6\a'");
                //system(@"printf '\e[9;1t'");
            }

            Console.Title = "PrintHub";

            PrintHubManager manager = new PrintHubManager();
            CommandDeclarations.InitializeCommands();
            while(!manager.QuitFlag)
            {
                CommandDeclarations.Commands["home"].Operation(manager, new List<string>());
                Console.Clear();
            }
        }
    }
}
