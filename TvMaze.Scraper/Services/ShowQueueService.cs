// <copyright file="ShowQueueService.cs" company="Davey Mathijssen">
// Copyright (c) Davey Mathijssen. All rights reserved.
// </copyright>

namespace TvMaze.Scraper.Services
{
    using AutoMapper;
    using TvMaze.Infrastructure.Repositories.Interfaces;
    using TvMaze.Scraper.Entities;
    using TvMaze.Scraper.Services.Interfaces;

    /// <summary>
    /// Queues shows collected from the TvMaze API and saves them to the repository.
    /// </summary>
    public class ShowQueueService : IShowQueueService
    {
        private readonly ILogger<ShowQueueService> log;
        private readonly IServiceProvider serviceProvider;
        private readonly IMapper mapper;
        private readonly Queue<TvMazeShow> queue = new();
        private bool delegateQueuedOrRunning;

        /// <summary>
        /// Initializes a new instance of the <see cref="ShowQueueService"/> class.
        /// </summary>
        /// <param name="log">Logging service.</param>
        /// <param name="serviceProvider">ServiceProvider.</param>
        public ShowQueueService(ILogger<ShowQueueService> log, IServiceProvider serviceProvider, IMapper mapper)
        {
            this.log = log;
            this.mapper = mapper;
            this.serviceProvider = serviceProvider;
        }

        /// <summary>
        /// Enqueue a new Event message.
        /// </summary>
        /// <param name="show">Show instance.</param>
        /// <returns>Returns <c>true</c> if the show is successfully queued; <c>false</c> otherwise.</returns>
        public bool Enqueue(TvMazeShow show)
        {
            // Only allow one pooled thread by locking the queue.
            lock (this.queue)
            {
                this.queue.Enqueue(show);

                // If no queue is being processed, start a new thread.
                if (!this.delegateQueuedOrRunning)
                {
                    this.delegateQueuedOrRunning = true;
                    ThreadPool.QueueUserWorkItem(this.ProcessQueuedItems, null);
                }
            }

            return true;
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1031:Do not catch general exception types", Justification = "We want to catch every exception.")]
        private void ProcessQueuedItems(object ignored)
        {
            using var scope = this.serviceProvider.CreateScope();
            var showRepository = scope.ServiceProvider.GetRequiredService<IShowRepository>();
            List<TvMazeShow> shows = new();

            while (true)
            {
                // Only allow one pooled thread by locking the queue.
                lock (this.queue)
                {
                    if (this.queue.Count == 0)
                    {
                        this.delegateQueuedOrRunning = false;
                        break;
                    }

                    shows = new(this.queue);
                    this.queue.Clear();
                }

                try
                {
                    if (!showRepository.CreateShows(this.mapper.Map<IEnumerable<Core.Entities.Show>>(shows)))
                    {
                        this.log.LogWarning("The following shows could not be saved to the database and will be added to the queue again: {Shows}", shows);
                        foreach (var show in shows)
                        {
                            if (DateTime.Parse(show.premiered) > new DateTime(2014, 1, 1))
                            {
                                this.Enqueue(show);
                            }
                        }

                        ThreadPool.QueueUserWorkItem(this.ProcessQueuedItems, null);
                    }
                }
                catch
                {
                    foreach (var show in shows)
                    {
                        this.Enqueue(show);
                    }

                    ThreadPool.QueueUserWorkItem(this.ProcessQueuedItems, null);
                }

                Thread.Sleep(5000);
            }
        }
    }
}
