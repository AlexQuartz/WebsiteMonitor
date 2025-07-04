//reading a list of websites from App.config and checking their HTMLs for a specified string
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
            var URLinProgram = new List<string>(ConfigurationManager.AppSettings["URLconfig"].Split(new char[] { ';' }));
            var stringInProgram = new List<string>(ConfigurationManager.AppSettings["stringConfig"].Split(new char[] { ';' }));
            //converting App.config variables into lists

            if (URLinProgram.Count != stringInProgram.Count)
            {
                Console.WriteLine("Error: Length of URLconfig in App.config does not match length of stringConfig in App.config");
            }
            else
            {
                for (int j = 0; j < URLinProgram.Count; j++)
                {
                    try
                    {
                        using HttpResponseMessage response = await client.GetAsync(URLinProgram[j]);
                        response.EnsureSuccessStatusCode();
                        string responseBody = await response.Content.ReadAsStringAsync();

                        Console.WriteLine("Looking for \"" + stringInProgram[j] + "\" in \"" + URLinProgram[j] + "\"");
                        Boolean success = false;

                        for (int i = 0; i < (responseBody.Length - stringInProgram[j].Length); i++)
                        {
                            if (responseBody.Substring(i, stringInProgram[j].Length).Equals(stringInProgram[j]))
                            {
                                success = true;
                                break;
                            }
                        }
                        if (success)
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("success");
                            Console.ForegroundColor = ConsoleColor.White;
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("failure");
                            Console.ForegroundColor = ConsoleColor.White;
                        }
                        Console.WriteLine();
                    }
                    catch (HttpRequestException e)
                    {
                        Console.WriteLine("\nException Caught!");
                        Console.WriteLine("Message :{0} ", e.Message);
                    }
                }
            }

        }
    }
}
