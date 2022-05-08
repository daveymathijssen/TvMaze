// <copyright file="IHttpService.cs" company="Davey Mathijssen">
// Copyright (c) Davey Mathijssen. All rights reserved.
// </copyright>

namespace TvMaze.Scraper.Services.Interfaces
{
    /// <summary>
    /// Service for sending HTTP requests.
    /// </summary>
    public interface IHttpService
    {
        /// <summary>
        /// Send a the <paramref name="request"/> request.
        /// </summary>
        /// <param name="request">The HTTP request.</param>
        /// <returns>The HTTP response message.</returns>
        Task<HttpResponseMessage> Send(HttpRequestMessage request);
    }
}
