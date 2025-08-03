using PO_Assignment_Project.Data.Enum;
using System.ComponentModel.DataAnnotations;

namespace PO_Assignment_Project.Models
{
    public class Login
    {
        public string UserName {  get; set; }
        [DataType(DataType.Password)]
        public string? Password { get; set; }
        public UserType UserType { get; set; }
    }
}
