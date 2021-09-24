using System;
using System.Collections.Generic;
using Framework.Validation;

namespace Framework
{
    class PrintHubManager
    {
        public List<Printer> Printers;
        public List<PrintFile> TrackedFiles;
        public List<PrintFile> ArchivedFiles;
        private StateValidator _validator;

        public PrintHubManager() 
        {
            Printers = new List<Printer>();
            TrackedFiles = new List<PrintFile>();
            ArchivedFiles = new List<PrintFile>();

            _validator = new StateValidator(this);
        }

        public void AddPrinter(string name)
        {
            Printers.Add(new Printer(name, Printers.Count));
        }

        public void RemovePrinter(int ID)
        {
            Printers.RemoveAll((p) => p.ID == ID);
        }

        public void TrackFile(string name, int PrinterRestriction)
        {
            if(!_validator.ValidateTrack(name))
                TrackedFiles.Add(new PrintFile(name, 1, PrinterRestriction));
            else
            {
                ArchivedFiles.Add(GetTrackedFile(name).Copy());
                GetTrackedFile(name).Version++;
            }
        }

        public PrintFile GetTrackedFile(string name)
        {
            return TrackedFiles.Find((f) => name.Equals(f.Name));
        }

        ///<summary>Use full search term to locate specific file.</summary>
        public PrintFile GetArchivedFile(SearchTerm search)
        {
            return ArchivedFiles.Find((f) => PrintFile.GetFileName(search).Equals(f.GetFileName()));
        }

        ///<summary>Use partial or full search term to get list of results. 
        ///For PrinterRestriction use -1 if no value is to be specified.</summary>
        public List<PrintFile> GetArchivedFiles(SearchTerm search)
        {
            List<PrintFile> Results = new List<PrintFile>();

            foreach(PrintFile file in ArchivedFiles)
            {
                bool invalid = false;

                if(!string.IsNullOrEmpty(search.Name))
                {
                    if(!search.Name.Equals(file.Name))
                    {
                        invalid = true;
                    }
                }

                if(!string.IsNullOrEmpty(search.Version))
                {
                    if(!search.Version.Equals(file.Version.ToString()))
                    {
                        invalid = true;
                    }
                }

                if(search.PrinterRestriction != -1)
                {
                    if(search.PrinterRestriction != file.PrinterRestriction)
                    {
                        invalid = true;
                    }
                }

                if(!invalid)
                {
                    Results.Add(file);
                }
            }

            return Results;
        }

        public void LinkFile(string name, string path)
        {
            if(_validator.ValidateTrack(name))
            {
                GetTrackedFile(name).LinkFile(path);
            }
        }
    }
}