using CovidTracking.API.Client.Implementation.CovidTracking;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace CovidTracking.API.Configurations
{
    public static class ClientsConfiguration
    {
        public static void AddAPIsClient(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpClient<ICovidTrackingAPIClient, CovidTrackingAPIClient>(httpClientConfig =>
            {
                var headerKey = configuration.GetSection("Clients:CovidTrackingAPI:Key").Get<string[]>();
                var headerHost = configuration.GetSection("Clients:CovidTrackingAPI:Host").Get<string[]>();

                httpClientConfig.BaseAddress = new Uri(configuration.GetValue<string>("Clients:CovidTrackingAPI:BaseUrl"));
                httpClientConfig.DefaultRequestHeaders.Add(name: headerKey[0], value: headerKey[1]);
                httpClientConfig.DefaultRequestHeaders.Add(name: headerHost[0], value: headerHost[1]);
            });
        }
    }
}
