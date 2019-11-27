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
            User dataUser = sys.login(user, password);
            if (dataUser != null)
            {
                string role = dataUser.Role;
                Session["user"] = dataUser.UserName;
                Session["name"] = dataUser.Name;
                Session["role"] = role;
                Session["id"] = dataUser.Id;

                switch (role)
                {
                    case "client":
                        return Redirect("/Client/Index");
                    case "guest":
                        return Redirect("/Guest/Index");
                    case "admin":
                        return Redirect("/Admin/Index");
                    default:
                        throw new Exception("invalid role");
                }
            }
            else
            {
                return Redirect("/Home/Index");
            }
            throw new NotImplementedException();
        }

        public ActionResult Logout()
        {
            Session["user"] = null;
            Session["name"] = null;
            Session["role"] = null;
            Session["idpurchase"] = null;
            return Redirect("/Home/Index");
        }
    }
}