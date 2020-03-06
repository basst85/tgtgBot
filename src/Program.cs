using System;
using System.Threading.Tasks;
using tgtgBot.Api;
using tgtgBot.Helpers;
using tgtgBot.Models;

namespace tgtgBot
{
    class Program
    {
        static async Task Main(string[] args)
        {
            string email = BotEnvironment.GetConfigVariable("email");
            string password = BotEnvironment.GetConfigVariable("password");

            Console.WriteLine("---- tgtgBot ----");
            Console.WriteLine("");

            User tgtgUser = new User()
            {
                email = email,
                password = password
            };

            string tgtgApiAccessToken = await Auth.GetAccessToken(tgtgUser);
            Console.WriteLine(tgtgApiAccessToken);
        }
    }
}
