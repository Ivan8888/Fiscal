using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClientMVC.Data;

namespace ClientMVC.Services
{
    public class ProductNumber : IProductNumber
    {
        private int Number { get; set; }
        private FiscalContext _context;

        public ProductNumber(FiscalContext context)
        {
            _context = context;
            Number = _context.Products.Count();
        }

        public void AddProductCount()
        {
            Number++;
        }

        public int GetProductCount()
        {
            return Number;
        }
    }
}
