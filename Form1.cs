using System;
using System.Diagnostics;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OctoPrintProgress
{
    public partial class Form1 : Form
    {

        private OctoPrint OctoPrint = new OctoPrint();
        private string applicationIdentifier = "OctoPrintProgress";

        //private static readonly HttpClient client = new HttpClient();


        public string PrinterStateString { get; set; } = "Unknown";

        public Form1()
        {
            InitializeComponent();
        }

      
        //private static async Task<string> CallOctoPrintApi(string url)
        //{
        //    try
        //    {
        //        //client.DefaultRequestHeaders.Accept.Clear();
        //        ////      client.DefaultRequestHeaders.Accept.Add(                new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));
        //        ////     client.DefaultRequestHeaders.Add("User-Agent", "Blah blah blah");
        //        ////client.DefaultRequestHeaders.Add("X-Api-Key", apiKey);

                
        //        //var stringTask = client.GetStringAsync(url);

        //   //     var msg = await stringTask;
   
        //        return msg;
        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine(e.Message);
        //        Debugger.Break();
        //        throw;
        //    }

        //}



        private void Form1_Load(object sender, EventArgs e)
        {


        
        }

  

        private async void btnConnect_Click(object sender, EventArgs e)
        {
   

            lblPrinterState.DataBindings.Add(new Binding("Text", OctoPrint, nameof(OctoPrint.State)));


         await   OctoPrint.Connect(applicationIdentifier, txtUsername.Text ,txtOctoPrintUrl.Text, txtApiKey.Text);

        }
    }
}
