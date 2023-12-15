using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Project_Test.Models.ViewModels
{
    public class RegisterDonorViewModel
    {
        [Required]
        [Display(Name = "First Name")]
        [MinLength(5, ErrorMessage = "the Minimum Characters is 5")]
        [MaxLength(15, ErrorMessage = "the Maximum Characters is 15")]
        public string FirstName { get; set; }
        [Required]
        [Display(Name = "Last Name")]
        [MinLength(2, ErrorMessage = "the Minimum Characters is 2")]
        [MaxLength(20, ErrorMessage = "the Maximum Characters is 8")]
        public string LastName { get; set; }
        public string UserName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        [Display(Name = "Age")]
        [MinLength(2, ErrorMessage = "Enter 2 numbers for Age")]
        [MaxLength(2, ErrorMessage = "Enter 2 numbers for Age")]
        public string Age { get; set; }
        [Required]
        [Display(Name = "Gender")]
        [MinLength(4, ErrorMessage = "the Minimum Characters is 4 (Male/FelMale)")]
        [MaxLength(6, ErrorMessage = "the Maximum Characters is 6 (Male/FelMale)")]
        public string Gender { get; set; }
        [Required]
        [Display(Name = "Blood Type")]
        public string BloodType { get; set; }
        public string Address { get; set; }
        [Required]
        [Display(Name = "Nationality Number")]
        [MinLength(10, ErrorMessage = "Enter 10 Numbers for Nationality Number")]
        [MaxLength(10, ErrorMessage = "Enter 10 Numbers for Nationality Number")]
        public string NationalityNum { get; set; }
        [Required]
        [Display(Name = "Phone Number")]
        [MinLength(10, ErrorMessage = "Phone Number Must Contain 10 Numbers")]
        [MaxLength(10, ErrorMessage = "Phone Number Must Contain 10 Numbers")]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }
        [Required]
        [Display(Name = "Password")]
        [MinLength(8, ErrorMessage = "the Minimum number of Passsword is 8 (Contain number ,characters,sympoles...etc")]
        [MaxLength(20, ErrorMessage = "the Maximum number of Passsword is 12 (Contain number ,characters,sympoles...etc")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [Compare("Password", ErrorMessage = "Password Not Match")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}
