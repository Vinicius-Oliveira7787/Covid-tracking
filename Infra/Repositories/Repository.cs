using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Common;
using Domain.Countries;

namespace Infra
{
    public class Repository<T> : IRepository<T> where T : Entity
    {
        protected readonly CovidContext covidContext;

        public Repository(CovidContext covidContext)
        {
            this.covidContext = covidContext;
        }

        public void Add(T entity)
        {
            covidContext.Add<T>(entity);
            covidContext.SaveChanges();
        }

        public void Update(Country countryUpdated)
        {
            var countryOutdated = Get(x => x.CountryName.ToLower() == countryUpdated.CountryName.ToLower());
            if (countryOutdated != null)
            {
                countryUpdated.Id = countryOutdated.Id;
                Delete(countryOutdated.CountryName);
                covidContext.Add(countryUpdated);
                covidContext.SaveChanges();
            }
        }

        public void Delete(string countryName)
        {
            var country = Get(x => x.CountryName.ToLower() == countryName.ToLower());
            covidContext.Remove(country);
            covidContext.SaveChanges();
        }

        public IList<Country> GetAll()
        {
            return covidContext.Countries.ToList();
        }

        public Country Get(Func<Country, bool> predicate)
        {
            // return covidContext.Set<T>().FirstOrDefault(predicate); //! Não está funcionando 😥
            return covidContext.Countries.FirstOrDefault(predicate);
        }
    }
}