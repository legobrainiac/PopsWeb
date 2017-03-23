using PopsWeb.Models;
using System.Web.Mvc;

namespace PopsWeb.Controllers
{
    public class PopsController : Controller
    {
        PopsDB pops = new PopsDB ();

        // GET: Pops
        public ActionResult Index()
        {
            return View(pops.list ());
        }

        public ActionResult Delete (int id)
        {
            pops.delete (id);
            return RedirectToAction ("index");
        }

        public ActionResult Edit (int id)
        {
            return View (pops.list (id)[0]);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit (PopsModel dados)
        {
            if (ModelState.IsValid)
            {
                pops.update (dados);
                return RedirectToAction ("index");
            }
            return View (dados);
        }


        public ActionResult Create ()
        {
            return View ();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create (PopsModel novo)
        {
            if (ModelState.IsValid)
            {
                pops.create (novo);
                return RedirectToAction ("index");
            }
            return View (novo);
        }
    }
}