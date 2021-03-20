using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blue_Fin_Inc.Models
{
    public abstract class Product
    {
        // fields
        private int productCode;
        private string name;
        private string discription;
        private int stock;
        private double price;

        // properties
        public int ProductCode 
        { 
            get => productCode; 

            private set
            {
                productCode = value;
            } 
        }

        public string Name
        {
            get => name;

            private set
            {
                name = value;
            }
        }

        public string Discription
        {
            get => discription;

            private set
            {
                discription = value;
            }
        }
        public int Stock
        { 
            get => stock;

            private set
            {
                stock = value;
            }
        }
        public double Price
        { 
            get => price;
            
            set
            {
                if(value > 0)
                {
                    price = value;
                }
                else
                {
                    throw new ArgumentException("Price must be grater than 0.");
                }
            }
        }

        // constructor 
        public Product(int _productCode, string _name, string _discription, double _price)
        {
            ProductCode = _productCode;
            Name = _name;
            Discription = _discription;
            Stock = 0;
            Price = _price;
        }

        // methods
        public virtual void AddStock(int amount)
        {
            if(amount <= 0)
            {
                throw new ArgumentException("Amount to add must be greater than 0.");
            }

            stock += amount;
        }
        public virtual void RemoveStock(int amount)
        {
            if ((stock - amount) < 0)
            {
                throw new ArgumentException("Not enough stock available for sale. Only " + stock + " left in stock.");
            }

            stock -= amount;
        }
    }
}
