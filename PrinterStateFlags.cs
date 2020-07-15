using System;
using System.Collections.Generic;
using System.Text;

namespace OctoPrintProgress
{
    public class PrinterStateFlags
    {
        public bool cancelling { get; set; }
        public bool closedOrError { get; set; }
        public bool error { get; set; }
        public bool finishing { get; set; }
        public bool operational { get; set; }
        public bool paused { get; set; }
        public bool pausing { get; set; }
        public bool printing { get; set; }
        public bool ready { get; set; }
        public bool resuming { get; set; }
        public bool sdReady { get; set; }
    }

}
