using System.Net;
using System;
using System.Net;
using System.IO;
using System.Configuration;
using System.Xml;

namespace ConsoleApp1
{
    public class Test
    {
        public static void Main(string[] args)
        {
            using WebClient client = new WebClient();

            string readURL = ConfigurationManager.AppSettings["checkURL"];
            string readString = ConfigurationManager.AppSettings["lookForString"];

            // Add a user agent header in case the
            // requested URI contains a query.

            client.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");

            using Stream data = client.OpenRead(readURL);
            using StreamReader reader = new StreamReader(data);
            string s = reader.ReadToEnd();
            
            for (int i = 0; i < (s.Length - readString.Length); i++) {
                if (s.Substring(i, readString.Length).Equals(readString)) {
                    Console.WriteLine(i + " recognised \"" + readString + "\"");
                    break;
                }
                else Console.WriteLine(i + " failed");
            }
            
        }
    }
}