using PopsWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace PopsWeb.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index (LoginModel dados)
        {
            LoginDB dbLogin = new LoginDB ();

            if (ModelState.IsValid)
            {
                UsersModel utilizador = dbLogin.validateLogin (dados);
                if (utilizador == null)
                {
                    ModelState.AddModelError ("", "Login Failed");
                    return View (dados);
                }
                else
                {
                    Session["usertype"] = utilizador.usertype;
                    Session["username"] = utilizador.username;
                    FormsAuthentication.SetAuthCookie (utilizador.username, false);

                    if (Request.QueryString["ReturnUrl"] == null)
                        return RedirectToAction ("Index", "Home");
                    else
                        return Redirect (Request.QueryString["ReturnUrl"].ToString ());
                }
            }
            return View (dados);
        }
    }
}