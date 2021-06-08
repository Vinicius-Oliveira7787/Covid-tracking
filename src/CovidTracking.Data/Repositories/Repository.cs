using System;
using System.Collections.Generic;
using System.Linq;
using CovidTracking.API.Models.Entities;

namespace CovidTracking.Data.Repositories
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
            // Context.Entry(await Context.MyDbSet.FirstOrDefaultAsync(x => x.Id == item.Id)).CurrentValues.SetValues(item);
            // return (await Context.SaveChangesAsync()) > 0;
        }

        public void Delete(T entity)
        {
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