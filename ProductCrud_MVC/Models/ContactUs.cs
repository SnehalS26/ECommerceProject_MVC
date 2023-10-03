using System.ComponentModel.DataAnnotations;

namespace ProductCrud_MVC.Models
{
    public class ContactUs
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Contact { get; set; }
        [Required]
        public string Message { get; set; }
    }
}
