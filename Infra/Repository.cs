using System;
using System.Text;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace Infra
{
    public class Repository
    {
        private static void InsertData()
        {
            using(var context = new CovidContext())
            {
                // Creates the database if not exists
                context.Database.EnsureCreated();
                // context.Database.Migrate();

                // Adds a publisher
                var publisher = new Publish
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Mariner Books"
                };

                context.Publisher.Add(publisher);

                // Adds some books
                context.Book.Add(new Book
                {
                    ISBN = Guid.NewGuid().ToString(),
                    Title = "The Lord of the Rings",
                    Author = "J.R.R. Tolkien",
                    Language = "English",
                    Pages = 1216,
                    Publisher = publisher
                });

                context.Book.Add(new Book
                {
                    ISBN = Guid.NewGuid().ToString(),
                    Title = "The Sealed Letter",
                    Author = "Emma Donoghue",
                    Language = "English",
                    Pages = 416,
                    Publisher = publisher
                });

                // Saves changes
                context.SaveChanges();
            }
        }

        public string PrintData()
        {
            InsertData();

            // Gets and prints all books in database
            using (var context = new CovidContext())
            {
                var books = context.Book.Include(p => p.Publisher);
                
                var data = new StringBuilder();
                foreach(var book in books)
                {
                    data.AppendLine($"ISBN: {book.ISBN}");
                    data.AppendLine($"Title: {book.Title}");
                    data.AppendLine($"Publisher: {book.Publisher.Name}");
                }

                return data.ToString();
            }
        }
    }
}