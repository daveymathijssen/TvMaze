using AutoMapper;
using TvMaze.Core.DTOs.Show;
using TvMaze.Infrastructure.Repositories.Contexts;
using TvMaze.Infrastructure.Repositories.Interfaces;

namespace TvMaze.Infrastructure.Repositories
{
    /// <summary>
    /// Repository interface that represents the methods for accessing show information.
    /// </summary>
    public class ShowRepository : IShowRepository
    {
        private readonly ShowContext context;
        private readonly IMapper mapper;

        public ShowRepository(ShowContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        /// <inheritdoc/>
        public bool CreateShow(CreateShowRequest request)
        {
            var show = this.mapper.Map<Core.Entities.Show>(request);
            this.context.Shows.Add(show);
            return this.context.SaveChanges() > 0;
        }

        /// <inheritdoc/>
        public bool CreateShows(IEnumerable<Core.Entities.Show> shows)
        {
            var newShows = new List<Core.Entities.Show>();
            foreach (var show in shows)
            {
                if (!this.context.Shows.Any(s => s.TvMazeId == show.TvMazeId))
                {
                    newShows.Add(show);
                }
            }
            this.context.Shows.AddRange(newShows);
            return this.context.SaveChanges() > 0;
        }

        /// <inheritdoc/>
        public void DeleteShowById(int showId)
        {
            var show = new Core.Entities.Show()
            {
                Id = showId,
            };
            this.context.Shows.Remove(show);
            this.context.SaveChanges();
        }

        /// <inheritdoc/>
        public int GetHighestMazeTvId()
        {
            if (!context.Shows.Any())
            {
                return 0;
            }

            return context.Shows.OrderBy(s => s.Id).Last().Id;
        }

        /// <inheritdoc/>
        public Core.Entities.Show? GetShowById(int showId)
        {
            return context.Shows.Where(s => s.Id == showId).FirstOrDefault();
        }

        /// <inheritdoc/>
        public Core.Entities.Show? GetShowByName(string name)
        {
            return context.Shows.Where(s => s.Name == name).FirstOrDefault();
        }

        /// <inheritdoc/>
        public bool GetShowExistsByTvMazeId(int tvMazeShowId)
        {
            return context.Shows.Where(s => s.TvMazeId == tvMazeShowId).Any();
        }

        /// <inheritdoc/>
        public IEnumerable<Core.Entities.Show> GetShows(int pageId, int pageSize)
        {
            return this.context.Shows
                .OrderBy(s => s.Id)
                .Skip(pageId * pageSize)
                .Take(pageSize)
                .ToList();
        }

        /// <inheritdoc/>
        public Core.Entities.Show? UpdateShowById(int showId, UpdateShowRequest request)
        {
            if (request == null)
            {
                return null;
            }

            var show = this.GetShowById(showId);
            if (show != null)
            {
                show.Summary = request.Summary;
                show.PremieredDate = request.PremieredDate;
                show.Name = request.Name;
                show.Genres = request.Genres;
                show.Language = request.Language;
                this.context.SaveChanges();
            }
            
            return show;
        }
    }
}
