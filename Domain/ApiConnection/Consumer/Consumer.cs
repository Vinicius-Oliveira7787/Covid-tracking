using System;
using System.Collections.Generic;
using System.Net.Http;
using Domain.ApiConnection.Instance;
using Domain.Countries;
using Newtonsoft.Json.Linq;

namespace Domain.ApiConnection.Consumers
{
    public class Consumer : IConsumer
    {
        public static string BaseUrl 
        {
            get
            {
                return "https://covid-19-tracking.p.rapidapi.com/v1";
            }
        }

        private (HttpResponseMessage message, bool isValid) GetResponseFromApi(string countryName = null)
        {
            try
            {
                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = String.IsNullOrEmpty(countryName) 
                        ? new Uri(BaseUrl)
                        : new Uri(BaseUrl + $"/{countryName}"),
                    Headers =
                    {
                        { "x-rapidapi-key", "f39163a60cmsh5dbbe815ab8b2bbp169ce1jsn363ad8afacf9" },
                        { "x-rapidapi-host", "covid-19-tracking.p.rapidapi.com" },
                    },
                };

                var response = HttpInstance.GetHttpClientInstance().SendAsync(request).Result;
                //! return (response.StatusCode, true);
                return (response, true);
            }

            catch (System.Exception)
            {
                return (null, false);
            }
        }

        public Country GetByName(string countryName)
        {
            var response = GetResponseFromApi(countryName);
            if (!response.isValid) return null;

            var covidData = JObject.Parse(response.message.Content.ReadAsStringAsync().Result);
            
            return JTokenToCountry(covidData);
        }

        private Country JTokenToCountry(JToken data)
        {
            var countryName = data["Country_text"].ToString();
            var activeCases = data["Active Cases_text"].ToObject<double>();
            var lastUpdate = data["Last Update"].ToString();
            var newCases = data["New Cases_text"].ToString();
            var newDeaths = data["New Deaths_text"].ToString();
            var totalCases = data["Total Cases_text"].ToString();
            var totalDeaths = data["Total Deaths_text"].ToString();
            var totalRecovered = data["Total Recovered_text"].ToString();

            return new Country(countryName, activeCases, lastUpdate, newCases, newDeaths, totalCases, totalDeaths, totalRecovered);
        }
        
        public IList<Country> GetAll()
        {
            var countries = new List<Country>();
            
            var response = GetResponseFromApi();
            var covidData = JArray.Parse(response.message.Content.ReadAsStringAsync().Result);

            foreach (var currentCountry in covidData)
            {
                try
                {
                    var country = JTokenToCountry(currentCountry);
                    countries.Add(country);
                }

                catch (System.Exception)
                {
                    continue;
                }
            }

            return countries;
        }
    }
}
