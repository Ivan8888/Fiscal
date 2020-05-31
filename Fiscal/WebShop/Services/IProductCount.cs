using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebShop.Services
{
    public interface IProductCount
    {
        int GetProductNumber();
        void AddProductNumber();

        void RemoveProductNumber();
    }
}
