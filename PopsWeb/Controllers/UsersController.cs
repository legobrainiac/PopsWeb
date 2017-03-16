using PopsWeb.Models;
using System.Web.Mvc;

namespace PopsWeb.Controllers
{
    public class UsersController : Controller
    {
        UsersDB users = new UsersDB();
        // GET: Users
        public ActionResult Index()
        {
            return View(users.lista ());
        }
    }
}