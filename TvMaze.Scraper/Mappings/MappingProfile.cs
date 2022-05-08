// <copyright file="MappingProfile.cs" company="Davey Mathijssen">
// Copyright (c) Davey Mathijssen. All rights reserved.
// </copyright>

namespace TvMaze.Scraper.Mappings
{
    using AutoMapper;

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            this.CreateMap<Scraper.Entities.TvMazeShow, Core.Entities.Show>()
                .ForMember(
                    dest => dest.Id,
                    opt => opt.Ignore())
                .ForMember(
                    dest => dest.TvMazeId,
                    opt => opt.MapFrom(src => src.id))
                .ForMember(
                    dest => dest.Genres,
                    opt => opt.MapFrom(src => src.genres))
                .ForMember(
                    dest => dest.PremieredDate,
                    opt => opt.MapFrom(src => src.premiered))
                .ReverseMap();
        }
    }
}
