using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using ClientMVC.Models;

namespace ClientMVC.Validators
{
    public class PriceBasedOnProductName : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            Product product = (Product)validationContext.ObjectInstance;

            if(product.Name.ToLower() == "rakija" && product.Price > 1500)
            {
                return new ValidationResult("Rakija can't be more then 1500");
            }

            return ValidationResult.Success;
        }
    }
}
