using System.ComponentModel.DataAnnotations;

namespace ProductCrud_MVC.Models
{
    public class Roles
    {
        [Key]
        public int Roleid { get; set; }
        [Required]
        public string Rolename { get; set; }
    }
}
