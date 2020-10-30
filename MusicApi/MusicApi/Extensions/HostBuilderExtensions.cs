﻿using Microsoft.Extensions.Hosting;
using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicApi.Extensions
{
    public static class HostBuilderExtensions
    {

        public static IHostBuilder UseStructureMap(this IHostBuilder builder)
        {
            return UseStructureMap(builder, registry: null);
        }

        public static IHostBuilder UseStructureMap(this IHostBuilder builder, Registry registry)
        {
            return builder.UseServiceProviderFactory(new StructureMapServiceProviderFactory(registry));
        }

    }
}
