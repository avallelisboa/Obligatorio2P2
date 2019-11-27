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
            if (Session["user"] != null && Convert.ToString(Session["role"]) == "admin")
            {
                SystemControl sys = SystemControl.getSystemControl();
                User user = sys.Users[Convert.ToInt32(Session["id"])];
                List<ProductStock> productStocks = sys.Catalogue;
                ViewBag.catalogue = productStocks;
                return View();
            }
            else return Redirect("/Home");
        }

        public ActionResult UpdatePrice(int stockId, int productId, int newPrice)
        {
            SystemControl sys = SystemControl.getSystemControl();
            sys.changeProductPrice(stockId, productId, newPrice);
            return Redirect("/Admin/Index");
        }

        public ActionResult UpdateDescription(int stockId, int productId, string newDescription)
        {
            SystemControl sys = SystemControl.getSystemControl();
            sys.changeProductDescription(stockId, productId, newDescription);
            return Redirect("/Admin/Index");
        }
    }
}