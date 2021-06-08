using System;
using System.Linq;
using CovidTracking.API.Models.Entities;

namespace CovidTracking.Data.Repositories
{
    public class CountriesRepository : Repository<Country>, ICountriesRepository
    {
        public CountriesRepository(CovidContext context) : base(context) {}

        public new Country Get(Func<Country, bool> predicate)
        {
            return covidContext.Countries.FirstOrDefault(predicate);
        }
    }
}