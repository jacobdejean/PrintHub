using System;
using System.Collections.Generic;
using System.IO;

namespace PrintHub.Framework
{
    class GcodeStream
    {
        public string LinkedFileName;
        public MemoryStream Stream;

        public GcodeStream(string name, MemoryStream fs)
        {
            LinkedFileName = name;
            Stream = fs;
        }
    }
}