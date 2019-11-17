using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ClientMVC.Models;

namespace ClientMVC.Controllers
{
    [Route("Cont")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index(int id)
        {
            return new ContentResult() { Content = string.Format("Index method. ID:{0}", id) };
        }

        [Route("Test/{id}/{value:int}")]
        public IActionResult TestRoute(int id, int value)
        {
            return new ContentResult() { Content = string.Format("Parameter id: {0}", id) };
        }
    }
}
