using PopsWeb.Models;
using System.Web.Mvc;

namespace PopsWeb.Controllers
{
    [Authorize]
    public class PopsRatingController : Controller
    {
        // GET: PopsRating
        public ActionResult Index()
        {
            PopsRatingDB db = new PopsRatingDB ();
            return View(db.list ());
        }

        public ActionResult Rate ()
        {
            PopsDB dbp = new PopsDB ();
            ViewBag.pops = dbp.list ();
            return View ();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Rate (PopsRating novo)
        {
            PopsRatingDB db = new PopsRatingDB ();

            if (ModelState.IsValid)
            {
                if (novo.rating_pos < 1 || novo.rating_pos > 5)
                {
                    return View (novo);
                }

                novo.id_user = int.Parse (Session["userid"].ToString ());
                db.create (novo);
                return RedirectToAction ("index");
            }
            return View (novo);
        }
    }

    
}