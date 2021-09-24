using System;
using System.IO;
using System.Threading.Tasks;
using PrintHub.Framework.Logging;

namespace PrintHub.Framework
{
    class PrintFile
    {
        public string Name;
        public int Version;
        public int PrinterRestriction;
        public GcodeStream LinkedStream;
        public bool LinkedToStream;

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

        public string GetFileName()
        {
            return Name + "_" + Version + "_" + PrinterRestriction;
        }

        public static string GetFileName(SearchTerm search)
        {
            return search.Name + "_" + search.Version + "_" + search.PrinterRestriction;
        }

        public void LinkFile(string path)
        {
            if(LinkedToStream)
            {
                Logger.PostLog(Severity.Warning, "Attempt to overwrite an existing link was blocked.");
            }
            else
            {
                LinkedStream = new GcodeStream(GetFileName(), ReadFile(path));

                LinkedToStream = LinkedStream.Stream != null;
            }
        }

        public MemoryStream ReadFile(string path) 
        {
            MemoryStream result = new MemoryStream();
            byte[] data = GetData(path);

            if(data == null)
                return new MemoryStream();

            for(int i = 0; i < data.Length; i++)
                result.WriteByte(data[i]);

            result.Seek(0, SeekOrigin.Begin);

            return result;
        }

        public byte[] GetData(string path)
        {
            try
            {
                return File.ReadAllBytes(path);
            }
            catch(Exception e)
            {
                Logger.PostLog(Severity.Error, "Error while reading byte data.", e);
                return null;
            }
        }

        

        public PrintFile Copy()
        {
            return new PrintFile(Name, Version, PrinterRestriction);
        }
    }
}