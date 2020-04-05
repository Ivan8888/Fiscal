using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FiscalClientMVC.ViewModels
{
    public class CreateRoleViewModel
    {
        [Required]
        [StringLength(13, MinimumLength = 3)]
        public string RoleName { get; set; }
    }
}
