using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestApp.Models;

namespace TestApp.ViewModels
{
    public class SupplierViewModel
    {
        public int SupplierId { get; set; }
        public string Name { get; set; }
        public string OfficeAddress { get; set; }
        public string Email { get; set; }
        public IEnumerable<ProductViewModel> Products { get; set; }
    }
}
