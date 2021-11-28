using AutoMapper;
using CovidTracking.API.Client.Implementation.CovidTracking;
using CovidTracking.Data.Models;
using EntityFrameworkCore.UnitOfWork.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CovidTracking.Services.Countries
{
    public class CountriesService : ICountriesService
    {
        private readonly ICovidTrackingAPIClient _covidTrackingAPICLient;
        private readonly IRepositoryFactory _repositoryFactory;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CountriesService(
            ICovidTrackingAPIClient covidTrackingAPICLient,
            IRepositoryFactory repositoryFactory,
            IUnitOfWork unitOfWork,
            IMapper mapper
        )
        {
            _covidTrackingAPICLient = covidTrackingAPICLient ?? throw new ArgumentNullException(nameof(covidTrackingAPICLient));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _repositoryFactory = repositoryFactory ?? _unitOfWork;
        }

        public async Task SaveCountryByNameAsync(string countryName)
        {
            var countryData = await _covidTrackingAPICLient.GetCountryDataByNameAsync(countryName);
            var country = _mapper.Map<Country>(countryData);
            
            var repository = _unitOfWork.Repository<Country>();
            
            repository.Add(country);
        }

        public void Delete(string countryName)
        {
            var repository = _unitOfWork.Repository<Country>();

            repository.Remove(GetCountry(countryName));
        }

        public async Task UpdateAsync(string countryName)
        {
            var countryData = await _covidTrackingAPICLient.GetCountryDataByNameAsync(countryName);
            var countryUpdated = _mapper.Map<Country>(countryData);

            var countryOutdated = GetCountry(countryName);
            countryUpdated.Id = countryOutdated.Id;

            var repository = _unitOfWork.Repository<Country>();

            repository.Update(countryUpdated);
        }

        public IEnumerable<Country> GetAll()
        {
            var repository = _repositoryFactory.Repository<Country>();
            var query = repository.MultipleResultQuery();

            return repository.Search(query);
        }

        public Country GetCountry(string countryName)
        {
            var repository = _repositoryFactory.Repository<Country>();
            var query = repository.SingleResultQuery()
                .AndFilter(x => x.CountryName.ToLower() == countryName.ToLower());

            return repository.SingleOrDefault(query);
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
                percentageDiference.Add($"Percentual da Diferença em casos ativos entre: {contriesOrdered[i - 1].CountryName} e {contriesOrdered[i].CountryName} = {percentage:N2}%");
            }

            return percentageDiference;
        }
    }
}