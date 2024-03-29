﻿using System;
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
                if(Session["d1"] != null && Session["d2"] != null)
                {
                    string d1 = Convert.ToString(Session["d1"]);
                    string d2 = Convert.ToString(Session["d2"]);
                    DateTime date1; DateTime date2;
                    if(DateTime.TryParse(d1, out date1) && DateTime.TryParse(d2, out date2))
                    {
                        List<Purchase> purchasesBetweenDates = sys.getPurchasesBetweenDates(date1, date2);
                        Session["purchasesBetweenDates"] = purchasesBetweenDates;
                        ViewBag.purchasesBetweenDates = purchasesBetweenDates;
                    }                   
                }
                User user = sys.Users[Convert.ToInt32(Session["id"])];
                List<ProductStock> productStocks = sys.Catalogue;
                ViewBag.catalogue = productStocks;
                List<User> users = sys.Users;
                ViewBag.users = users;
                ViewBag.message = Convert.ToString(Session["message"]);
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

        public ActionResult UpdateClient(int userId, string role)
        {
            SystemControl sys = SystemControl.getSystemControl();
            throw new NotImplementedException();
        }

        public ActionResult CreateUser(string user, string password, string name, string role)
        {
            SystemControl sys = SystemControl.getSystemControl();
            List<User> _users = sys.Users;
            SystemControl.registerStatus registerStatus = sys.addUser(user, password, name, role);
            Session["message"] = registerStatus.message;
            return Redirect("/Admin/Index");
        }

        public ActionResult ModifyUser(int userId, string role)
        {
            SystemControl sys = SystemControl.getSystemControl();
            List<User> _users = sys.Users;
            string message =  sys.setRole(_users[userId], role);
            Session["message"] = message;
            return Redirect("/Admin/Index");
        }

        public ActionResult CreateCommonClient(string name, int identificationCard, string celular, string mail, string address, string user, string password, bool isFromMontevideo)
        {
            SystemControl sys = SystemControl.getSystemControl();
            List<User> _users = sys.Users;
            SystemControl.registerStatus status = sys.addCommonClient(name, identificationCard, celular, mail, address, user, password, isFromMontevideo);
            Session["message"] = status.message;
            return Redirect("/Admin/Index");
        }
        [HttpPost]
        public ActionResult CreateCompanyClient(string companyName, string bussinesName, int rut, string mail, string phone, string address, string user, string password, bool isFromMontevideo, int discount)
        {
            SystemControl sys = SystemControl.getSystemControl();
            List<User> _users = sys.Users;
            SystemControl.registerStatus status = sys.addCompanyClient(companyName,bussinesName, rut, mail, phone, address, user, password, isFromMontevideo, discount);
            Session["message"] = status.message;
            return Redirect("/Admin/Index");
        }
        [HttpPost]
        public ActionResult CreateProduct(int catalogueId, string productName, int price, string description, bool isExclusive, int quantity)
        {
            SystemControl sys = SystemControl.getSystemControl();
            string resultMessage = sys.Catalogue[catalogueId].addProduct(productName, price, description, isExclusive, quantity);
            Session["message"] = resultMessage;
            return Redirect("/Admin/Index");
        }

        public ActionResult GetPurchasesBetweenDates(string d1, string d2)
        {
            if (Convert.ToString(Session["role"]) =="admin")
            {
                Session["d1"] = d1;
                Session["d2"] = d2;                
            }
            return Redirect("/Admin/Index");
        }
    }
}