using CovidTracking.API.Client.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CovidTracking.API.Client.Implementation.CovidTracking
{
    public interface ICovidTrackingAPIClient
    {
        Task<CountryData> GetCountryDataByNameAsync(string name);
        Task<IEnumerable<CountryData>> GetAllCountriesDataAsync();
    }
}