using System;

namespace Domain.Countries
{
    public class CountriesService
    {
        public CreatedCountryDTO Create(
            string activeCases,
            string countryName,
            string lastUpdate,
            string newCases,
            string newDeaths,
            string totalCases,
            string totalDeaths,
            string totalRecovered
        )
        {
            var country = new Country(activeCases, countryName, lastUpdate, newCases, newDeaths, totalCases, totalDeaths, totalRecovered);
            return new CreatedCountryDTO(Guid.NewGuid());
        }
    }
}