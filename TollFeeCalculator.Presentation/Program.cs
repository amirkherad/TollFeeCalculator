using TollFeeCalculator.Presentation.Extensions;
using TollFeeCalculator.Presentation.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

var configurationRoot = builder
    .Environment
    .ConfigurationRootBuilder();

builder.Services.AddSingleton(configurationRoot);

builder.Services.AddServices();

var webApplication = builder.Build();

webApplication.UseServices();

try
{
    webApplication.Run();
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}