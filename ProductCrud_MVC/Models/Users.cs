using System.ComponentModel.DataAnnotations;

namespace ProductCrud_MVC.Models
{
    public class Users
    {
        [Key]
        public int Uid { get; set; }

        [Required(ErrorMessage ="Please Enter First Name")]
        [Display(Name ="FirstName")]
        public string First_Name { get; set; }

        [Required(ErrorMessage = "Please Enter Last Name")]
        [Display(Name = "LastName")]
        public string Last_Name { get; set; }

        [Required(ErrorMessage = "Please Enter Valid Email")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please Enter Password")]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name ="Confirm Password")]
        [Required(ErrorMessage ="Please Enter Confirm Password")]
        [Compare("Password" , ErrorMessage ="Password doesn't match. Type again !")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Please Enter Your Phone Number")]
        [Display(Name = "Phone Number")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Please Select the Gender")]
        [Display(Name = "Gender")]
        public string Gender { get; set; }

        [Required(ErrorMessage = "Please Enter City Name")]
        [Display(Name = "City")]
        public string City { get; set; }

        [Required(ErrorMessage = "Please Enter State")]
        [Display(Name = "State")]
        public string State { get; set; }

        public int Roleid { get; set; }
    }
    
}
