using GenericRepositoryPattern.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GenericRepositoryPattern.Data
{
    public class LibraryContext : DbContext
    {
        public LibraryContext(DbContextOptions<LibraryContext> options) : base(options)
        {}

        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Author>().HasData(
                new Author
                {
                    Id = 1,
                    FirstName = "Marko",
                    LastName = "Marković",
                    DateInserted = DateTime.Now
                },
                new Author
                {
                    Id = 2,
                    FirstName = "Janko",
                    LastName = "Janković",
                    DateInserted = DateTime.Now
                });

            modelBuilder.Entity<Book>().HasData(
                new Book
                {
                    Id = 1,
                    Title = "Markova knjiga",
                    DateInserted = DateTime.Now
                },
                new Book
                {
                    Id = 2,
                    Title = "Jankova knjiga",
                    DateInserted = DateTime.Now
                });
        }
    }
}
