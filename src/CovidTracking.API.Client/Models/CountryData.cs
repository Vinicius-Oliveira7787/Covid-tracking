using Newtonsoft.Json;
using System;

namespace CovidTracking.API.Client.Models
{
    public class CountryData
    {
        [JsonProperty("Country_text")]
        public string CountryName { get; set; }

        [JsonProperty("Active Cases_text")]
        public double ActiveCases { get; set; }

        [JsonProperty("Last Update")]
        public DateTime LastUpdate { get; set; }

        [JsonProperty("New Cases_text")]
        public double NewCases { get; set; }

        [JsonProperty("New Deaths_text")]
        public double NewDeaths { get; set; }

        [JsonProperty("Total Cases_text")]
        public double TotalCases { get; set; }

        [JsonProperty("Total Deaths_text")]
        public double TotalDeaths { get; set; }

        [JsonProperty("Total Recovered_text")]
        public double TotalRecovered { get; set; }
    }
}
