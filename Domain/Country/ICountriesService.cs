namespace Domain.Countries
{
    public interface ICountriesService
    {
        CreatedCountryDTO Create(string countryName);
        
        Country GetCountry(string countryName);
    }
}