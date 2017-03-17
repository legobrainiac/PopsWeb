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

        public ActionResult Delete (int id)
        {
            users.delete (id);
            return RedirectToAction ("index");
        }

        public ActionResult Create ()
        {
            return View ();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create (UsersModel novo)
        {
            if (ModelState.IsValid)
            {
                users.create (novo);
                return RedirectToAction ("index");
            }
            return View (novo);
        }
    }
}