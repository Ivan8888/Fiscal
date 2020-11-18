using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GenericRepositoryPattern.Models;
using GenericRepositoryPattern.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GenericRepositoryPattern.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        IRepository<Book> _repository;
        public BookController(IRepository<Book> repository)
        {
            _repository = repository;
        }

        public IEnumerable<Book> GetAll()
        {
            return _repository.GetAll();
        }

        public Book GetById(int id)
        {
            return _repository.GetById(id);
        }

        [HttpPost]
        public void Insert(Book book)
        {
            _repository.Insert(book);
        }

        [HttpPost]
        public void Update(Book book)
        {
            _repository.Update(book);
        }

        [HttpPost]
        public void Delete(int id)
        {
            _repository.Delete(id);
        }
    }
}