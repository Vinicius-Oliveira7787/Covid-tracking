using System;
using System.Linq.Expressions;
using Domain.Countries;

namespace Domain.Common
{
    public interface IRepository<T> where T : Entity
    {
        void SaveEntity(T entity);
                
        T Get(Expression<Func<T, bool>> predicate);
        
        T Get(Guid id);
    }
}