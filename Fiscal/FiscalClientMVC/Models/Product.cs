using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FiscalClientMVC.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public List<InvoiceIteam> InvoiceIteams { get; set; }
    }
}

