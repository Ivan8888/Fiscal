using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestApp.Data;
using TestApp.ViewModels;

namespace TestApp.Controllers
{
    public class ProductController : Controller
    {
        public SiteContext _context;
        public IMapper _mapper;
        public ProductController(SiteContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            var products = _context.Products.ToArray();
            var pvm = _mapper.Map<ProductViewModel[]>(products);

            return View(pvm);
        }
    }
}
