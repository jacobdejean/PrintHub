using System;
using System.IO;
using System.Threading.Tasks;

namespace Framework
{
    class PrintFile
    {
        public string Name;
        public int Version;
        public int PrinterRestriction;
        public GcodeStream LinkedStream;

        public PrintFile(string Name)
        {
            this.Name = Name;
        }

        public PrintFile(string Name, int Version)
        {
            this.Name = Name;
            this.Version = Version;
        }

        public PrintFile(string Name, int Version, int PrinterRestriction)
        {
            this.Name = Name;
            this.Version = Version;
            this.PrinterRestriction = PrinterRestriction;
        }

        public void LinkFile(string path)
        {
            LinkedStream = new GcodeStream(GetFileName(), ReadFile(path));
        }

        public Func<string, MemoryStream> ReadFile = (s) => 
        {
            Console.WriteLine("Reading bytes from path...");

            MemoryStream result = new MemoryStream();
            byte[] data = File.ReadAllBytes(s);

            Console.WriteLine("Writing data to stream...");
            for(int i = 0; i < data.Length; i++)
            {
                result.WriteByte(data[i]);
            }

            result.Seek(0, SeekOrigin.Begin);
            Console.WriteLine("Finished copying data");

            return result;
        };

        public string GetFileName()
        {
            return Name + "_" + Version + "_" + PrinterRestriction;
        }

        public static string GetFileName(SearchTerm search)
        {
            return search.Name + "_" + search.Version + "_" + search.PrinterRestriction;
        }

        public PrintFile Copy()
        {
            return new PrintFile(Name, Version, PrinterRestriction);
        }
    }
}