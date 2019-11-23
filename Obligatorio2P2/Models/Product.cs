using System;
using System.Collections.Generic;
using System.Text;

namespace ShopSystem
{
    public class Product
    {
        private int id;
        private int stockId;
        private string name;
        private int price;
        private string description;
        private bool isExclusive;
        private int quantity;

        public int Id { get { return id; } set { id = value; } }
        public int StockId { get { return stockId; } }
        public string Name { get { return name; } }
        public int Price { get { return price; } }
        public string Description { get { return description; } }
        public int Quantity { get { return quantity; } }
        public bool IsExclusive { get { return isExclusive; } }

        private Product(string name, int id, int stockId, int price, string description, bool isExclusive)
        {
            this.id = id;
            this.stockId = stockId;
            this.name = name;
            this.price = price;
            this.description = description;
            this.isExclusive = isExclusive;
            this.quantity = 0;
        }

        public void addProducts(int quantity)
        {
            this.quantity += quantity;
        }

        public static Product createProduct(string name, int id, int stockId, int price, string description, bool isExclusive)
        {
            Product product = new Product(name, id, stockId, price, description, isExclusive);
            return product;
        }

        public void removeProducts(int quantity)
        {
            this.quantity -= quantity;
        }
    }
}
