using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Server.Models;
using Microsoft.AspNetCore.Hosting;

namespace Server.Controllers
{
    [Route("api/[controller]/[action]")]
    public class TestController : Controller
    {
        IWebHostEnvironment _environment;

        public TestController(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        [AcceptVerbs("Get")]
        public IActionResult GetAll()
        {
            return Content("GettAll");
        }

        [HttpPost]
        [ActionName("GetAll")]
        public IActionResult InsertProduct(/*Product product*/)
        {
            return Content("InsertProduct");
        }

        [HttpPut]
        public IActionResult UpdateProduct(/*int id, Product product*/)
        {
            return Content("UpdateProduct");
        }

        [HttpDelete]
        public IActionResult DeleteProduct()
        {
            return Content("DeleteProduct");
        }
    }
}