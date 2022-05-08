using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;
using TvMaze.Infrastructure.Repositories;
using TvMaze.Infrastructure.Repositories.Contexts;
using TvMaze.Infrastructure.Repositories.Interfaces;

namespace TvMaze.Infrastructure.Setup
{
    /// <summary>
    /// Contains methods to add and configure the TvMaze infrastructure.
    /// </summary>
    public static class InfrastructureSetup
    {
        /// <summary>
        /// Add and configure the infrastructure.
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        [ExcludeFromCodeCoverage]
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var defaultConnectionString = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<ShowContext>(options =>
               options.UseSqlServer(defaultConnectionString));

            services.AddScoped<IShowRepository, ShowRepository>();

            var serviceProvider = services.BuildServiceProvider();
            try
            {
                var dbContext = serviceProvider.GetRequiredService<ShowContext>();
                dbContext.Database.Migrate();
            }
            catch 
            {
            }

            return services;
        }
    }
}
