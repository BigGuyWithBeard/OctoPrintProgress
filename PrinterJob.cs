using System;
using System.Collections.Generic;
using System.Text;

namespace OctoPrintProgress
{

    public class JobInformationResponse
    {
        public Job job { get; set; }
        public Progress progress { get; set; }
        public string state { get; set; }
    }

    public class Job
    {
        public File file { get; set; }
        public int estimatedPrintTime { get; set; }
        public Filament filament { get; set; }
    }

    public class File
    {
        public string name { get; set; }
        public string origin { get; set; }
        public int size { get; set; }
        public int date { get; set; }
    }

    public class Filament
    {
        public int length { get; set; }
        public float volume { get; set; }
    }

    public class Progress
    {
        public float completion { get; set; }
        public int filepos { get; set; }
        public int printTime { get; set; }
        public int printTimeLeft { get; set; }
    }

}
