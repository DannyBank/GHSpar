using GHSpar;
using Microsoft.AspNetCore;
using Serilog;

BuildWebHost(args).Run();

IWebHost BuildWebHost(string[] args)
{
    return WebHost.CreateDefaultBuilder(args)
        .UseSerilog((hostingContext, loggerConfiguration) =>
        {
            loggerConfiguration
                .MinimumLevel.Information()
                .WriteTo.Console();
        })
        .UseStartup<Startup>()
        .ConfigureAppConfiguration((hostingContext, config) =>
        {
            config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
        }).Build();
}