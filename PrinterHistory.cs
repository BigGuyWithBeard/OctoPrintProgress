namespace OctoPrintProgress
{

    public class PrinterHistory
    {
        public Sd sd { get; set; }

        public PrinterState state { get; set; }
        public Temperature temperature { get; set; }
    }


    /// <summary>
    /// data about the SD card
    /// </summary>
    public class Sd
    {
        public bool ready { get; set; }
    }




    public class Temperature
    {
        public Bed bed { get; set; }
        public History[] history { get; set; }
        public Tool0 tool0 { get; set; }
    }

    public class Bed
    {
        public float actual { get; set; }
        public int offset { get; set; }
        public float target { get; set; }
    }

    public class Tool0
    {
        public float actual { get; set; }
        public int offset { get; set; }
        public float target { get; set; }
    }

    public class History
    {
        public Bed1 bed { get; set; }
        public int time { get; set; }
        public Tool01 tool0 { get; set; }
    }

    public class Bed1
    {
        public float actual { get; set; }
        public float target { get; set; }
    }

    public class Tool01
    {
        public float actual { get; set; }
        public float target { get; set; }
    }


}
