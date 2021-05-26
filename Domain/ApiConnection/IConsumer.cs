using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using Domain.ApiConnection.Instance;
using Domain.Countries;
using Newtonsoft.Json.Linq;

namespace Domain.ApiConnection.Consumers
{
    public interface IConsumer
    {
        Country GetSingleCountryByName(string countryName);
        
        IList<Country> GetAllContries();
    }
}
