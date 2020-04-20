using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace FiscalClientMVC.Models
{
    public class Product
    {
        public int ProductId { get; set; }

        [Display(Name = "Product name:")]
        public string Name { get; set; }

        [Display(Name = "Product price")]
        public decimal Price { get; set; }
        public List<InvoiceIteam> InvoiceIteams { get; set; }
    }
}

