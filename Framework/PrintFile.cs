using System;

namespace Framework
{
    class PrintFile
    {
        public string Name;
        public int Version;
        public int PrinterRestriction;
        
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

        public PrintFile Copy()
        {
            return new PrintFile(Name, Version, PrinterRestriction);
        }
    }
}