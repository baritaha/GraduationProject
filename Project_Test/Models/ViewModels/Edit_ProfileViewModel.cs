using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Project_Test.Models.ViewModels
{
    public class Edit_ProfileViewModel
    {
        [Required]
        [Display(Name = "First Name")]
        [MinLength(2, ErrorMessage = "the Minimum Characters is 2")]
        [MaxLength(20, ErrorMessage = "the Maximum Characters is 8")]
        public string FirstName { get; set; }
        [Required]
        [Display(Name = "Last Name")]
        [MinLength(2, ErrorMessage = "the Minimum Characters is 2")]
        [MaxLength(20, ErrorMessage = "the Maximum Characters is 8")]
        public string LastName { get; set; }
        public string UserName { get; set; }

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
        [MinLength(1, ErrorMessage = "the Minimum Characters is 2(i.e A+,A,O....etc)")]
        [MaxLength(2, ErrorMessage = "the Maximum Characters is 2(i.e A+,A,O....etc)")]
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
    }
}
