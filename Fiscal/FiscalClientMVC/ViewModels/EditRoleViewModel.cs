using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FiscalClientMVC.ViewModels
{
    public class EditRoleViewModel
    {
        public EditRoleViewModel()
        {
            Users = new List<string>();
        }

        [Required]
        public string RoleId { get; set; }
        [Required]
        [StringLength(13, MinimumLength = 3)]
        public string RoleName { get; set; }
        public List<string> Users { get; set; }
    }
}
