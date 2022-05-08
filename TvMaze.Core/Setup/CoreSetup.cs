// <copyright file="CoreSetup.cs" company="Davey Mathijssen">
// Copyright (c) Davey Mathijssen. All rights reserved.
// </copyright>

namespace TvMaze.Core.Setup
{
    using System.Diagnostics.CodeAnalysis;
    using System.Reflection;
    using AutoMapper;
    using Microsoft.Extensions.DependencyInjection;

    /// <summary>
    /// Contains methods to add and configure the TvMaze core.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public static class CoreSetup
    {
        public static IServiceCollection AddCore(this IServiceCollection services)
        {
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            return services;
        }
    }
}
