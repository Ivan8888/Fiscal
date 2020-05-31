using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FiscalServer.Models;
using FiscalServer.Data;

namespace FiscalServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [FormatFilter]
    public class ProductController : ControllerBase
    {
        FiscalContext _context;
        public ProductController(FiscalContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<List<Product>> Index()
        {
            return _context.Products.ToList();
        }

        [HttpGet("[action]/{id}.{format?}")]
        //[HttpGet]
        //[Produces("application/xml")]
        //[Route("[action]/{id}")]
        public ActionResult<Product> GetById(int id)
        {
            Product product = _context.Products.SingleOrDefault(p => p.ProductId == id);
            return product;
        }

        [HttpGet("[action]/{id}")]
        public ActionResult<string> Get(string id)
        {
            if (id == "1")
                return NotFound();

            return "ivan";
        }


    }
}