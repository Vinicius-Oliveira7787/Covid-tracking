using System;
using System.Collections.Generic;
using Domain.Countries;

namespace Domain.Common
{
    public interface IRepository<T> where T : Entity
    {
        void Add(T entity);

        void Delete(Guid id);
        
        void Update(T outdatedEntity, T updatedEntity);
        
        IList<T> GetAll();

        T Get(Func<T, bool> predicate);
    }
}
