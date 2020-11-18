using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebShop.CustomModelBinders;

namespace WebShop.Models
{
    //[ModelBinder(BinderType = typeof(CustomerModelBinder))]
    public class Customer
    {
        public int CustomerId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public bool IsRetail { get; set; }
        public virtual ICollection<Invoice> Invoices { get; set; }
    }
}
