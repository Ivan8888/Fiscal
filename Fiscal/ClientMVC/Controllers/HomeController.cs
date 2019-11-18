using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ClientMVC.Models;
using ClientMVC.Data;

namespace ClientMVC.Controllers
{
    [Route("Cont")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        FiscalContext _context;

        public HomeController(ILogger<HomeController> logger, FiscalContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index(int id)
        {
            return new ContentResult() { Content = string.Format("Name of first product is: {0}", _context.Products.First().Name) };
        }

        [Route("Test/{id}/{value:int}")]
        public IActionResult TestRoute(int id, int value)
        {
            return new ContentResult() { Content = string.Format("Parameter id: {0}", id) };
        }
    }
}
