using CovidTracking.API.Client.Implementation.CovidTracking;
using CovidTracking.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CovidTracking.Services.Countries
{
    public class CountriesService : ICountriesService
    {
        private static ICountriesRepository _repository { get; set; }
        private static ICovidTrackingAPIClient _CovidTrackingAPICLient { get; set; }

        public CountriesService(ICountriesRepository repository, ICovidTrackingAPIClient covidTrackingAPICLient)
        {
            _repository = repository;
            _CovidTrackingAPICLient = covidTrackingAPICLient;
        }

        public async Task SaveCountryByNameAsync(string countryName)
        {
            var entity = await _CovidTrackingAPICLient.GetCountryDataByNameAsync(countryName);
            _repository.Add();
        }

        public void Delete(string countryName)
        {
            _repository.Delete(GetCountry(countryName));
        }

        public void Update(string countryName)
        {
            var countryUpdated = _consumer.GetByName(countryName);

            var countryOutdated = GetCountry(countryName);
            countryUpdated.Id = countryOutdated.Id;

            _repository.Update(countryOutdated, countryUpdated);
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