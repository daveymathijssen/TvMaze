// <copyright file="Show.cs" company="Davey Mathijssen">
// Copyright (c) Davey Mathijssen. All rights reserved.
// </copyright>

namespace TvMaze.Core.Entities
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// Entity describing a show.
    /// </summary>
    public class Show
    {
        /// <summary>
        /// Gets or sets the Id of the show, used to distinguish the show in our systems.
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the TvMaze Id of the show, used by TvMaze (www.tvmaze.com) to distinguish the shows in their systems.
        /// </summary>
        public int? TvMazeId { get; set; }

        /// <summary>
        /// Gets or sets the name of the show.
        /// </summary>
        public string Name { get; set; } = null!;

        /// <summary>
        /// Gets or sets the language of the show.
        /// </summary>
        public string Language { get; set; } = null!;

        /// <summary>
        /// Gets or sets the premiering date of the show.
        /// </summary>
        [DataType(DataType.Date)]
        public DateTime PremieredDate { get; set; }

        /// <summary>
        /// Gets or sets the genres of the show.
        /// </summary>
        public IEnumerable<string> Genres { get; set; } = null!;

        /// <summary>
        /// Gets or sets the summary of the show.
        /// </summary>
        public string Summary { get; set; } = null!;
    }
}
