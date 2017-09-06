using System;
using System.Collections.Generic;
using Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
namespace Webjob.Registration
{
    public static class StorageExtensions
    {
        public static void AddDataLayer(this IServiceCollection services, IConfigurationRoot configuration, ServiceLifetime serviceLifetime = ServiceLifetime.Scoped)
        {
            services.AddDbContext<ApplicationDbContext>(dbContextOptions =>
                        dbContextOptions.UseSqlServer(configuration.GetConnectionString("ApplicationDatabase"),
                        sqlOptions => sqlOptions.EnableRetryOnFailure()), serviceLifetime);
        }       
    }
}