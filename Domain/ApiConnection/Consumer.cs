using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using Domain.ApiConnection.Instance;
using Newtonsoft.Json.Linq;

namespace Domain.ApiConnection.Consumers
{
    public class Consumer
    {
        public string BaseUrl 
        {
            get
            {
                return "https://covid-19-tracking.p.rapidapi.com/v1";
            }
        }

        private JArray HandleGetRequestToCovidApi()
        {
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(BaseUrl),
                Headers =
                {
                    { "x-rapidapi-key", "f39163a60cmsh5dbbe815ab8b2bbp169ce1jsn363ad8afacf9" },
                    { "x-rapidapi-host", "covid-19-tracking.p.rapidapi.com" },
                },
            };

            var response = HttpInstance.GetHttpClientInstance().SendAsync(request).Result;
            
            var covidDataArrayOfJson = JArray.Parse(response.Content.ReadAsStringAsync().Result);
            return covidDataArrayOfJson;            
        }

        public List<string> FilterData()
        {
            var covidData = HandleGetRequestToCovidApi();
            // return covidData[1]["Total Cases_text"].ToString();
            // return covidData[1].ToString();
            var teste = new List<string>();

            foreach (var item in covidData)
            {
                try
                {
                    teste.Add(item["Active Cases_text"].ToString());
                }

                catch (System.Exception)
                {
                    continue;
                }
            }

            return teste;
        }
    }
}
