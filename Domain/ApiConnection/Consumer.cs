using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using Domain.ApiConnection.Instance;
using Domain.Countries;
using Newtonsoft.Json.Linq;

namespace Domain.ApiConnection.Consumers
{
    public class Consumer
    {
        public static string BaseUrl 
        {
            get
            {
                return "https://covid-19-tracking.p.rapidapi.com/v1";
            }
        }

        private HttpResponseMessage SendRequestAndGetResponseToTheCovidApi(string countryName)
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
            return response;
        }

        private HttpResponseMessage SendRequestAndGetResponseToTheCovidApi()
        {
            return SendRequestAndGetResponseToTheCovidApi(string.Empty);
        }

        public string GetSingleCountry(string countryName)
        {
            var response = SendRequestAndGetResponseToTheCovidApi(countryName);
            var covidData = JObject.Parse(response.Content.ReadAsStringAsync().Result);
            return covidData.ToString();
        }
        
        public IList<Country> HandleDataToPutInDb()
        {
            var countries = new List<Country>();
            
            var response = SendRequestAndGetResponseToTheCovidApi();
            var covidData = JArray.Parse(response.Content.ReadAsStringAsync().Result);

            foreach (var currentCountry in covidData)
            {
                try
                {
                    var activeCases = currentCountry["Active Cases_text"].ToString();
                    var countryName = currentCountry["Country_text"].ToString().ToString();
                    var lastUpdate = currentCountry["Last Update"].ToString();
                    var newCases = currentCountry["New Cases_text"].ToString();
                    var newDeaths = currentCountry["New Deaths_text"].ToString();
                    var totalCases = currentCountry["Total Cases_text"].ToString();
                    var totalDeaths = currentCountry["Total Deaths_text"].ToString();
                    var totalRecovered = currentCountry["Total Recovered_text"].ToString();

                    var country = new Country(activeCases, countryName, lastUpdate, newCases, newDeaths, totalCases, totalDeaths, totalRecovered);
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
