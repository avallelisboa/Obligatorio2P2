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
        [HttpGet]
        public ActionResult Index()
        {
            if (Session["user"] != null && Convert.ToString(Session["role"]) == "client")
            {
                SystemControl sys = SystemControl.getSystemControl();
                User user = sys.Users[Convert.ToInt32(Session["id"])];
                List<Product> products = sys.getLast10Products(user.Client);//Obtener últimas 10 compras
                ViewBag.products = products;
                List<Product> mostBought = sys.getMostBoughtProducts(user.Client);//Obtener productos más comprados
                ViewBag.mostBought = mostBought;
                DateTime _datePurchase = sys.getDateLastPurchase(user.Client);
                ViewBag.dateLastPurchase = _datePurchase;
                List<ProductStock> productStocks = sys.Catalogue;
                ViewBag.catalogue = productStocks;
                ViewBag.resultMessage = Convert.ToString(Session["resultMessage"]);
                if (Session["idpurchase"] != null)
                {
                    int purchaseId = Convert.ToInt32(Session["idpurchase"]);
                    Purchase _purchase = sys.Purchases[purchaseId];
                    return View(_purchase);
                }
                else return View();
            }
            else return Redirect("/Home");
        }
        [HttpGet]
        public ActionResult Checkout()
        {
            if(Session["user"]!= null)
            {
                SystemControl sys = SystemControl.getSystemControl();
                int purchaseId = Convert.ToInt32(Session["idpurchase"]);
                Purchase _purchase = sys.Purchases[purchaseId];
                string result = _purchase.buy();
                Session["resultMessage"] = result;
                ViewBag.resultMessage = result;
            }
            return Redirect("/Client/Index");
        }

        public ActionResult ConfirmCheckout()
        {
            Session["resultMessage"] = null;
            Session["idpurchase"] = null;
            return Redirect("/Client/Index");
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
                return Redirect("/Client/Index/");
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