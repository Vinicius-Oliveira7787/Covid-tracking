using System;

namespace Domain.Countries
{
    public class CountriesService : ICountriesService
    {
        private static ICountriesRepository _repository { get; set; }

        public CountriesService(ICountriesRepository repository)
        {
            _repository = repository;
        }

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
            _repository.SaveEntity(country);
            return new CreatedCountryDTO(country.Id);
        }
    }
}