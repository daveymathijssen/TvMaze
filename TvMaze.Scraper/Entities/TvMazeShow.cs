// <copyright file="TvMazeShow.cs" company="Davey Mathijssen">
// Copyright (c) Davey Mathijssen. All rights reserved.
// </copyright>

namespace TvMaze.Scraper.Entities
{
    public class TvMazeShow
    {
        public int id { get; set; }
        public string name { get; set; }
        public string language { get; set; }
        public IList<string> genres { get; set; }
        public string premiered { get; set; }
        public string summary { get; set; }
    }
}
