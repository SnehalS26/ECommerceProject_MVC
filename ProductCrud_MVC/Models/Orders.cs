using System.ComponentModel.DataAnnotations;

namespace ProductCrud_MVC.Models
{
    public class Orders
    {
        [Key]
        public int Oid { get; set; }
        public int Prodid { get; set; }
        public decimal Price { get; set; }
        public int Uid { get; set; }
        public int Quantity { get; set; }
    }
}
