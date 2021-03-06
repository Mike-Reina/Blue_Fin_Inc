using DataAnnotationsExtensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Blue_Fin_Inc.Models
{
    public enum NotificationType
    {
        error,
        success,
        warning
    }

    public abstract class Product
    {

        // properties
        [Required]
        [Key]
        [DisplayName("Product Code")]
        public int ProductCode { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Name must be at least 3 character longs!")]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        [Min(0, ErrorMessage = "Stock must be Zero or Greater!")]
        public int Stock { get; set; }


        [Required]
        [DisplayName("Price(€)")]
        [Min(.1, ErrorMessage = "Price must be greater than 0!")]
        public double Price { get; set; }

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

            Stock += amount;
        }
        public virtual void RemoveStock(int amount)
        {
            if ((Stock - amount) < 0)
            {
                throw new ArgumentException("Not enough stock available for sale. Only " + Stock + " left in stock.");
            }

            Stock -= amount;
        }
    }
}
