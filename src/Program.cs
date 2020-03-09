using System;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using tgtgBot.Services;
using tgtgBot.Helpers;
using tgtgBot.Models;

namespace tgtgBot
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("---- tgtgBot ----");
            Console.WriteLine("");

            User tgtgUser = new User()
            {
                email = BotEnvironment.GetConfigVariable("email"),
                password = BotEnvironment.GetConfigVariable("password")
            };

            string tgtgApiAccessToken = await tgtgApi.GetAccessToken(tgtgUser);

            if (!string.IsNullOrEmpty(tgtgApiAccessToken)){
                string latitude = BotEnvironment.GetConfigVariable("latitude");
                string longitude = BotEnvironment.GetConfigVariable("longitude");
                string radius = BotEnvironment.GetConfigVariable("radius");

                var items = await tgtgApi.GetItems(tgtgUser, latitude, longitude, radius);

                foreach (var tgtgItem in items.SelectToken("items"))
                {
                    Console.WriteLine("Name: {0}", tgtgItem["display_name"].ToString());
                    Console.WriteLine("Items available: {0}", tgtgItem["items_available"].ToString());
                    Console.WriteLine("-----");
                }
            }
            else
            {
                Console.WriteLine("Could not get API token");
            }
        }
    }
}
