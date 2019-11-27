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
            if (Session["user"] != null && Convert.ToString(Session["role"]) == "client")
            {
                SystemControl sys = SystemControl.getSystemControl();
                List<ProductStock> productStocks = sys.Catalogue;
                ViewBag.catalogue = productStocks;
                if(Session["idpurchase"] != null)
                {
                    Purchase _purchase = getPurchase();
                    ViewBag.purchase = _purchase;
                }
                return View();
            }
            else return Redirect("/Home");
        }

        [HttpGet]
        public ActionResult AddToPurchase(int stockId, int productId, int quantity)
        {
            SystemControl sys = SystemControl.getSystemControl();
            if (Convert.ToString(Session["role"]) == "client")
            {
                Purchase _purchase;
                if(Session["idpurchase"] == null)
                {
                    _purchase = getPurchase();
                    Session["idpurchase"] = _purchase.Id;
                }
                else
                {
                    _purchase = sys.Purchases[Convert.ToInt32(Session["idpurchase"])];
                }
                _purchase.addToPurchase(stockId, productId, quantity);
                return Redirect("/Client/Index");
            }
            else
            {
                return Redirect("/Client/Index");
            }            
        }

        private Purchase getPurchase()
        {
            SystemControl sys = SystemControl.getSystemControl();
            int userId = Convert.ToInt32(Session["id"]);
            Purchase _purchase = sys.getPurchase(userId);
            return _purchase;
        }
    }
}