using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Markup;
using System.Linq;

namespace OctoPrintProgress
{
    public class OctoPrint : INotifyPropertyChanged
    {
        //https://docs.octoprint.org/en/master/api/job.html#retrieve-information-about-the-current-job
        public event PropertyChangedEventHandler PropertyChanged;


        private static readonly HttpClient client = new HttpClient();

        #region private member variables
        private string state = "Not Connected";
        #endregion region

        #region "Public Properties"
        public string State
        {
            get => state;
            set
            {
                state = value;
                OnPropertyChanged(nameof(State));
            }
        }
        public string ServerBaseUrl { get; set; } = "";
        public string ApiKey { get; set; } = "";
        public bool IsWorkflowSupported { get; set; } = false;
        private string ApplicationIdentifier { get; set; } = "";
        private string UserName { get; set; } = ""; 
        #endregion 


        public OctoPrint()
        { }



        protected virtual void OnPropertyChanged(string property)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(property));
        }


        public async Task Connect(string applicationIdentifier, string userName, string serverBaseUrl, string apiKey)
        {
            ApplicationIdentifier = applicationIdentifier;
            UserName = userName;
            ServerBaseUrl = serverBaseUrl;
            ApiKey = apiKey;

            //   https://docs.octoprint.org/en/master/api/printer.html#retrieve-the-current-printer-state

            State = "Connecting: Getting Workflow Status...";

            //IsWorkflowSupported = GetWorkflowSupported(ServerBaseUrl).Result;
            State = $"Connecting: Got Workflow Status ({IsWorkflowSupported})...";

            if (IsWorkflowSupported)
            {
                // disabled for now.
                //send a key request:
                //PrinterKeyRequest pkr= new PrinterKeyRequest() { app = ApplicationIdentifier, user = UserName };

                //State = "Connecting: Requesting Key...";
                //var foo = RequestAppKey(pkr, serverBaseUrl);
                Debugger.Break();
          
            }
            else
            {
                State = "Requesting History...";
                PrinterHistory printerHistory = await GetPrinterHistory(apiKey, ServerBaseUrl );
                State = printerHistory.state.text;

                // get the state of the current job
                var FFF = await GetCurrentJob(apiKey, ServerBaseUrl);
                Debugger.Break();
            }


        }
        //private static async Task<bool> GetWorkflowSupported(string baseUrl)
        //{

        //    bool returnValue = false;
        //    var url = System.IO.Path.Join(baseUrl, "/plugin/appkeys/probe");

        //    try
        //    {
        //        client.DefaultRequestHeaders.Accept.Clear();

        //        // var responseMessage = await client.GetAsync(url);

        //        // var response = await client.GetAsync(url);
        //        // this syntax is less likely deadlock
        //        // https://stackoverflow.com/questions/10343632/httpclient-getasync-never-returns-when-using-await-async
        //        var response = Task.Run(() => client.GetAsync(url)).Result;
        //        //TODO fix this async issue
        //        // this non-deadlock version is not runing async, but at least it works (unlike the original version)



        //        // examine the return status code.
        //        // 204 (No Content) means workflow is available
        //        // anything else (normally 404) means it is not.
        //        // see https://docs.octoprint.org/en/master/bundledplugins/appkeys.html#sec-bundledplugins-appkeys-api-probe
        //        returnValue = (response.StatusCode == System.Net.HttpStatusCode.NoContent);

        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine(e.Message);
        //        Debugger.Break();
        //        throw;
        //    }

        //    return returnValue;
        //}



        //private static async Task<bool> RequestAppKey(PrinterKeyRequest pkr, string baseUrl)
        //{

        //    bool returnValue = false;
        //    var url = System.IO.Path.Join(baseUrl, "/plugin/appkeys/request");

        //    try
        //    {
        //        client.DefaultRequestHeaders.Accept.Clear();

        //        var json = JsonSerializer.Serialize(pkr);
        //        var data = new StringContent(json, Encoding.UTF8, "application/json");
        //        var responseMessage = await client.PostAsync(url, data);


        //        // examine the return status code.
        //        // 201 Created - auth process started.
        //        // polling URL can be found in Location header.
        //        if(responseMessage.StatusCode!= System.Net.HttpStatusCode.Created)
        //        {
        //            Debugger.Break();
        //            throw new Exception("Unable to start Auth process");
        //        }
        //        var location = "";
        //        IEnumerable<string> values;
        //        if (responseMessage.Headers.TryGetValues("Location", out values))
        //        {
        //            location = values.First();
        //        }
        //       Debugger.Break();



        //        Debugger.Break();
        //        returnValue = false;
        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine(e.Message);
        //        Debugger.Break();
        //        throw;
        //    }

        //    return returnValue;
        //}



        public static async Task<JobInformationResponse> GetCurrentJob(string apiKey, string baseUrl)
        {
            try
            {
                client.DefaultRequestHeaders.Accept.Clear();
                //      client.DefaultRequestHeaders.Accept.Add(                new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));
                //     client.DefaultRequestHeaders.Add("User-Agent", "Blah blah blah");
                client.DefaultRequestHeaders.Add("X-Api-Key", apiKey);
                client.DefaultRequestHeaders.Add("Host", "example.com");


                var stringTask = client.GetStringAsync(System.IO.Path.Join(baseUrl, "/api/job"));

                var msg = await stringTask;
                Debugger.Break();
                var printerJob = JsonSerializer.Deserialize<JobInformationResponse>(msg);
                Debugger.Break();
                return printerJob;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Debugger.Break();
                throw;
            }

        }


        public static async Task<PrinterHistory> GetPrinterHistory(string apiKey, string baseUrl)
        {
            try
            {
                client.DefaultRequestHeaders.Accept.Clear();
                //      client.DefaultRequestHeaders.Accept.Add(                new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));
                //     client.DefaultRequestHeaders.Add("User-Agent", "Blah blah blah");
                client.DefaultRequestHeaders.Add("X-Api-Key", apiKey);

                var stringTask = client.GetStringAsync(System.IO.Path.Join(baseUrl, "/api/printer?history=true&limit=2"));

                var msg = await stringTask;
                var printerHistory = JsonSerializer.Deserialize<PrinterHistory>(msg);

                return printerHistory;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Debugger.Break();
                throw;
            }

        }




    }
}
