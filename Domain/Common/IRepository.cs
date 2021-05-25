using System;

namespace Domain.Common
{
    public interface IRepository<T> where T : Entity
    {
        void SaveEntity(T entity);
    }
}