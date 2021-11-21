using System;
using System.Collections.Generic;
using CovidTracking.API.Models.Entities;

namespace CovidTracking.Services.Countries
{
    public interface ICountriesService
    {
        void CreateAsync(string countryName);
        
        Country GetCountry(string countryName);

        void Delete(string countryName);

        void Update(string countryName);
        
        Country GetCountry(Guid id);

        IList<Country> GetAll();

        IList<string> PercentageDiference();
    }
}