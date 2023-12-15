using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Project_Test.Models.Role
{
    public class EditRoleViewModel
    {
        public EditRoleViewModel()
        {
            UsersNames = new List<string>();
        }

        public string RoleID { get; set; }
        [Required]
        public string RoleName { get; set; }
        public List<string> UsersNames { get; set; }
    }
}
