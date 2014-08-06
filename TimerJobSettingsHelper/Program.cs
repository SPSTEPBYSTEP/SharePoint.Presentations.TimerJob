using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SharePoint.Administration;
using MyTimerJob.Timer_Jobs;

namespace TimerJobSettingsHelper
{
    class Program
    {

        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Usage();
            }
            else
            {
                string url = args[0];
                string key = args[1];
                string value = string.Empty;

                if (args.Select(x => x.ToUpper()).Contains("GET"))
                {
                    GetTimerJobSetting(url, key);
                }
                else if (args.Select(x => x.ToUpper()).Contains("SET"))
                {
                    value = args[2];
                    SetTimerJobSetting(url, key, value);
                }
            }
        }

        private static void Usage()
        {
            var bg = Console.BackgroundColor;
            var tc = Console.ForegroundColor;

            Console.BackgroundColor = ConsoleColor.Green;
            Console.ForegroundColor = ConsoleColor.Black;

            // Console.Clear();
            Console.WriteLine("About");
            Console.WriteLine("----------");
            Console.WriteLine("Retrieves or sets the property bag in the web application used for the SharePoint Timer Job");
            Console.WriteLine("Usage");
            Console.WriteLine("----------");
            Console.WriteLine("MyTimerJobSettingsHelper.exe [url] [type] [key] [value - optional] [operation]");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("Where");
            Console.WriteLine("[url] = url of the web application");
            Console.WriteLine("[key] = key for the configuration item");
            Console.WriteLine("[value] = value for the configuration item (optional, only on SET operation)");
            Console.WriteLine("[operation] = GET or SET");
            Console.WriteLine("");
            Console.WriteLine("Examples:");
            Console.WriteLine("");
            Console.WriteLine("MyTimerJobSettingsHelper.exe http://intranet Property1 GET");
            Console.WriteLine("");
            Console.WriteLine(@"MyTimerJobSettingsHelper.exe http://intranet Property1 ""Property 1 New Value"" SET");
            Console.WriteLine("");

            Console.BackgroundColor = bg;
            Console.ForegroundColor = tc;
        }

        private static void GetTimerJobSetting(string url, string key)
        {
            var webApp = SPWebApplication.Lookup(new Uri(url));
            // Delete the job's settings.
            var jobSettings = webApp.GetChild<MyTimerJobSettings>(MyTimerJobSettings.SettingsName);
            if (jobSettings != null)
            {
                Console.WriteLine(jobSettings.GetFieldValue<string>(key));
            }
        }
        private static void SetTimerJobSetting(string url, string key, string value)
        {
            var webApp = SPWebApplication.Lookup(new Uri(url));
            var jobSettings = webApp.GetChild<MyTimerJobSettings>(MyTimerJobSettings.SettingsName);

            if (jobSettings != null)
            {
                jobSettings.SetFieldValue(key, value);
                jobSettings.Update(true);
                webApp.Update(true);
            }
        }
    }
}
