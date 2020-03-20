using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FiscalClientMVC.Models
{
    public class InvoiceIteam
    {
        public int InvoiceIteamId { get; set; }
        public int InvoiceId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public Invoice Invoice { get; set; }
        public Product Product { get; set; }
    }
}
