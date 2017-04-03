using PopsWeb.Models;
using System.Web;
using System.Web.Mvc;

namespace PopsWeb.Controllers
{
    [Authorize]
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

        public ActionResult Details (int id)
        {
            PopsRatingDB db = new PopsRatingDB ();
            ViewBag.rating = db.avg_rating (id);
            return View (pops.list (id)[0]);
        }

        public ActionResult Edit (int id)
        {
            PopsCollectionDB db = new PopsCollectionDB ();
            ViewBag.collections = db.list ();
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
            PopsCollectionDB db = new PopsCollectionDB ();
            ViewBag.collections = db.list ();
            return View ();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create (PopsModel novo, HttpPostedFileBase image)
        {
            if (ModelState.IsValid)
            {
                if (image == null)
                {
                    ModelState.AddModelError ("", "You can see the pop right? Take a picture of it");
                    return View (novo);
                }
                int id = pops.create (novo); 

                string filepath = Server.MapPath ("~/Content/Pops/") + id + ".jpg";
                image.SaveAs (filepath);

                
                return RedirectToAction ("index");
            }
            return View (novo);
        }
    }
}