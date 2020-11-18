using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace TestApp.Controllers
{
    public class HomeController : Controller
    {
        const string num_visit_key = "num_visit";

        public IActionResult Index()
        {
            int? count = HttpContext.Session.GetInt32(num_visit_key);
            if (count.HasValue)
            {
                count++;
            }
            else
            {
                count = 1;
            }

            HttpContext.Session.SetInt32(num_visit_key, count.Value);
            ViewBag.visit = count;

            return View();
        }
    }
}
