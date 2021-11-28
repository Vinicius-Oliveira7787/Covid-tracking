using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;

namespace CovidTracking.Data.Factories
{
    public class DataBaseContextFactory
    {
        public static DataBaseContext CreateDbContext(string connectionString)
        {
            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new ArgumentException($"'{nameof(connectionString)}' cannot be null or whitespace.", nameof(connectionString));
            }

            var builder = new DbContextOptionsBuilder<DataBaseContext>()
                .UseMySQL(connectionString, MySqlOptions =>
                {
                    var assembly = typeof(DataBaseContext).Assembly;
                    var assemblyName = assembly.GetName();

                    MySqlOptions.MigrationsAssembly(assemblyName.Name);
                })
                .ConfigureWarnings(x => x.Ignore(RelationalEventId.AmbientTransactionWarning));

            return (DataBaseContext)Activator.CreateInstance(typeof(DataBaseContext), new[] { builder.Options });
        }
    }
}
