using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using FiscalClientMVC.Models;

namespace FiscalClientMVC.Validators
{
    public class IzRetailBasedOnDooInName : ValidationAttribute
    {
        public IzRetailBasedOnDooInName()
        {

        }
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            Customer customer = (Customer)validationContext.ObjectInstance;

            if (customer.Name.ToUpper().Contains("D.O.O") && customer.IsRetail)
            {
                return new ValidationResult($"When Name of customer {customer.Name} contain string [D.O.O] customer is probably not retail!!!");
            }

            return ValidationResult.Success;
        }
    }
}
