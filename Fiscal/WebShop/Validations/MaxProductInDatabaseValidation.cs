using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebShop.Services;

namespace WebShop.Validations
{
    public class MaxProductInDatabaseValidation : ValidationAttribute
    {
        int max_number;
        public MaxProductInDatabaseValidation(int number)
        {
            max_number = number;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            IProductCount service = (IProductCount)validationContext.GetService(typeof(IProductCount));
            int current_number = service.GetProductNumber();
            if(current_number >= max_number)
            {
                return new ValidationResult($"There is {current_number} product in database, max number is: {max_number}");
            }

            return ValidationResult.Success;
        }
    }
}
