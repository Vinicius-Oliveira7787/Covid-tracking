namespace CovidTracking.API.Models.Entities
{
    public class Country : Entity
    {
        public string CountryName { get; set; }
        public double ActiveCases { get; set; }
        public string LastUpdate { get; set; }
        public string NewCases { get; set; }
        public string NewDeaths { get; set; }
        public string TotalCases { get; set; }
        public string TotalDeaths { get; set; }
        public string TotalRecovered { get; set; }

        public Country(
            string countryName,
            double activeCases,
            string lastUpdate,
            string newCases,
            string newDeaths,
            string totalCases,
            string totalDeaths,
            string totalRecovered
        ) : base()
        {
            CountryName = countryName;
            ActiveCases = activeCases;
            LastUpdate = lastUpdate;
            NewCases = newCases;
            NewDeaths = newDeaths;
            TotalCases = totalCases;
            TotalDeaths = totalDeaths;
            TotalRecovered = totalRecovered;
        }
    }
}
