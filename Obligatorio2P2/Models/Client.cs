using System;
using System.Collections.Generic;
using System.Text;

namespace ShopSystem
{
    public class Client
    {
        private int id;
        private string name;
        private string phone;
        private string address;
        private string mail;
        private DateTime registerDate;
        private bool isFromMontevideo;
        private List<Purchase> purchases;

        public class clientValidation
        {
            public clientValidation(bool isMailUsed)
            {
                this.isMailUsed = isMailUsed;
            }
            public bool isMailUsed;
        }

        public int Id { get { return id; } }
        public string Phone { get { return phone; } }
        public string Address { get { return address; } }
        public string Mail { get { return mail; } }
        public DateTime RegisterDate { get { return registerDate; } }
        public bool IsFromMontevideo { get { return isFromMontevideo; } }
        public List<Purchase> Purchases { get { return purchases; } }

        internal Client(int id, string name, string address, string mail, string phone, bool isFromMontevideo)
        {
            this.id = id;
            this.name = name;
            this.address = address;
            this.mail = mail;
            this.phone = phone;
            this.isFromMontevideo = isFromMontevideo;
            registerDate = DateTime.Today;
        }

        public static clientValidation isInformationCorrect(List<Client> clients, string user, string mail)
        {
            int id = clients.Count;
            bool isMailUsed = false;
            foreach (Client c in clients)
            {
                if (c.Mail == mail)
                {
                    isMailUsed = true;
                }
                if (isMailUsed) break;
            }
            clientValidation clientValidation = new clientValidation(isMailUsed);
            return clientValidation;
        }
        public void addPurchase(Purchase purchase)
        {
            purchases.Add(purchase);
        }
        public List<Product> getLast10Products()
        {
            List<Product> last10Products = new List<Product>();
            int productsAddedQuantity = 0;
            foreach (Purchase c in purchases)
            {
                if (productsAddedQuantity == 10) break;
                foreach (Product p in c.ProductsToBuy)
                {
                    if (productsAddedQuantity == 10) break;
                    last10Products.Add(p);
                    productsAddedQuantity++;
                }
            }
            return last10Products;
        }
        public List<Product> getMostBoughtProduct()
        {
            List<Product> mostBoughtProducts = new List<Product>();
            int mostBoughtProductQuantity = 0;
            foreach (Purchase c in purchases)
            {
                foreach (var p in c.GetDataProductsToBuy)
                {
                    if (mostBoughtProductQuantity < p.quantity)
                    {
                        mostBoughtProductQuantity = p.quantity;
                        mostBoughtProducts.Clear();
                        foreach (Product _p in c.ProductsToBuy)
                        {
                            if (p.productId == _p.Id) mostBoughtProducts.Add(_p);
                        }
                    }
                    else if (mostBoughtProductQuantity == p.quantity)
                    {
                        foreach (Product _p in c.ProductsToBuy)
                        {
                            if (p.productId == _p.Id)
                            {
                                mostBoughtProducts.Add(_p);
                                break;
                            }
                        }
                    }
                }
            }
            return mostBoughtProducts;
        }

        public DateTime getDateLastPurchase()
        {
            DateTime lastPurchaseDate;
            int count = purchases.Count;
            lastPurchaseDate = purchases[0].Date;
            for (int i = 1; i < count; i++)
            {
                int result = DateTime.Compare(purchases[i].Date, purchases[i - 1].Date);
                if (result > 0) lastPurchaseDate = purchases[i].Date;
            }
            return lastPurchaseDate;
        }

        public virtual string getName()
        {
            return "";
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
