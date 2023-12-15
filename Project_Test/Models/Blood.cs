using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Project_Test.Models
{
    public class Blood
    {
        [Key]
        public int BloodID { get; set; }
        public String Type { get; set; }
        public String Code { get; set; }
        public DateTime Start_Date { get; set; }
        public DateTime Expire_Date { get; set; }


    }
}
