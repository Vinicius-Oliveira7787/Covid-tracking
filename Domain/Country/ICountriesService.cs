using System;
using System.Collections.Generic;

namespace Domain.Countries
{
    public interface ICountriesService
    {
        CreatedCountryDTO Create(string countryName);
        
        Country GetCountry(string countryName);

        bool Delete(string countryName);

        (string message, bool isValid) Update(string countryName);
        
        Country GetCountry(Guid id);

        IList<Country> GetAll();

        IList<string> PercentageDiference();
    }
}