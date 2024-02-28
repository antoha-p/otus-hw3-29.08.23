using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Otus.Hw3.Solid.Game;
using Otus.Hw3.Solid.Interfaces;
using Otus.Hw3.Solid.Models;
using Otus.Hw3.Solid.Services;

namespace Otus.Hw3.Solid;

public class Program
{
    public static void Main(string[] args)
    {
        // create service collection
        var serviceCollection = new ServiceCollection();
        ConfigureServices(serviceCollection);

        // create service provider
        var serviceProvider = serviceCollection.BuildServiceProvider();

        // run app
        serviceProvider.GetService<App>().Run();
    }

    private static void ConfigureServices(IServiceCollection serviceCollection)
    {
        serviceCollection.AddLogging();

        // build configuration
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("app-settings.json", false)
            .Build();

        serviceCollection.AddOptions();
        serviceCollection.Configure<AppSettings>(configuration.GetSection("Configuration"));
        ConfigureConsole(configuration);

        // add services
        serviceCollection.AddTransient<ITestService, TestService>();

        serviceCollection.AddTransient<IGameSettings, DefaultGameSettings>();
        serviceCollection.AddTransient<IDisplay, ConsoleDisplay>();
        serviceCollection.AddTransient<IGame, Game.Game>();

        // add app
        serviceCollection.AddTransient<App>();
    }

    private static void ConfigureConsole(IConfigurationRoot configuration)
    {
        System.Console.Title = configuration.GetSection("Configuration:ConsoleTitle").Value;
    }
}