using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestApp.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public IEnumerable<InvoiceItem> InvoiceItems { get; set; }
        public int SupplierId { get; set; }
        public Supplier Supplier { get; set; }
    }
}
