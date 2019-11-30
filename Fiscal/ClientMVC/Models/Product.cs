using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using ClientMVC.Validators;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClientMVC.Models
{
    [ProductCountValidation(4)]
    public class Product
    {
        public int ProductId { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "Length must be from 5 to 50 characters")]
        [Display(Name ="Product name:")]
        public string Name { get; set; }

        [Range(1000, 2000, ErrorMessage = "Price is invalid")]
        [PriceBasedOnProductName]
        [Column(TypeName = "decimal(18, 4)")]
        [Display(Name = "Product price:")]
        public decimal Price { get; set; }

        public List<InvoiceItem> InvoiceIteams { get; set; }
    }
}
