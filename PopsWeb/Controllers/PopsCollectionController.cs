using PopsWeb.Models;
using System.Web.Mvc;

namespace PopsWeb.Controllers
{
    public class PopsCollectionController : Controller
    {
        PopsCollectionDB popsCollection = new PopsCollectionDB ();

        // GET: PopsCollection
        public ActionResult Index()
        {
            return View(popsCollection.list ());
        }

        public ActionResult Delete (int id)
        {
            popsCollection.delete (id);
            return RedirectToAction ("index");
        }

        public ActionResult Edit (int id)
        {
            return View (popsCollection.list (id)[0]);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit (PopsCollectionModel dados)
        {
            if (ModelState.IsValid)
            {
                popsCollection.update (dados);
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
        public ActionResult Create (PopsCollectionModel novo)
        {
            if (ModelState.IsValid)
            {
                popsCollection.create (novo);
                return RedirectToAction ("index");
            }
            return View (novo);
        }
    }
}