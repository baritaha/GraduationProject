using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Project_Test.Models
{
    public class BloodStock
    {
        [Key]
        public int BloodStockID { get; set; }
        public int Qty { get; set; }
        public List<Blood> Bloods { get; set; }
        public string BankName { get; set; }


    }
}
