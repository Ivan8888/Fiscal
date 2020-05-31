using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebShop.Validations;

namespace WebShop.Models
{
    [MaxProductInDatabaseValidation(4)]
    public class Product
    {
        public int ProductId { get; set; }
        [Required]
        [StringLength(10)]
        [Display(Name="Product name")]
        public string Name { get; set; }
        [Range(100, 2000)]
        [Display(Name="Product price")]
        [ForbidenPriceValidation(220, ErrorMessage = "Price is forbiden")]
        public decimal Price { get; set; }
        public virtual ICollection<InvoiceIteam> invoiceIteams { get; set; }

        public decimal GetPriceWithTax(decimal tax_percent)
        {
            return Price + (Price / 100 * tax_percent);
        }
    }
}
