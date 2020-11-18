using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using TestApp.Validations;

namespace TestApp.Models
{
    [CustomerCountValidation(5)]
    public class Customer
    {
        public int CustomerId { get; set; }
        [Required]
        [StringLength(10)]
        public string Name { get; set; }
        [Required(ErrorMessage = "You must enter the address")]
        [StringLength(30)]
        [Display(Name="CustomerAddress")]
        public string Address { get; set; }
        public string Email { get; set; }
        public bool IsRetail { get; set; }
        public IEnumerable<Invoice> Invoices { get; set; }
    }
}
