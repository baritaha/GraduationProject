using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Project_Test.Models
{
    public class Donor:IdentityUser
    {
       
        public int DonorId { get;  set; }
      
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Age { get; set; }
        public string Gender { get; set; }
        public string BloodType { get; set; }
        public string Address { get; set; }
        public string NationalityNum { get; set; }
        //public Donation_Box donation { get; set; }
    }
}
