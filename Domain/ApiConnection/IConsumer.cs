using System.Collections.Generic;
using Domain.Countries;

namespace Domain.ApiConnection.Consumers
{
    public interface IConsumer
    {
        Country GetByName(string countryName);
        
        IList<Country> GetAllContries();
    }
}
