using System;
using System.Collections.Generic;
using CovidTracking.API.Models.Entities;

namespace CovidTracking.Data.Repositories
{
    public interface IRepository<T> where T : Entity
    {
        void Add(T entity);

        void Delete(T entity);
        
        void Update(T outdatedEntity, T updatedEntity);
        
        IList<T> GetAll();

        T Get(Func<T, bool> predicate);
    }
}
