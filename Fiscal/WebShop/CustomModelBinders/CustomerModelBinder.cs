using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebShop.Models;

namespace WebShop.CustomModelBinders
{
    public class CustomerModelBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if(bindingContext == null)
            {
                throw new ArgumentNullException();
            }

            var value = bindingContext.ValueProvider.GetValue("Value");
            if(value.Length == 0)
            {
                return Task.CompletedTask;
            }

            var split_data = value.FirstValue.Split("|");

            Customer result_customer = new Customer
            {
                CustomerId = Convert.ToInt32(split_data[0]),
                Name = split_data[1],
                Address = split_data[2],
                Email = split_data[3],
                IsRetail = split_data[4] == "1" ? true : false
            };

            bindingContext.Result = ModelBindingResult.Success(result_customer);

            return Task.CompletedTask;
        }
    }
}
