using System;
using Microsoft.Extensions.Configuration;

namespace tgtgBot.Helpers
{
    public class BotEnvironment
    {
        public static string GetConfigVariable(string variableName)
        {
            string variableValue = Environment.GetEnvironmentVariable(variableName);

            if (!string.IsNullOrEmpty(variableValue))
                return variableValue;

            IConfigurationRoot settings = new ConfigurationBuilder().
                AddJsonFile("appsettings.json").
                Build();

            return settings.GetSection(variableName).Value;
        }

        public static string GetEnvironmentVariable(string variableName)
        {
            throw new NotImplementedException();
        }
    }
}
