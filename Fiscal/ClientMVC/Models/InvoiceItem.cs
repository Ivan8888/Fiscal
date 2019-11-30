using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClientMVC.Models
{
    public class InvoiceItem
    {
        public int InvoiceItemId { get; set; }
        public int InvoiceId { get; set; }
        public int ProductID { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal Quantity { get; set; }
        public Invoice Invoice { get; set; }
        public Product Product { get; set; }
    }
}
