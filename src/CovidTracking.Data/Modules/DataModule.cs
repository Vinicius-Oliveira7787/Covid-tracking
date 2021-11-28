using CovidTracking.Data.Factories;
using EntityFrameworkCore.UnitOfWork.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace CovidTracking.Data.Modules
{
    public class DataModule
    {
        public string ModuleName => nameof(DataModule);

        public void Registry(IServiceCollection services, IConfiguration configuration)
        {
            if (services is null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (configuration is null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }

            var connectionString = configuration.GetConnectionString("ConnectionString");
            services.AddTransient(provider => DataBaseContextFactory.CreateDbContext(connectionString));
            services.AddUnitOfWork<DataBaseContext>();
        }
    }
}
