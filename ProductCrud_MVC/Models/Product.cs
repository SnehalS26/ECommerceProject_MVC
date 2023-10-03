using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace ProductCrud_MVC.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        public double Price { get; set; }       
        public string? Imageurl { get; set; }
        [Required]
        [Display(Name = "Category id")]
        public int Cid { get; set; }

        [Display(Name = "Category name")]
        public string Cname { get; set; }
        [NotMapped]
        public int Quantity { get; set; }
        [NotMapped]
        public int Cartid { get; set; }
        [NotMapped]
        public DateTime Date { get; set; }
        [NotMapped]
        public int Oid { get; set; }
    }
}
