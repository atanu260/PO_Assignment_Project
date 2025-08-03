using Microsoft.AspNetCore.Identity;
using PO_Assignment_Project.Data.Enum;

namespace PO_Assignment_Project.Data
{
    public class AppUser:IdentityUser
    {
        public UserType UserType {  get; set; }
    }
}
