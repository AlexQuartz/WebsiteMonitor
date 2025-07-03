namespace ConsoleApp1
{
    using System.Net;
    using System;
    using System.IO;
    using System.Configuration;
    using System.Xml;
    public class WebsiteMonitor
    {
        public static async Task Main()
        {
            HttpClient client = new HttpClient();

            string readURL = ConfigurationManager.AppSettings["checkURL"];
            string readString = ConfigurationManager.AppSettings["lookForString"];

            try
            {
                using HttpResponseMessage response = await client.GetAsync(readURL);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();

                for (int i = 0; i < (responseBody.Length - readString.Length); i++)
                {
                    if (responseBody.Substring(i, readString.Length).Equals(readString))
                    {
                        Console.WriteLine(i + " recognised \"" + readString + "\"");
                        break;
                    }
                    else Console.WriteLine(i + " failed");
                }
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
            }
            
        }
    }
}
