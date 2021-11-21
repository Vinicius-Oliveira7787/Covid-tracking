using AutoMapper;
using CovidTracking.API.Client.Implementation.CovidTracking;
using CovidTracking.Data.Models;
using CovidTracking.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CovidTracking.Services.Countries
{
    public class CountriesService : ICountriesService
    {
        private readonly ICountriesRepository _repository;
        private readonly ICovidTrackingAPIClient _CovidTrackingAPICLient;
        private readonly IMapper _mapper;

        public CountriesService(
            ICountriesRepository repository,
            ICovidTrackingAPIClient covidTrackingAPICLient,
            IMapper mapper
        )
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _CovidTrackingAPICLient = covidTrackingAPICLient ?? throw new ArgumentNullException(nameof(covidTrackingAPICLient));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task SaveCountryByNameAsync(string countryName)
        {
            var countryData = await _CovidTrackingAPICLient.GetCountryDataByNameAsync(countryName);
            var country = _mapper.Map<Country>(countryData);
            _repository.Add(country);
        }

        public void Delete(string countryName)
        {
            _repository.Delete(GetCountry(countryName));
        }

        public async Task UpdateAsync(string countryName)
        {
            var countryData = await _CovidTrackingAPICLient.GetCountryDataByNameAsync(countryName);
            var countryUpdated = _mapper.Map<Country>(countryData);

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

        public Country GetCountry(int id)
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