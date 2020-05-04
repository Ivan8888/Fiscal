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
    [ApiController]
    public class TestController : ControllerBase
    {
        IWebHostEnvironment _environment;
        public TestController(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        public class Person
        {
           public string Name { get; set; }
        }

        [HttpGet]
        //[HttpGet]
        public IActionResult GetProduct(Person person)
        {
            return Content($"GetProduct: name: {person.Name}");
        }

        [AcceptVerbs("Get")]
        public IActionResult GetAll()
        {
            return Content("GettAll");
        }

        [HttpPost("dodajjosovo")]
        [ActionName("GlupaAkcija")]
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