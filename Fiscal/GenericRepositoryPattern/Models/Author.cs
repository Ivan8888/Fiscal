using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GenericRepositoryPattern.Models {
    public class Author : BaseEntity {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public virtual IEnumerable<Book> Books { get; set; }
    }
}
