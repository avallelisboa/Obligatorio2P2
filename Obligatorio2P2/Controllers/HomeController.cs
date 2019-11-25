using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShopSystem;

namespace Obligatorio2P2.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {

            SystemControl sys = SystemControl.getSystemControl();
            sys.preLoad();
            return View();
        }

        [HttpPost]
        public ActionResult Login(string user, string password)
        {
            SystemControl sys = SystemControl.getSystemControl();
            if(sys.login(user, password))
            {
                return Content("<p>Inicio de sesión exitoso</p>");
            }
            else
            {
                return Redirect("/Home/Index");
            }
            throw new NotImplementedException();
        }
    }
}