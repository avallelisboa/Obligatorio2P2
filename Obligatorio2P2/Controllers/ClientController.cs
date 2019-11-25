using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShopSystem;

namespace Obligatorio2P2.Controllers
{
    public class ClientController : Controller
    {
        // GET: Client
        public ActionResult Index()
        {
            if (Session["user"] != null && Session["role"] == "client")
            {
                SystemControl sys = SystemControl.getSystemControl();
                List<ProductStock> productStocks = sys.getCatalogue();
                ViewBag.catalogue = productStocks;
                return View(productStocks);
            }
            else return Redirect("/Home");
        }
    }
}