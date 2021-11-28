using Microsoft.Extensions.DependencyInjection;
using System;

namespace CovidTracking.Data.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void ApplyMigrations(this IServiceCollection services)
        {
            if (services is null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            using var serviceProvider = services.BuildServiceProvider();

            serviceProvider.ApplyMigrations();
        }
    }
}
