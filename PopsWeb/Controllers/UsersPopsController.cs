using System.Web.Mvc;
using PopsWeb.Models;
using System.Web.Routing;

namespace PopsWeb.Controllers
{
    [Authorize]
    public class UsersPopsController : Controller
    {
        UsersPopsDB usersPops = new UsersPopsDB ();

        // GET: UsersPops
        public ActionResult Index ()
        {
            return View (usersPops.list ());
        }

        public ActionResult Indexe (int id)
        {
            return View (usersPops.list_user_collection (id));
        }

        public ActionResult Details (int id)
        {
            int _id = id;
            return RedirectToAction ("Details", "Pops", new { id = _id});
        }

        public ActionResult Edit (int id)
        {
            return View (usersPops.list (id)[0]);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit (UsersPopsModel dados)
        {
            if (ModelState.IsValid)
            {
                usersPops.update (dados);
                return RedirectToAction ("index");
            }
            return View (dados);
        }

        public ActionResult Delete (int id)
        {
            usersPops.delete (id);
            return RedirectToAction ("index");
        }

        public ActionResult Create ()
        {
            PopsDB db = new PopsDB ();
            ViewBag.pops = db.list ();
            return View ();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create (UsersPopsModel novo)
        {
            if (ModelState.IsValid)
            {
                novo.id_user = int.Parse(Session["userid"].ToString ());
                usersPops.create (novo);
                return RedirectToAction ("index");
            }
            return View (novo);
        }
    }
}