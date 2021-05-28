using System;
using System.Collections.Generic;

namespace Domain.Countries
{
    public interface ICountriesService
    {
        CreatedCountryDTO Create(string countryName);
        
        Country GetCountryByName(string countryName);

        bool Delete(string countryName);

        bool Update(string countryName);
        
        Country GetCountryByID(Guid id);

        IList<Country> GetAll();

        IList<string> PercentageDiference();
    }
}