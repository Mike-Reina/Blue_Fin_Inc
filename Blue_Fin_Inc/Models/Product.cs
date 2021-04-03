using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Blue_Fin_Inc.Models
{
    public abstract class Product
    {
        // fields
        private int productCode;
        private string name;
        private string description;
        private int stock;
        private double price;

        // properties
        [Required]
        [Key]
        public int ProductCode 
        { 
            get => productCode; 

            set
            {
                productCode = value;
            } 
        }

        [Required]
        public string Name
        {
            get => name;

            set
            {
                name = value;
            }
        }

        [Required]
        public string Description
        {
            get => description;

            set
            {
                description = value;
            }
        }

        [Required]
        public int Stock
        { 
            get => stock;

            set
            {
                stock = value;
            }
        }

        [Required]
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
        public Product(string _name, string _description, double _price)
        {
            Name = _name;
            Description = _description;
            Stock = 0;
            Price = _price;
        }

        public Product()
        {

        }

        // virtual methods
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
