// <copyright file="IShowQueueService.cs" company="Davey Mathijssen">
// Copyright (c) Davey Mathijssen. All rights reserved.
// </copyright>

namespace TvMaze.Scraper.Services.Interfaces
{
    using TvMaze.Scraper.Entities;

    /// <summary>
    /// Show queueing service interface.
    /// </summary>
    public interface IShowQueueService
    {
        /// <summary>
        /// Place a show in the queue which will be processed at a later time.
        /// </summary>
        /// <param name="show">Show instance that will be added to the queue.</param>
        /// <returns>Returns <c>true</c> if the show is succesfully sucessfuly queued; <c>false</c> otherwise.</returns>
        public bool Enqueue(TvMazeShow show);
    }
}
