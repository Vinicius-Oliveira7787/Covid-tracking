using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Domain.Common;
using Domain.Countries;
using Microsoft.EntityFrameworkCore;

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

        public void Update(T outdatedEntity, T updatedEntity)
        {
            covidContext.Remove(outdatedEntity);
            Add(updatedEntity);
        }

        public void Delete(Guid id)
        {
            var entity = Get(x => x.Id == id);
            covidContext.Remove(entity);
            covidContext.SaveChanges();
        }

        public IList<T> GetAll()
        {
            return covidContext.Set<T>().ToList();
        }

        public T Get(Func<T, bool> predicate)
        {
            return covidContext.Set<T>().SingleOrDefault(predicate);
        }
    }
}