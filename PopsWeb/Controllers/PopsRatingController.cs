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
            return View();
        }


        public ActionResult Rate (int id)
        {
            return View ();
        }

        public ActionResult Rate (PopsRating novo)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction ("index");
            }
            return View (novo);
        }
    }

    
}