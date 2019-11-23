using System;
using System.Collections.Generic;
using System.Text;

namespace ShopSystem
{
    public class ProductStock
    {
        private List<Product> products = new List<Product>();
        private int stockId;
        private string name;
        private int price;
        public class _product
        {
            public int id;
            public int quantity;
            public int price;
            public bool wasAdded;
        }

        public int StockId { get { return stockId; } }
        public string Name { get { return name; } }
        public int Price { get { return price; } set { price = value; } }
        public List<Product> Products { get { return products; } }
        public int ProductsQuantity { get { return products.Count; } }

        public string addProduct(string name, int price, string description, bool isExclusive, int quantity) //agregas productos
        {
            int id = products.Count;
            bool productExists = false;
            if (name == "" || name == " " || name.Length < 4) return "The name must be valid";
            else
            {
                if (price > 0)
                {
                    foreach (Product p in products)
                    {
                        if (p.Name == name) productExists = true;
                        break;
                    }
                    if (!productExists)
                    {
                        products.Add(Product.createProduct(name, id, stockId, price, description, isExclusive));
                        products[products.Count - 1].addProducts(quantity);
                        return "The product was added correctly";
                    }
                    else return "The product already exists";
                }
                else return "The price must be greater than 0";
            }
        }

        public _product addToPurchase(int quantity, int productId)
        {
            int count = products.Count;
            int price = 0;
            bool wasFounded = false;
            foreach (Product p in products)
            {
                if (p.Id == productId)
                {
                    wasFounded = true;
                    price = p.Price;
                    break;
                }
            }
            if (wasFounded)
            {
                _product productAdded = new _product();
                productAdded.id = productId;
                productAdded.quantity = quantity;
                productAdded.price = price;
                productAdded.wasAdded = true;
                return productAdded;
            }
            else
            {
                _product productAdded = new _product();
                productAdded.wasAdded = false;
                return productAdded;
            }
        }

        public ProductStock(string name, int id)
        {
            this.name = name;
            this.stockId = id;
        }

        public string removeProduct(int id, int quantity)
        {
            int productsNumber = Products.Count;
            if (quantity > productsNumber) return "There are not enough products";
            else
            {
                for (int i = 0; i < productsNumber; i++)
                {
                    if (Products[i].Id == id) Products.RemoveAt(i);
                }
                return "Products added";
            }
        }
    }
}
