using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GenericRepositoryPattern.Models
{
    interface IEntity
    {
        public int Id { get; set; }
        public DateTime DateInserted { get; set; }
    }
}
