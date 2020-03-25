using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using FiscalClientMVC.Services;


namespace FiscalClientMVC.Validators
{
    public class CountCustomerValidation : ValidationAttribute
    {
        int max_number;
        int current_number;
        public CountCustomerValidation(int number)
        {
            max_number = number;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            ICustomerCount customerCount = (ICustomerCount)validationContext.GetService(typeof(ICustomerCount));
            current_number = customerCount.GetCustomerNumber();

            if(current_number >= max_number)
            {
                return new ValidationResult($"Can't insert customer, current number is: {current_number} max number: {max_number}");
            }

            return ValidationResult.Success;
        }
    }
}
