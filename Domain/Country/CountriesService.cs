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

        public (string message, bool isValid) Create(string countryName)
        {
            var country = _consumer.GetByName(countryName);

            try
            {
                _repository.Add(country);
            }
            
            catch (System.Exception ex)
            {
                return (ex.Message, false);
            }

            return ("Success", true);
        }

        public void Delete(string countryName)
        {
            _repository.Delete(GetCountry(countryName));
        }

        public (string message, bool isValid) Update(string countryName)
        {
            try
            {
                var countryUpdated = _consumer.GetByName(countryName);
                
                var countryOutdated = GetCountry(countryName);
                countryUpdated.Id = countryOutdated.Id;

                _repository.Update(countryOutdated, countryUpdated);
                return ("Success", true);
            }

            catch (Exception ex)
            {
                return (ex.Message, false);
            }
        }

        public IList<Country> GetAll()
        {
            return _repository.GetAll();
        }

        public Country GetCountry(string countryName)
        {
            return _repository.Get(x => x.CountryName.ToLower() == countryName.ToLower());
        }

        public Country GetCountry(Guid id)
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