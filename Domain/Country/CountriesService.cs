using System;
using System.Collections.Generic;
using System.Linq;
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
            var country = _consumer.GetByName(countryName);
            _repository.Add(country);
            return new CreatedCountryDTO(country.Id);
        }

        public Country GetCountryByName(string countryName)
        {
            return _repository.Get(x => x.CountryName.ToLower() == countryName.ToLower());
        }

        public bool Delete(string countryName)
        {
            try
            {
                _repository.Delete(countryName);
                return true;
            }

            catch (System.Exception)
            {
                return false;
            }
        }

        public bool Update(string countryName)
        {
            try
            {
                var countryUpdated = _consumer.GetByName(countryName);
                _repository.Update(countryUpdated);
                return true;
            }

            catch (System.Exception)
            {
                return false;
            }
        }

        public IList<Country> GetAll()
        {
            return _repository.GetAll();
        }

        public Country GetCountryByID(Guid id)
        {
            return _repository.Get(x => x.Id == id);
        }

        public IList<string> PercentageDiference()
        {
            var contries = GetAll();
            var contriesOrdered = contries.OrderByDescending(x => x.ActiveCases).ToList();

            var percentageDiference = new List<string>();
            for (int i = 1; i < contriesOrdered.Count; i++)
            {
                var finalValue = contriesOrdered[i - 1].ActiveCases;
                var inicialValue = contriesOrdered[i].ActiveCases;
                
                var percentage = (finalValue - inicialValue) / inicialValue * 100;
                percentageDiference.Add($"Percentual da Diferença em casos ativos entre: {contriesOrdered[i - 1].CountryName} e {contriesOrdered[i].CountryName} = {percentage.ToString("N2")}%");
            }
            
            return percentageDiference;
        }
    }
}