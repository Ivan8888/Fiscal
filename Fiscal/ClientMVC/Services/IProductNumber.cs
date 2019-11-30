using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClientMVC.Services
{
    public interface IProductNumber
    {
        public void AddProductCount();
        public int GetProductCount();
    }
}
