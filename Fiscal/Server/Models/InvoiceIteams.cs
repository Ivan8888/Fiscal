using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Models
{
    public class InvoiceIteam
    {
        public int InvoiceIteamsId { get; set; }
        public int InvoiceId { get; set; }
        public int ProductID { get; set; }
        public decimal Quantity { get; set; }
        public virtual Invoice Invoice { get; set; }
        public virtual Product Product { get; set; }
    }
}
