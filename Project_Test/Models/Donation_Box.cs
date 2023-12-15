using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Project_Test.Models
{
    public class Donation_Box
    {
       [Key]
        public int DonationID { get; set; }
        public string Donor_Location { get; set; }
        public string Donor_Email { get; set; }
        public string Donor_BloodType { get; set; }
        public string Patient_Name { get; set; }
        public string Patient_Age { get; set; }
        public string Patient_Gender { get; set; }
        public string Patient_BloodType { get; set; }
        public string Patient_Hospital { get; set; }
        public string Patient_NationalityNum { get; set; }
        public string Diseases { get; set; }
        public string Rejection_Reason { get; set; }
        public Status Donation_Status { get; set; }

        public enum Status 
        {
            Pending,
            Accepted,
            Rejected
        }
    }
}
