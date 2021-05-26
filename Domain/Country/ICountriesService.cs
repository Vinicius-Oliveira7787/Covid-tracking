namespace Domain.Countries
{
    public interface ICountriesService
    {
        CreatedCountryDTO Create(
            string activeCases,
            string countryName,
            string lastUpdate,
            string newCases,
            string newDeaths,
            string totalCases,
            string totalDeaths,
            string totalRecovered
        );
    }
}