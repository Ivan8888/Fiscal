using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestApp.Data;

namespace TestApp.Services
{
    public class CustomerCount : ICustomerCount
    {
        public CustomerCount(SiteContext context)
        {
            customer_number = context.Customers.Count();
        }

        private int customer_number;

        public int GetCustomerNumber()
        {
            return customer_number;
        }
        public void IncreaseCustomerNumber()
        {
            customer_number++;
        }
        public void DecreaseCustomerNumber()
        {
            customer_number--;
        }
    }
}
