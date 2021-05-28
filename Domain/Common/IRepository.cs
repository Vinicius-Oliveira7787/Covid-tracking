using System;
using System.Collections.Generic;
using Domain.Countries;

namespace Domain.Common
{
    public interface IRepository<T> where T : Entity
    {
        void Add(T entity);

        void Delete(string countryName);
        
        void Update(Country country);
        
        IList<Country> GetAll();

        Country Get(Func<Country, bool> predicate);
        // T Get(Func<T, bool> predicate);
    }
}
