using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClientMVC.Models
{
    public class Invoice
    {
        public int InvoiceId { get; set; }
        public int CustomerId { get; set; }
        public DateTime DateCreated { get; set; }
        public Customer Customer { get; set; }
        public List<InvoiceItem> InvoiceItems { get; set; }
    }
}
