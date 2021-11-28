using CovidTracking.Data.Modules;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace CovidTracking.API.Configurations
{
    public static class ModulesConfiguration
    {
        public static void AddModulesConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            if (services is null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (configuration is null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }

            new DataModule().Registry(services, configuration);
        }
    }
}
