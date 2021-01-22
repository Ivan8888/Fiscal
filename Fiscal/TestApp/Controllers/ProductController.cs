using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestApp.Data;
using TestApp.Models;
using TestApp.ViewModels;

namespace TestApp.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        public SiteContext _context;
        public IMapper _mapper;
        public ProductController(SiteContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var products = _context.Products.ToArray();
            var pvm = _mapper.Map<ProductViewModel[]>(products);

            return Ok(pvm);
        }

        [HttpGet("{id:int}")]
        public IActionResult Get(int id)
        {
            var product = _context.Products.SingleOrDefault(p => p.ProductId == id);
            if(product == null)
            {
                return NotFound();
            }

            var pvm = _mapper.Map<ProductViewModel>(product);
            return Ok(pvm);
        }

        [HttpPost]
        public IActionResult Post(ProductViewModel model)
        {
            //[api controller] atrubute change model binding source, from body for complex type and from route from simple types made default
            //[api controller] atrubute implement mehanizam that return 400 and all model state error wihtout ModelState.IsValid code part

            var product = _mapper.Map<Product>(model);
            var sup = _context.Suppliers.SingleOrDefault(s => s.SupplierId == model.SupplierId);
            if(sup == null)
            {
                return BadRequest($"Supplier with id: {model.SupplierId} doesn't exists!");
            }

            _context.Add(product);
            if(_context.SaveChanges() > 0)
            {
                return CreatedAtAction("Get", new { id = product.ProductId }, _mapper.Map<ProductViewModel>(product));
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPut]
        public IActionResult Put(int id, [FromBody]ProductViewModel model)
        {
            var old_product = _context.Products.SingleOrDefault(p => p.ProductId == id);
            if(old_product == null)
            {
                return BadRequest($"Product with id: {id} not exists");
            }

            var sup = _context.Suppliers.SingleOrDefault(s => s.SupplierId == model.SupplierId);
            if(sup == null)
            {
                return BadRequest($"Supplier with id: {model.SupplierId} not exists");
            }

            _mapper.Map(model, old_product);
            if(_context.SaveChanges() > 0)
            {
                return Ok(_mapper.Map<ProductViewModel>(old_product));
            }
            else
            {
                return BadRequest("Failed to save product");
            }
        }
    }
}
