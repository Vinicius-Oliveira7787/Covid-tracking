using System;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Domain;
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

        public void SaveEntity(T entity)
        {
            covidContext.Add<T>(entity);
            covidContext.SaveChanges();
        }

        // TODO: Fazer get genérico funcionar
        public T Get(Expression<Func<T, bool>> predicate)
        {
            return covidContext
                .Set<T>()
                .Include(x => x.Id)
                .Where(predicate)
                .FirstOrDefault();
        }
        
        public T Get(Guid id)
        {
            return Get(x => x.Id == id);
            // return covidContext.Set<T>().Find(id); // Funcionando
        }
    }
}