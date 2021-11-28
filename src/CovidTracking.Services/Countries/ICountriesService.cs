using CovidTracking.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CovidTracking.Services.Countries
{
    public interface ICountriesService
    {
        Task SaveCountryByNameAsync(string countryName);

        Country GetCountry(string countryName);

        void Delete(string countryName);

        Task UpdateAsync(string countryName);

        IEnumerable<Country> GetAll();

        IList<string> PercentageDiference();
    }
}