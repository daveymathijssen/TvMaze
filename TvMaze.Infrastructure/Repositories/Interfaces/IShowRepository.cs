using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TvMaze.Core.DTOs.Show;

namespace TvMaze.Infrastructure.Repositories.Interfaces
{
    /// <summary>
    /// Interface for the show repository.
    /// </summary>
    public interface IShowRepository
    {
        /// <summary>
        /// Get all shows.
        /// </summary>
        /// <remarks>The amount of returned instances is limited by the pageSize.</remarks>
        /// <param name="pageId">The id of the page.</param>
        /// <param name="pageSize">The size of the page (number of shows per page).</param>
        /// <returns>Show instances.</returns>
        IEnumerable<Core.Entities.Show> GetShows(int pageId, int pageSize);

        /// <summary>
        /// Get a show by its name.
        /// </summary>
        /// <param name="name">The name of the show.</param>
        /// <returns>The requested show.</returns>
        Core.Entities.Show? GetShowByName(string name);

        /// <summary>
        /// Get a show by its id.
        /// </summary>
        /// <param name="showId">The id of the show.</param>
        /// <returns>The requested show.</returns>
        Core.Entities.Show? GetShowById(int showId);

        /// <summary>
        /// Get a show by its TvMaze show id.
        /// </summary>
        /// <param name="tvMazeShowId">The TvMaze show id.</param>
        /// <returns>The requested show.</returns>
        bool GetShowExistsByTvMazeId(int tvMazeShowId);

        /// <summary>
        /// Delete a show by its id.
        /// </summary>
        /// <param name="showId">The id of the show.</param>
        void DeleteShowById(int showId);

        /// <summary>
        /// Create a new show.
        /// </summary>
        /// <param name="request">The new show to be created.</param>
        /// <returns>Returns <c>true</c> if the show is sucessfully saved to the database; <c>false</c> otherwise.</returns>
        bool CreateShow(CreateShowRequest request);

        /// <summary>
        /// Create new shows.
        /// </summary>
        /// <remarks>Only creates new shows when the corresponding TvMaze show id does not already exist.</remarks>
        /// <param name="shows">The new shows to be created.</param>
        /// <returns>Returns <c>true</c> if the showsare sucessfully saved to the database; <c>false</c> otherwise.</returns>
        bool CreateShows(IEnumerable<Core.Entities.Show> shows);

        /// <summary>
        /// Update a show by it's id.
        /// </summary>
        /// <param name="showId">The id of the show.</param>
        /// <param name="request">The new information for the show.</param>
        /// <returns>The updated show.</returns>
        Core.Entities.Show UpdateShowById(int showId, UpdateShowRequest request);

        /// <summary>
        /// Get the highest MazeTv show id stored in the show repository.
        /// </summary>
        /// <returns>Highest MazeTv show id.</returns>
        int GetHighestMazeTvId();
    }
}
