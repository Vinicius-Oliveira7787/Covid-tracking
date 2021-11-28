using Microsoft.EntityFrameworkCore;
using System;

namespace CovidTracking.Data.Extensions
{
    public static class ServiceProviderExtensions
    {
        public static void ApplyMigrations(this IServiceProvider serviceProvider)
        {
            if (serviceProvider is null)
            {
                throw new ArgumentNullException(nameof(serviceProvider));
            }

            var dbContext = (DbContext)serviceProvider.GetService(typeof(DataBaseContext));

            if (dbContext is not null) dbContext.Database.Migrate();
        }
    }
}
