// <copyright file="TvMazeScraperService.cs" company="Davey Mathijssen">
// Copyright (c) Davey Mathijssen. All rights reserved.
// </copyright>

namespace TvMaze.Scraper.Services
{
    using System.Net;
    using AutoMapper;
    using Newtonsoft.Json;
    using TvMaze.Infrastructure.Repositories.Interfaces;
    using TvMaze.Scraper.Entities;
    using TvMaze.Scraper.Services.Interfaces;

    public class TvMazeScraperService : BackgroundService
    {
        private const string TvMazeApiUri = "https://api.tvmaze.com/shows?page=";
        private const int TvMazeMaxPageResults = 250;

        private readonly ILogger<TvMazeScraperService> log;
        private readonly IMapper mapper;
        private readonly IServiceProvider serviceProvider;
        private readonly IShowQueueService showQueueService;
        private int previousLastPage = -1;

        public TvMazeScraperService(
            ILogger<TvMazeScraperService> logger,
            IMapper mapper,
            IServiceProvider serviceProvider,
            IShowQueueService queueService)
        {
            this.log = logger;
            this.mapper = mapper;
            this.serviceProvider = serviceProvider;
            this.showQueueService = queueService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                foreach (var show in await this.RequestShows().ConfigureAwait(false))
                {
                    this.showQueueService.Enqueue(show);
                }

                await Task.Delay(5000, stoppingToken).ConfigureAwait(false);
            }
        }

        /// <summary>
        /// Get the last page number where the scraper stopped scraping.
        /// </summary>
        /// <param name="showRepository">Show repository.</param>
        /// <param name="previousLastPage">The previous last page. This will prevent loading the same page over and over again.</param>
        /// <returns>The last page number.</returns>
        private static int GetLastPageNumber(IShowRepository showRepository, int previousLastPage)
        {
            var page = showRepository.GetHighestMazeTvId() / TvMazeMaxPageResults;
            if (previousLastPage > page)
            {
                page = previousLastPage;
            }

            return page;
        }

        private async Task<List<TvMazeShow>> RequestShows()
        {
            using var scope = this.serviceProvider.CreateScope();
            var httpService = scope.ServiceProvider.GetRequiredService<IHttpService>();
            var showRepository = scope.ServiceProvider.GetRequiredService<IShowRepository>();
            var shows = new List<TvMazeShow>();

            using var request = new HttpRequestMessage()
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(TvMazeApiUri + GetLastPageNumber(showRepository, ++this.previousLastPage)),
            };

            var result = await httpService.Send(request).ConfigureAwait(false);

            if (result.StatusCode == HttpStatusCode.OK)
            {
                var resultedShows = await result.Content.ReadAsStringAsync().ConfigureAwait(false);
                shows.AddRange(JsonConvert.DeserializeObject<List<TvMazeShow>>(resultedShows));
                this.log.LogDebug("Succesfully requested shows from TvMaze");
            }
            else
            {
                this.log.LogWarning("Error while retrieving shows from TvMaze resulted in HTTP {StatusCode}", result.StatusCode);
            }

            return shows;
        }
    }
}
