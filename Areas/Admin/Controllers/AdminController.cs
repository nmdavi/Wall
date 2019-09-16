using Microsoft.AspNet.Identity.Owin;
using System;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Wall.App_Start;
using Wall.Models;

namespace Wall.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private ApplicationUserManager _userManager;

        public AdminController()
        {

        }
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

        public async Task<ActionResult> UserList()
        {
            return View(await UserManager.Users.ToListAsync());
        }

        [HttpGet]
        public async Task<ActionResult> Edit(string id)
        {
            var model = await UserManager.FindByIdAsync(id);

            if (model == null || model.LockoutEnabled || model.UserName == "Administrador")
            {
                return HttpNotFound();
            }
            else
            {
                return View(model);
            }
        }

        [HttpPost]
        public async Task<ActionResult> Edit(ApplicationUser model)
        {
            if (ModelState.IsValid)
            {
                await UserManager.UpdateAsync(model);

                return RedirectToAction("UserList");
            }

            return View(model);
        }

        public async Task<ActionResult> Delete(string id)
        {
            var user = await UserManager.FindByIdAsync(id);

            if (user == null)
            {
                return HttpNotFound();
            }

            user.LockoutEnabled = true;
            await UserManager.SetLockoutEndDateAsync(id, DateTime.Today.AddYears(10));

            return RedirectToAction("UserList");
        }

    }
}
