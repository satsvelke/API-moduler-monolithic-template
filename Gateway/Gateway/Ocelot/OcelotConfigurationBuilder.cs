using Newtonsoft.Json;
using Ocelot.Configuration.File;

namespace Gateway.Ocelot;

public static class OcelotConfigurationBuilder
{
    public static IConfigurationBuilder CreateOcelotConfigFiles(this IConfigurationBuilder configurationBuilder, string folder)
    {
        const string coreConfigFile = "ocelot.json";

        var files = new DirectoryInfo(folder).EnumerateFiles().ToList();

        var fileConfiguration = new FileConfiguration();

        foreach (var file in files)
        {
            if (files.Count > 1 && file.Name.Equals(coreConfigFile, StringComparison.OrdinalIgnoreCase))
            {
                continue;
            }

            var lines = File.ReadAllText(file.FullName);

            var config = JsonConvert.DeserializeObject<FileConfiguration>(lines);
            fileConfiguration.GlobalConfiguration = config?.GlobalConfiguration;
            fileConfiguration.Aggregates.AddRange(config!.Aggregates);
            fileConfiguration.Routes.AddRange(config!.Routes);
        }

        var json = JsonConvert.SerializeObject(fileConfiguration);

        File.WriteAllText(coreConfigFile, json);

        configurationBuilder.AddJsonFile(coreConfigFile, optional: false, reloadOnChange: true);

        return configurationBuilder;
    }
}
