using CovidTracking.API.Client.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace CovidTracking.API.Client.Implementation.CovidTracking
{
    public class CovidTrackingAPIClient : HttpClientBase, ICovidTrackingAPIClient
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public CovidTrackingAPIClient(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
        }

        public async Task<CountryData> GetCountryDataByNameAsync(string countryName)
        {
            var response = await GetAsync<CountryData>($"/{countryName}").ConfigureAwait(continueOnCapturedContext: false);

            return response;
        }

        public async Task<IEnumerable<CountryData>> GetAllCountriesDataAsync()
        {
            var response = await GetAsync<IEnumerable<CountryData>>(string.Empty).ConfigureAwait(continueOnCapturedContext: false);

            return response;
        }

        protected override HttpClient CreateHttpClient()
        {
            return _httpClientFactory.CreateClient(nameof(CovidTrackingAPIClient));
        }
    }
}
