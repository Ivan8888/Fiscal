using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestApp.Data;
using TestApp.Models;
using TestApp.ViewModels;

namespace TestApp.Controllers
{
    public class SupplierController : Controller
    {
        SiteContext _context;
        IMapper _mapper;
        public SupplierController(SiteContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            var sup = _context.Suppliers.ToArray();
            var result = _mapper.Map<SupplierViewModel[]>(sup);
            
            return View(result);
        }

        public IActionResult Details(int id)
        {
            var sup = _context.Suppliers
                .Include(s => s.Products)
                .SingleOrDefault(s => s.SupplierId == id);
            var svm = _mapper.Map<SupplierViewModel>(sup);

            return View(svm);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var s = _context.Suppliers.SingleOrDefault(s => s.SupplierId == id);
            var res = _mapper.Map<SupplierViewModel>(s);

            return View(res);
        }

        [HttpPost]
        public IActionResult Edit(int id, SupplierViewModel model)
        {
            Supplier supplier_old = _context.Suppliers.SingleOrDefault(s => s.SupplierId == id);
            if(supplier_old != null)
            {
                _mapper.Map(model, supplier_old);

                int num = _context.SaveChanges();
                if (num > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
            }

            return View(model);
        }
    }
}
