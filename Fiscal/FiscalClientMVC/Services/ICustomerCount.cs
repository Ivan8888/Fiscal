using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FiscalClientMVC.Services
{
    public interface ICustomerCount
    {
        int GetCustomerNumber();
        void AddCustomerNumber();
    }
}
