using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace TestApp.Data
{
    public class SiteContext : IdentityDbContext<AppUser>
    {
        public SiteContext(DbContextOptions<SiteContext> options) : base(options) {}

        public DbSet<Product> Products { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<InvoiceItem> InvoiceItems { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    ProductId = 1,
                    Name = "Beer",
                    Price = 100,
                    Description = "",
                    SupplierId = 1
                },
                new Product
                {
                    ProductId = 2,
                    Name = "Vodka",
                    Price = 500,
                    Description = "",
                    SupplierId = 1
                },
                new Product
                {
                    ProductId = 3,
                    Name = "Tequila",
                    Price = 1200,
                    Description = "",
                    SupplierId = 2
                }
            );

            modelBuilder.Entity<Customer>().HasData(
                new Customer
                {
                    CustomerId = 1,
                    Name = "Beer pub",
                    Address = "Beer pub address",
                    Email = "beerpub@gmail.com",
                    IsRetail = false
                },
                new Customer
                {
                    CustomerId = 2,
                    Name = "Ivan",
                    Address = "Ivan Address",
                    Email = "ivan@gmail.com",
                    IsRetail = true
                }
            );

            modelBuilder.Entity<Supplier>().HasData(
                new Supplier
                {
                    SupplierId = 1,
                    Name = "DrinkStore",
                    Address = "Drink Store Address",
                    Email = "drinkstore@gmail.com",
                    PhoneNumber = "011/1111-11111"
                },
                new Supplier
                {
                    SupplierId = 2,
                    Name = "E Diskont",
                    Address = "E diskont Address",
                    Email = "ediskont@gmail.com",
                    PhoneNumber = "011/2222-22222"
                }
            );

            modelBuilder.Entity<Invoice>().HasData(
                new Invoice
                {
                    InvoiceId = 1,
                    CustomerId = 1,
                    DateCreated = DateTime.Today,
                },
                new Invoice
                {
                    InvoiceId = 2,
                    CustomerId = 2,
                    DateCreated = DateTime.Today,
                }
            );

            modelBuilder.Entity<InvoiceItem>().HasData(
                new InvoiceItem
                {
                    InvoiceItemId = 1,
                    InvoiceId = 1,
                    ProductId = 1,
                    Quantity = 10
                },
                new InvoiceItem
                {
                    InvoiceItemId = 2,
                    InvoiceId = 1,
                    ProductId = 2,
                    Quantity = 2
                },
                new InvoiceItem
                {
                    InvoiceItemId = 3,
                    InvoiceId = 2,
                    ProductId = 1,
                    Quantity = 20
                },
                new InvoiceItem
                {
                    InvoiceItemId = 4,
                    InvoiceId = 2,
                    ProductId = 3,
                    Quantity = 3
                }
            );

            //Fluent API
            modelBuilder.Entity<Product>().ToTable("ProductTable");
            //fluent API validation, important with this validation is created just on storage, not on server object(entity) for validation 
            //on entity we need to use validation data anotation or ....
            //this kind of fluent API of course can't be used with view models
            //modelBuilder.Entity<Supplier>()
            //    .Property(p => p.Name)
            //    .IsRequired()
            //    .HasMaxLength(15);

            base.OnModelCreating(modelBuilder);
        }
    }
}
