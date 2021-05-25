using System;
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

        // public string PrintData()
        // {
        //     InsertData();

        //     // Gets and prints all books in database
        //     using (var context = new CovidContext())
        //     {
        //         var books = context.Book.Include(p => p.Publisher);
                
        //         var data = new StringBuilder();
        //         foreach(var book in books)
        //         {
        //             data.AppendLine($"ISBN: {book.ISBN}");
        //             data.AppendLine($"Title: {book.Title}");
        //             data.AppendLine($"Publisher: {book.Publisher.Name}");
        //         }

        //         return data.ToString();
        //     }
        // }
    }
}