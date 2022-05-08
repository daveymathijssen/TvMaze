using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TvMaze.Core.Entities;

namespace TvMaze.Infrastructure.Repositories.Contexts
{
    /// <summary>
    /// 
    /// </summary>
    public class ShowContext : DbContext
    {
        public ShowContext(DbContextOptions<ShowContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            var splitStringConverter = new ValueConverter<IEnumerable<string>, string>(v => string.Join(";", v), v => v.Split(new[] { ';' }));
            builder.Entity<Show>()
                   .Property(nameof(Show.Genres))
                   .HasConversion(splitStringConverter);
        }

        public DbSet<Show> Shows { get; set; }
    }
}
