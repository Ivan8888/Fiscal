using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using FiscalClientMVC.Validators;

namespace FiscalClientMVC.Models
{
    [IzRetailBasedOnDooInName]
    [CountCustomerValidation(4)]
    public class Customer
    {
        [Display(Name = "ID")]
        public int CustomerId { get; set; }

        [Required(ErrorMessage = "Name of customer must be inserted")]
        [StringLength(30, MinimumLength = 4)]
        [Display(Name = "Customer Name")]
        [DataType(DataType.MultilineText)]
        public string Name { get; set; }

        [Display(Name = "Email Address")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Address must be inserted")]
        [StringLength(30)]
        [Display(Name = "Home Address")]
        public string Address { get; set; }

        [Display(Name = "Is Retail")]
        public bool IsRetail { get; set; }
        public List<Invoice> Invoices { get; set; }
    }
}
