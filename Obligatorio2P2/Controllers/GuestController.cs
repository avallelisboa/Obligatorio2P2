using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShopSystem;

namespace Obligatorio2P2.Controllers
{
    public class GuestController : Controller
    {
        // GET: Guest
        public ActionResult Index()
        {
            if (Session["user"] != null && Session["role"] == "guest")
            {
                SystemControl sys = SystemControl.getSystemControl();
                List<ProductStock> productStocks = sys.getCatalogue();
                ViewBag.catalogue = productStocks;
                return View();
            }
            else return Redirect("/Home");
        }
    }
}