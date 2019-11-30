using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using ClientMVC.Services;

namespace ClientMVC.Validators
{
    public class ProductCountValidation : ValidationAttribute
    {
        private int _number;
        public ProductCountValidation(int number)
        {
            _number = number;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            IProductNumber service = (IProductNumber)validationContext.GetService(typeof(IProductNumber));
            int current_number = service.GetProductCount();

            if(current_number >= _number)
            {
                return new ValidationResult(String.Format("There is alreday {0} product in database, max number is {1}", current_number, _number));
            }

            return ValidationResult.Success;
        }
    }
}
