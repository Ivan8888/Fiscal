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
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        IRepository<Author> _repository;

        public AuthorController(IRepository<Author> repository)
        {
            _repository = repository;
        }

        public IEnumerable<Author> GetAll()
        {
            return _repository.GetAll();
        }

        public Author GetById(int id)
        {
            return _repository.GetById(id);
        }

        [HttpPost]
        public void Insert(Author author)
        {
            _repository.Insert(author);
        }

        [HttpPost]
        public void Update(Author author)
        {
            _repository.Update(author);
        }

        [HttpPost]
        public void Delete(int id)
        {
            _repository.Delete(id);
        }
    }
}