using System;
using Domain.ApiConnection.Consumers;

namespace Domain.Countries
{
    public class CountriesService : ICountriesService
    {
        private static ICountriesRepository _repository { get; set; }
        private static IConsumer _consumer { get; set; }

        public CountriesService(ICountriesRepository repository, IConsumer consumer)
        {
            _repository = repository;
            _consumer = consumer;
        }

        public CreatedCountryDTO Create(string countryName)
        {
            var country = _consumer.GetSingleCountryByName(countryName);
            _repository.SaveEntity(country);
            return new CreatedCountryDTO(country.Id);
        }

        public Country GetCountry(string countryName)
        {
            return _repository.Get(Guid.Parse("c9f1a5df-de1e-4cf4-8092-a153e8dec6af"));
        }
    }
}