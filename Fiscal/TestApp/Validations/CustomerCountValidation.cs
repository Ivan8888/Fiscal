using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TestApp.Services;

namespace TestApp.Validations
{
    public class CustomerCountValidation : ValidationAttribute
    {
        private int max_number;
        public CustomerCountValidation(int max_number)
        {
            this.max_number = max_number;
        }
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            ICustomerCount service = (ICustomerCount)validationContext.GetService(typeof(ICustomerCount));
            int current_customer_number = service.GetCustomerNumber();

            if(max_number > current_customer_number)
            {
                return ValidationResult.Success;
            }

            return new ValidationResult($"Current number of customers is {current_customer_number}, max number of customers is {max_number}");
        }
    }
}
