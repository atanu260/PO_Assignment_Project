using PO_Assignment_Project.Data.Enum;
using System.ComponentModel.DataAnnotations;

namespace PO_Assignment_Project.Models
{
    public class Register
    {
        public int Id { get; set; }
        public string? UserName { get; set; }
        [DataType(DataType.Password)]
        public string? Password { get; set; }
        [DataType(DataType.Password),Compare(nameof(Password))]
        public string? ConfirmPassword { get; set; }
        public UserType UserType { get; set; }

    }
}
