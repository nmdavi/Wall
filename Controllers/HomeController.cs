using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using Wall.Models;

namespace Wall.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly EFContext _ctx = new EFContext();

        public HomeController()
        {

        }

        public ActionResult Index()
        {
            return View(_ctx.Frases.ToList());
        }

        [HttpGet]
        public ActionResult AddEdit(int? id)
        {
            var model = _ctx.Frases.Find(id);

            if (model == null)
            {
                return View(new FraseViewModel());
            }
            else
            {
                if (model.Autor == User.Identity.Name || User.Identity.Name == "Administrador" || 
                    (model.Autor == "Anônimo" && User.Identity.Name == "Administrador"))
                {
                    return View(model);
                }
                else
                {
                    return HttpNotFound();
                }
            }
        }

        [HttpPost]
        public ActionResult AddEdit(FraseViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.Id == 0)
                {                 
                    _ctx.Frases.Add(model);
                }
                else
                {
                    _ctx.Entry(model).State = EntityState.Modified;
                }

                _ctx.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(model);
        }


        public ActionResult Delete(int? id)
        {
            var model = _ctx.Frases.Find(id);

            if (model != null && (User.Identity.Name == model.Autor || User.Identity.Name == "Administrador"))
            {

                _ctx.Frases.Remove(model);
                _ctx.SaveChanges();
                return RedirectToAction("Index");

            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        [AllowAnonymous]
        public ViewResult About()
        {
            return View();
        }

        [AllowAnonymous]
        public ViewResult Contact()
        {
            return View();
        }
    }
}
