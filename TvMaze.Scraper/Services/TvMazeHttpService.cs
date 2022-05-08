// <copyright file="TvMazeHttpService.cs" company="Davey Mathijssen">
// Copyright (c) Davey Mathijssen. All rights reserved.
// </copyright>

namespace TvMaze.Scraper.Services
{
    using TvMaze.Scraper.Services.Interfaces;

    /// <summary>
    /// Service for sending HTTP requests (for UFTP Messages).
    /// </summary>
    public class TvMazeHttpService : IHttpService
    {
        private HttpClient client;

        /// <summary>
        /// Initializes a new instance of the <see cref="TvMazeHttpService"/> class.
        /// </summary>
        /// <param name="client">Http client.</param>
        public TvMazeHttpService(HttpClient client)
        {
            this.client = client;
        }

        /// <inheritdoc/>
        public async Task<HttpResponseMessage> Send(HttpRequestMessage request)
        {
            return await this.client.SendAsync(request).ConfigureAwait(false);
        }
    }
}
