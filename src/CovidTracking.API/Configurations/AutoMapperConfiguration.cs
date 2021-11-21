using Microsoft.Extensions.DependencyInjection;
using System;

namespace CovidTracking.API.Configurations
{
    public static class AutoMapperConfiguration
    {
        public static void AddAutoMapperConfiguration(this IServiceCollection services)
        {
            services = services ?? throw new ArgumentNullException(nameof(services));

            var assemblies = new[] { typeof(Startup).Assembly };
            services.AddAutoMapper((_, mapperConfiguration) => 
                mapperConfiguration.AddMaps(assemblies), assemblies
            );
        }
    }
}
