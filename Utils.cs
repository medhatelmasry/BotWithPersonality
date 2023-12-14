using Microsoft.Extensions.Configuration;

namespace BotWithPersonality;

public class Utils {
    public static string GetConfigValue(string config) {

        IConfigurationBuilder builder = new ConfigurationBuilder();

        if (System.IO.File.Exists("appsettings.json"))
            builder.AddJsonFile("appsettings.json", false, true);

        if (System.IO.File.Exists("appsettings.Development.json"))
            builder.AddJsonFile("appsettings.Development.json", false, true);

        IConfigurationRoot root = builder.Build();

        return root[config]!;
    }
}

