using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace WebShop.Validations
{
    public class ForbidenPriceValidation : ValidationAttribute
    {
        int forbiden_price;
        public ForbidenPriceValidation(int price)
        {
            forbiden_price = price;
        }
        public override bool IsValid(object value)
        {
            if ((decimal)value == forbiden_price)
            {
                return false;
            }

            return true;
        }
    }
}
