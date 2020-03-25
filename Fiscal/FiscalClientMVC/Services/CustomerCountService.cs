using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FiscalClientMVC.Data;

namespace FiscalClientMVC.Services
{
    public class CustomerCountService : ICustomerCount
    {
        private int _number;
      
        public CustomerCountService(FiscalContext context)
        {
            _number = context.Customers.Count();
        }

        public void AddCustomerNumber()
        {
            _number++;
        }

        public int GetCustomerNumber()
        {
            return _number;
        }
    }
}
