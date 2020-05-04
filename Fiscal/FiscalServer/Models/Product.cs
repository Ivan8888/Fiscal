using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FiscalServer.Models
{
    public class Product
    {
        public Product(int id, string name, decimal price)
        {
            ProductId = id;
            Name = name;
            Price = price;
        }

        public Product()
        {}

        public int ProductId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
    }
}
