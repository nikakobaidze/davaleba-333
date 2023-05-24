using davaleba_333.Models;
using davaleba_333.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace davaleba_333.Controllers
{
    public class LoginController : Controller
    {
        private readonly Microsoft.AspNetCore.Identity.UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        public LoginController(Microsoft.AspNetCore.Identity.UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> GetUserByUsername(string UserName)
        {
            var user = await _userManager.FindByNameAsync(UserName);
            return View(user);

        }
        [HttpGet]
        public IActionResult Login()
        {
            return View(new UserFormViewModel());
        }


        [HttpPost]
        public async Task<IActionResult> Login(UserFormViewModel model, string returnurl=null)
        {
            returnurl ??=Url.Content("/");
            var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password,false,lockoutOnFailure:false);
            if (result.Succeeded)
            {
                return LocalRedirect(returnurl);
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View(new RegisterViewModel());
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model, string returnurl = null)
        {
            returnurl ??= Url.Content("/");
            var user = new User
            {
                UserName = model.UserName,
            };
            var result=await _userManager.CreateAsync(user,model.Password);
            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, isPersistent:false);
                return LocalRedirect(returnurl);
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(HomeController.Index),"Home");
        }
    }
}
