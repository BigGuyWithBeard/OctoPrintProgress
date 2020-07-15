using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace OctoPrintProgress
{
    public class PrinterState
    {
        public PrinterStateFlags flags { get; set; }
        public string text { get; set; }
    }
}
