using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using FiscalServer.Models;

namespace FiscalServer.Data
{
    public class FiscalContext : DbContext
    {

        public FiscalContext(DbContextOptions<FiscalContext> options) : base(options)
        {

        }

        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasData(
                new Product(1, "Pivo", 100),
                new Product(2, "Rakija", 500),
                new Product(3, "Vinjak", 700)
            );

            base.OnModelCreating(modelBuilder);
        }
    }
}
