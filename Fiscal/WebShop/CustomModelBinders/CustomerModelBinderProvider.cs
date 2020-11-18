using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebShop.Models;

namespace WebShop.CustomModelBinders
{
    public class CustomerModelBinderProvider : IModelBinderProvider
    {
        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            if(context.Metadata.ModelType == typeof(Customer))
            {
                return new CustomerModelBinder(); 
            }

            return null;
        }
    }
}
