using System.Web.Mvc;
using System.Web.Security;

namespace PopsWeb.Controllers
{
    public class LogOutController : Controller
    {
        // GET: LogOut
        public ActionResult Index()
        {
            Session.Clear ();
            FormsAuthentication.SignOut ();
            return RedirectToAction ("Index", "Home");
        }
    }
}