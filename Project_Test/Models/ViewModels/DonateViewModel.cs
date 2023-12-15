using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Project_Test.Models.ViewModels
{
    public class DonateViewModel
    {
        public List<ReciptionBank> BloodBanks { get; set; }
        [Required]
        [Display(Name = "Which blood bank will you donate from?")]
        public string Donor_Location { get; set; }
        [Required]
        [Display(Name ="Please Enter Patient Name")]
        public string Patient_Name { get; set; }
        [Required]
        [Display(Name = "Please Enter Patient Age")]
        public string Patient_Age { get; set; }
        [Required]
        [Display(Name = "Please Enter Patient Gender")]
        public string Patient_Gender { get; set; }
        [Required]
        [Display(Name = "Please Enter Patient Blood Type")]
        public string Patient_BloodType { get; set; }
        [Required]
        [Display(Name = "Please Enter Patient Hospital")]
        public string Patient_Hospital { get; set; }
        [Required]
        [Display(Name = "Please Enter Patient Nationality Number")]
        public string Patient_NationalityNum { get; set; }
        [Required]
        [Display(Name = "Do you have any diseases?")]
        public string Diseases { get; set; }

    }
}
