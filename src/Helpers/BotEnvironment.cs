using System;
using Microsoft.Extensions.Configuration;

namespace tgtgBot.Helpers
{
    public class BotEnvironment
    {
        public static string GetConfigVariable(string variableName)
        {
            string variableValue = Environment.GetEnvironmentVariable(variableName);

            if (!string.IsNullOrEmpty(variableValue)){
                return variableValue;
            }
            else
            {
                try{
                    IConfigurationRoot settings = new ConfigurationBuilder().
                        AddJsonFile("appsettings.json").
                        Build();

                    return settings.GetSection(variableName).Value;
                }
                catch (Exception exception)
                {
                    Console.WriteLine("Variable or appsettings.json not found");
                    throw exception;
                }
            }
        }
    }
}
