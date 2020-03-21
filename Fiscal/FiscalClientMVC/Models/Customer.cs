using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace FiscalClientMVC.Models
{
    public class Customer
    {
        [Display(Name = "ID")]
        public int CustomerId { get; set; }

        [Display(Name = "Customer Name")]
        [DataType(DataType.MultilineText)]
        public string Name { get; set; }

        [Display(Name = "Email Address")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Display(Name = "Home Address")]
        public string Address { get; set; }

        [Display(Name = "Is Retail")]
        public bool IsRetail { get; set; }
        public List<Invoice> Invoices { get; set; }
    }
}
