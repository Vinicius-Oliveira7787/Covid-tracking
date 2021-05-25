using Domain.Common;

namespace Domain.Countries
{
    public class Country : Entity
    {
        public string ActiveCases { get; private set; }
        public string CountryName { get; private set; }
        public string LastUpdate { get; private set; }
        public string NewCases { get; private set; }
        public string NewDeaths { get; private set; }
        public string TotalCases { get; private set; }
        public string TotalDeaths { get; private set; }
        public string TotalRecovered { get; private set; }

        public Country(
            string activeCases,
            string countryName,
            string lastUpdate,
            string newCases,
            string newDeaths,
            string totalCases,
            string totalDeaths,
            string totalRecovered
        ) : base()
        {
            ActiveCases = activeCases;
            CountryName = countryName;
            LastUpdate = lastUpdate;
            NewCases = newCases;
            NewDeaths = newDeaths;
            TotalCases = totalCases;
            TotalDeaths = totalDeaths;
            TotalRecovered = totalRecovered;
        }
    }
}
