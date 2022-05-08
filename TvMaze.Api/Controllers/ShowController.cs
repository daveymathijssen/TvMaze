// <copyright file="ShowController.cs" company="Davey Mathijssen">
// Copyright (c) Davey Mathijssen. All rights reserved.
// </copyright>

namespace TvMaze.Api.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using TvMaze.Core.DTOs.Show;
    using TvMaze.Infrastructure.Repositories.Interfaces;

    [ApiController]
    [Route("api/v1/[controller]")]
    public class ShowController : ControllerBase
    {
        private readonly ILogger<ShowController> log;
        private readonly IShowRepository showRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="ShowController"/> class.
        /// </summary>
        /// <param name="logger">Logger instance.</param>
        /// <param name="showRepository">Show repository.</param>
        public ShowController(ILogger<ShowController> logger, IShowRepository showRepository)
        {
            this.log = logger;
            this.showRepository = showRepository;
        }

        /// <summary>
        /// Create a new show.
        /// </summary>
        /// <param name="show">The new show instance.</param>
        /// <returns>The created <see cref="Show"/>.</returns>
        /// <response code="201">The newly created show.</response>
        /// <response code="400">Incorrect show instance provided.</response>
        /// <response code="500">Internal server error.</response>
        [HttpPost]
        [ProducesResponseType(typeof(CreateShowRequest), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Create([FromBody] CreateShowRequest show)
        {
            if (!this.ModelState.IsValid)
            {
                this.log.LogInformation("Client send invalid model while trying to create a new show {Show}.", show);
                return this.BadRequest(this.ModelState);
            }

            if (show == null)
            {
                this.log.LogInformation("Client provided a null instance while trying to create a show.");
                return this.BadRequest("Please provide show details that need to be created.");
            }

            if (!this.showRepository.CreateShow(show))
            {
                return this.BadRequest();
            }

            return this.CreatedAtAction(nameof(this.Get), new { name = show.Name }, show);
        }

        /// <summary>
        /// Get the show with given name.
        /// </summary>
        /// <param name="name">Name of the show.</param>
        /// <returns>HTTP 200 with <see cref="Show"/>.</returns>
        /// <response code="200">Requested show instance.</response>
        /// <response code="404">Requested show not found.</response>
        /// <response code="500">Internal server error.</response>
        [HttpGet("{name}")]
        [ProducesResponseType(typeof(Show), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Get(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return this.BadRequest("Please provide a show name.");
            }

            var show = this.showRepository.GetShowByName(name);

            if (show == null)
            {
                return this.NotFound();
            }

            return this.Ok(show);
        }

        /// <summary>
        /// Get all shows, limited by a given page size (max 500).
        /// </summary>
        /// <param name="pageId">The id of the page.</param>
        /// <param name="pageSize">The size of the page (number of shows per page).</param>
        /// <returns>HTTP 200 with all <see cref="Show"/> instances.</returns>
        /// <response code="200">Requested show instance.</response>
        /// <response code="404">Requested show not found.</response>
        /// <response code="500">Internal server error.</response>
        [HttpGet]
        [ProducesResponseType(typeof(Show), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Get(int pageId = 0, int pageSize = 25)
        {
            if (pageId < 0)
            {
                pageId = 0;
            }

            if (pageSize < 0 || pageSize > 500)
            {
                pageSize = 25;
            }

            return this.Ok(this.showRepository.GetShows(pageId, pageSize));
        }

        /// <summary>
        /// Update show details.
        /// </summary>
        /// <param name="id">The íd of the show that will be updated.</param>
        /// <param name="show">The show that will be updated (if not null).</param>
        /// <returns>HTTP Status 200 if the show has been succesfully updated.</returns>
        /// <response code="200">Show updated.</response>
        /// <response code="400">No show details provided.</response>
        /// <response code="404">Could not find the show.</response>
        /// <response code="500">Internal server error.</response>
        [HttpPatch("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Update(int id, [FromBody] UpdateShowRequest show)
        {
            if (show == null)
            {
                return this.BadRequest();
            }

            if (this.showRepository.UpdateShowById(id, show) == null)
            {
                return this.NotFound();
            }

            return this.Ok();
        }

        /// <summary>
        /// Delete the show with given id.
        /// </summary>
        /// <param name="id">Id of the show.</param>
        /// <returns>HTTP 204 if the show has been deleted.</returns>
        /// <response code="204">Show deleted for given id.</response>
        /// <response code="500">Internal server error.</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Delete(int id)
        {
            this.showRepository.DeleteShowById(id);
            return this.NoContent();
        }
    }
}