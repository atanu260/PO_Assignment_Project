using PO_Assignment_Project.Data.Enum;
using System.ComponentModel.DataAnnotations;

namespace PO_Assignment_Project.ViewModels
{
    public class RegisterVM
    {
        [EmailAddress]
        [Required(ErrorMessage = "Email is required.")]
        
        public string Email { get; set; }
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Password is required.")]
        public string Password { get; set; }
        [Compare("Password", ErrorMessage = "Passwords do not match.")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
        [Required]
        public UserType UserType { get; set; }
    }
}
