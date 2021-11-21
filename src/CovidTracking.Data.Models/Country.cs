using System;

namespace CovidTracking.Data.Models
{
    public class Country
    {
        public string CountryName { get; set; }
        public double ActiveCases { get; set; }
        public DateTime LastUpdate { get; set; }
        public double NewCases { get; set; }
        public double NewDeaths { get; set; }
        public double TotalCases { get; set; }
        public double TotalDeaths { get; set; }
        public double TotalRecovered { get; set; }
    }
}
