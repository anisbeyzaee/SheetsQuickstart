using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;


namespace SheetsQuickstart
{
    class scrapping
    {
        
        public scrapping() { }

        public void scrape()
        {
            Uri url = new Uri("https://f5f1d618-31fe-4bc4-b371-359bbef5e33f-nofireimages.s3.amazonaws.com/ForestNoFireImages.html");
            if (url.Scheme == Uri.UriSchemeHttp)
            {
                //Create Request Object
                HttpWebRequest objRequest = (HttpWebRequest)HttpWebRequest.Create(url);
                //Set Request Method
                objRequest.Method = WebRequestMethods.Http.Get;
                //Get response from requested url
                HttpWebResponse objResponse = (HttpWebResponse)objRequest.GetResponse();
                //Read response in stream reader
                StreamReader reader = new StreamReader(objResponse.GetResponseStream());
                string tmp = reader.ReadToEnd();
                objResponse.Close();
                //Set response data to container
                Console.Write(tmp + "line 33");
            }
        }


        public Dictionary<string, string> read()
        {
            string url = "https://f5f1d618-31fe-4bc4-b371-359bbef5e33f-nofireimages.s3.amazonaws.com/ForestNoFireImages.html";
            string strResult = "";

            WebResponse objResponse;
            WebRequest objRequest = System.Net.HttpWebRequest.Create(url);

            objResponse = objRequest.GetResponse();

            using (StreamReader sr = new StreamReader(objResponse.GetResponseStream()))
            {
                strResult = sr.ReadToEnd();
                //Close and clean up the StreamReader
                sr.Close();
            }

            string fileContent = strResult;

            string table_pattern = ">(https.*)</a>";       // @"(<a\s*.*>(\s*.*) \s*</a>)";

            MatchCollection table_matches = Regex.Matches(fileContent, table_pattern);
            
            List<string> tableContents = new List<string>();
            Dictionary<string, string> fileNamesDic = new Dictionary<string, string>();
            foreach (Match match in table_matches)
            {
                GroupCollection data = match.Groups;
                tableContents.Add(data[1].ToString());
                fileNamesDic.Add(data[1].ToString().Split('/').Last(), data[1].ToString());
                Console.WriteLine("Key is {0}  and the value {1} ", data[1].ToString().Split('/').Last(), data[1] );
            }

            return fileNamesDic;
                




            // Display results to a webpage
            //Console.Write(strResult);
            Console.Read();
        }

    }
}
