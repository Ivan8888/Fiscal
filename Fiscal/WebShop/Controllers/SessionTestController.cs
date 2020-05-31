using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebShop.Controllers
{
    public class SessionTestController : Controller
    {

        public IActionResult Index()
        {
            //return NotFound();
            //throw new Exception();

            const string key = "visit_count";

            int? num_times = HttpContext.Session.GetInt32(key);
            if (num_times.HasValue)
            {
                num_times++;
            }
            else
            {
                num_times = 1;
            }

            HttpContext.Session.SetInt32(key, num_times.Value);
            ViewData["num_visit"] = num_times;

            return View();
        }
    }
}