// <copyright file="CreateShowRequest.cs" company="Davey Mathijssen">
// Copyright (c) Davey Mathijssen. All rights reserved.
// </copyright>

namespace TvMaze.Core.DTOs.Show
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Describes the request to create a show.
    /// </summary>
    public class CreateShowRequest
    {
        /// <summary>
        /// Gets or sets the name of the show.
        /// </summary>
        [Required]
        public string Name { get; set; } = null!;

        /// <summary>
        /// Gets or sets the language of the show.
        /// </summary>
        [Required]
        public string Language { get; set; } = null!;

        /// <summary>
        /// Gets or sets the premiering date of the show.
        /// </summary>
        [DataType(DataType.Date)]
        public DateTime PremieredDate { get; set; }

        /// <summary>
        /// Gets or sets the genres of the show.
        /// </summary>
        [Required]
        public IEnumerable<string> Genres { get; set; } = null!;

        /// <summary>
        /// Gets or sets the summary of the show.
        /// </summary>
        [Required]
        public string Summary { get; set; } = null!;
    }
}
