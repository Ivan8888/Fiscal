using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClientMVC.Models
{
    public class Customer
    {
        public int CustomerId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public List<Invoice> Invoices { get; set; }
    }
}
