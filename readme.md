# TvMaze Scraper and API
This project is able to scrape shows from the [TvMaze API](https://www.tvmaze.com/api).
Shows since 2014 are saved to a mssql database, accessed by using [EF Core](https://docs.microsoft.com/en-us/ef/core/).
Additionally, this project contains an API which enables users to query the stored shows,
add new shows (which are possibly not available on TvMaze), update shows, and delete shows.

A .Net 6 Domain Driven Design/Onion architecture/clean design is used, using [Automapper](https://automapper.org/) to map objects.
Only a few Unit tests are available, which use the [xUnit framework](https://xunit.net/). However, in the future these need to be expanded.

# How to use
1. Open this solution in Visual Studio 2022 or newer.
2. Change the database connection strings in the appsettings.json files of the TvMaze.Api and TvMaze.Scraper projects to connect to your database instance.
3. Open the Package Manager Console by navigating to Tools -> NuGet Package Manager -> Package Manager Console
4. Change the default project within the Package Manager Console to 'TvMaze.Infrastructure' and run the following command:
`Update-Database`
5. You can now run the TvMaze.Api and TvMaze.Scraper projects.

# Todo
- [ ] Implement [Serilog](https://serilog.net/) to enable structured logging.
- [ ] Add more unit tests
- [ ] Move hardcoded values (such as API endpoint URIs, time values, and constants) to the appsettings.json and create Option configuration classes.
- [ ] Fix all StyleCop warnings (mainly add more documentation to public classes, properties, and methods).
