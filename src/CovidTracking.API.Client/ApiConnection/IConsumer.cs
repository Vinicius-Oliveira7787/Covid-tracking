using System.Collections.Generic;
using CovidTracking.API.Models.Entities;

namespace CovidTracking.API.Client.ApiConnection
{
    public interface IConsumer
    {
        Country GetByName(string countryName);
        
        IList<Country> GetAll();
    }
}
