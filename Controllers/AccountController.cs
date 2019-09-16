using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Wall.App_Start;
using Wall.Helpers;
using Wall.Models;

namespace Wall.Controllers
{
    public class AccountController : Controller
    {
        private ApplicationUserManager _userManager;
        private ApplicationSignInManager _signInManager;


        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }

            private set
            {
                _userManager = value;
            }
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }

            private set
            {
                _signInManager = value;
            }
        }

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }


        [HttpGet]
        [AllowAnonymousOnly]
        public ViewResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymousOnly]
        public async Task<ActionResult> Login(LoginViewModel login)
        {
            if (ModelState.IsValid)
            {
                var result = await SignInManager.PasswordSignInAsync(login.UserName, login.Password, login.RememberMe, shouldLockout: true);

                switch (result)
                {
                    case SignInStatus.Success:
                        var identity = (ClaimsIdentity)User.Identity;
                        AuthenticationManager.SignIn(new AuthenticationProperties()
                        {
                            IsPersistent = login.RememberMe
                        }, identity);
                        return RedirectToAction("Index", "Home");
                    default:
                        break;
                }
            }

            return View(login);
        }

        [HttpGet]
        [AllowAnonymousOnly]
        public ViewResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymousOnly]
        public ActionResult Register(RegisterViewModel register)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = register.UserName,
                    Email = register.Email
                };

                var result = UserManager.Create(user, register.Password);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                }
            }

            return View(register);
        }

        [Authorize]
        public RedirectToRouteResult LogOut()
        {
            HttpContext.GetOwinContext().Authentication.SignOut();

            return RedirectToAction("Login");
        }
    }
}