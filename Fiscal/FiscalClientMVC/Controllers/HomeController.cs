using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace FiscalClientMVC.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            const string VISIT_KEY = "VISIT_KEY";

            int? numVisit = HttpContext.Session.GetInt32(VISIT_KEY);
            //int? numVisit = (int?)TempData[VISIT_KEY];
            if (numVisit.HasValue)
            {
                numVisit++;
            }
            else
            {
                numVisit = 1;
            }

            //TempData[VISIT_KEY] = numVisit;
            HttpContext.Session.SetInt32(VISIT_KEY, numVisit.Value);
            ViewBag.NumVisit = numVisit;

            return View();
        }
    }
}