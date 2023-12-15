using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Project_Test.Models.ViewModels
{
    public class LoginBankViewModel
    {
        [Required(ErrorMessage = "Enter Hospital Name")]
        [Display(Name = "Hospital Name")]
        public string HospitalName { get; set; }
        [Required]
        [Display(Name = "Password")]
        [MinLength(8, ErrorMessage = "the Minimum number of Passsword is 8 (Contain number ,characters,sympoles...etc")]
        [MaxLength(20, ErrorMessage = "the Maximum number of Passsword is 12 (Contain number ,characters,sympoles...etc")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
