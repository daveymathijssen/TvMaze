// <copyright file="ShowProfile.cs" company="Davey Mathijssen">
// Copyright (c) Davey Mathijssen. All rights reserved.
// </copyright>

namespace TvMaze.Core.Mappings
{
    using AutoMapper;
    using TvMaze.Core.DTOs.Show;

    public class ShowProfile : Profile
    {
        public ShowProfile()
        {
            this.CreateMap<CreateShowRequest, Entities.Show>()
                .ForMember(
                    dest => dest.Id,
                    opt => opt.Ignore())
                .ForMember(
                    dest => dest.TvMazeId,
                    opt => opt.Ignore());

            this.CreateMap<UpdateShowRequest, Entities.Show>()
                .ForMember(
                    dest => dest.Id,
                    opt => opt.Ignore())
                .ForMember(
                    dest => dest.TvMazeId,
                    opt => opt.Ignore());

            this.CreateMap<Show, Entities.Show>()
                .ForMember(
                    dest => dest.TvMazeId,
                    opt => opt.Ignore());
        }
    }
}
