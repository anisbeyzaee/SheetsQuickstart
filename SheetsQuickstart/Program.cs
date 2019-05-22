using Google.Apis.Auth.OAuth2;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System;
using System.Web;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Text;
using Microsoft.VisualBasic.FileIO;
using ChoETL;
using System.Net;
using System.Drawing;
using System.Drawing.Imaging;

namespace SheetsQuickstart
{
    class Program
    {
        // If modifying these scopes, delete your previously saved credentials
        // at ~/.credentials/sheets.googleapis.com-dotnet-quickstart.json
        static string[] Scopes = { SheetsService.Scope.SpreadsheetsReadonly };
        static string ApplicationName = "Google Sheets API .NET Quickstart";
        static string userSubscribersFilePAth = AppDomain.CurrentDomain.BaseDirectory+ "\\EMailSubscriber.txt";
        static string userSubscriber = "";

        static string imageUrl;
        static Bitmap bitmap;
        static void Main(string[] args)
        {
            UserCredential credential;

            using (var stream =
                new FileStream("credentials_Drive.json", FileMode.Open, FileAccess.Read))
            {
                // The file token.json stores the user's access and refresh tokens, and is created
                // automatically when the authorization flow completes for the first time.
                string credPath = "token.json";
                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    Scopes,
                    "user",
                    CancellationToken.None,
                    new FileDataStore(credPath, true)).Result;
                Console.WriteLine("Credential file saved to: " + credPath);
            }

            // Create Google Sheets API service.
            var service = new SheetsService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });

            // Define request parameters.
            String spreadsheetId = "1UBT6oEX70xAp49NNG4lRnZksyIiyMytPdzZ60ilC1FA";
            String range = "Form Responses 1!A2:D";
            SpreadsheetsResource.ValuesResource.GetRequest request =
                    service.Spreadsheets.Values.Get(spreadsheetId, range);

            // Prints the names and majors of students in a sample spreadsheet:
            // https://docs.google.com/spreadsheets/d/1BxiMVs0XRA5nFMdKvBdBZjgmUUqptlbs74OgvE2upms/edit
            ValueRange response = request.Execute();
            IList<IList<Object>> values = response.Values;
            if (values != null && values.Count > 0)
            {

                //Console.WriteLine("Email, Name, Phone Number");

                try
                {
                    if (File.Exists(userSubscribersFilePAth))
                    {

                        File.Delete(userSubscribersFilePAth);

                    }

                    using (FileStream fs = File.Create(userSubscribersFilePAth))
                    {
                        foreach (var row in values)
                        {
                            Console.WriteLine("{0}, {1}, {2}", row[1], row[3], row[2]);
                            userSubscriber = string.Format("{0}, {1}, {2}, \n", row[1].ToString(), row[3], row[2]);
                            byte[] info = new UTF8Encoding(true).GetBytes(userSubscriber);
                            fs.Write(info, 0, info.Length);
                        }

                    }

                }
                catch (Exception e)
                {
                    //System.Windows.MessageBox.Show("Error Occured on Adding the LOG to file" + e);
                }

            }
            else
            {
                Console.WriteLine("No data found.");
            }
           
            getALlSubscribers();
           
            string url = "https://e6b54de4-0ece-4091-ab5c-8685906034d8-forestfire.s3.amazonaws.com/01-boreal-forest-nasa.ngsversion.1466703666154.adapt.1900.1_1.jpg";
            WebClient webClient = new WebClient();
            string path = AppDomain.CurrentDomain.BaseDirectory+ "DownloadImages"; // Create a folder named 'Images' in your root directory
            string fileName = Path.GetFileName(url);
            webClient.DownloadFile(url, path + "\\" + fileName);
            int a = 22;

            var inf1 = new Info("http://servername/username/123.xml");

            // 

            scrapping sc = new scrapping();
            sc.read();
            //sc.scrape();



            Console.Read();
        }

       






        public static List<USerSubscriber> getALlSubscribers()
        {
            // install package Install-Package ChoETL -Version 1.1.0.2


            List<USerSubscriber> allsubscribers = new List<USerSubscriber>();

            using (var reader = new ChoCSVReader<USerSubscriber>(AppDomain.CurrentDomain.BaseDirectory + @"\EMailSubscriber.txt"))
            {
                foreach (var item in reader)
                {
                    allsubscribers.Add(item);
                    
                }
            }



            return allsubscribers;
        }


        public System.Drawing.Image DownloadImageFromUrl(string imageUrl)
        {
            System.Drawing.Image image = null;

            try
            {
                System.Net.HttpWebRequest webRequest = (System.Net.HttpWebRequest)System.Net.HttpWebRequest.Create(imageUrl);
                webRequest.AllowWriteStreamBuffering = true;
                webRequest.Timeout = 30000;

                System.Net.WebResponse webResponse = webRequest.GetResponse();

                System.IO.Stream stream = webResponse.GetResponseStream();

                image = System.Drawing.Image.FromStream(stream);

                webResponse.Close();
            }
            catch (Exception ex)
            {
                return null;
            }

            return image;
        }

       
            





        public class USerSubscriber
        {
            public string name { get; set; }
            public string emailAddress { get; set; }
            public string phoneNumber { get; set; }
        }


        public static void GetAllImaeNames()
        {

        }

        






    }
}