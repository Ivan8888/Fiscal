using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ClientMVC.Models
{
    public class Customer
    {
        public int CustomerId { get; set; }

        [Display(Name="Customer name")]
        [Required(ErrorMessage = "Name must be entered, it is required")]
        public string Name { get; set; }

        [StringLength(20, MinimumLength = 10)]
        public string Address { get; set; }

        [StringLength(10, MinimumLength =5)]
        public string Email { get; set; }
        public List<Invoice> Invoices { get; set; }
    }
}
