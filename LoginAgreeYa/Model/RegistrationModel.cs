using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace LoginAgreeYa.Model
{
    public class RegistrationModel : IdentityUser
    {
       
        public string?  FirstName { set; get; }
        [RegularExpression(@"^[A-Z][a-z]{2,}$", ErrorMessage = "Please enter a valid Last Name")]
        public string? LastName { set; get; }
      
        [RegularExpression(@"^[a-z0-9_-]{3,15}$", ErrorMessage = "Please enter a valid user Name")]
        public string? LoginUser { set; get; }
        [RegularExpression(@"^.*(?=.*[A-Z])*(?=.*[0-9])*(?=.*[a-z])*(?=.*[!@#$%^&*_+]{1})(.{8,})$", ErrorMessage = "Please enter a valid password")]
        public string Password { set; get; }

    }
    public class Registration
    {
        [Key]
        public int Id { set; get; }
        [RegularExpression(@"^[A-Z][a-z]{2,}$", ErrorMessage = "Please enter a valid first Name")]
        public string? FirstName { set; get; }
        [RegularExpression(@"^[A-Z][a-z]{2,}$", ErrorMessage = "Please enter a valid Last Name")]
        public string? LastName { set; get; }
        [RegularExpression(@"^.*(?=.*[A-Z])*(?=.*[0-9])*(?=.*[a-z])*(?=.*[!@#$%^&*_+]{1})(.{8,})$", ErrorMessage = "Please enter a valid password")]
        public string? Password { set; get; }
        [RegularExpression(@"^[a-z0-9_-]{3,15}$", ErrorMessage = "Please enter a valid user Name")]
        public string? LoginUser { set; get; }
        [Required]
        [EmailAddress]
        public string? Email { set; get; }
        [RegularExpression(@"^[6-9]\d{9}$", ErrorMessage = "Please enter a valid Mobile Number")]
        public string? PhoneNumber { set; get; }

    }
    public class RegisterViewModel
    {
        public Registration Registration { set; get; }
        public string role { set; get; }
    }
}
