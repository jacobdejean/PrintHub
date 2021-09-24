using System;
using System.Collections.Generic;

namespace Framework.Logging
{
    class Logger
    {
        public static List<Log> Logs = new List<Log>();
        public static bool Verbose = true;

        public static void PostLog(Severity severity, string message, Exception exception)
        {
            Logs.Add(new Log(severity, message, exception));

            if(Verbose)
                Print(Logs[Logs.Count - 1]);
        }

        private static void Print(Log log)
        {
            Console.WriteLine(
                "[{0}:{1}:{2}.{3}] {4}: {5}", 
                log.Timestamp.Hour, 
                log.Timestamp.Minute, 
                log.Timestamp.Second, 
                log.Timestamp.Millisecond, 
                log.Severity, 
                log.Message);
            
            if(log.Exception != null)
            {
                Console.WriteLine(
                    "Exception Information: {0}\n{1}", 
                    log.Exception.Message, 
                    log.Exception.StackTrace);
            }
        }
    }
}