using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PopsWeb.Models;

namespace PopsWeb.Controllers
{
    public class UsersPopsController : Controller
    {
        UsersPopsDB usersPops = new UsersPopsDB ();

        // GET: UsersPops
        public ActionResult Index ()
        {
            return View (usersPops.list ());
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
            return View ();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create (UsersPopsModel novo)
        {
            if (ModelState.IsValid)
            {
                usersPops.create (novo);
                return RedirectToAction ("index");
            }
            return View (novo);
        }
    }
}