using PO_Assignment_Project.Data.Enum;
using System.ComponentModel.DataAnnotations;

namespace PO_Assignment_Project.ViewModels
{
    public class LoginVM
    {
        [EmailAddress]
        [Required]
        public string Email { get; set; }
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Password is required.")]
        public string Password { get; set; }
        [Required]
        public UserType UserType { get; set; }
        public bool RememberMe { get; set; }
    }
}
