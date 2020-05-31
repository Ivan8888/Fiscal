using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebShop.Models
{
    public class Invoice
    {
        public int InvoiceId { get; set; }
        public DateTime DateCreated { get; set; }
        public int CustomerId { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual ICollection<InvoiceIteam> InvoiceIteams { get; set; }
    }
}
