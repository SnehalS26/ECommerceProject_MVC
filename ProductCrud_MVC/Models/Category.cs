using System.ComponentModel.DataAnnotations;

namespace ProductCrud_MVC.Models
{
    public class Category
    {
        [Key]
        public int Cid { get; set; }
        [Required]
        public string Cname { get; set; }
    }
}
