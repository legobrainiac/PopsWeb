using PopsWeb.Models;
using System.Web.Mvc;

namespace PopsWeb.Controllers
{
    public class UsersController : Controller
    {
        UsersDB users = new UsersDB ();

        public ActionResult Index ()
        {
            return View (users.list ());
        }

        public ActionResult Edit (int id)
        {
            return View (users.list (id)[0]);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit (UsersModel dados)
        {
            if (ModelState.IsValid)
            {
                users.update (dados);
                return RedirectToAction ("index");
            }
            return View (dados);
        }
    }
}