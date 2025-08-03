using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PO_Assignment_Project.Data;
using PO_Assignment_Project.Data.Enum;
using PO_Assignment_Project.ViewModels;


namespace PO_Assignment_Project.Controllers
{
    // For admin-only access
   // [Authorize(Roles = "Admin")]

    // For vendor-only access
   // [Authorize(Roles = "Vendor")]

    // For contractor-only access
  //  [Authorize(Roles = "Contractor")]
    public class AccountController : Controller
    {

        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _signInManager = signInManager;
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM registerVM)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var user = new AppUser
                    {
                        UserName = registerVM.Email,
                        Email = registerVM.Email,
                        UserType = registerVM.UserType
                    };

                    var result = await _userManager.CreateAsync(user, registerVM.Password);
                    if (result.Succeeded)
                    {
                        await _userManager.AddToRoleAsync(user, registerVM.UserType.ToString());
                        await _signInManager.SignInAsync(user, isPersistent: false);

                        switch (registerVM.UserType)
                        {
                            case UserType.Admin:
                                return RedirectToAction("Index", "Home");
                            case UserType.Vendor:
                                return RedirectToAction("Index", "Vendor");
                            case UserType.Contractor:
                                return RedirectToAction("Index", "Contractor");
                            default:
                                return RedirectToAction("Index", "Home");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"An error occurred while registering: {ex.Message}");
            }
            return View(registerVM);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginVM loginVM)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var user = await _userManager.FindByEmailAsync(loginVM.Email.ToString());
                    if(user !=null && user.UserType == loginVM.UserType)
                    {
                        var result = await _signInManager.PasswordSignInAsync(user, loginVM.Password, loginVM.RememberMe, lockoutOnFailure: false);
                        if(result.Succeeded)
                        {
                            switch (loginVM.UserType)
                            {
                                case UserType.Admin:
                                    return RedirectToAction("Index", "Admin");
                                case UserType.Vendor:
                                    return RedirectToAction("Index", "Vendor");
                                case UserType.Contractor:
                                    return RedirectToAction("Index", "Contractor");
                                default:
                                    return RedirectToAction("Index", "Home");
                            }
                        }
                    }
                    
                }
                
            }

            catch (Exception ex) {
            
                ModelState.AddModelError("", $"An error occurred while logging in: {ex.Message}");
            }
            return View(loginVM);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            try
            {
                await _signInManager.SignOutAsync();
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"An error occurred while logging out: {ex.Message}");
                return RedirectToAction("Index", "Home");
            }
        }

    }
}
