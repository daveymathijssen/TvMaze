using TvMaze.Core.Setup;
using TvMaze.Infrastructure.Setup;
using TvMaze.Scraper.Helpers.Polly;
using TvMaze.Scraper.Services;
using TvMaze.Scraper.Services.Interfaces;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration((hostcontext, config) =>
    {
        config.AddJsonFile($"appsettings{"." + Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT") ?? string.Empty}.json", false, true);
    })
    .ConfigureServices((hostcontext, services) =>
    {
        services.AddCore();
        services.AddInfrastructure(hostcontext.Configuration);
        services.AddHttpClient<IHttpService, TvMazeHttpService>()
        .SetHandlerLifetime(TimeSpan.FromMinutes(10))
        .AddPolicyHandler(PollyPolicies.GetRetryPolicy(5, 10));
        services.AddSingleton<IShowQueueService, ShowQueueService>();
        services.AddHostedService<TvMazeScraperService>();
    })
    .Build();

await host.RunAsync();
