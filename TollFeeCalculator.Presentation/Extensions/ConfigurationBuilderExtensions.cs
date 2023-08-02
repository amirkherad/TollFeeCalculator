namespace TollFeeCalculator.Presentation.Extensions;

public static class ConfigurationBuilderExtensions
{
    public static IConfigurationRoot ConfigurationRootBuilder(this IWebHostEnvironment webHostEnvironment)
    {
        var configurationBuilder = new ConfigurationBuilder();
        
        configurationBuilder.SetBasePath(webHostEnvironment.ContentRootPath)
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"appsettings.{webHostEnvironment.EnvironmentName.ToLower()}.json", optional: true, reloadOnChange: true)
            .AddJsonFile($"appsettings.{Environment.MachineName}.json", optional: true, reloadOnChange: true)
            .AddEnvironmentVariables();

        return configurationBuilder.Build();
    }
}