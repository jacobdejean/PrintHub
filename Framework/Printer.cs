using System;
using System.Collections.Generic;

namespace PrintHub.Framework
{
    class Printer
    {
        public string PrinterModel;
        public int ID;

        public Printer(string PrinterModel, int ID)
        {
            this.PrinterModel = PrinterModel;
            this.ID = ID;
        }
    }
}