using System.ComponentModel.DataAnnotations;

namespace Project_Test.Models
{
    public class AdminLogin
    {
        [Key]
        public int AdminId { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
