using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Project_Test.Models
{
    public class ReciptionBank:IdentityUser
    {
        
        public int HospitalId { get; set; }

        [Required(ErrorMessage = "Enter Hospital Location")]
        [Display(Name = "Hospital Location")]
        public string Location { get; set; }

        [Required(ErrorMessage = "Enter Hospital Name")]
        [Display(Name = "Hospital Name")]
        public string HospitalName { get; set; }
        [Required]
        [Display(Name = "Min Number Units")]
        public int MinUnits { get; set; }
        [Required]
        [Display(Name = "Max Number Units")]
        public int MaxUnits { get; set; }
        [Required]
        [Display(Name = "Current Qty Of Units")]
        public int Current_Qty { get; set; }

    }
}
