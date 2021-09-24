using System;
using System.Collections.Generic;

namespace Framework.Logging
{
    class Log
    {
        public Severity Severity;
        public string Message;
        public Exception? Exception;
        public DateTime Timestamp;

        public Log(Severity severity, string message, Exception? exception = null)
        {
            Severity = severity;
            Message = message;
            Exception = exception;
            Timestamp = DateTime.Now;
        }
    }
}