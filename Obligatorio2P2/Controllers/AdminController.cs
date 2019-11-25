using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShopSystem;

namespace Obligatorio2P2.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            if (Session["user"] != null && Session["role"] == "admin")
            {
                SystemControl sys = SystemControl.getSystemControl();
                return View();
            }
            else return Redirect("/Home");
        }
    }
}