using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Project_Test.Models.ViewModels
{
    public class Login_Driver_ViewModel
    {
        [Required]
        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [Display(Name = "Password")]
        [MinLength(8, ErrorMessage = "the Minimum number of Passsword is 8 (Contain number ,characters,sympoles...etc")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
