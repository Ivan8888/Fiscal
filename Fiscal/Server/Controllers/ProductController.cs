using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Server.Models;
using Server.Data;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        FiscalContext _context;
        public ProductController(FiscalContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("[action]")]
        public ActionResult<List<Product>> GetAll()
        {
            

            var result = from p in _context.Products
                         select p;
            //var result = _context.Products.ToList();
            return result.ToList();
        }

        [HttpGet]
        [Route("[action]/{id}")]
        public ActionResult<Product> GetById(int id)
        {
            var result = (from p in _context.Products
                          where p.ProductId == id
                          select p).SingleOrDefault();
            //var result = _context.Products.SingleOrDefault(p => p.ProductId == id);
            return result;
        }
    }
}