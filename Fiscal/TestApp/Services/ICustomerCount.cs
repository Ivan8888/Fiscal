using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestApp.Services
{
    public interface ICustomerCount
    {
        int GetCustomerNumber();
        void IncreaseCustomerNumber();
        void DecreaseCustomerNumber();
    }
}
