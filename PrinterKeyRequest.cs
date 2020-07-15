using System;
using System.Collections.Generic;
using System.Text;

namespace OctoPrintProgress
{
  public   class PrinterKeyRequest
    {

        /// <summary>
        /// Application identifier to use for the request
        /// </summary>
        public string app { get; set; }
        /// <summary>
        /// User identifier/name to restrict the request to
        /// </summary>
        public string user { get; set; }
    }
}
