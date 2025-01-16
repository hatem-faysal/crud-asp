
using crud2.Data;
using crud2.Data.ViewModels;
using crud2.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace crud2.Controllers
{
    public class AccountController : Controller
    {
        private readonly EcommerceDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountController(
            EcommerceDbContext context,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public IActionResult Login()
        {
            var Result = new LoginVM();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginVM model)
        {
            if(ModelState.IsValid)  return View(model);
            var user = await _userManager.FindByEmailAsync(model.EmailAddress);
                    Console.WriteLine(user);
            if (user != null)
            {
                //Check Password
                var passwordCheck = await _userManager.CheckPasswordAsync(user, model.Password);
                    Console.WriteLine("out line");
                if(passwordCheck)
                {
                    Console.WriteLine("in passwordCheck");
                    var Result = await _signInManager.PasswordSignInAsync(user, model.Password, false, false);
                    if(Result.Succeeded)
                    {
                        return RedirectToAction("Index", "Products");
                    }
                }
                return View(model);
            }
            return View(model);
        }
    }

}
